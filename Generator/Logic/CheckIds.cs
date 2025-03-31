namespace TPRandomizer
{
    using System;
    using System.Collections.Generic;

    public class CheckIdClass
    {
        // Do not edit idChars ever. The list index must map to the same id forever.
        private static readonly string idChars = "012345abcdefghijklmnopqrstuvwxyz";
        private static Dictionary<string, string> nameToId;
        private static Dictionary<int, string> idNumToName;

        static CheckIdClass()
        {
            // The names in this list match exactly with the json files in the
            // "Checks" directory. If support for a new check is ever added, add
            // the check name to the bottom of the list. This order which items
            // were placed in the list was arbitrary, but now that they are
            // there they cannot change.

            // THE ORDER OF THIS LIST MUST NEVER CHANGE. The generated item
            // placements on the server use the generated ids to determine which
            // item goes at each check, and adjusting the order would break
            // backwards compatibility with the data on the server.
            List<string> checkNames = new List<string>
            {
                "Arbiters Grounds Big Key Chest",
                "Arbiters Grounds Death Sword Chest",
                "Arbiters Grounds Dungeon Reward",
                "Arbiters Grounds East Lower Turnable Redead Chest",
                "Arbiters Grounds East Turning Room Poe",
                "Arbiters Grounds East Upper Turnable Chest",
                "Arbiters Grounds East Upper Turnable Redead Chest",
                "Arbiters Grounds Entrance Chest",
                "Arbiters Grounds Ghoul Rat Room Chest",
                "Arbiters Grounds Hidden Wall Poe",
                "Arbiters Grounds North Turning Room Chest",
                "Arbiters Grounds Spinner Room First Small Chest",
                "Arbiters Grounds Spinner Room Lower Central Small Chest",
                "Arbiters Grounds Spinner Room Lower North Chest",
                "Arbiters Grounds Spinner Room Second Small Chest",
                "Arbiters Grounds Spinner Room Stalfos Alcove Chest",
                "Arbiters Grounds Stallord Heart Container",
                "Arbiters Grounds Torch Room East Chest",
                "Arbiters Grounds Torch Room Poe",
                "Arbiters Grounds Torch Room West Chest",
                "Arbiters Grounds West Chandelier Chest",
                "Arbiters Grounds West Poe",
                "Arbiters Grounds West Small Chest Behind Block",
                "Arbiters Grounds West Stalfos Northeast Chest",
                "Arbiters Grounds West Stalfos West Chest",
                "City in The Sky Aeralfos Chest",
                "City in The Sky Argorok Heart Container",
                "City in The Sky Baba Tower Alcove Chest",
                "City in The Sky Baba Tower Narrow Ledge Chest",
                "City in The Sky Baba Tower Top Small Chest",
                "City in The Sky Big Key Chest",
                "City in The Sky Central Outside Ledge Chest",
                "City in The Sky Central Outside Poe Island Chest",
                "City in The Sky Chest Behind North Fan",
                "City in The Sky Chest Below Big Key Chest",
                "City in The Sky Dungeon Reward",
                "City in The Sky East First Wing Chest After Fans",
                "City in The Sky East Tile Worm Small Chest",
                "City in The Sky East Wing After Dinalfos Alcove Chest",
                "City in The Sky East Wing After Dinalfos Ledge Chest",
                "City in The Sky East Wing Lower Level Chest",
                "City in The Sky Garden Island Poe",
                "City in The Sky Poe Above Central Fan",
                "City in The Sky Underwater East Chest",
                "City in The Sky Underwater West Chest",
                "City in The Sky West Garden Corner Chest",
                "City in The Sky West Garden Ledge Chest",
                "City in The Sky West Garden Lone Island Chest",
                "City in The Sky West Garden Lower Chest",
                "City in The Sky West Wing Baba Balcony Chest",
                "City in The Sky West Wing First Chest",
                "City in The Sky West Wing Narrow Ledge Chest",
                "City in The Sky West Wing Tile Worm Chest",
                "Forest Temple Big Baba Key",
                "Forest Temple Big Key Chest",
                "Forest Temple Central Chest Behind Stairs",
                "Forest Temple Central Chest Hanging From Web",
                "Forest Temple Central North Chest",
                "Forest Temple Diababa Heart Container",
                "Forest Temple Dungeon Reward",
                "Forest Temple East Tile Worm Chest",
                "Forest Temple East Water Cave Chest",
                "Forest Temple Entrance Vines Chest",
                "Forest Temple Gale Boomerang",
                "Forest Temple North Deku Like Chest",
                "Forest Temple Second Monkey Under Bridge Chest",
                "Forest Temple Totem Pole Chest",
                "Forest Temple West Deku Like Chest",
                "Forest Temple West Tile Worm Chest Behind Stairs",
                "Forest Temple West Tile Worm Room Vines Chest",
                "Forest Temple Windless Bridge Chest",
                "Goron Mines After Crystal Switch Room Magnet Wall Chest",
                "Goron Mines Beamos Room Chest",
                "Goron Mines Chest Before Dangoro",
                "Goron Mines Crystal Switch Room Small Chest",
                "Goron Mines Crystal Switch Room Underwater Chest",
                "Goron Mines Dangoro Chest",
                "Goron Mines Dungeon Reward",
                "Goron Mines Entrance Chest",
                "Goron Mines Fyrus Heart Container",
                "Goron Mines Gor Amato Chest",
                "Goron Mines Gor Amato Key Shard",
                "Goron Mines Gor Amato Small Chest",
                "Goron Mines Gor Ebizo Chest",
                "Goron Mines Gor Ebizo Key Shard",
                "Goron Mines Gor Liggs Chest",
                "Goron Mines Gor Liggs Key Shard",
                "Goron Mines Magnet Maze Chest",
                "Goron Mines Main Magnet Room Bottom Chest",
                "Goron Mines Main Magnet Room Top Chest",
                "Goron Mines Outside Beamos Chest",
                "Goron Mines Outside Clawshot Chest",
                "Goron Mines Outside Underwater Chest",
                "Hyrule Castle Big Key Chest",
                "Hyrule Castle East Wing Balcony Chest",
                "Hyrule Castle East Wing Boomerang Puzzle Chest",
                "Hyrule Castle Graveyard Grave Switch Room Back Left Chest",
                "Hyrule Castle Graveyard Grave Switch Room Front Left Chest",
                "Hyrule Castle Graveyard Grave Switch Room Right Chest",
                "Hyrule Castle Graveyard Owl Statue Chest",
                "Hyrule Castle King Bulblin Key",
                "Hyrule Castle Lantern Staircase Chest",
                "Hyrule Castle Main Hall Northeast Chest",
                "Hyrule Castle Main Hall Northwest Chest",
                "Hyrule Castle Main Hall Southwest Chest",
                "Hyrule Castle Southeast Balcony Tower Chest",
                "Hyrule Castle Treasure Room Eighth Small Chest",
                "Hyrule Castle Treasure Room Fifth Chest",
                "Hyrule Castle Treasure Room Fifth Small Chest",
                "Hyrule Castle Treasure Room First Chest",
                "Hyrule Castle Treasure Room First Small Chest",
                "Hyrule Castle Treasure Room Fourth Chest",
                "Hyrule Castle Treasure Room Fourth Small Chest",
                "Hyrule Castle Treasure Room Second Chest",
                "Hyrule Castle Treasure Room Second Small Chest",
                "Hyrule Castle Treasure Room Seventh Small Chest",
                "Hyrule Castle Treasure Room Sixth Small Chest",
                "Hyrule Castle Treasure Room Third Chest",
                "Hyrule Castle Treasure Room Third Small Chest",
                "Hyrule Castle West Courtyard Central Small Chest",
                "Hyrule Castle West Courtyard North Small Chest",
                "Lakebed Temple Before Deku Toad Alcove Chest",
                "Lakebed Temple Before Deku Toad Underwater Left Chest",
                "Lakebed Temple Before Deku Toad Underwater Right Chest",
                "Lakebed Temple Big Key Chest",
                "Lakebed Temple Central Room Chest",
                "Lakebed Temple Central Room Small Chest",
                "Lakebed Temple Central Room Spire Chest",
                "Lakebed Temple Chandelier Chest",
                "Lakebed Temple Deku Toad Chest",
                "Lakebed Temple Dungeon Reward",
                "Lakebed Temple East Lower Waterwheel Bridge Chest",
                "Lakebed Temple East Lower Waterwheel Stalactite Chest",
                "Lakebed Temple East Second Floor Southeast Chest",
                "Lakebed Temple East Second Floor Southwest Chest",
                "Lakebed Temple East Water Supply Clawshot Chest",
                "Lakebed Temple East Water Supply Small Chest",
                "Lakebed Temple Lobby Left Chest",
                "Lakebed Temple Lobby Rear Chest",
                "Lakebed Temple Morpheel Heart Container",
                "Lakebed Temple Stalactite Room Chest",
                "Lakebed Temple Underwater Maze Small Chest",
                "Lakebed Temple West Lower Small Chest",
                "Lakebed Temple West Second Floor Central Small Chest",
                "Lakebed Temple West Second Floor Northeast Chest",
                "Lakebed Temple West Second Floor Southeast Chest",
                "Lakebed Temple West Second Floor Southwest Underwater Chest",
                "Lakebed Temple West Water Supply Chest",
                "Lakebed Temple West Water Supply Small Chest",
                "Palace of Twilight Big Key Chest",
                "Palace of Twilight Central First Room Chest",
                "Palace of Twilight Central Outdoor Chest",
                "Palace of Twilight Central Tower Chest",
                "Palace of Twilight Collect Both Sols",
                "Palace of Twilight East Wing First Room East Alcove",
                "Palace of Twilight East Wing First Room North Small Chest",
                "Palace of Twilight East Wing First Room West Alcove",
                "Palace of Twilight East Wing First Room Zant Head Chest",
                "Palace of Twilight East Wing Second Room Northeast Chest",
                "Palace of Twilight East Wing Second Room Northwest Chest",
                "Palace of Twilight East Wing Second Room Southeast Chest",
                "Palace of Twilight East Wing Second Room Southwest Chest",
                "Palace of Twilight West Wing Chest Behind Wall of Darkness",
                "Palace of Twilight West Wing First Room Central Chest",
                "Palace of Twilight West Wing Second Room Central Chest",
                "Palace of Twilight West Wing Second Room Lower South Chest",
                "Palace of Twilight West Wing Second Room Southeast Chest",
                "Palace of Twilight Zant Heart Container",
                "Snowpeak Ruins Ball and Chain",
                "Snowpeak Ruins Blizzeta Heart Container",
                "Snowpeak Ruins Broken Floor Chest",
                "Snowpeak Ruins Chapel Chest",
                "Snowpeak Ruins Chest After Darkhammer",
                "Snowpeak Ruins Courtyard Central Chest",
                "Snowpeak Ruins Dungeon Reward",
                "Snowpeak Ruins East Courtyard Buried Chest",
                "Snowpeak Ruins East Courtyard Chest",
                "Snowpeak Ruins Ice Room Poe",
                "Snowpeak Ruins Lobby Armor Poe",
                "Snowpeak Ruins Lobby Chandelier Chest",
                "Snowpeak Ruins Lobby East Armor Chest",
                "Snowpeak Ruins Lobby Poe",
                "Snowpeak Ruins Lobby West Armor Chest",
                "Snowpeak Ruins Mansion Map",
                "Snowpeak Ruins Northeast Chandelier Chest",
                "Snowpeak Ruins Ordon Pumpkin Chest",
                "Snowpeak Ruins West Cannon Room Central Chest",
                "Snowpeak Ruins West Cannon Room Corner Chest",
                "Snowpeak Ruins West Courtyard Buried Chest",
                "Snowpeak Ruins Wooden Beam Central Chest",
                "Snowpeak Ruins Wooden Beam Chandelier Chest",
                "Snowpeak Ruins Wooden Beam Northwest Chest",
                "Temple of Time Armogohma Heart Container",
                "Temple of Time Armos Antechamber East Chest",
                "Temple of Time Armos Antechamber North Chest",
                "Temple of Time Armos Antechamber Statue Chest",
                "Temple of Time Big Key Chest",
                "Temple of Time Chest Before Darknut",
                "Temple of Time Darknut Chest",
                "Temple of Time Dungeon Reward",
                "Temple of Time First Staircase Armos Chest",
                "Temple of Time First Staircase Gohma Gate Chest",
                "Temple of Time First Staircase Window Chest",
                "Temple of Time Floor Switch Puzzle Room Upper Chest",
                "Temple of Time Gilloutine Chest",
                "Temple of Time Lobby Lantern Chest",
                "Temple of Time Moving Wall Beamos Room Chest",
                "Temple of Time Moving Wall Dinalfos Room Chest",
                "Temple of Time Poe Above Scales",
                "Temple of Time Poe Behind Gate",
                "Temple of Time Scales Gohma Chest",
                "Temple of Time Scales Upper Chest",
                "Barnes Bomb Bag",
                "Bridge of Eldin Female Phasmid",
                "Bridge of Eldin Male Phasmid",
                "Bridge of Eldin Owl Statue Chest",
                "Bridge of Eldin Owl Statue Sky Character",
                "Cats Hide and Seek Minigame",
                "Death Mountain Alcove Chest",
                "Death Mountain Trail Poe",
                "Eldin Field Bomb Rock Chest",
                "Eldin Field Bomskit Grotto Lantern Chest",
                "Eldin Field Bomskit Grotto Left Chest",
                "Eldin Field Female Grasshopper",
                "Eldin Field Male Grasshopper",
                "Eldin Field Stalfos Grotto Left Small Chest",
                "Eldin Field Stalfos Grotto Right Small Chest",
                "Eldin Field Stalfos Grotto Stalfos Chest",
                "Eldin Field Water Bomb Fish Grotto Chest",
                "Eldin Lantern Cave First Chest",
                "Eldin Lantern Cave Lantern Chest",
                "Eldin Lantern Cave Poe",
                "Eldin Lantern Cave Second Chest",
                "Eldin Spring Underwater Chest",
                "Eldin Stockcave Lantern Chest",
                "Eldin Stockcave Lowest Chest",
                "Eldin Stockcave Upper Chest",
                "Gift From Ralis",
                "Goron Springwater Rush",
                "Hidden Village Poe",
                "Kakariko Gorge Double Clawshot Chest",
                "Kakariko Gorge Female Pill Bug",
                "Kakariko Gorge Male Pill Bug",
                "Kakariko Gorge Owl Statue Chest",
                "Kakariko Gorge Owl Statue Sky Character",
                "Kakariko Gorge Poe",
                "Kakariko Gorge Spire Heart Piece",
                "Kakariko Graveyard Golden Wolf",
                "Kakariko Graveyard Grave Poe",
                "Kakariko Graveyard Lantern Chest",
                "Kakariko Graveyard Male Ant",
                "Kakariko Graveyard Open Poe",
                "Kakariko Inn Chest",
                "Kakariko Village Bomb Rock Spire Heart Piece",
                "Kakariko Village Bomb Shop Poe",
                "Kakariko Village Female Ant",
                "Kakariko Village Malo Mart Hawkeye",
                "Kakariko Village Malo Mart Hylian Shield",
                "Kakariko Village Malo Mart Red Potion",
                "Kakariko Village Malo Mart Wooden Shield",
                "Kakariko Village Watchtower Poe",
                "Kakariko Watchtower Alcove Chest",
                "Kakariko Watchtower Chest",
                "Rutelas Blessing",
                "Skybook From Impaz",
                "Talo Sharpshooting",
                "Coro Bottle",
                "Faron Field Bridge Chest",
                "Faron Field Corner Grotto Left Chest",
                "Faron Field Corner Grotto Rear Chest",
                "Faron Field Corner Grotto Right Chest",
                "Faron Field Female Beetle",
                "Faron Field Male Beetle",
                "Faron Field Poe",
                "Faron Field Tree Heart Piece",
                "Faron Mist Cave Lantern Chest",
                "Faron Mist Cave Open Chest",
                "Faron Mist North Chest",
                "Faron Mist Poe",
                "Faron Mist South Chest",
                "Faron Mist Stump Chest",
                "Faron Woods Golden Wolf",
                "Faron Woods Owl Statue Chest",
                "Faron Woods Owl Statue Sky Character",
                "Lost Woods Boulder Poe",
                "Lost Woods Lantern Chest",
                "Lost Woods Waterfall Poe",
                "North Faron Woods Deku Baba Chest",
                "Sacred Grove Baba Serpent Grotto Chest",
                "Sacred Grove Female Snail",
                "Sacred Grove Male Snail",
                "Sacred Grove Master Sword Poe",
                "Sacred Grove Past Owl Statue Chest",
                "Sacred Grove Pedestal Master Sword",
                "Sacred Grove Pedestal Shadow Crystal",
                "Sacred Grove Spinner Chest",
                "Sacred Grove Temple of Time Owl Statue Poe",
                "South Faron Cave Chest",
                "Bulblin Camp First Chest Under Tower At Entrance",
                "Bulblin Camp Poe",
                "Bulblin Camp Roasted Boar",
                "Bulblin Camp Small Chest in Back of Camp",
                "Bulblin Guard Key",
                "Cave of Ordeals Floor 17 Poe",
                "Cave of Ordeals Floor 33 Poe",
                "Cave of Ordeals Floor 44 Poe",
                "Cave of Ordeals Great Fairy Reward",
                "Gerudo Desert Campfire East Chest",
                "Gerudo Desert Campfire North Chest",
                "Gerudo Desert Campfire West Chest",
                "Gerudo Desert East Canyon Chest",
                "Gerudo Desert East Poe",
                "Gerudo Desert Female Dayfly",
                "Gerudo Desert Golden Wolf",
                "Gerudo Desert Lone Small Chest",
                "Gerudo Desert Male Dayfly",
                "Gerudo Desert North Peahat Poe",
                "Gerudo Desert North Small Chest Before Bulblin Camp",
                "Gerudo Desert Northeast Chest Behind Gates",
                "Gerudo Desert Northwest Chest Behind Gates",
                "Gerudo Desert Owl Statue Chest",
                "Gerudo Desert Owl Statue Sky Character",
                "Gerudo Desert Peahat Ledge Chest",
                "Gerudo Desert Poe Above Cave of Ordeals",
                "Gerudo Desert Rock Grotto First Poe",
                "Gerudo Desert Rock Grotto Lantern Chest",
                "Gerudo Desert Rock Grotto Second Poe",
                "Gerudo Desert Skulltula Grotto Chest",
                "Gerudo Desert South Chest Behind Wooden Gates",
                "Gerudo Desert West Canyon Chest",
                "Outside Arbiters Grounds Lantern Chest",
                "Outside Arbiters Grounds Poe",
                "Outside Bulblin Camp Poe",
                "Agitha Female Ant Reward",
                "Agitha Female Beetle Reward",
                "Agitha Female Butterfly Reward",
                "Agitha Female Dayfly Reward",
                "Agitha Female Dragonfly Reward",
                "Agitha Female Grasshopper Reward",
                "Agitha Female Ladybug Reward",
                "Agitha Female Mantis Reward",
                "Agitha Female Phasmid Reward",
                "Agitha Female Pill Bug Reward",
                "Agitha Female Snail Reward",
                "Agitha Female Stag Beetle Reward",
                "Agitha Male Ant Reward",
                "Agitha Male Beetle Reward",
                "Agitha Male Butterfly Reward",
                "Agitha Male Dayfly Reward",
                "Agitha Male Dragonfly Reward",
                "Agitha Male Grasshopper Reward",
                "Agitha Male Ladybug Reward",
                "Agitha Male Mantis Reward",
                "Agitha Male Phasmid Reward",
                "Agitha Male Pill Bug Reward",
                "Agitha Male Snail Reward",
                "Agitha Male Stag Beetle Reward",
                "Auru Gift To Fyer",
                "Castle Town Malo Mart Magic Armor",
                "Charlo Donation Blessing",
                "Doctors Office Balcony Chest",
                "East Castle Town Bridge Poe",
                "Fishing Hole Bottle",
                "Fishing Hole Heart Piece",
                "Flight By Fowl Fifth Platform Chest",
                "Flight By Fowl Fourth Platform Chest",
                "Flight By Fowl Ledge Poe",
                "Flight By Fowl Second Platform Chest",
                "Flight By Fowl Third Platform Chest",
                "Flight By Fowl Top Platform Reward",
                "Hyrule Field Amphitheater Owl Statue Chest",
                "Hyrule Field Amphitheater Owl Statue Sky Character",
                "Hyrule Field Amphitheater Poe",
                "Isle of Riches Poe",
                "Iza Helping Hand",
                "Iza Raging Rapids Minigame",
                "Jovani 20 Poe Soul Reward",
                "Jovani 60 Poe Soul Reward",
                "Jovani House Poe",
                "Lake Hylia Alcove Poe",
                "Lake Hylia Bridge Bubble Grotto Chest",
                "Lake Hylia Bridge Cliff Chest",
                "Lake Hylia Bridge Cliff Poe",
                "Lake Hylia Bridge Female Mantis",
                "Lake Hylia Bridge Male Mantis",
                "Lake Hylia Bridge Owl Statue Chest",
                "Lake Hylia Bridge Owl Statue Sky Character",
                "Lake Hylia Bridge Vines Chest",
                "Lake Hylia Dock Poe",
                "Lake Hylia Shell Blade Grotto Chest",
                "Lake Hylia Tower Poe",
                "Lake Hylia Underwater Chest",
                "Lake Hylia Water Toadpoli Grotto Chest",
                "Lake Lantern Cave Eighth Chest",
                "Lake Lantern Cave Eleventh Chest",
                "Lake Lantern Cave End Lantern Chest",
                "Lake Lantern Cave Fifth Chest",
                "Lake Lantern Cave Final Poe",
                "Lake Lantern Cave First Chest",
                "Lake Lantern Cave First Poe",
                "Lake Lantern Cave Fourteenth Chest",
                "Lake Lantern Cave Fourth Chest",
                "Lake Lantern Cave Ninth Chest",
                "Lake Lantern Cave Second Chest",
                "Lake Lantern Cave Second Poe",
                "Lake Lantern Cave Seventh Chest",
                "Lake Lantern Cave Sixth Chest",
                "Lake Lantern Cave Tenth Chest",
                "Lake Lantern Cave Third Chest",
                "Lake Lantern Cave Thirteenth Chest",
                "Lake Lantern Cave Twelfth Chest",
                "Lanayru Field Behind Gate Underwater Chest",
                "Lanayru Field Bridge Poe",
                "Lanayru Field Female Stag Beetle",
                "Lanayru Field Male Stag Beetle",
                "Lanayru Field Poe Grotto Left Poe",
                "Lanayru Field Poe Grotto Right Poe",
                "Lanayru Field Skulltula Grotto Chest",
                "Lanayru Field Spinner Track Chest",
                "Lanayru Ice Block Puzzle Cave Chest",
                "Lanayru Spring Back Room Lantern Chest",
                "Lanayru Spring Back Room Left Chest",
                "Lanayru Spring Back Room Right Chest",
                "Lanayru Spring East Double Clawshot Chest",
                "Lanayru Spring Underwater Left Chest",
                "Lanayru Spring Underwater Right Chest",
                "Lanayru Spring West Double Clawshot Chest",
                "North Castle Town Golden Wolf",
                "Outside Lanayru Spring Left Statue Chest",
                "Outside Lanayru Spring Right Statue Chest",
                "Outside South Castle Town Double Clawshot Chasm Chest",
                "Outside South Castle Town Female Ladybug",
                "Outside South Castle Town Fountain Chest",
                "Outside South Castle Town Golden Wolf",
                "Outside South Castle Town Male Ladybug",
                "Outside South Castle Town Poe",
                "Outside South Castle Town Tektite Grotto Chest",
                "Outside South Castle Town Tightrope Chest",
                "Plumm Fruit Balloon Minigame",
                "STAR Prize 1",
                "STAR Prize 2",
                "Upper Zoras River Female Dragonfly",
                "Upper Zoras River Poe",
                "West Hyrule Field Female Butterfly",
                "West Hyrule Field Golden Wolf",
                "West Hyrule Field Helmasaur Grotto Chest",
                "West Hyrule Field Male Butterfly",
                "Zoras Domain Chest Behind Waterfall",
                "Zoras Domain Chest By Mother and Child Isles",
                "Zoras Domain Extinguish All Torches Chest",
                "Zoras Domain Light All Torches Chest",
                "Zoras Domain Male Dragonfly",
                "Zoras Domain Mother and Child Isle Poe",
                "Zoras Domain Underwater Goron",
                "Zoras Domain Waterfall Poe",
                "Herding Goats Reward",
                "Links Basement Chest",
                "Ordon Cat Rescue",
                "Ordon Ranch Grotto Lantern Chest",
                "Ordon Shield",
                "Ordon Spring Golden Wolf",
                "Ordon Sword",
                "Sera Shop Slingshot",
                "Uli Cradle Delivery",
                "Wooden Sword Chest",
                "Wrestling With Bo",
                "Ashei Sketch",
                "Snowboard Racing Prize",
                "Snowpeak Above Freezard Grotto Poe",
                "Snowpeak Blizzard Poe",
                "Snowpeak Cave Ice Lantern Chest",
                "Snowpeak Cave Ice Poe",
                "Snowpeak Freezard Grotto Chest",
                "Snowpeak Icy Summit Poe",
                "Snowpeak Poe Among Trees",
                // Add new check names right above this line. The name should
                // match exactly with the json filename in the "Checks"
                // directory.
            };

            nameToId = new(checkNames.Count);
            idNumToName = new(checkNames.Count);

            for (int i = 0; i < checkNames.Count; i++)
            {
                nameToId.Add(checkNames[i], NumToId(i));
                idNumToName.Add(i, checkNames[i]);
            }
        }

        private static string NumToId(int num)
        {
            if (num == 0)
            {
                return idChars.Substring(0, 1);
            }

            List<char> characters = new();
            int currentNum = num;
            while (currentNum > 0)
            {
                int charIndex = currentNum % idChars.Length;
                characters.Add(idChars[charIndex]);
                currentNum -= charIndex;
                currentNum /= idChars.Length;
            }

            characters.Reverse();
            return String.Join("", characters);
        }

        private static int IdToNum(string id)
        {
            string bitStr = "";

            for (int i = 0; i < id.Length; i++)
            {
                bitStr += Convert.ToString(idChars.IndexOf(id[i]), 2).PadLeft(5, '0');
            }

            return Convert.ToInt32(bitStr, 2);
        }

        public static string FromString(string checkName)
        {
            if (nameToId.ContainsKey(checkName))
            {
                return nameToId[checkName];
            }
            return null;
        }

        public static string GetCheckName(int idNumber)
        {
            if (idNumToName.ContainsKey(idNumber))
            {
                return idNumToName[idNumber];
            }
            return null;
        }

        public static int GetCheckIdNum(string checkName)
        {
            if (nameToId.ContainsKey(checkName))
            {
                return IdToNum(nameToId[checkName]);
            }
            return -1;
        }

        public static bool IsValidCheckId(int idNumber)
        {
            return GetCheckName(idNumber) != null;
        }

        public static bool IsValidCheckName(string checkName)
        {
            return GetCheckIdNum(checkName) >= 0;
        }

        public static SortedDictionary<string, int> GetNameToIdNumDictionary()
        {
            SortedDictionary<string, int> nameToIdNum = new();

            foreach (KeyValuePair<string, string> item in nameToId)
            {
                nameToIdNum[item.Key] = IdToNum(item.Value);
            }

            return nameToIdNum;
        }
    }
}
