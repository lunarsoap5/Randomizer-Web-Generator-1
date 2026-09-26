using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TPRandomizer.SSettings.Enums;
using TPRandomizer.Util;

namespace TPRandomizer
{
    public enum Trick
    {
        // ***WARNING: DO NOT SORT OR ADD TO THE MIDDLE OF THIS LIST.*** New tricks must be added to
        // the BOTTOM of the list.
        [Description("Destroy Webs With Bombs and Ball and Chain")]
        can_smash_webs = 0,

        [Description("Use Bombs and Boomerang To Destroy Out of Reach Rocks")]
        launch_bombs_with_boomerang,

        [Description("Grab Freestanding Items With Ball and Chain")]
        freestandings_with_bnc,

        [Description("Drained Magic Armor As A Substitute for Iron Boots")]
        drained_MA_as_irons,

        [Description("Defeat Shadow Beasts Without MDH")]
        shadow_beasts_without_mdh,

        [Description("Faron Mist Stump Chest as Wolf")]
        mist_stump_chest_as_wolf,

        [Description("Baba Serpent Grotto With Wolf")]
        baba_serpent_grotto_with_wolf,

        [Description("Kak Gorge Double Clawshot Chest as Wolf")]
        kak_gorge_skip_claws_with_wolf,

        [Description("Kak Village Bomb Rock Spire Heart Piece With Epona and Clawshot")]
        kak_rock_spire_poh_with_epona_claw,

        [Description("Kak Village Bomb Rock Spire Heart Piece With Boomerang")]
        kak_rock_spire_poh_with_boomerang,

        [Description("Kak Village Watchtower Alcove Chest as Wolf")]
        watchtower_alcove_chest_as_wolf,

        [Description("Death Mountain Geysers Shield Skip")]
        geysers_shield_skip,

        [Description("Death Mountain Climb Without Irons")]
        dm_climb_without_irons,

        [Description("Bridge of Eldin Owl Statue Chest as Wolf")]
        bridge_owl_chest_as_wolf,

        [Description("Eldin Lava Cave Upper Chest With Nothing")]
        lava_cave_itemless_upper_chest,

        [Description("Eldin Lava Cave Wolf Jump to Bottom")]
        lava_cave_wolf_jump_to_bottom,

        [Description("Hidden Village Checks Without Clawshot")]
        hv_without_claw,

        [Description("Helmasaur Grotto With Only Clawshot")]
        helmasaur_grotto_with_claw,

        [Description("STAR 1 Without Clawshot")]
        star_1_without_claw,

        [Description("STAR 2 Without Double Clawshot")]
        star_2_without_2_claws,

        [Description("OCT South Double Claw Chest With Wolf")]
        outside_ct_chasm_with_wolf,

        [Description("OCT South Double Claw Chest With Sword")]
        outside_ct_chasm_with_sword,

        [Description("OCT South Double Claw Chest With Irons")]
        outside_ct_chasm_with_irons,

        [Description("OCT South Double Claw Chest Itemless")]
        outside_ct_chasm_itemless,

        [Description("OCT South Fountain Chest With Sword + Back Slice")]
        outside_ct_fountain_with_bs,

        [Description("Izas Helping Hand With Boomerang")]
        iza_1_with_boomerang,

        [Description("Legendary Hylian Loach With Frog Lure")]
        loach_with_frog_lure,

        [Description("ZD Underwater Rupees Without Zora Armor")]
        zd_underwater_rupees_without_za,

        [Description("ZD Waterfall Ledge With Box and Sword")]
        zd_waterfall_ledge_box_and_sword,

        [Description("ZD Wafterfall Ledge With Spinner")]
        zd_waterfall_ledge_spinner,

        [Description("Underwater Goron Without Zora Armor")]
        underwater_goron_without_za,

        [Description("Lake Lantern Cave Without Lantern")]
        llc_no_lantern,

        [Description("Shell Blade Grotto With Normal Bombs")]
        shell_blade_grotto_normal_bombs,

        [Description("Water Toadpoli Grotto With Wolf")]
        toadpoli_grotto_with_wolf,

        [Description("Bulblin Camp Boar With Irons")]
        camp_boar_with_irons,

        [Description("CoO Fairies As Access To Springs")]
        coo_fairy_access,

        [Description("FT Lobby Without Ranged Items")]
        ft_lobby_without_ranged_items,

        [Description("FT Lobby With Wolf")]
        ft_lobby_with_wolf,

        [Description("FT West Wing With Bombling")]
        ft_west_wing_bombling,

        [Description("FT North Tile Worm Boost To Chest")]
        ft_north_tile_worm_boost,

        [Description("FT North Wing With Bomb Boosts")]
        ft_north_bomb_boosts,

        [Description("FT Ook access With Midna")]
        ft_ook_with_midna,

        [Description("Defeat Fyrus Without Sword")]
        fyrus_without_sword,

        [Description("Defeat Fyrus Without Irons")]
        fyrus_without_irons,

        [Description("LBT Chandelier Drop")]
        lbt_chandelier_drop,

