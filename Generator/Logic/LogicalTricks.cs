using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TPRandomizer.SSettings.Enums;

namespace TPRandomizer
{
    public enum Trick
    {
        // TODO: add instructions here and at bottom of this enum.
        [Description("Destroy Webs With Bombs and Ball and Chain")]
        can_smash_webs = 0,
        launch_bombs_with_boomerang,
        freestandings_with_bnc,
        drained_MA_as_irons,
        shadow_beasts_without_mdh,

        // // Ordon Tricks

        // // Faron Tricks
        mist_stump_chest_as_wolf,
        baba_serpent_grotto_with_wolf,

        // // Eldin Tricks
        kak_gorge_skip_claws_with_wolf,
        kak_rock_spire_poh_with_epona_claw,
        kak_rock_spire_poh_with_boomerang,
        watchtower_alcove_chest_as_wolf,
        geysers_shield_skip,
        dm_climb_without_irons,
        bridge_owl_chest_as_wolf,
        lava_cave_itemless_upper_chest,
        lava_cave_wolf_jump_to_bottom,
        hv_without_claw,

        // // Lanayru Tricks
        helmasaur_grotto_with_claw,
        star_1_without_claw,
        star_2_without_2_claws,
        outside_ct_chasm_with_wolf,
        outside_ct_chasm_with_sword,
        outside_ct_chasm_with_irons,
        outside_ct_chasm_itemless,
        outside_ct_fountain_with_bs,
        iza_1_with_boomerang,
        loach_with_frog_lure,
        zd_underwater_rupees_without_za,
        zd_waterfall_ledge_box_and_sword,
        zd_waterfall_ledge_spinner,
        underwater_goron_without_za,
        llc_no_lantern,
        shell_blade_grotto_normal_bombs,
        toadpoli_grotto_with_wolf,

        // // Desert Tricks
        camp_boar_with_irons,
        coo_fairy_access,

        // // Snowpeak Tricks

        // // Forest Temple
        ft_lobby_without_ranged_items,
        ft_lobby_with_wolf,
        ft_west_wing_bombling,
        ft_north_tile_worm_boost,
        ft_north_bomb_boosts,
        ft_ook_with_midna,

        // // Goron Mines
        fyrus_without_sword,
        fyrus_without_irons,

        // // Lakebed Temple
        lbt_chandelier_drop,
        lbt_bk_without_bombs,
        lbt_bk_skip,
        morpheel_without_sword,

        // // Arbiters Grounds
        ag_entrance_chain_without_claw,
        ag_pillar_jump,
        ag_bk_with_wolf,

        // // Snowpeak Ruins
        spr_lobby_chandelier_without_wolf,
        spr_ne_chandelier_with_bs,
        spr_ladder_freezard_cancel,

        // // Temple of Time
        tot_crystal_switches_with_claw,

        // // City in the Sky
        cits_entrance_with_bnc,
        cits_fan_skip,
        cits_itemless_central_room,
        cits_itemless_east_first_room,
        cits_dinalfos_with_claw,
        cits_central_outside_ledge_chest_with_claw,
        cits_compass_chest_with_2_claws,
        cits_north_with_2_claws,

        // // Palace of Twilight

        // // Hyrule Castle
        hc_skip_main_hall_barrier,
        hc_chandeliers_with_1_claw,
        hc_painting_switch_with_bombs,
        hc_painting_switch_with_js,
        hc_painting_switch_with_bs,
        hc_tower_climb_with_1_claw,
        beast_ganon_without_wolf,
    }

    public class LogicTricks
    {
        public class UiDisplayItem
        {
            // Ignores the numerical ID completely when it's null (for dividers)
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public int? Id { get; set; }

            // Converts enum to string ("Trick") and ignores when null
            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(StringEnumConverter))]
            public Trick? Value { get; set; }

            [JsonProperty("displayName")]
            public string DisplayName { get; set; } = string.Empty;

            [JsonProperty("isDivider")]
            public bool IsDivider { get; set; }

            public static UiDisplayItem Trick(Trick value) =>
                new()
                {
                    Id = (int)value,
                    Value = value,
                    DisplayName = DisplayNames[value],
                    IsDivider = false
                };

