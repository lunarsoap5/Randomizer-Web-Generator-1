namespace TPRandomizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Assets;
    using Hints;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using TPRandomizer.FcSettings.Enums;
    using TPRandomizer.SSettings.Enums;

    /// <summary>
    /// Generates a randomizer seed given a settings string.
    /// </summary>
    public class Randomizer
    {
        /// <summary>
        /// A reference to all logic functions that need to be used by the randomizer.
        /// </summary>
        public static readonly LogicFunctions Logic = new();

        /// <summary>
        /// A reference to all check-related functions that need to be used by the randomizer.
        /// </summary>
        public static readonly CheckFunctions Checks = new();

        /// <summary>
        /// A reference to all room-related functions that need to be used by the randomizer.
        /// </summary>
        public static readonly RoomFunctions Rooms = new();

        /// <summary>
        /// A reference to all item lists and functions that need to be used by the randomizer.
        /// </summary>
        public static readonly ItemFunctions Items = new();

        /// <summary>
        /// A reference to all Entrance definitions and functions that need to be used by the randomizer.
        /// </summary>
        public static readonly EntranceRando EntranceRandomizer = new();

        /// <summary>
        /// A reference to the sSettings.
        /// </summary>
        public static SharedSettings SSettings = new();

        public static int RequiredDungeons = 0;

        public static PlaythroughSpheres GenerateSpoilerLog(Room startingRoom)
        {
            Randomizer.Items.GenerateItemPool();

            foreach (Item startingItem in Randomizer.SSettings.startingItems)
            {
                Randomizer.Items.heldItems.Add(startingItem);
            }

            bool isPlaythroughValid = BackendFunctions.ValidatePlaythrough(startingRoom, true);
            if (!isPlaythroughValid || SSettings.logicRules == LogicRules.No_Logic)
            {
                return new PlaythroughSpheres(null, null, null);
            }

            return BackendFunctions.CalculateOptimalPlaythrough2(startingRoom);
        }

        private static string GenerateInputJsonContent(
            string settingsString,
            string seed,
            int seedHash,
            bool isRaceSeed,
            List<List<KeyValuePair<int, Item>>> spheres,
            CustomMsgData customMsgData
        )
        {
            Dictionary<string, Item> checkIdToItemId = new();
            List<string> placementStrParts = new();
            SortedDictionary<int, byte> checkNumIdToItemId = new();

            List<KeyValuePair<int, Item>> dungeonRewards = new();

            foreach (KeyValuePair<string, Check> checkList in Checks.CheckDict.ToList())
            {
                // We don't store itemIds in the json for vanilla checks to save space.
                if (checkList.Value.checkStatus != "Vanilla")
                {
                    string checkId = CheckIdClass.FromString(checkList.Key);
                    int checkIdNum = CheckIdClass.GetCheckIdNum(checkList.Key);
                    if (checkId == null || checkIdNum < 0)
                    {
                        throw new Exception(
                            "Need to update CheckId to support check named \""
                                + checkList.Key
                                + "\"."
                        );
                    }

                    Check check = checkList.Value;

                    byte itemIdByte = (byte)check.itemId;

                    // For generating consistent filenames.
                    placementStrParts.Add(checkId + "_" + itemIdByte);
                    checkIdToItemId[checkId] = check.itemId;

                    // For storing placements in json file.
                    checkNumIdToItemId.Add(checkIdNum, itemIdByte);

                    if (check.checkCategory.Contains("Dungeon Reward"))
                    {
                        dungeonRewards.Add(
                            new KeyValuePair<int, Item>(
                                CheckIdClass.GetCheckIdNum(check.checkName),
                                check.itemId
                            )
                        );
                    }
                }
            }

            // StringComparer is needed because the default sort order is
            // different on Linux and Windows
            placementStrParts.Sort(StringComparer.Ordinal);
            string itemPlacementPart = String.Join("-", placementStrParts);

            // Only need to take settings into account which are important to part2.
            SortedDictionary<string, object> part2SettingsForString = GenPart2Settings();

            string part2SettingsPart = JsonConvert.SerializeObject(part2SettingsForString);

            string seedHashAsString = seedHash.ToString("x8");

            string filenameInput = String.Join(
                "%%%%",
                new List<string> { itemPlacementPart, part2SettingsPart, seedHashAsString }
            );

            int filenameBits = Util.Hash.CalculateMD5(filenameInput);
            List<string> playthroughNames = Util.PlaythroughName.GenNames(filenameBits);

            // When generating the filename, the following should be taken into account:

            // - item placements for non-vanilla placements (sorted so that
            //   always the same)
            // - settings which affect things other than item-placement (also
            //   sort). When new settings are added in the future, they should
            //   only be taken into account in the hash when their setting would
            //   cause the gameplay to differ from a seed that was generated
            //   before that setting existed. For example, let's say a seed was
            //   generated at point A. At point B, we now have a setting for
            //   super-clawshot (on or off, off by default). If we generate a
            //   seed at point B with super-clawshot turned off, the playthrough
            //   will be identical to the one generated at point A, so the
            //   filenames should remain the same. However, we still need to
            //   specify the super-clawshot setting in the json because if
            //   someone were to generate a GCI using the seed from point B as
            //   input and now super-clawshot is on by default (this is just an
            //   example), their playthrough experience would be different even
            //   though the filename is the same.

            // TODO: review if the above comment needs a little revision

            SeedGenResults.Builder builder = new();
            // inputs
            builder.settingsString = settingsString;
            builder.seed = seed;
            builder.isRaceSeed = isRaceSeed;
            // outputs
            builder.seedHashString = seedHashAsString;
            builder.playthroughName = playthroughNames[0];
            builder.wiiPlaythroughName = playthroughNames[1];
            builder.requiredDungeons = (byte)Randomizer.RequiredDungeons;
            builder.SetItemPlacements(checkNumIdToItemId);
            builder.SetSpheres(spheres);
            builder.SetEntrances();
            builder.SetCustomMsgData(customMsgData);
            Console.WriteLine(builder.GetEntrances(builder.entrances));
            return builder.ToString();
        }

        private static SortedDictionary<string, object> GenPart2Settings()
        {
            // Please read the comments below when updating the sSettings to
            // determine if/how this method needs to be updated.

            // Generally speaking, it should be added if it affects starting
            // state or the ability to traverse the game graph.

            // StringComparer is needed because the default sort order is
            // different on Linux and Windows
            SortedDictionary<string, object> part2Settings = new(StringComparer.Ordinal);

            // If a setting matches what the game behavior would have been
            // before that setting existed, we leave it off.

            // We don't add a setting when its only effect has to do with
            // itemPlacement. itemPlacement is handled on its own. If one seed
            // generation had smallKeys in OwnDungeon and another had them in
            // AnyDungeon and the placement ended up being the exact same, (all
            // other things being the same) the playthroughName should match
            // because the generated file would be the same, meaning the player
            // has already played this exact scenario.

            // A setting is only added if it is set to a value which has a
            // definite impact on the game either. For example, it affects the
            // starting state (such as you start with these flags already set
            // and these items in your inventory). Another example would be if
            // there was a setting that had an impact on your ability to
            // navigate an edge of the graph during gameplay (such as trying to
            // enter the Stallord boss fight from the back causes you to
            // teleport back to the mirror chamber).

            // Note that the string keys must never be changed after initial
            // release. It is okay if the name does not match exactly with the
            // sSettings property.

            // Multi-option fields which are only included for certain values
            if (SSettings.castleRequirements != CastleRequirements.Vanilla)
                part2Settings.Add("castleRequirements", SSettings.castleRequirements);
            if (SSettings.palaceRequirements != PalaceRequirements.Vanilla)
                part2Settings.Add("palaceRequirements", SSettings.palaceRequirements);
            // TODO: Change this one to a boolean called "faronWoodsOpen"
            if (SSettings.faronWoodsLogic == FaronWoodsLogic.Open)
                part2Settings.Add("faronWoodsLogic", SSettings.faronWoodsLogic);
            if (SSettings.smallKeySettings == SmallKeySettings.Keysy)
                part2Settings.Add("smallKeySettings", SSettings.smallKeySettings);
            if (SSettings.bigKeySettings == BigKeySettings.Keysy)
                part2Settings.Add("bigKeySettings", SSettings.bigKeySettings);
            if (SSettings.mapAndCompassSettings == MapAndCompassSettings.Start_With)
                part2Settings.Add("mapAndCompassSettings", SSettings.mapAndCompassSettings);

            // Boolean fields included when true
            if (SSettings.skipPrologue)
                part2Settings.Add("skipPrologue", SSettings.skipPrologue);
            if (SSettings.faronTwilightCleared)
                part2Settings.Add("faronTwilightCleared", SSettings.faronTwilightCleared);
            if (SSettings.eldinTwilightCleared)
                part2Settings.Add("eldinTwilightCleared", SSettings.eldinTwilightCleared);
            if (SSettings.lanayruTwilightCleared)
                part2Settings.Add("lanayruTwilightCleared", SSettings.lanayruTwilightCleared);
            if (SSettings.skipMdh)
                part2Settings.Add("skipMdh", SSettings.skipMdh);
            if (SSettings.skipMinorCutscenes)
                part2Settings.Add("skipMinorCutscenes", SSettings.skipMinorCutscenes);
            if (SSettings.fastIronBoots)
                part2Settings.Add("fastIronBoots", SSettings.fastIronBoots);
            if (SSettings.quickTransform)
                part2Settings.Add("quickTransform", SSettings.quickTransform);
            if (SSettings.transformAnywhere)
                part2Settings.Add("transformAnywhere", SSettings.transformAnywhere);
            if (SSettings.increaseWallet)
                part2Settings.Add("increaseWallet", SSettings.increaseWallet);
            if (SSettings.modifyShopModels)
                part2Settings.Add("modifyShopModels", SSettings.modifyShopModels);

            if (SSettings.goronMinesEntrance != GoronMinesEntrance.Closed)
                part2Settings.Add("goronMinesEntrance", SSettings.goronMinesEntrance);
            if (SSettings.skipLakebedEntrance)
                part2Settings.Add("skipLakebedEntrance", SSettings.skipLakebedEntrance);
            if (SSettings.skipArbitersEntrance)
                part2Settings.Add("skipArbitersEntrance", SSettings.skipArbitersEntrance);
            if (SSettings.skipSnowpeakEntrance)
                part2Settings.Add("skipSnowpeakEntrance", SSettings.skipSnowpeakEntrance);
            if (SSettings.totEntrance != TotEntrance.Closed)
                part2Settings.Add("totEntrance", SSettings.totEntrance);
            if (SSettings.skipCityEntrance)
                part2Settings.Add("skipCityEntrance", SSettings.skipCityEntrance);
            if (SSettings.instantText)
                part2Settings.Add("instantText", SSettings.instantText);
            if (SSettings.openMap)
                part2Settings.Add("openMap", SSettings.openMap);
            if (SSettings.increaseSpinnerSpeed)
                part2Settings.Add("increaseSpinnerSpeed", SSettings.increaseSpinnerSpeed);
            if (SSettings.openDot)
                part2Settings.Add("openDot", SSettings.openDot);

            // Complex fields
            if (SSettings.startingItems?.Count > 0)
            {
                List<Item> startingItems = new(SSettings.startingItems);
                startingItems.Sort();
                part2Settings.Add("startingItems", startingItems);
            }

            return part2Settings;
        }

        public static bool GenerateFinalOutput2(
            string id,
            string fcSettingsString,
            string itemPlacementString,
            string settingsString
        )
        {
            FileCreationSettings fcSettings = FileCreationSettings.FromString(fcSettingsString);

            // Generate the dictionary values that are needed and initialize the data for the selected logic type.
            DeserializeCheckData(SSettings, fcSettings);
            DeserializeRooms(SSettings);

            SeedGenResults seedGenResults = new SeedGenResults(
                id,
                settingsString,
                itemPlacementString
            );

            SSettings = SharedSettings.FromString(settingsString);
            PropertyInfo[] randoSettingProperties = SSettings.GetType().GetProperties();

            foreach (PropertyInfo settingProperty in randoSettingProperties)
            {
                Console.WriteLine(
                    settingProperty.Name + ": " + settingProperty.GetValue(SSettings, null)
                );
            }

            foreach (KeyValuePair<int, byte> kvp in seedGenResults.itemPlacements.ToList())
            {
                // key is checkId, value is itemId
                string checkName = CheckIdClass.GetCheckName(kvp.Key);
                Console.WriteLine(checkName);
                if (Randomizer.Checks.CheckDict.ContainsKey(checkName))
                {
                    Randomizer.Checks.CheckDict[checkName].itemId = (Item)kvp.Value;
                }
            }

            foreach (KeyValuePair<string, Check> checkList in Randomizer.Checks.CheckDict.ToList())
            {
                Console.WriteLine(checkList.Key + " : " + checkList.Value.itemId);
            }

            Console.WriteLine("\nGenerating Seed Data.");

            // sSettings from input.json
            // seedGenResults from input.json, such as required dungeons
            // fcSettings

            List<Tuple<Dictionary<string, object>, byte[]>> fileDefs = new();

            if (fcSettings.gameRegion == GameRegion.All)
            {
                // For now, 'All' only generates for GameCube until we do more
                // work related to Wii code.
                List<GameRegion> gameRegionsForAll = new()
                {
                    GameRegion.GC_USA,
                    GameRegion.GC_EUR,
                    GameRegion.GC_JAP,
                };

                // Create files for all regions
                // foreach (GameRegion gameRegion in GameRegion.GetValues(typeof(GameRegion)))
                foreach (GameRegion gameRegion in gameRegionsForAll)
                {
                    if (gameRegion != GameRegion.All)
                    {
                        // Update language to be used with resource system.
                        string langTag = fcSettings.GetLanguageTagString(gameRegion);
                        Res.UpdateCultureInfo(langTag);

                        fileDefs.Add(GenGciFileDef(id, seedGenResults, fcSettings, gameRegion));
                    }
                }
            }
            else
            {
                // Update language to be used with resource system.
                string langTag = fcSettings.GetLanguageTagString();
                Res.UpdateCultureInfo(langTag);

                // Create file for one region
                fileDefs.Add(GenGciFileDef(id, seedGenResults, fcSettings, fcSettings.gameRegion));
            }

            PrintFileDefs(id, seedGenResults, fcSettings, fileDefs);

            // Console.WriteLine("Done!");
            // Console.WriteLine("Generating Spoiler Log.");
            // BackendFunctions.GenerateSpoilerLog(startingRoom, seedHash);
            //         IEnumerable<string> fileList = new string[]
            //         {
            //             "TPR-v1.0-" + seedHash + ".txt",
            //             "TPR-v1.0-" + seedHash + "-Seed-Data.gci"
            //         };
            //         BackendFunctions.CreateZipFile("Seed/TPR-v1.0-" + seedHash + ".zip", fileList);
            //         Console.WriteLine("Generation Complete!");
            //         generationStatus = true;
            //         break;
            //     }
            //     // If for some reason the assumed fill fails, we want to dump everything and start over.
            //     catch (ArgumentOutOfRangeException a)
            //     {
            //         Console.WriteLine(a + " No checks remaining, starting over..");
            // StartOver();
            //         continue;
            //     }
            // }

            CleanUp();
            // return generationStatus;

            return true;
        }

        private static Tuple<Dictionary<string, object>, byte[]> GenGciFileDef(
            string seedId,
            SeedGenResults seedGenResults,
            FileCreationSettings fcSettings,
            GameRegion gameRegionOverride
        )
        {
            byte[] bytes = SeedData.GenerateSeedDataBytes(
                seedGenResults,
                fcSettings,
                gameRegionOverride
            );

            Dictionary<string, object> dict = new();

            string gameVer;
            switch (gameRegionOverride)
            {
                case GameRegion.GC_USA:
                    gameVer = "E";
                    break;
                case GameRegion.GC_EUR:
                    gameVer = "P";
                    break;
                case GameRegion.GC_JAP:
                    gameVer = "J";
                    break;
                default:
                    throw new Exception("Did not specify output region");
            }

            string fileName =
                "Tpr-" + gameVer + "-" + seedGenResults.playthroughName + "-" + seedId;

            fileName += ".gci";

            dict.Add("name", fileName);
            dict.Add("length", bytes.Length);

            return new(dict, bytes);
        }

        private static void PrintFileDefs(
            string seedId,
            SeedGenResults seedGenResults,
            FileCreationSettings fcSettings,
            List<Tuple<Dictionary<string, object>, byte[]>> fileDefs
        )
        {
            if (fileDefs.Count > 1)
            {
                // Write ZIP file instead
                string zipFilename = $"TPR--{seedGenResults.playthroughName}--{seedId}.zip";
                fileDefs = MergeFileDefsToZip(zipFilename, fileDefs);
            }

            List<Dictionary<string, object>> jsonRoot = fileDefs
                .Select(tuple => tuple.Item1)
                .ToList();

            string fileDefMetaJson = JsonConvert.SerializeObject(jsonRoot);
            string hexValue = fileDefMetaJson.Length.ToString("x8");

            // Write 8 chars of hex for length of json
            // json looks like: [{name: '', length: number}, {}]
            Console.Write("BYTES:");
            Console.Write(hexValue);
            Console.Write(fileDefMetaJson);

            // Write file bytes
            for (int i = 0; i < fileDefs.Count; i++)
            {
                byte[] bytes = fileDefs[i].Item2;

                using (Stream myOutStream = Console.OpenStandardOutput())
                {
                    myOutStream.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private static List<Tuple<Dictionary<string, object>, byte[]>> MergeFileDefsToZip(
            string filename,
            List<Tuple<Dictionary<string, object>, byte[]>> fileDefs
        )
        {
            byte[] compressedBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (
                    ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true)
                )
                {
                    for (int i = 0; i < fileDefs.Count; i++)
                    {
                        string name = (string)fileDefs[i].Item1["name"];
                        byte[] bytes = fileDefs[i].Item2;

                        ZipArchiveEntry demoFile = archive.CreateEntry(name);

                        using (Stream entryStream = demoFile.Open())
                        {
                            using (BinaryWriter streamWriter = new BinaryWriter(entryStream))
                            {
                                streamWriter.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }
                }

                compressedBytes = memoryStream.ToArray();
            }

            Dictionary<string, object> meta = new();
            meta.Add("name", filename);
            meta.Add("length", compressedBytes.Length);

            List<Tuple<Dictionary<string, object>, byte[]>> list = new();

            list.Add(new(meta, compressedBytes));

            return list;
        }

        /// <summary>
        /// Places a given item into a given check.
        /// </summary>
        /// <param name="startingRoom"> The room that the player will start the game from. </param>
        /// <returns> A complete playthrough graph for the player to traverse. </returns>
        public static List<Room> GeneratePlaythroughGraph(Room startingRoom)
        {
            List<Room> playthroughGraph = new();
            Room availableRoom;

            int availableRooms = 1;
            List<Room> roomsToExplore = new();

            foreach (KeyValuePair<string, Room> roomList in Randomizer.Rooms.RoomDict.ToList())
            {
                Room currentRoom = roomList.Value;
                currentRoom.Visited = false;
                currentRoom.ReachedByPlaythrough = false;
                Randomizer.Rooms.RoomDict[currentRoom.RoomName] = currentRoom;
            }

            startingRoom.Visited = true;
            playthroughGraph.Add(startingRoom);
            if (Randomizer.SSettings.openMap)
            {
                if (Randomizer.SSettings.faronTwilightCleared)
                {
                    if (LogicFunctions.CanUse(Item.Shadow_Crystal))
                    {
                        availableRoom = Randomizer.Rooms.RoomDict["South Faron Woods"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;

                        availableRoom = Randomizer.Rooms.RoomDict["North Faron Woods"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;
                    }
                }

                if (Randomizer.SSettings.eldinTwilightCleared)
                {
                    if (LogicFunctions.CanUse(Item.Shadow_Crystal))
                    {
                        availableRoom = Randomizer.Rooms.RoomDict["Lower Kakariko Village"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;

                        availableRoom = Randomizer.Rooms.RoomDict["Kakariko Gorge"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;

                        availableRoom = Randomizer.Rooms.RoomDict["Death Mountain Volcano"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;
                    }
                }

                if (Randomizer.SSettings.lanayruTwilightCleared)
                {
                    if (LogicFunctions.CanUse(Item.Shadow_Crystal))
                    {
                        availableRoom = Randomizer.Rooms.RoomDict["Lake Hylia"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;

                        availableRoom = Randomizer.Rooms.RoomDict["Outside Castle Town West"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;

                        availableRoom = Randomizer.Rooms.RoomDict["Zoras Throne Room"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;
                    }
                }

                if (Randomizer.SSettings.skipSnowpeakEntrance)
                {
                    if (LogicFunctions.CanUse(Item.Shadow_Crystal))
                    {
                        availableRoom = Randomizer.Rooms.RoomDict["Snowpeak Summit Upper"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;
                    }
                }

                if (Randomizer.SSettings.totEntrance != TotEntrance.Closed)
                {
                    if (LogicFunctions.CanUse(Item.Shadow_Crystal))
                    {
                        availableRoom = Randomizer.Rooms.RoomDict["Sacred Grove Lower"];
                        playthroughGraph.Add(availableRoom);
                        availableRoom.Visited = true;
                    }
                }
            }

            // Build the world by parsing through each room, linking their neighbours, and setting the logic for the checks in the room to reflect the world.
            while (availableRooms > 0)
            {
                availableRooms = 0;
                roomsToExplore.Add(startingRoom);
                foreach (KeyValuePair<string, Room> roomList in Randomizer.Rooms.RoomDict.ToList())
                {
                    Room currentRoom = roomList.Value;
                    currentRoom.Visited = false;
                    Randomizer.Rooms.RoomDict[currentRoom.RoomName] = currentRoom;
                }
                while (roomsToExplore.Count > 0)
                {
                    //Console.WriteLine("Currently Exploring: " + roomsToExplore[0].RoomName);
                    for (int i = 0; i < roomsToExplore[0].Exits.Count; i++)
                    {
                        // If you can access the neighbour and it hasnt been visited yet.
                        //Console.WriteLine("Exit: " + roomsToExplore[0].Exits[i].GetOriginalName());
                        if (roomsToExplore[0].Exits[i].ConnectedArea != "")
                        {
                            if (
                                Randomizer
                                    .Rooms
                                    .RoomDict[roomsToExplore[0].Exits[i].ConnectedArea]
                                    .Visited == false
                            )
                            {
                                // Parse the neighbour's requirements to find out if we can access it
                                var areNeighbourRequirementsMet = false;
                                /*Console.WriteLine(
                                    "Checking neighbor: "
                                        + Randomizer.Rooms.RoomDict[
                                            roomsToExplore[0].Exits[i].ConnectedArea
                                        ].RoomName
                                );*/
                                if (SSettings.logicRules == LogicRules.No_Logic)
                                {
                                    areNeighbourRequirementsMet = true;
                                }
                                else
                                {
                                    areNeighbourRequirementsMet = Logic.EvaluateRequirements(
                                        roomsToExplore[0].RoomName,
                                        roomsToExplore[0].Exits[i].Requirements
                                    );
                                }

                                if ((bool)areNeighbourRequirementsMet == true)
                                {
                                    if (
                                        !Randomizer
                                            .Rooms
                                            .RoomDict[roomsToExplore[0].Exits[i].ConnectedArea]
                                            .ReachedByPlaythrough
                                    )
                                    {
                                        availableRooms++;
                                        Randomizer
                                            .Rooms
                                            .RoomDict[roomsToExplore[0].Exits[i].ConnectedArea]
                                            .ReachedByPlaythrough = true;
                                        playthroughGraph.Add(
                                            Randomizer.Rooms.RoomDict[
                                                roomsToExplore[0].Exits[i].ConnectedArea
                                            ]
                                        );
                                    }
                                    roomsToExplore.Add(
                                        Randomizer.Rooms.RoomDict[
                                            roomsToExplore[0].Exits[i].ConnectedArea
                                        ]
                                    );
                                    Randomizer
                                        .Rooms
                                        .RoomDict[roomsToExplore[0].Exits[i].ConnectedArea]
                                        .Visited = true;

                                    /* Console.WriteLine(
                                         "Neighbour: "
                                             + Randomizer.Rooms.RoomDict[
                                                 roomsToExplore[0].Exits[i].ConnectedArea
                                             ].RoomName
                                             + " added to room list."
                                     );*/
                                }
                                /*else
                                {
                                    Console.WriteLine(
                                        "Neighbour: "
                                            + Randomizer.Rooms.RoomDict[
                                                roomsToExplore[0].Exits[i].ConnectedArea
                                            ].RoomName
                                            + " requirement not met"
                                    );
                                }*/
                            }
                        }
                    }

                    roomsToExplore.Remove(roomsToExplore[0]);
                }
            }

            return playthroughGraph;
        }

        /// <summary>
        /// Places a given item into a given check.
        /// </summary>
        /// <param name="item"> The item to be placed in the check. </param>
        /// <param name="check"> The check to recieve the item. </param>
        private static void PlaceItemInCheck(Item item, Check check)
        {
            // Console.WriteLine("Placing item in check.");
            check.itemWasPlaced = true;
            check.itemId = item;

            //Console.WriteLine("Placed " + check.itemId + " in check " + check.checkName);
        }

        private static void StartOver()
        {
            // If we are restarting we want to empty the player's inventory since we don't know what items we have and it won't matter if we are restarting.
            Randomizer.Items.heldItems.Clear();

            // Next we want to change any checks that were marked as unrequired since the generator could select different dungeons next time. We also want to make all checks available to be placed again.
            foreach (KeyValuePair<string, Check> checkList in Checks.CheckDict.ToList())
            {
                Check currentCheck = checkList.Value;
                if (currentCheck.checkStatus == "Excluded-Unrequired")
                {
                    currentCheck.checkStatus = "Ready";
                }
                currentCheck.hasBeenReached = false;
                currentCheck.itemWasPlaced = false;
                Checks.CheckDict[currentCheck.checkName] = currentCheck;
            }

            // Next for Entrance rando, we want to clear the current room and entrance tables since they will be re-generated as the generator will try to re-shuffle the entrances a different way to find a placement that is successful.
            Randomizer.Rooms.RoomDict.Clear();
            DeserializeRooms(SSettings);
            Randomizer.EntranceRandomizer.SpawnTable.Clear();

            // Finally set the required dungeons to 0 since the value may change during the next attempt.
            Randomizer.RequiredDungeons = 0;
        }

        private static void SetupGraph()
        {
            // We want to be safe and make sure that the room classes are prepped and ready to be linked together. Then we define our starting room.
            foreach (KeyValuePair<string, Room> roomList in Randomizer.Rooms.RoomDict.ToList())
            {
                Room currentRoom = roomList.Value;
                currentRoom.Visited = false;
                Randomizer.Rooms.RoomDict[currentRoom.RoomName] = currentRoom;
            }

            // This line is just filler until we have a random starting room
            Room startingRoom = Randomizer.Rooms.RoomDict["Outside Links House"];

            Entrance rootExit = new();
            rootExit.ConnectedArea = startingRoom.RoomName;
            rootExit.Requirements = "(true)";

            Randomizer.Rooms.RoomDict["Root"].Exits.Add(rootExit);
        }

        private static void DeserializeChecks(SharedSettings SSettings)
        {
            string[] files;

            // We keep the logic files seperate based on their logic. GC and Wii should use the same logic.
            if (SSettings.logicRules == LogicRules.Glitchless)
            {
                files = System.IO.Directory.GetFiles(
                    Global.CombineRootPath("./World/Checks/"),
                    "*",
                    SearchOption.AllDirectories
                );
            }
            else
            {
                files = System.IO.Directory.GetFiles(
                    Global.CombineRootPath("./Glitched-World/Checks/"),
                    "*",
                    SearchOption.AllDirectories
                );
            }

            // Sort so that the item placement algorithm produces the exact same
            // result in production and development.
            // If we have already generated a dictionary from DeserializeCheckMetadata, then we only need to apply the logic data from the files.
            Array.Sort(files, new FilenameComparer());
            if (Checks.CheckDict.Count == 0)
            {
                foreach (string file in files)
                {
                    string contents = File.ReadAllText(file);
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Checks.CheckDict.Add(fileName, new Check());
                    Checks.CheckDict[fileName] = JsonConvert.DeserializeObject<Check>(contents);
                    Check currentCheck = Checks.CheckDict[fileName];
                    currentCheck.checkName = fileName;
                    currentCheck.requirements = "(" + currentCheck.requirements + ")";
                    currentCheck.checkStatus = "Ready";
                    currentCheck.itemWasPlaced = false;
                    currentCheck.isRequired = false;
                    Checks.CheckDict[fileName] = currentCheck;
                }
            }
            else
            {
                foreach (string file in files)
                {
                    string contents = File.ReadAllText(file);
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Check currentCheck = JsonConvert.DeserializeObject<Check>(contents);
                    Checks.CheckDict[fileName].requirements = "(" + currentCheck.requirements + ")";
                    Checks.CheckDict[fileName].checkCategory = currentCheck.checkCategory;
                    Checks.CheckDict[fileName].checkName = fileName;
                    Checks.CheckDict[fileName].checkStatus = "Ready";
                    Checks.CheckDict[fileName].itemWasPlaced = false;
                    Checks.CheckDict[fileName].isRequired = false;
                    Checks.CheckDict[fileName].itemId = currentCheck.itemId;
                }
            }
        }

        private static void DeserializeCheckData(
            SharedSettings SSettings,
            FileCreationSettings FcSettings
        )
        {
            string[] files = null;

            // The GC/Wii files have different offsets for the data that is needed to replace certain checks.
            switch (FcSettings.gameRegion)
            {
                // For now, 'All' only generates for GameCube until we do more
                // work related to Wii code.
                case GameRegion.GC_USA:
                case GameRegion.GC_EUR:
                case GameRegion.GC_JAP:
                case GameRegion.All:
                {
                    files = System.IO.Directory.GetFiles(
                        Global.CombineRootPath("./Assets/CheckMetadata/Gamecube/"),
                        "*",
                        SearchOption.AllDirectories
                    );
                    break;
                }

                case GameRegion.WII_10_USA:
                case GameRegion.WII_10_EU:
                case GameRegion.WII_10_JP:
                {
                    files = System.IO.Directory.GetFiles(
                        Global.CombineRootPath("./Assets/CheckMetadata/Wii1.0/"),
                        "*",
                        SearchOption.AllDirectories
                    );
                    break;
                }
            }

            // Sort so that the item placement algorithm produces the exact same
            // result in production and development.
            Array.Sort(files, new FilenameComparer());

            foreach (string file in files)
            {
                string contents = File.ReadAllText(file);
                string fileName = Path.GetFileNameWithoutExtension(file);
                Checks.CheckDict.Add(fileName, new Check());
                Checks.CheckDict[fileName] = JsonConvert.DeserializeObject<Check>(contents);
                Checks.CheckDict[fileName].checkName = fileName;
            }

            DeserializeChecks(SSettings);
        }

        public static void DeserializeRooms(SharedSettings SSettings)
        {
            //Before anything, create an entry for the root of the world
            Randomizer.Rooms.RoomDict.Add("Root", new Room());
            Randomizer.Rooms.RoomDict["Root"].RoomName = "Root";
            Randomizer.Rooms.RoomDict["Root"].Exits = new();
            Randomizer.Rooms.RoomDict["Root"].Checks = new();
            Randomizer.Rooms.RoomDict["Root"].Visited = false;

            string[] files;
            if (SSettings.logicRules == LogicRules.Glitchless)
            {
                files = System.IO.Directory.GetFiles(
                    Global.CombineRootPath("./World/Rooms/"),
                    "*",
                    SearchOption.AllDirectories
                );
            }
            else
            {
                files = System.IO.Directory.GetFiles(
                    Global.CombineRootPath("./Glitched-World/Rooms/"),
                    "*",
                    SearchOption.AllDirectories
                );
            }

            // Sort so that the item placement algorithm produces the exact same
            // result in production and development.
            Array.Sort(files, new FilenameComparer());

            foreach (string file in files)
            {
                string contents = File.ReadAllText(file);
                string fileName = Path.GetFileNameWithoutExtension(file);

                //Console.WriteLine("Loading Room File: " + fileName);

                List<Room> fileRooms = JsonConvert.DeserializeObject<List<Room>>(contents);
                foreach (Room room in fileRooms)
                {
                    Randomizer.Rooms.RoomDict.Add(room.RoomName, new Room());
                    Randomizer.Rooms.RoomDict[room.RoomName] = room;
                    Room currentRoom = Randomizer.Rooms.RoomDict[room.RoomName];
                    currentRoom.Visited = false;
                    for (int i = 0; i < currentRoom.Exits.Count; i++)
                    {
                        currentRoom.Exits[i].Requirements =
                            "(" + currentRoom.Exits[i].Requirements + ")";

                        currentRoom.Exits[i].ParentArea = currentRoom.RoomName;
                        currentRoom.Exits[i].OriginalConnectedArea = currentRoom
                            .Exits[i]
                            .ConnectedArea;
                    }

                    Randomizer.Rooms.RoomDict[room.RoomName] = currentRoom;
                    //Console.WriteLine("Room created: " + room.RoomName);
                }

                //Console.WriteLine("Room File Loaded " + fileName);
            }
        }

        /// <summary>
        /// summary text.
        /// </summary>
        private static void CleanUp()
        {
            Checks.CheckDict.Clear();
            Rooms.RoomDict.Clear();
            Items.ShuffledDungeonRewards.Clear();
            Items.RandomizedDungeonRegionItems.Clear();
            Randomizer.Items.RandomizedImportantItems.Clear();
            Items.JunkItems.Clear();
            Randomizer.Items.heldItems.Clear();
            Randomizer.Items.BaseItemPool.Clear();
        }

        public struct requiredDungeons
        {
            public string dungeonReward;
            public bool isRequired;
            public List<String> requirementChecks;

            public requiredDungeons(
                string dungeonReward,
                bool isRequired,
                List<string> requirementChecks
            )
            {
                this.dungeonReward = dungeonReward;
                this.isRequired = isRequired;
                this.requirementChecks = requirementChecks;
            }
        };
    }

    internal class FilenameComparer : IComparer<string>
    {
        int IComparer<string>.Compare(string s1, string s2)
        {
            return String.CompareOrdinal(
                Path.GetFileNameWithoutExtension(s1),
                Path.GetFileNameWithoutExtension(s2)
            );
        }
    }
}