        [Description("LBT Big Key Chest Without Bombs")]
        lbt_bk_without_bombs,

        [Description("LBT Big Key Skip")]
        lbt_bk_skip,

        [Description("Defeat Morpheel Without Sword")]
        morpheel_without_sword,

        [Description("AG Entrance Chain Without Clawshot")]
        ag_entrance_chain_without_claw,

        [Description("AG Pillar Jump")]
        ag_pillar_jump,

        [Description("AG Big Key With Wolf")]
        ag_bk_with_wolf,

        [Description("SPR Lobby Chandelier Chest Without Wolf")]
        spr_lobby_chandelier_without_wolf,

        [Description("SPR Northeast Chandelier Chest With Back Slice")]
        spr_ne_chandelier_with_bs,

        [Description("SPR Ladder Freezard Cancel")]
        spr_ladder_freezard_cancel,

        [Description("ToT Crystal Switches With Clawshot")]
        tot_crystal_switches_with_claw,

        [Description("CitS Entrance Crystal Switch With Ball and Chain")]
        cits_entrance_with_bnc,

        [Description("CitS Fan Skip")]
        cits_fan_skip,

        [Description("CitS Central Oocca Room With No Items")]
        cits_itemless_central_room,

        [Description("CitS East Wing First Room With No Items")]
        cits_itemless_east_first_room,

        [Description("CitS Dinalfos Room With Clawshot")]
        cits_dinalfos_with_claw,

        [Description("CitS Central Outside Ledge Chest With Clawshot")]
        cits_central_outside_ledge_chest_with_claw,

        [Description("CitS Compass Chest With Double Clawshots")]
        cits_compass_chest_with_2_claws,

        [Description("CitS North Wing With Double Clawshots")]
        cits_north_with_2_claws,

        [Description("HC Skip Main Hall Barrier")]
        hc_skip_main_hall_barrier,

        [Description("HC Chandeliers With Single Clawshot")]
        hc_chandeliers_with_1_claw,

        [Description("HC Painting Switch With Bombs")]
        hc_painting_switch_with_bombs,

        [Description("HC Painting Switch With Jump Strike")]
        hc_painting_switch_with_js,

        [Description("HC Painting Switch With Back Slice")]
        hc_painting_switch_with_bs,

        [Description("HC Tower Climb With Single Clawshot")]
        hc_tower_climb_with_1_claw,

        [Description("Defeat Dark Beast Ganon Without Wolf")]
        beast_ganon_without_wolf,
        // Add new tricks directly above this line. DO NOT SORT THESE ENUMS. The numerical IDs are
        // auto-generated and they must be consistent. After adding the new enum directly above this
        // comment block, update the `GetUiDisplayTricks` method directly below this so that the new
        // trick displays in the UI.
    }