            public static UiDisplayItem Divider(string label) =>
                new() { DisplayName = label, IsDivider = true };
        }

        private static readonly Dictionary<Trick, string> DisplayNames =
            new()
            {
                // { Plaintext name, logic name }
                // ex: Generator side: Lake Lantern Cave Without Lantern, logic side: llc_no_lantern

                // General Tricks
                { Trick.can_smash_webs, "Destroy Webs With Bombs and Ball and Chain" },
                {
                    Trick.launch_bombs_with_boomerang,
                    "Use Bombs and Boomerang To Destroy Out of Reach Rocks"
                },
                { Trick.freestandings_with_bnc, "Grab Freestanding Items With Ball and Chain" },
                { Trick.drained_MA_as_irons, "Drained Magic Armor As A Substitute for Iron Boots" },
                { Trick.shadow_beasts_without_mdh, "Defeat Shadow Beasts Without MDH" },
                // Ordon Tricks

                // Faron Tricks
                { Trick.mist_stump_chest_as_wolf, "Faron Mist Stump Chest as Wolf" },
                { Trick.baba_serpent_grotto_with_wolf, "Baba Serpent Grotto With Wolf" },
                // Eldin Tricks
                {
                    Trick.kak_gorge_skip_claws_with_wolf,
                    "Kak Gorge Double Clawshot Chest as Wolf"
                },
                {
                    Trick.kak_rock_spire_poh_with_epona_claw,
                    "Kak Village Bomb Rock Spire Heart Piece With Epona and Clawshot"
                },
                {
                    Trick.kak_rock_spire_poh_with_boomerang,
                    "Kak Village Bomb Rock Spire Heart Piece With Boomerang"
                },
                {
                    Trick.watchtower_alcove_chest_as_wolf,
                    "Kak Village Watchtower Alcove Chest as Wolf"
                },
                { Trick.geysers_shield_skip, "Death Mountain Geysers Shield Skip" },
                { Trick.dm_climb_without_irons, "Death Mountain Climb Without Irons" },
                { Trick.bridge_owl_chest_as_wolf, "Bridge of Eldin Owl Statue Chest as Wolf" },
                {
                    Trick.lava_cave_itemless_upper_chest,
                    "Eldin Lava Cave Upper Chest With Nothing"
                },
                { Trick.lava_cave_wolf_jump_to_bottom, "Eldin Lava Cave Wolf Jump to Bottom" },
                { Trick.hv_without_claw, "Hidden Village Checks Without Clawshot" },
                // Lanayru Tricks
                { Trick.helmasaur_grotto_with_claw, "Helmasaur Grotto With Only Clawshot" },
                { Trick.star_1_without_claw, "STAR 1 Without Clawshot" },
                { Trick.star_2_without_2_claws, "STAR 2 Without Double Clawshot" },
                { Trick.outside_ct_chasm_with_wolf, "OCT South Double Claw Chest With Wolf" },
                { Trick.outside_ct_chasm_with_sword, "OCT South Double Claw Chest With Sword" },
                { Trick.outside_ct_chasm_with_irons, "OCT South Double Claw Chest With Irons" },
                { Trick.outside_ct_chasm_itemless, "OCT South Double Claw Chest Itemless" },
                {
                    Trick.outside_ct_fountain_with_bs,
                    "OCT South Fountain Chest With Sword + Back Slice"
                },
                { Trick.iza_1_with_boomerang, "Izas Helping Hand With Boomerang" },
                { Trick.loach_with_frog_lure, "Legendary Hylian Loach With Frog Lure" },
                {
                    Trick.zd_underwater_rupees_without_za,
                    "ZD Underwater Rupees Without Zora Armor"
                },
                { Trick.zd_waterfall_ledge_box_and_sword, "ZD Waterfall Ledge With Box and Sword" },
                { Trick.zd_waterfall_ledge_spinner, "ZD Wafterfall Ledge With Spinner" },
                { Trick.underwater_goron_without_za, "Underwater Goron Without Zora Armor" },
                { Trick.llc_no_lantern, "Lake Lantern Cave Without Lantern" },
                { Trick.shell_blade_grotto_normal_bombs, "Shell Blade Grotto With Normal Bombs" },
                { Trick.toadpoli_grotto_with_wolf, "Water Toadpoli Grotto With Wolf" },
                // Desert Tricks
                { Trick.camp_boar_with_irons, "Bulblin Camp Boar With Irons" },
                { Trick.coo_fairy_access, "CoO Fairies As Access To Springs" },
                // Snowpeak Tricks

                // Forest Temple
                { Trick.ft_lobby_without_ranged_items, "FT Lobby Without Ranged Items" },
                { Trick.ft_lobby_with_wolf, "FT Lobby With Wolf" },
                { Trick.ft_west_wing_bombling, "FT West Wing With Bombling" },
                { Trick.ft_north_tile_worm_boost, "FT North Tile Worm Boost To Chest" },
                { Trick.ft_north_bomb_boosts, "FT North Wing With Bomb Boosts" },
                { Trick.ft_ook_with_midna, "FT Ook access With Midna" },
                // Goron Mines
                { Trick.fyrus_without_sword, "Defeat Fyrus Without Sword" },
                { Trick.fyrus_without_irons, "Defeat Fyrus Without Irons" },
                // Lakebed Temple
                { Trick.lbt_chandelier_drop, "LBT Chandelier Drop" },
                { Trick.lbt_bk_without_bombs, "LBT Big Key Chest Without Bombs" },
                { Trick.lbt_bk_skip, "LBT Big Key Skip" },
                { Trick.morpheel_without_sword, "Defeat Morpheel Without Sword" },
                // Arbiters Grounds
                { Trick.ag_entrance_chain_without_claw, "AG Entrance Chain Without Clawshot" },
                { Trick.ag_pillar_jump, "AG Pillar Jump" },
                { Trick.ag_bk_with_wolf, "AG Big Key With Wolf" },
                // Snowpeak Ruins
                {
                    Trick.spr_lobby_chandelier_without_wolf,
                    "SPR Lobby Chandelier Chest Without Wolf"
                },
                {
                    Trick.spr_ne_chandelier_with_bs,
                    "SPR Northeast Chandelier Chest With Back Slice"
                },
                { Trick.spr_ladder_freezard_cancel, "SPR Ladder Freezard Cancel" },
                // Temple of Time
                { Trick.tot_crystal_switches_with_claw, "ToT Crystal Switches With Clawshot" },
                // City in the Sky
                {
                    Trick.cits_entrance_with_bnc,
                    "CitS Entrance Crystal Switch With Ball and Chain"
                },
                { Trick.cits_fan_skip, "CitS Fan Skip" },
                { Trick.cits_itemless_central_room, "CitS Central Oocca Room With No Items" },
                { Trick.cits_itemless_east_first_room, "CitS East Wing First Room With No Items" },
                { Trick.cits_dinalfos_with_claw, "CitS Dinalfos Room With Clawshot" },
                {
                    Trick.cits_central_outside_ledge_chest_with_claw,
                    "CitS Central Outside Ledge Chest With Clawshot"
                },
                {
                    Trick.cits_compass_chest_with_2_claws,
                    "CitS Compass Chest With Double Clawshots"
                },
                { Trick.cits_north_with_2_claws, "CitS North Wing With Double Clawshots" },
                // Palace of Twilight

                // Hyrule Castle
                { Trick.hc_skip_main_hall_barrier, "HC Skip Main Hall Barrier" },
                { Trick.hc_chandeliers_with_1_claw, "HC Chandeliers With Single Clawshot" },
                { Trick.hc_painting_switch_with_bombs, "HC Painting Switch With Bombs" },
                { Trick.hc_painting_switch_with_js, "HC Painting Switch With Jump Strike" },
                { Trick.hc_painting_switch_with_bs, "HC Painting Switch With Back Slice" },
                { Trick.hc_tower_climb_with_1_claw, "HC Tower Climb With Single Clawshot" },
                { Trick.beast_ganon_without_wolf, "Defeat Dark Beast Ganon Without Wolf" },
            };

        public static List<UiDisplayItem> GetUiDisplayItems()
        {
            return new()
            {
                UiDisplayItem.Divider("General"),
                UiDisplayItem.Trick(Trick.can_smash_webs),
                UiDisplayItem.Trick(Trick.launch_bombs_with_boomerang),
                UiDisplayItem.Trick(Trick.freestandings_with_bnc),
                UiDisplayItem.Trick(Trick.drained_MA_as_irons),
                UiDisplayItem.Trick(Trick.shadow_beasts_without_mdh),
                // Add "Ordon" here once needed.

                UiDisplayItem.Divider("Faron"),
                UiDisplayItem.Trick(Trick.mist_stump_chest_as_wolf),
                UiDisplayItem.Trick(Trick.baba_serpent_grotto_with_wolf),
                UiDisplayItem.Divider("Eldin"),
                UiDisplayItem.Trick(Trick.kak_gorge_skip_claws_with_wolf),
                UiDisplayItem.Trick(Trick.kak_rock_spire_poh_with_epona_claw),
                UiDisplayItem.Trick(Trick.kak_rock_spire_poh_with_boomerang),
                UiDisplayItem.Trick(Trick.watchtower_alcove_chest_as_wolf),
                UiDisplayItem.Trick(Trick.geysers_shield_skip),
                UiDisplayItem.Trick(Trick.dm_climb_without_irons),
                UiDisplayItem.Trick(Trick.bridge_owl_chest_as_wolf),
                UiDisplayItem.Trick(Trick.lava_cave_itemless_upper_chest),
                UiDisplayItem.Trick(Trick.lava_cave_wolf_jump_to_bottom),
                UiDisplayItem.Trick(Trick.hv_without_claw),
                UiDisplayItem.Divider("Lanayru"),
                UiDisplayItem.Trick(Trick.helmasaur_grotto_with_claw),
                UiDisplayItem.Trick(Trick.star_1_without_claw),
                UiDisplayItem.Trick(Trick.star_2_without_2_claws),
                UiDisplayItem.Trick(Trick.outside_ct_chasm_with_wolf),
                UiDisplayItem.Trick(Trick.outside_ct_chasm_with_sword),
                UiDisplayItem.Trick(Trick.outside_ct_chasm_with_irons),
                UiDisplayItem.Trick(Trick.outside_ct_chasm_itemless),
                UiDisplayItem.Trick(Trick.outside_ct_fountain_with_bs),
                UiDisplayItem.Trick(Trick.iza_1_with_boomerang),
                UiDisplayItem.Trick(Trick.loach_with_frog_lure),
                UiDisplayItem.Trick(Trick.zd_underwater_rupees_without_za),
                UiDisplayItem.Trick(Trick.zd_waterfall_ledge_box_and_sword),
                UiDisplayItem.Trick(Trick.zd_waterfall_ledge_spinner),
                UiDisplayItem.Trick(Trick.underwater_goron_without_za),
                UiDisplayItem.Trick(Trick.llc_no_lantern),
                UiDisplayItem.Trick(Trick.shell_blade_grotto_normal_bombs),
                UiDisplayItem.Trick(Trick.toadpoli_grotto_with_wolf),
                UiDisplayItem.Divider("Desert"),
                UiDisplayItem.Trick(Trick.camp_boar_with_irons),
                UiDisplayItem.Trick(Trick.coo_fairy_access),
                // Add "Snowpeak" here once needed.

                UiDisplayItem.Divider("Forest Temple"),
                UiDisplayItem.Trick(Trick.ft_lobby_without_ranged_items),
                UiDisplayItem.Trick(Trick.ft_lobby_with_wolf),
                UiDisplayItem.Trick(Trick.ft_west_wing_bombling),
                UiDisplayItem.Trick(Trick.ft_north_tile_worm_boost),
                UiDisplayItem.Trick(Trick.ft_north_bomb_boosts),
                UiDisplayItem.Trick(Trick.ft_ook_with_midna),
                UiDisplayItem.Divider("Goron Mines"),
                UiDisplayItem.Trick(Trick.fyrus_without_sword),
                UiDisplayItem.Trick(Trick.fyrus_without_irons),
                UiDisplayItem.Divider("Lakebed Temple"),
                UiDisplayItem.Trick(Trick.lbt_chandelier_drop),
                UiDisplayItem.Trick(Trick.lbt_bk_without_bombs),
                UiDisplayItem.Trick(Trick.lbt_bk_skip),
                UiDisplayItem.Trick(Trick.morpheel_without_sword),
                UiDisplayItem.Divider("Arbiter's Grounds"),
                UiDisplayItem.Trick(Trick.ag_entrance_chain_without_claw),
                UiDisplayItem.Trick(Trick.ag_pillar_jump),
                UiDisplayItem.Trick(Trick.ag_bk_with_wolf),
                UiDisplayItem.Divider("Snowpeak Ruins"),
                UiDisplayItem.Trick(Trick.spr_lobby_chandelier_without_wolf),
                UiDisplayItem.Trick(Trick.spr_ne_chandelier_with_bs),
                UiDisplayItem.Trick(Trick.spr_ladder_freezard_cancel),
                UiDisplayItem.Divider("Temple of Time"),
                UiDisplayItem.Trick(Trick.tot_crystal_switches_with_claw),
                UiDisplayItem.Divider("City in the Sky"),
                UiDisplayItem.Trick(Trick.cits_entrance_with_bnc),
                UiDisplayItem.Trick(Trick.cits_fan_skip),
                UiDisplayItem.Trick(Trick.cits_itemless_central_room),
                UiDisplayItem.Trick(Trick.cits_itemless_east_first_room),
                UiDisplayItem.Trick(Trick.cits_dinalfos_with_claw),
                UiDisplayItem.Trick(Trick.cits_central_outside_ledge_chest_with_claw),
                UiDisplayItem.Trick(Trick.cits_compass_chest_with_2_claws),
                UiDisplayItem.Trick(Trick.cits_north_with_2_claws),
                // Add "Palace of Twilight" here once needed.

                UiDisplayItem.Divider("Hyrule Castle"),
                UiDisplayItem.Trick(Trick.hc_skip_main_hall_barrier),
                UiDisplayItem.Trick(Trick.hc_chandeliers_with_1_claw),
                UiDisplayItem.Trick(Trick.hc_painting_switch_with_bombs),
                UiDisplayItem.Trick(Trick.hc_painting_switch_with_js),
                UiDisplayItem.Trick(Trick.hc_painting_switch_with_bs),
                UiDisplayItem.Trick(Trick.hc_tower_climb_with_1_claw),
                UiDisplayItem.Trick(Trick.beast_ganon_without_wolf),
            };
        }

        public static Dictionary<string, Trick> listOfTricks =
            new()
            {
                // { Plaintext name, logic name }
                // ex: Generator side: Lake Lantern Cave Without Lantern, logic side: llc_no_lantern

                // General Tricks
                { "Destroy Webs With Bombs and Ball and Chain", Trick.can_smash_webs },
                {
                    "Use Bombs and Boomerang To Destroy Out of Reach Rocks",
                    Trick.launch_bombs_with_boomerang
                },
                { "Grab Freestanding Items With Ball and Chain", Trick.freestandings_with_bnc },
                { "Drained Magic Armor As A Substitute for Iron Boots", Trick.drained_MA_as_irons },
                { "Defeat Shadow Beasts Without MDH", Trick.shadow_beasts_without_mdh },
                // Ordon Tricks

                // Faron Tricks
                { "Faron Mist Stump Chest as Wolf", Trick.mist_stump_chest_as_wolf },
                { "Baba Serpent Grotto With Wolf", Trick.baba_serpent_grotto_with_wolf },
                // Eldin Tricks
                {
                    "Kak Gorge Double Clawshot Chest as Wolf",
                    Trick.kak_gorge_skip_claws_with_wolf
                },
                {
                    "Kak Village Bomb Rock Spire Heart Piece With Epona and Clawshot",
                    Trick.kak_rock_spire_poh_with_epona_claw
                },
                {
                    "Kak Village Bomb Rock Spire Heart Piece With Boomerang",
                    Trick.kak_rock_spire_poh_with_boomerang
                },
                {
                    "Kak Village Watchtower Alcove Chest as Wolf",
                    Trick.watchtower_alcove_chest_as_wolf
                },
                { "Death Mountain Geysers Shield Skip", Trick.geysers_shield_skip },
                { "Death Mountain Climb Without Irons", Trick.dm_climb_without_irons },
                { "Bridge of Eldin Owl Statue Chest as Wolf", Trick.bridge_owl_chest_as_wolf },
                {
                    "Eldin Lava Cave Upper Chest With Nothing",
                    Trick.lava_cave_itemless_upper_chest
                },
                { "Eldin Lava Cave Wolf Jump to Bottom", Trick.lava_cave_wolf_jump_to_bottom },
                { "Hidden Village Checks Without Clawshot", Trick.hv_without_claw },
                // Lanayru Tricks
                { "Helmasaur Grotto With Only Clawshot", Trick.helmasaur_grotto_with_claw },
                { "STAR 1 Without Clawshot", Trick.star_1_without_claw },
                { "STAR 2 Without Double Clawshot", Trick.star_2_without_2_claws },
                { "OCT South Double Claw Chest With Wolf", Trick.outside_ct_chasm_with_wolf },
                { "OCT South Double Claw Chest With Sword", Trick.outside_ct_chasm_with_sword },
                { "OCT South Double Claw Chest With Irons", Trick.outside_ct_chasm_with_irons },
                { "OCT South Double Claw Chest Itemless", Trick.outside_ct_chasm_itemless },
                {
                    "OCT South Fountain Chest With Sword + Back Slice",
                    Trick.outside_ct_fountain_with_bs
                },
                { "Izas Helping Hand With Boomerang", Trick.iza_1_with_boomerang },
                { "Legendary Hylian Loach With Frog Lure", Trick.loach_with_frog_lure },
                {
                    "ZD Underwater Rupees Without Zora Armor",
                    Trick.zd_underwater_rupees_without_za
                },
                { "ZD Waterfall Ledge With Box and Sword", Trick.zd_waterfall_ledge_box_and_sword },
                { "ZD Wafterfall Ledge With Spinner", Trick.zd_waterfall_ledge_spinner },
                { "Underwater Goron Without Zora Armor", Trick.underwater_goron_without_za },
                { "Lake Lantern Cave Without Lantern", Trick.llc_no_lantern },
                { "Shell Blade Grotto With Normal Bombs", Trick.shell_blade_grotto_normal_bombs },
                { "Water Toadpoli Grotto With Wolf", Trick.toadpoli_grotto_with_wolf },
                // Desert Tricks
                { "Bulblin Camp Boar With Irons", Trick.camp_boar_with_irons },
                { "CoO Fairies As Access To Springs", Trick.coo_fairy_access },
                // Snowpeak Tricks

                // Forest Temple
                { "FT Lobby Without Ranged Items", Trick.ft_lobby_without_ranged_items },
                { "FT Lobby With Wolf", Trick.ft_lobby_with_wolf },
                { "FT West Wing With Bombling", Trick.ft_west_wing_bombling },
                { "FT North Tile Worm Boost To Chest", Trick.ft_north_tile_worm_boost },
                { "FT North Wing With Bomb Boosts", Trick.ft_north_bomb_boosts },
                { "FT Ook access With Midna", Trick.ft_ook_with_midna },
                // Goron Mines
                { "Defeat Fyrus Without Sword", Trick.fyrus_without_sword },
                { "Defeat Fyrus Without Irons", Trick.fyrus_without_irons },
                // Lakebed Temple
                { "LBT Chandelier Drop", Trick.lbt_chandelier_drop },
                { "LBT Big Key Chest Without Bombs", Trick.lbt_bk_without_bombs },
                { "LBT Big Key Skip", Trick.lbt_bk_skip },
                { "Defeat Morpheel Without Sword", Trick.morpheel_without_sword },
                // Arbiters Grounds
                { "AG Entrance Chain Without Clawshot", Trick.ag_entrance_chain_without_claw },
                { "AG Pillar Jump", Trick.ag_pillar_jump },
                { "AG Big Key With Wolf", Trick.ag_bk_with_wolf },
                // Snowpeak Ruins
                {
                    "SPR Lobby Chandelier Chest Without Wolf",
                    Trick.spr_lobby_chandelier_without_wolf
                },
                {
                    "SPR Northeast Chandelier Chest With Back Slice",
                    Trick.spr_ne_chandelier_with_bs
                },
                { "SPR Ladder Freezard Cancel", Trick.spr_ladder_freezard_cancel },
                // Temple of Time
                { "ToT Crystal Switches With Clawshot", Trick.tot_crystal_switches_with_claw },
                // City in the Sky
                {
                    "CitS Entrance Crystal Switch With Ball and Chain",
                    Trick.cits_entrance_with_bnc
                },
                { "CitS Fan Skip", Trick.cits_fan_skip },
                { "CitS Central Oocca Room With No Items", Trick.cits_itemless_central_room },
                { "CitS East Wing First Room With No Items", Trick.cits_itemless_east_first_room },
                { "CitS Dinalfos Room With Clawshot", Trick.cits_dinalfos_with_claw },
                {
                    "CitS Central Outside Ledge Chest With Clawshot",
                    Trick.cits_central_outside_ledge_chest_with_claw
                },
                {
                    "CitS Compass Chest With Double Clawshots",
                    Trick.cits_compass_chest_with_2_claws
                },
                { "CitS North Wing With Double Clawshots", Trick.cits_north_with_2_claws },
                // Palace of Twilight

                // Hyrule Castle
                { "HC Skip Main Hall Barrier", Trick.hc_skip_main_hall_barrier },
                { "HC Chandeliers With Single Clawshot", Trick.hc_chandeliers_with_1_claw },
                { "HC Painting Switch With Bombs", Trick.hc_painting_switch_with_bombs },
                { "HC Painting Switch With Jump Strike", Trick.hc_painting_switch_with_js },
                { "HC Painting Switch With Back Slice", Trick.hc_painting_switch_with_bs },
                { "HC Tower Climb With Single Clawshot", Trick.hc_tower_climb_with_1_claw },
                { "Defeat Dark Beast Ganon Without Wolf", Trick.beast_ganon_without_wolf },
            };

        // public static Dictionary<string, int> generateTrickList()
        // {
        //     Dictionary<string, int> trickDict = new();
        //     var tricks = listOfTricks.Keys.ToList();
        //     for (int i = 0; i < tricks.Count; i++)
        //     {
        //         trickDict.Add(" " + tricks[i], i);
        //     }
        //     return trickDict;
        // }

        public static Dictionary<string, int> generateTrickList()
        {
            Trick[] trickEnums = (Trick[])Enum.GetValues(typeof(Trick));

            Dictionary<string, int> trickDict = new();

            foreach (KeyValuePair<string, Trick> pair in listOfTricks)
            {
                int trickId = (int)pair.Value;
                trickDict.Add(" " + pair.Key, trickId);
            }

            // var tricks = listOfTricks.Keys.ToList();
            // for (int i = 0; i < tricks.Count; i++)
            // {
            // }
            return trickDict;
        }

        public static Trick GetTrickFromNumber(int trickId)
        {
            Trick trick = (Trick)trickId;
            if (!Enum.IsDefined(trick))
                throw new Exception($"trickId '{trickId}' is not a valid value.");
            return trick;
        }

        public static Trick GetTrickFromString(string trickName)
        {
            if (!Enum.TryParse(trickName, out Trick trick))
                throw new Exception($"Failed to parse '{trickName}' to Trick enum.");
            return trick;
        }

        public static bool isTrickEnabled(string trickName)
        {
            // Glitched Logic assumes all tricks are enabled. May update this to a setting called "Advanced Logic" later.
            if (Randomizer.SSettings.logicRules == LogicRules.Glitched)
            {
                return true;
            }
            Trick trick = GetTrickFromString(trickName);
            return Randomizer.SSettings.logicalTricks.Contains(trick);
            // .Values.Contains(trick);
            // return Randomizer.SSettings.logicalTricks.Values.Contains(trick);
        }
    }
}
