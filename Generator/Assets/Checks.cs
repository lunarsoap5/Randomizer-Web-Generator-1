namespace TPRandomizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TPRandomizer.SSettings.Enums;

    /// <summary>
    /// Identifies the basic structure containing multiple fields used to identify a check in the randomizer..
    /// </summary>
    public class Check
    {
        public string checkName { get; set; } // The common name for the check this can be used in the randomizer to identify the check."

        public string checkStatus { get; set; } // Identifies if the check is excluded or not. We can write the randomizer to not place important items in excluded checks

        public List<string> checkCategory { get; set; } // Allows grouping of checks to make it easier to randomize them based on their type, region, exclusion status, etc.
        public List<string> dataCategory { get; set; } // Allows grouping of checks to make it easier to randomize them based on their type, region, exclusion status, etc.

        public bool itemWasPlaced { get; set; } // Identifies if we already placed an item on this check.

        public bool hasBeenReached { get; set; } // indicates that we can get the current check. Prevents unneccesary repetitive parsing.

        // Data that will be stored in the rando-data .gci file.
        public Item itemId { get; set; } // The original item id of the check. This allows us to make an array of all items in the item pool for randomization purposes. Also is useful for documentation purposes.

        public List<byte> stageIDX { get; set; } // Used by DZX, SHOP, POE, SKILL, and BOSS checks. The index of the stage where the check is located. In flag checks, this could be the nodeID

        public byte roomIDX { get; set; } // Used by SKILL checks to determine which wolf is being learned from.

        public List<string> hash { get; set; } // Used by DZX checks. The hash of the actor that will be modified by a DZX-based check replacement.

        public List<string> dzxTag { get; set; } // Used by DZX checks. The type of actor that will be modified.

        public List<string[]> actrData { get; set; } // Used by DZX checks. The data structure that will replace the current loaded ACTR.

        public string flag { get; set; } // Used by POE, Event and SKILL checks. The flag to check to determine which check to replace.

        public List<byte> fileDirectoryType { get; set; } // Used by ARC checks. The type of file directory where the item is stored.

        public List<byte> replacementType { get; set; } // Used by ARC checks. The type of replacement taking place.

        public List<string> moduleID { get; set; } // Used by REL checks. The module ID for the rel file being loaded.

        public List<string> relOffsets { get; set; } // Used by REL checks.
        public List<byte> relReplacementType { get; set; } // Used by ARC checks. The type of replacement taking place.

        public List<string> arcOffsets { get; set; } // Used by ARC checks.

        public string fileName { get; set; }

        public List<string> overrideInstruction { get; set; } // Used by REL checks. The override instruction to be used when replacing the item in the rel.
        public string chestLowerNibble { get; set; } // Used by Chests to determine functionality

        public bool isRequired { get; set; }
    }

    /// <summary>
    /// Contains function and structure definitions for all usages related to the Check class.
    /// </summary>
    public class CheckFunctions
    {
        public static List<string> forestRequirementChecks =
            new()
            {
                "FT Big Baba Key",
                "FT Big Key Chest",
                "FT Central Chest Behind Stairs",
                "FT Central Chest Hanging From Web",
                "FT Central North Chest",
                "FT East Tile Worm Chest",
                "FT East Water Cave Chest",
                "FT Entrance Vines Chest",
                "FT Gale Boomerang",
                "FT North Deku Like Chest",
                "FT Second Monkey Under Bridge Chest",
                "FT Totem Pole Chest",
                "FT West Deku Like Chest",
                "FT West Tile Worm Chest Behind Stairs",
                "FT West Tile Worm Room Vines Chest",
                "FT Windless Bridge Chest",
                "Catch A Greengill",
                "Catch An Ordon Catfish"
            };
        public static List<string> minesRequirementChecks =
            new()
            {
                "GM After Crystal Switch Room Magnet Wall Chest",
                "GM Beamos Room Chest",
                "GM Chest Before Dangoro",
                "GM Crystal Switch Room Small Chest",
                "GM Crystal Switch Room Underwater Chest",
                "GM Dangoro Chest",
                "GM Entrance Chest",
                "GM Gor Amato Chest",
                "GM Gor Amato Key Shard",
                "GM Gor Amato Small Chest",
                "GM Gor Ebizo Chest",
                "GM Gor Ebizo Key Shard",
                "GM Gor Liggs Chest",
                "GM Gor Liggs Key Shard",
                "GM Magnet Maze Chest",
                "GM Main Magnet Room Bottom Chest",
                "GM Main Magnet Room Top Chest",
                "GM Outside Beamos Chest",
                "GM Outside Clawshot Chest",
                "GM Outside Underwater Chest",
                "Catch A Greengill",
                "Catch A Hyrule Bass"
            };

        public static List<string> lakebedRequirementChecks =
            new()
            {
                "LBT Before Deku Toad Alcove Chest",
                "LBT Before Deku Toad Underwater Left Chest",
                "LBT Before Deku Toad Underwater Right Chest",
                "LBT Big Key Chest",
                "LBT Central Room Chest",
                "LBT Central Room Small Chest",
                "LBT Central Room Spire Chest",
                "LBT Chandelier Chest",
                "LBT Deku Toad Chest",
                "LBT East Lower Waterwheel Bridge Chest",
                "LBT East Lower Waterwheel Stalactite Chest",
                "LBT East Second Floor Southeast Chest",
                "LBT East Second Floor Southwest Chest",
                "LBT East Water Supply Clawshot Chest",
                "LBT East Water Supply Small Chest",
                "LBT Lobby Left Chest",
                "LBT Lobby Rear Chest",
                "LBT Stalactite Room Chest",
                "LBT Underwater Maze Small Chest",
                "LBT West Lower Small Chest",
                "LBT West Second Floor Central Small Chest",
                "LBT West Second Floor Northeast Chest",
                "LBT West Second Floor Southeast Chest",
                "LBT West Second Floor Southwest Underwater Chest",
                "LBT West Water Supply Chest",
                "LBT West Water Supply Small Chest",
                "Catch A Greengill",
                "Catch A Hylian Loach"
            };

        public static List<string> arbitersRequirementChecks =
            new()
            {
                "AG Big Key Chest",
                "AG Death Sword Chest",
                "AG East Lower Turnable Redead Chest",
                "AG East Turning Room Poe",
                "AG East Upper Turnable Chest",
                "AG East Upper Turnable Redead Chest",
                "AG Entrance Chest",
                "AG Ghoul Rat Room Chest",
                "AG Hidden Wall Poe",
                "AG North Turning Room Chest",
                "AG Spinner Room First Small Chest",
                "AG Spinner Room Lower Central Small Chest",
                "AG Spinner Room Lower North Chest",
                "AG Spinner Room Second Small Chest",
                "AG Spinner Room Stalfos Alcove Chest",
                "AG Torch Room East Chest",
                "AG Torch Room Poe",
                "AG Torch Room West Chest",
                "AG West Chandelier Chest",
                "AG West Poe",
                "AG West Small Chest Behind Block",
                "AG West Stalfos Northeast Chest",
                "AG West Stalfos West Chest",
            };

        public static List<string> snowpeakRequirementChecks =
            new()
            {
                "SPR Ball and Chain",
                "SPR Broken Floor Chest",
                "SPR Chapel Chest",
                "SPR Chest After Darkhammer",
                "SPR Courtyard Central Chest",
                "SPR East Courtyard Buried Chest",
                "SPR East Courtyard Chest",
                "SPR Ice Room Poe",
                "SPR Lobby Armor Poe",
                "SPR Lobby Chandelier Chest",
                "SPR Lobby East Armor Chest",
                "SPR Lobby Poe",
                "SPR Lobby West Armor Chest",
                "SPR Mansion Map",
                "SPR Northeast Chandelier Chest",
                "SPR Ordon Pumpkin Chest",
                "SPR West Cannon Room Central Chest",
                "SPR West Cannon Room Corner Chest",
                "SPR West Courtyard Buried Chest",
                "SPR Wooden Beam Central Chest",
                "SPR Wooden Beam Chandelier Chest",
                "SPR Wooden Beam Northwest Chest",
            };

        public static List<string> totRequirementChecks =
            new()
            {
                "ToT Armos Antechamber East Chest",
                "ToT Armos Antechamber North Chest",
                "ToT Armos Antechamber Statue Chest",
                "ToT Big Key Chest",
                "ToT Chest Before Darknut",
                "ToT Darknut Chest",
                "ToT First Staircase Armos Chest",
                "ToT First Staircase Gohma Gate Chest",
                "ToT First Staircase Window Chest",
                "ToT Floor Switch Puzzle Room Upper Chest",
                "ToT Guillotine Chest",
                "ToT Lobby Lantern Chest",
                "ToT Moving Wall Beamos Room Chest",
                "ToT Moving Wall Dinalfos Room Chest",
                "ToT Poe Above Scales",
                "ToT Poe Behind Gate",
                "ToT Scales Gohma Chest",
                "ToT Scales Upper Chest",
            };

        public static List<string> cityRequirementChecks =
            new()
            {
                "CitS Aeralfos Chest",
                "CitS Baba Tower Alcove Chest",
                "CitS Baba Tower Narrow Ledge Chest",
                "CitS Baba Tower Top Small Chest",
                "CitS Big Key Chest",
                "CitS Central Outside Ledge Chest",
                "CitS Central Outside Poe Island Chest",
                "CitS Chest Behind North Fan",
                "CitS Chest Below Big Key Chest",
                "CitS East First Wing Chest After Fans",
                "CitS East Tile Worm Small Chest",
                "CitS East Wing After Dinalfos Alcove Chest",
                "CitS East Wing After Dinalfos Ledge Chest",
                "CitS East Wing Lower Level Chest",
                "CitS Garden Island Poe",
                "CitS Poe Above Central Fan",
                "CitS Underwater East Chest",
                "CitS Underwater West Chest",
                "CitS West Garden Corner Chest",
                "CitS West Garden Ledge Chest",
                "CitS West Garden Lone Island Chest",
                "CitS West Garden Lower Chest",
                "CitS West Wing Baba Balcony Chest",
                "CitS West Wing First Chest",
                "CitS West Wing Narrow Ledge Chest",
                "CitS West Wing Tile Worm Chest",
            };

        public static List<string> palaceRequirementChecks =
            new()
            {
                "PoT Big Key Chest",
                "PoT Central First Room Chest",
                "PoT Central Outdoor Chest",
                "PoT Central Tower Chest",
                "PoT Collect Both Sols",
                "PoT East Wing First Room East Alcove Chest",
                "PoT East Wing First Room North Small Chest",
                "PoT East Wing First Room West Alcove Chest",
                "PoT East Wing First Room Zant Head Chest",
                "PoT East Wing Second Room Northeast Chest",
                "PoT East Wing Second Room Northwest Chest",
                "PoT East Wing Second Room Southeast Chest",
                "PoT East Wing Second Room Southwest Chest",
                "PoT West Wing Chest Behind Wall of Darkness",
                "PoT West Wing First Room Central Chest",
                "PoT West Wing Second Room Central Chest",
                "PoT West Wing Second Room Lower South Chest",
                "PoT West Wing Second Room Southeast Chest",
            };

        public static List<string> postFyrusChecks =
            new()
            {
                "Kak Village Malo Mart Hawkeye",
                "Talo Sharpshooting",
                "Death Mountain Trail Poe",
            };

        public static List<string> postBlizettaChecks = new() { "Snowboard Racing Prize", };

        public static List<string> postArmogohmaChecks =
            new()
            {
                "Renados Letter",
                "Telma Invoice",
                "Wooden Statue",
                "Ilia Charm",
                "Ilia Memory Reward",
                "HV Poe",
                "Skybook From Impaz",
                "Doctors Office Balcony Chest",
                "North CT Golden Wolf",
                "Cats Hide and Seek Minigame",
            };

        public static List<string> questChecks =
            new() { "Renados Letter", "Telma Invoice", "Wooden Statue", "Ilia Charm", };

        // All of these checks are forced to be vanilla until a way to randomize them is figured out or if they are not meant to be randomized for the sake of events and the like.
        public static List<string> vanillaChecks =
            new()
            {
                "South Faron Portal",
                "North Faron Portal",
                "Sacred Grove Portal",
                "Kak Gorge Portal",
                "Kak Village Portal",
                "Death Mountain Portal",
                "Bridge of Eldin Portal",
                "CT Portal",
                "ZD Portal",
                "Lake Hylia Portal",
                "Desert Portal",
                "Snowpeak Portal",
                "Mirror Chamber Portal",
                "UZR Portal",
                "FT Diababa",
                "GM Fyrus",
                "LBT Morpheel",
                "AG Stallord",
                "SPR Blizzeta",
                "ToT Armogohma",
                "CitS Argorok",
                "PoT Zant",
                "HC Ganondorf",
            };

        // These are checks that appear in multiple places that, due to being in dungeons, can be isolated from some connections
        public static List<string> IsolatableChecks =
            new()
            {
                "Catch A Greengill",
                "Catch A Hyrule Bass",
                "Catch An Ordon Catfish",
                "Catch A Hylian Loach",
                "Catch A Hylian Pike"
            };

        /// <summary>
        /// A dictionary of all randomizer locations.
        /// </summary>
        public Dictionary<string, Check> CheckDict = new();

        /// <summary>
        /// summary text.
        /// </summary>
        public static void GenerateCheckList()
        {
            SharedSettings parseSetting = Randomizer.SSettings;
            var dungeonSkSettings = new[]
            {
                parseSetting.ftSmallKeySettings,
                parseSetting.gmSmallKeySettings,
                parseSetting.lbtSmallKeySettings,
                parseSetting.agSmallKeySettings,
                parseSetting.sprSmallKeySettings,
                parseSetting.totSmallKeySettings,
                parseSetting.citsSmallKeySettings,
                parseSetting.potSmallKeySettings,
                parseSetting.hcSmallKeySettings,
            };

            var dungeonBkSettings = new[]
            {
                parseSetting.ftBigKeySettings,
                parseSetting.gmBigKeySettings,
                parseSetting.lbtBigKeySettings,
                parseSetting.agBigKeySettings,
                parseSetting.sprBigKeySettings,
                parseSetting.totBigKeySettings,
                parseSetting.citsBigKeySettings,
                parseSetting.potBigKeySettings,
                parseSetting.hcBigKeySettings,
            };

            var dungeonMcSettings = new[]
            {
                parseSetting.ftMapAndCompassSettings,
                parseSetting.gmMapAndCompassSettings,
                parseSetting.lbtMapAndCompassSettings,
                parseSetting.agMapAndCompassSettings,
                parseSetting.sprMapAndCompassSettings,
                parseSetting.totMapAndCompassSettings,
                parseSetting.citsMapAndCompassSettings,
                parseSetting.potMapAndCompassSettings,
                parseSetting.hcMapAndCompassSettings,
            };
            foreach (KeyValuePair<string, Check> check in Randomizer.Checks.CheckDict)
            {
                Check currentCheck = check.Value;

                for (int i = 0; i < RoomFunctions.AllDungeonNames.Count(); i++)
                {
                    if (
                        dungeonSkSettings[i] == SmallKeySettings.Vanilla
                        && ValidateDungeonSmallKeyCheck(
                            currentCheck,
                            RoomFunctions.AllDungeonNames[i]
                        )
                    )
                    {
                        currentCheck.checkStatus = "Vanilla";
                        break;
                    }

                    if (
                        dungeonBkSettings[i] == BigKeySettings.Vanilla
                        && ValidateDungeonBigKeyCheck(
                            currentCheck,
                            RoomFunctions.AllDungeonNames[i]
                        )
                    )
                    {
                        currentCheck.checkStatus = "Vanilla";
                        break;
                    }

                    if (
                        dungeonMcSettings[i] == MapAndCompassSettings.Vanilla
                        && ValidateDungeonMapCompassCheck(
                            currentCheck,
                            RoomFunctions.AllDungeonNames[i]
                        )
                    )
                    {
                        currentCheck.checkStatus = "Vanilla";
                        break;
                    }
                }

                // Some NPCs give dungeon items (Yeta give dungeon map, Elders give key shards) so we need to account for the possibility of conflicting settings.
                if (!parseSetting.shuffleNpcItems)
                {
                    if (currentCheck.checkCategory.Contains("Npc"))
                    {
                        bool isExcluded = false;
                        for (int i = 0; i < RoomFunctions.AllDungeonNames.Count(); i++)
                        {
                            if (
                                (
                                    dungeonSkSettings[i] == SmallKeySettings.Keysy
                                    && ValidateDungeonSmallKeyCheck(
                                        currentCheck,
                                        RoomFunctions.AllDungeonNames[i]
                                    )
                                )
                                || (
                                    dungeonBkSettings[i] == BigKeySettings.Keysy
                                    && ValidateDungeonBigKeyCheck(
                                        currentCheck,
                                        RoomFunctions.AllDungeonNames[i]
                                    )
                                )
                                || (
                                    dungeonMcSettings[i] == MapAndCompassSettings.Start_With
                                    && ValidateDungeonMapCompassCheck(
                                        currentCheck,
                                        RoomFunctions.AllDungeonNames[i]
                                    )
                                )
                            )
                            {
                                currentCheck.checkStatus = "Excluded";
                                isExcluded = true;
                                break;
                            }
                        }
                        if (!isExcluded)
                        {
                            currentCheck.checkStatus = "Vanilla";
                            Randomizer.Items.RandomizedImportantItems.Remove(currentCheck.itemId);
                            Randomizer.Items.RandomizedDungeonRegionItems.Remove(
                                currentCheck.itemId
                            );
                            Randomizer.Items.alwaysItems.Remove(currentCheck.itemId);
                        }
                    }
                }

                switch (parseSetting.shufflePoes)
                {
                    case PoeSettings.Vanilla:
                    {
                        if (currentCheck.checkCategory.Contains("Poe"))
                        {
                            currentCheck.checkStatus = "Vanilla";
                        }
                        break;
                    }

                    case PoeSettings.Overworld:
                    {
                        if (
                            currentCheck.checkCategory.Contains("Poe")
                            && !currentCheck.checkCategory.Contains("Overworld")
                        )
                        {
                            currentCheck.checkStatus = "Vanilla";
                        }
                        break;
                    }

                    case PoeSettings.Dungeons:
                    {
                        if (
                            currentCheck.checkCategory.Contains("Poe")
                            && !currentCheck.checkCategory.Contains("Dungeon")
                        )
                        {
                            currentCheck.checkStatus = "Vanilla";
                        }
                        break;
                    }
                }

                if (!parseSetting.shuffleGoldenBugs)
                {
                    if (currentCheck.checkCategory.Contains("Golden Bug"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleHiddenSkills)
                {
                    if (currentCheck.checkCategory.Contains("Hidden Skill"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                        Randomizer.Items.RandomizedImportantItems.Remove(currentCheck.itemId);
                    }
                }

                if (!parseSetting.shuffleSkyCharacters)
                {
                    if (currentCheck.checkCategory.Contains("Sky Book"))
                    {
                        if (parseSetting.skipCityEntrance)
                        {
                            currentCheck.checkStatus = "Excluded";
                        }
                        else
                        {
                            currentCheck.checkStatus = "Vanilla";
                            Randomizer.Items.RandomizedImportantItems.Remove(currentCheck.itemId);
                        }
                    }
                }

                if (!parseSetting.shuffleShopItems)
                {
                    if (
                        currentCheck.checkCategory.Contains("Shop")
                        || currentCheck.checkCategory.Contains("Npc - Shop")
                    )
                    {
                        currentCheck.checkStatus = "Vanilla";
                        Randomizer.Items.RandomizedImportantItems.Remove(currentCheck.itemId);
                        Randomizer.Items.alwaysItems.Remove(currentCheck.itemId);
                        foreach (Item startingItem in parseSetting.startingItems)
                        {
                            if (currentCheck.itemId == startingItem)
                            {
                                // If we are starting with the shop item and it is not randomized, replace it with a junk item.
                                currentCheck.checkStatus = "Excluded";
                            }
                        }
                    }
                }

                if (!parseSetting.shuffleHiddenRupees)
                {
                    if (currentCheck.checkCategory.Contains("Rupee - Hidden"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleFreestandingRupees)
                {
                    if (currentCheck.checkCategory.Contains("Rupee - Freestanding"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleFishJournals)
                {
                    if (currentCheck.checkCategory.Contains("Fish Journal"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleAnimalConversations)
                {
                    if (currentCheck.checkCategory.Contains("Animal Conversation"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleMinigames)
                {
                    if (currentCheck.checkCategory.Contains("Minigame"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }

                if (!parseSetting.shuffleLegendaryLoach)
                {
                    if (currentCheck.checkCategory.Contains("Legendary Loach"))
                    {
                        currentCheck.checkStatus = "Vanilla";
                    }
                }
            }

            List<string> removedQuestChecks = new();

            switch (parseSetting.iliaQuest)
            {
                case IliaQuest.Letter:
                {
                    removedQuestChecks.Add("Renados Letter");
                    Randomizer.Items.RandomizedImportantItems.Add(Item.Renados_Letter);
                    break;
                }
                case IliaQuest.Invoice:
                {
                    removedQuestChecks.Add("Renados Letter");
                    removedQuestChecks.Add("Telma Invoice");
                    Randomizer.Items.RandomizedImportantItems.Add(Item.Invoice);
                    break;
                }
                case IliaQuest.Statue:
                {
                    removedQuestChecks.Add("Renados Letter");
                    removedQuestChecks.Add("Telma Invoice");
                    removedQuestChecks.Add("Wooden Statue");
                    Randomizer.Items.RandomizedImportantItems.Add(Item.Wooden_Statue);
                    break;
                }
                case IliaQuest.Charm:
                {
                    removedQuestChecks.Add("Renados Letter");
                    removedQuestChecks.Add("Telma Invoice");
                    removedQuestChecks.Add("Wooden Statue");
                    removedQuestChecks.Add("Ilia Charm");
                    Randomizer.Items.RandomizedImportantItems.Add(Item.Ilias_Charm);
                    break;
                }

                default:
                {
                    break;
                }
            }

            foreach (string questCheck in removedQuestChecks)
            {
                questChecks.Remove(questCheck);
                Randomizer.Checks.CheckDict[questCheck].checkStatus = "Excluded";
                Randomizer.Items.RandomizedImportantItems.Remove(
                    Randomizer.Checks.CheckDict[questCheck].itemId
                );
            }

            // set up the vanilla checks

            vanillaChecks.AddRange(questChecks);
            if (Randomizer.SSettings.castleBKRequirements != CastleBKRequirements.None)
            {
                vanillaChecks.Add("HC Big Key Chest");
            }

            foreach (string vanillaCheck in vanillaChecks)
            {
                Randomizer.Checks.CheckDict[vanillaCheck].checkStatus = "Vanilla";
                Randomizer.Items.RandomizedImportantItems.Remove(
                    Randomizer.Checks.CheckDict[vanillaCheck].itemId
                );
            }

            foreach ((string checkName, Item item) in parseSetting.plandoChecks)
            {
                Randomizer.Checks.CheckDict[checkName].checkStatus = "Plando";
                Randomizer.Checks.CheckDict[checkName].itemId = item;
            }
        }

        public static bool ValidateDungeonSmallKeyCheck(Check smallKeyCheck, string Dungeon)
        {
            if (
                smallKeyCheck.checkCategory.Contains(Dungeon)
                && smallKeyCheck.checkCategory.Contains("Small Key")
            )
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDungeonBigKeyCheck(Check smallKeyCheck, string Dungeon)
        {
            if (
                smallKeyCheck.checkCategory.Contains(Dungeon)
                && smallKeyCheck.checkCategory.Contains("Big Key")
            )
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDungeonMapCompassCheck(Check smallKeyCheck, string Dungeon)
        {
            if (
                smallKeyCheck.checkCategory.Contains(Dungeon)
                && (
                    smallKeyCheck.checkCategory.Contains("Dungeon Map")
                    || smallKeyCheck.checkCategory.Contains("Compass")
                )
            )
            {
                return true;
            }
            return false;
        }
    }
}