    public class LogicTricks
    {
        public static List<UiDisplay> GetUiDisplayTricks()
        {
            return new()
            {
                UiDisplay.Divider("General"),
                UiDisplay.Trick(Trick.can_smash_webs),
                UiDisplay.Trick(Trick.launch_bombs_with_boomerang),
                UiDisplay.Trick(Trick.freestandings_with_bnc),
                UiDisplay.Trick(Trick.drained_MA_as_irons),
                UiDisplay.Trick(Trick.shadow_beasts_without_mdh),
                // Add "Ordon" here once needed.

                UiDisplay.Divider("Faron"),
                UiDisplay.Trick(Trick.mist_stump_chest_as_wolf),
                UiDisplay.Trick(Trick.baba_serpent_grotto_with_wolf),
                UiDisplay.Divider("Eldin"),
                UiDisplay.Trick(Trick.kak_gorge_skip_claws_with_wolf),
                UiDisplay.Trick(Trick.kak_rock_spire_poh_with_epona_claw),
                UiDisplay.Trick(Trick.kak_rock_spire_poh_with_boomerang),
                UiDisplay.Trick(Trick.watchtower_alcove_chest_as_wolf),
                UiDisplay.Trick(Trick.geysers_shield_skip),
                UiDisplay.Trick(Trick.dm_climb_without_irons),
                UiDisplay.Trick(Trick.bridge_owl_chest_as_wolf),
                UiDisplay.Trick(Trick.lava_cave_itemless_upper_chest),
                UiDisplay.Trick(Trick.lava_cave_wolf_jump_to_bottom),
                UiDisplay.Trick(Trick.hv_without_claw),
                UiDisplay.Divider("Lanayru"),
                UiDisplay.Trick(Trick.helmasaur_grotto_with_claw),
                UiDisplay.Trick(Trick.star_1_without_claw),
                UiDisplay.Trick(Trick.star_2_without_2_claws),
                UiDisplay.Trick(Trick.outside_ct_chasm_with_wolf),
                UiDisplay.Trick(Trick.outside_ct_chasm_with_sword),
                UiDisplay.Trick(Trick.outside_ct_chasm_with_irons),
                UiDisplay.Trick(Trick.outside_ct_chasm_itemless),
                UiDisplay.Trick(Trick.outside_ct_fountain_with_bs),
                UiDisplay.Trick(Trick.iza_1_with_boomerang),
                UiDisplay.Trick(Trick.loach_with_frog_lure),
                UiDisplay.Trick(Trick.zd_underwater_rupees_without_za),
                UiDisplay.Trick(Trick.zd_waterfall_ledge_box_and_sword),
                UiDisplay.Trick(Trick.zd_waterfall_ledge_spinner),
                UiDisplay.Trick(Trick.underwater_goron_without_za),
                UiDisplay.Trick(Trick.llc_no_lantern),
                UiDisplay.Trick(Trick.shell_blade_grotto_normal_bombs),
                UiDisplay.Trick(Trick.toadpoli_grotto_with_wolf),
                UiDisplay.Divider("Desert"),
                UiDisplay.Trick(Trick.camp_boar_with_irons),
                UiDisplay.Trick(Trick.coo_fairy_access),
                // Add "Snowpeak" here once needed.

                UiDisplay.Divider("Forest Temple"),
                UiDisplay.Trick(Trick.ft_lobby_without_ranged_items),
                UiDisplay.Trick(Trick.ft_lobby_with_wolf),
                UiDisplay.Trick(Trick.ft_west_wing_bombling),
                UiDisplay.Trick(Trick.ft_north_tile_worm_boost),
                UiDisplay.Trick(Trick.ft_north_bomb_boosts),
                UiDisplay.Trick(Trick.ft_ook_with_midna),
                UiDisplay.Divider("Goron Mines"),
                UiDisplay.Trick(Trick.fyrus_without_sword),
                UiDisplay.Trick(Trick.fyrus_without_irons),
                UiDisplay.Divider("Lakebed Temple"),
                UiDisplay.Trick(Trick.lbt_chandelier_drop),
                UiDisplay.Trick(Trick.lbt_bk_without_bombs),
                UiDisplay.Trick(Trick.lbt_bk_skip),
                UiDisplay.Trick(Trick.morpheel_without_sword),
                UiDisplay.Divider("Arbiter's Grounds"),
                UiDisplay.Trick(Trick.ag_entrance_chain_without_claw),
                UiDisplay.Trick(Trick.ag_pillar_jump),
                UiDisplay.Trick(Trick.ag_bk_with_wolf),
                UiDisplay.Divider("Snowpeak Ruins"),
                UiDisplay.Trick(Trick.spr_lobby_chandelier_without_wolf),
                UiDisplay.Trick(Trick.spr_ne_chandelier_with_bs),
                UiDisplay.Trick(Trick.spr_ladder_freezard_cancel),
                UiDisplay.Divider("Temple of Time"),
                UiDisplay.Trick(Trick.tot_crystal_switches_with_claw),
                UiDisplay.Divider("City in the Sky"),
                UiDisplay.Trick(Trick.cits_entrance_with_bnc),
                UiDisplay.Trick(Trick.cits_fan_skip),
                UiDisplay.Trick(Trick.cits_itemless_central_room),
                UiDisplay.Trick(Trick.cits_itemless_east_first_room),
                UiDisplay.Trick(Trick.cits_dinalfos_with_claw),
                UiDisplay.Trick(Trick.cits_central_outside_ledge_chest_with_claw),
                UiDisplay.Trick(Trick.cits_compass_chest_with_2_claws),
                UiDisplay.Trick(Trick.cits_north_with_2_claws),
                // Add "Palace of Twilight" here once needed.

                UiDisplay.Divider("Hyrule Castle"),
                UiDisplay.Trick(Trick.hc_skip_main_hall_barrier),
                UiDisplay.Trick(Trick.hc_chandeliers_with_1_claw),
                UiDisplay.Trick(Trick.hc_painting_switch_with_bombs),
                UiDisplay.Trick(Trick.hc_painting_switch_with_js),
                UiDisplay.Trick(Trick.hc_painting_switch_with_bs),
                UiDisplay.Trick(Trick.hc_tower_climb_with_1_claw),
                UiDisplay.Trick(Trick.beast_ganon_without_wolf),
            };
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

        public static bool isEnabled(Trick trick)
        {
            // Glitched Logic assumes all tricks are enabled. May update this to a setting called "Advanced Logic" later.
            if (Randomizer.SSettings.logicRules == LogicRules.Glitched)
                return true;
            return Randomizer.SSettings.logicalTricks.Contains(trick);
        }

        public static List<string> GetListForSpoiler(List<Trick> tricksList)
        {
            if (!ListUtils.isEmpty(tricksList))
            {
                return tricksList.Select(trick => EnumUtils.GetDescription(trick)).ToList();
            }
            return new();
        }

        public class UiDisplay
        {
            // Ignores the numerical ID completely when null (for dividers)
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

            public static UiDisplay Trick(Trick trick) =>
                new()
                {
                    Id = (int)trick,
                    Value = trick,
                    DisplayName = EnumUtils.GetDescription(trick),
                    IsDivider = false
                };

            public static UiDisplay Divider(string label) =>
                new() { DisplayName = label, IsDivider = true };
        }
    }
}
