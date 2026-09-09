namespace TPRandomizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using TPRandomizer.SSettings.Enums;

    /// <summary>
    /// summary text.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Gets or sets the name of the room. This is the name we give the room to identify it (it can be a series of rooms that don't have requirements between each other to make the algorithm go faster).
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// Gets or sets the room name of the rooms adjacent to the current room.
        /// </summary>
        public List<Entrance> Exits { get; set; }

        /// <summary>
        /// Gets or sets a list of checks contained inside the room.
        /// </summary>
        public List<CheckData> Checks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current room has been visited in the current playthrough.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current room has been visited at least once in the current world generation.
        /// </summary>
        public bool ReachedByPlaythrough { get; set; }

        /// <summary>
        /// Gets or sets the logical region that the room is contained in.
        /// </summary>
        public string Region { get; set; }

        public List<string> getCheckNames()
        {
            List<string> listOfChecks = new();
            foreach (CheckData roomCheckData in Checks)
            {
                listOfChecks.Add(roomCheckData.CheckName);
            }
            return listOfChecks;
        }
    }

    public class CheckData
    {
        public string CheckName { get; set; }
        public string Requirements { get; set; }
        public string GlitchedRequirements { get; set; }
        public bool IsIsolated { get; set; }

        private LogicAST reqsCache;

        public LogicAST CachedRequirements()
        {
            if (reqsCache != null)
            {
                return reqsCache;
            }

            return reqsCache = Parser.Parse(Requirements);
        }
    }

    public enum StageIDs : int
    {
        Lakebed_Temple = 0x0,
        Morpheel = 0x1,
        Deku_Toad,
        Goron_Mines,
        Fyrus,
        Dangoro,
        Forest_Temple,
        Diababa,
        Ook,
        Temple_of_Time,
        Armogohma,
        Darknut,
        City_in_the_Sky,
        Argorok,
        Aeralfos,
        Palace_of_Twilight,
        Zant_Main_Room,
        Phantom_Zant_1,
        Phantom_Zant_2,
        Zant_Fight,
        Hyrule_Castle,
        Ganondorf_Castle,
        Ganondorf_Field,
        Ganondorf_Defeated,
        Arbiters_Grounds,
        Stallord,
        Death_Sword,
        Snowpeak_Ruins,
        Blizzeta,
        Darkhammer,
        Lanayru_Ice_Puzzle_Cave,
        Cave_of_Ordeals,
        Eldin_Long_Cave,
        Lake_Hylia_Long_Cave,
        Eldin_Goron_Stockcave,
        Grotto_1,
        Grotto_2,
        Grotto_3,
        Grotto_4,
        Grotto_5,
        Faron_Woods_Cave,
        Ordon_Ranch,
        Title_Screen,
        Ordon_Village,
        Ordon_Spring,
        Faron_Woods,
        Kakariko_Village,
        Death_Mountain,
        Kakariko_Graveyard,
        Zoras_River,
        Zoras_Domain,
        Snowpeak,
        Lake_Hylia,
        Castle_Town,
        Sacred_Grove,
        Bulblin_Camp,
        Hyrule_Field,
        Outside_Castle_Town,
        Bulblin_2,
        Gerudo_Desert,
        Mirror_Chamber,
        Upper_Zoras_River,
        Fishing_Pond,
        Hidden_Village,
        Hidden_Skill,
        Ordon_Village_Interiors,
        Hyrule_Castle_Sewers,
        Faron_Woods_Interiors,
        Kakariko_Village_Interiors,
        Death_Mountain_Interiors,
        Castle_Town_Interiors,
        Fishing_Pond_Interiors,
        Hidden_Village_Interiors,
        Castle_Town_Shops,
        Star_Game,
        Kakariko_Graveyard_Interiors,
        Light_Arrows_Cutscene,
        Hyrule_Castle_Cutscenes,
    };

    /// <summary>
    /// summary text.
    /// </summary>
    public class RoomFunctions
    {
        public static List<string> WarpableStages =
            new()
            {
                "South Faron Woods",
                "South Faron Woods Behind Gate",
                "South Faron Woods Coros Ledge",
                "South Faron Woods Owl Statue Area",
                "South Faron Woods Above Owl Statue",
                "Mist Area Near Faron Woods Cave",
                "Mist Area Under Owl Statue Chest",
                "Mist Area Outside Faron Mist Cave",
                "Mist Area Near North Faron Woods",
                "North Faron Woods",
                "Lost Woods",
                "Lost Woods Lower Battle Arena",
                "Lost Woods Upper Battle Arena",
                "Sacred Grove Before Block",
                "Sacred Grove Upper",
                "Sacred Grove Lower",
                "Faron Field",
                "Kak Gorge",
                "Kak Gorge Behind Gate",
                "Death Mountain Near Kak",
                "Death Mountain Trail",
                "Death Mountain Volcano",
                "Death Mountain Outside Sumo Hall",
                "Death Mountain Elevator Lower",
                "Eldin Field",
                "Eldin Field Near CT",
                "Eldin Field From Lava Cave Lower",
                "Eldin Field Grotto Platform",
                "Eldin Field Outside HV",
                "Lanayru Field",
                "Lanayru Field Behind Boulder",
                "Hyrule Field Near Spinner Rails",
                "UZR River",
                "Fishing Hole",
                "ZD Waterfall Area",
                "ZD West Ledge",
                "ZD Throne Room",
                "Snowpeak Climb Lower",
                "Snowpeak Climb Upper",
                "Snowpeak Summit Upper",
                "Snowpeak Summit Lower",
                "BCT",
                "BCT Grotto Ledge",
                "CT West",
                "CT Center",
                "CT East",
                "CT Doctors Office Balcony",
                "OCT East",
                "CT South",
                "OCT South",
                "Lake Hylia Bridge",
                "Lake Hylia",
                "Gerudo Desert",
                "Desert Basin",
                "Desert Outside Bulblin Camp",
                "Bulblin Camp",
                "Mirror Chamber Lower",
                "Mirror Chamber Upper",
                "Mirror Chamber Portal",
                "Ordon Village",
                "Outside Links House",
                "Ordon Spring",
                "Ordon Bridge",
            };

        public static List<string> timeFlowStages =
            new()
            {
                "South Faron Woods",
                "South Faron Woods Behind Gate",
                "South Faron Woods Coros Ledge",
                "South Faron Woods Owl Statue Area",
                "South Faron Woods Above Owl Statue",
                "North Faron Woods",
                "Sacred Grove Before Block",
                "Sacred Grove Upper",
                "Sacred Grove Lower",
                "Faron Field",
                "Kak Gorge",
                //"Kak Gorge Cave Entrance",
                "Kak Gorge Behind Gate",
                "Death Mountain Near Kak",
                "Death Mountain Trail",
                "Death Mountain Volcano",
                "Death Mountain Outside Sumo Hall",
                "Death Mountain Elevator Lower",
                "Eldin Field",
                "Eldin Field Near CT",
                "Eldin Field Lava Cave Ledge",
                "Eldin Field From Lava Cave Lower",
                "Eldin Field Grotto Platform",
                "Eldin Field Outside HV",
                "Lanayru Field",
                //"Lanayru Field Cave Entrance",
                "Lanayru Field Behind Boulder",
                "Hyrule Field Near Spinner Rails",
                "UZR River",
                "Fishing Hole",
                "ZD Waterfall Area",
                "ZD West Ledge",
                "ZD Throne Room",
                "Snowpeak Climb Lower",
                "Snowpeak Climb Upper",
                "Snowpeak Summit Lower",
                "BCT",
                "BCT Grotto Ledge",
                "CT West",
                "CT Center",
                "CT East",
                "CT Doctors Office Balcony",
                "OCT East",
                "CT South",
                "OCT South",
                "Lake Hylia Bridge",
                "Lake Hylia Bridge Grotto Ledge",
                "Lake Hylia",
                "Lake Hylia Cave Entrance",
                "Lake Hylia LBT Entrance",
                "Gerudo Desert",
                "Desert Cave of Ordeals Plateau",
                "Desert Basin",
                "Desert North East Ledge",
                "Desert Outside Bulblin Camp",
                "Bulblin Camp",
                "Mirror Chamber Lower",
                "Mirror Chamber Upper",
                "Mirror Chamber Portal",
            };

        public static List<string> OrdonaMapRooms =
            new()
            {
                "Ordon Village",
                "Ordon Ranch",
                "Ordon Spring",
                "Outside Links House",
                "Ordon Bridge",
            };

        public static List<string> FaronMapRooms =
            new()
            {
                "South Faron Woods",
                "South Faron Woods Behind Gate",
                "South Faron Woods Coros Ledge",
                "South Faron Woods Owl Statue Area",
                "South Faron Woods Above Owl Statue",
                "Mist Area Near Faron Woods Cave",
                "Mist Area Inside Mist",
                "Mist Area Under Owl Statue Chest",
                "Mist Area Near Owl Statue Chest",
                "Mist Area Center Stump",
                "Mist Area Outside Faron Mist Cave",
                "Mist Area Near North Faron Woods",
                "Mist Area Faron Mist Cave",
                "North Faron Woods",
                "Lost Woods",
                "Lost Woods Lower Battle Arena",
                "Lost Woods Upper Battle Arena",
                "Sacred Grove Before Block",
                "Sacred Grove Upper",
                "Sacred Grove Lower",
                "Sacred Grove Past",
                "Sacred Grove Past Behind Window",
                "Faron Field",
            };

        public static List<string> EldinMapRooms =
            new()
            {
                "Kak Gorge",
                "Kak Gorge Cave Entrance",
                "Kak Gorge Behind Gate",
                "Lower Kak Village",
                "Upper Kak Village",
                "Kak Top of Watchtower",
                "Kak Village Behind Gate",
                "Kak Graveyard",
                "Death Mountain Near Kak",
                "Death Mountain Trail",
                "Death Mountain Volcano",
                "Death Mountain Hot Spring",
                "Death Mountain Outside Sumo Hall",
                "Death Mountain Elevator Lower",
                "Eldin Field",
                "Eldin Field Lava Cave Ledge",
                "Eldin Field From Lava Cave Lower",
                // Note that the rooms listed below do not unlock Eldin Province
                // on their own. When approaching from Lanayru Province, you
                // must be able to enter a room listed above in order to unlock
                // Eldin warping (by either destroying the North Eldin rocks,
                // approaching from the north when the rocks are already
                // destroyed, or entering from CT when the bridge is already
                // repaired).

                // "Eldin Field Near CT",
                // "North Eldin Field",
                // "Eldin Field Grotto Platform",
                // "Outside Hidden Village",
                // "Hidden Village",
            };

        public static List<string> LanayruMapRooms =
            new()
            {
                "Lanayru Field",
                "Lanayru Field Cave Entrance",
                "Lanayru Field Behind Boulder",
                "Hyrule Field Near Spinner Rails",
                "UZR River",
                "Fishing Hole",
                "ZD Waterfall Area",
                "ZD West Ledge",
                "ZD Throne Room",
                "ZD Top of Waterfall",
                "BCT",
                "BCT Grotto Ledge",
                "CT West",
                "CT Center",
                "CT North",
                "CT North Behind First Door",
                "CT North Inside Barrier",
                "CT East",
                "CT Doctors Office Balcony",
                "OCT East",
                "CT South",
                "South CT Doors",
                "OCT South",
                "Lake Hylia Bridge",
                "Lake Hylia Bridge Grotto Ledge",
                "Lake Hylia",
                "Lake Hylia Flight By Fowl",
                "Lake Hylia Cave Entrance",
                "Lake Hylia LBT Entrance",
                "Lake Hylia Lanayru Spring",
            };

        public static List<string> SnowpeakMapRooms =
            new()
            {
                "Snowpeak Climb Lower",
                "Snowpeak Climb Upper",
                "Snowpeak Summit Upper",
                "Snowpeak Summit Lower",
            };

        public static List<string> GerudoMapRooms =
            new()
            {
                "Gerudo Desert",
                "Desert CoO Plateau",
                "Desert Basin",
                "Desert North East Ledge",
                "Desert Outside Bulblin Camp",
                "Bulblin Camp",
                "Outside AG",
                "Mirror Chamber Lower",
                "Mirror Chamber Upper",
                "Mirror Chamber Portal",
            };

        public static List<string> DungeonNames =
            new() { "FT", "GM", "LBT", "AG", "SPR", "ToT", "CitS", "PoT" };
        public static List<string> AllDungeonNames =
            new()
            {
                "Forest Temple",
                "Goron Mines",
                "Lakebed Temple",
                "Arbiters Grounds",
                "Snowpeak Ruins",
                "Temple of Time",
                "City in The Sky",
                "Palace of Twilight",
                "Hyrule Castle"
            };

        /// <summary>
        /// A dictionary of all of the rooms that will be used to generate a playthrough graph.
        /// </summary>
        public Dictionary<string, Room> RoomDict = new();

        /// <summary>
        /// summary text.
        /// </summary>
        /// <param name="itemToPlace">The item being checked.</param>
        /// <param name="currentCheck">The check being verified.</param>
        /// <param name="currentRoom">The room where the check is located.</param>
        /// <returns>A value that determines if the specified item and check meet the regional requirements set by the generation.</returns>
        public static bool IsRegionCheck(Item itemToPlace, Check currentCheck, Room currentRoom)
        {
            SharedSettings parseSetting = Randomizer.SSettings;
            string itemName = itemToPlace.ToString();
            itemName = itemName.Replace("_", " ");
            var dungeonConfigs = new[]
            {
                new
                {
                    Region = "Forest Temple",
                    SmallKeySetting = parseSetting.ftSmallKeySettings,
                    BigKeySetting = parseSetting.ftBigKeySettings,
                    MapCompassSetting = parseSetting.ftMapAndCompassSettings
                },
                new
                {
                    Region = "Goron Mines",
                    SmallKeySetting = parseSetting.gmSmallKeySettings,
                    BigKeySetting = parseSetting.gmBigKeySettings,
                    MapCompassSetting = parseSetting.gmMapAndCompassSettings
                },
                new
                {
                    Region = "Lakebed Temple",
                    SmallKeySetting = parseSetting.lbtSmallKeySettings,
                    BigKeySetting = parseSetting.lbtBigKeySettings,
                    MapCompassSetting = parseSetting.lbtMapAndCompassSettings
                },
                new
                {
                    Region = "Arbiters Grounds",
                    SmallKeySetting = parseSetting.agSmallKeySettings,
                    BigKeySetting = parseSetting.agBigKeySettings,
                    MapCompassSetting = parseSetting.agMapAndCompassSettings
                },
                new
                {
                    Region = "Snowpeak Ruins",
                    SmallKeySetting = parseSetting.sprSmallKeySettings,
                    BigKeySetting = parseSetting.sprBigKeySettings,
                    MapCompassSetting = parseSetting.sprMapAndCompassSettings
                },
                new
                {
                    Region = "Temple of Time",
                    SmallKeySetting = parseSetting.totSmallKeySettings,
                    BigKeySetting = parseSetting.totBigKeySettings,
                    MapCompassSetting = parseSetting.totMapAndCompassSettings
                },
                new
                {
                    Region = "City in The Sky",
                    SmallKeySetting = parseSetting.citsSmallKeySettings,
                    BigKeySetting = parseSetting.citsBigKeySettings,
                    MapCompassSetting = parseSetting.citsMapAndCompassSettings
                },
                new
                {
                    Region = "Palace of Twilight",
                    SmallKeySetting = parseSetting.potSmallKeySettings,
                    BigKeySetting = parseSetting.potBigKeySettings,
                    MapCompassSetting = parseSetting.potMapAndCompassSettings
                },
                new
                {
                    Region = "Hyrule Castle",
                    SmallKeySetting = parseSetting.hcSmallKeySettings,
                    BigKeySetting = parseSetting.hcBigKeySettings,
                    MapCompassSetting = parseSetting.hcMapAndCompassSettings
                }
            };

            bool isDungeonItem = false;
            bool ownDungeon = false;
            bool anyDungeon = false;
            bool overworld = false;

            if (Randomizer.Items.RegionSmallKeys.Contains(itemToPlace))
            {
                isDungeonItem = true;

                if (
                    Randomizer.SSettings.noSmallKeysOnBosses
                    && ItemFunctions.IsSmallKeyOnBossCheck(itemToPlace, currentCheck)
                )
                {
                    return false;
                }

                foreach (var config in dungeonConfigs)
                {
                    if (!itemName.Contains(config.Region))
                    {
                        continue;
                    }

                    ownDungeon = config.SmallKeySetting == SmallKeySettings.Own_Dungeon;

                    anyDungeon = config.SmallKeySetting == SmallKeySettings.Any_Dungeon;

                    overworld = config.SmallKeySetting == SmallKeySettings.Overworld;

                    break;
                }
            }
            else if (Randomizer.Items.DungeonBigKeys.Contains(itemToPlace))
            {
                isDungeonItem = true;

                foreach (var config in dungeonConfigs)
                {
                    if (!itemName.Contains(config.Region))
                    {
                        continue;
                    }

                    ownDungeon = config.BigKeySetting == BigKeySettings.Own_Dungeon;

                    anyDungeon = config.BigKeySetting == BigKeySettings.Any_Dungeon;

                    overworld = config.BigKeySetting == BigKeySettings.Overworld;

                    break;
                }
            }
            else if (Randomizer.Items.DungeonMapsAndCompasses.Contains(itemToPlace))
            {
                isDungeonItem = true;

                foreach (var config in dungeonConfigs)
                {
                    if (!itemName.Contains(config.Region))
                    {
                        continue;
                    }

                    ownDungeon = config.MapCompassSetting == MapAndCompassSettings.Own_Dungeon;

                    anyDungeon = config.MapCompassSetting == MapAndCompassSettings.Any_Dungeon;

                    overworld = config.MapCompassSetting == MapAndCompassSettings.Overworld;

                    break;
                }
            }

            if (isDungeonItem)
            {
                bool sameDungeon = dungeonConfigs.Any(
                    config =>
                        itemName.Contains(config.Region)
                        && currentCheck.checkCategory.Contains(config.Region)
                );

                bool inDungeon = currentCheck.checkCategory.Contains("Dungeon");

                bool inOverworld = currentCheck.checkCategory.Contains("Overworld");

                if (
                    (ownDungeon && sameDungeon)
                    || (anyDungeon && inDungeon)
                    || (overworld && inOverworld)
                )
                {
                    return checkBarrenRegionLocation(currentRoom, currentCheck, itemName);
                }
            }

            return false;
        }

        private static bool checkBarrenRegionLocation(
            Room currentRoom,
            Check currentCheck,
            string itemName
        )
        {
            SharedSettings parseSetting = Randomizer.SSettings;
            if (parseSetting.barrenDungeons)
            {
                if (
                    !itemName.Contains(currentRoom.Region)
                    && currentCheck.checkStatus.Contains("Excluded")
                )
                {
                    return false;
                }
                //Console.WriteLine("Can place " + itemName + " in " + currentCheck.checkName);
            }
            return true;
        }
    }
}
