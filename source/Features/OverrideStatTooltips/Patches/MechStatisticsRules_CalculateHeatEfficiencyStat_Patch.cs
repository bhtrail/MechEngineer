﻿using System;
using BattleTech;
using MechEngineer.Features.OverrideStatTooltips.Helper;

namespace MechEngineer.Features.OverrideStatTooltips.Patches;

[HarmonyPatch(typeof(MechStatisticsRules), nameof(MechStatisticsRules.CalculateHeatEfficiencyStat))]
public static class MechStatisticsRules_CalculateHeatEfficiencyStat_Patch
{
    [HarmonyPrefix]
    public static void Prefix(ref bool __runOriginal, MechDef mechDef, ref float currentValue, ref float maxValue)
    {
        if (!__runOriginal)
        {
            return;
        }

        try
        {
            var value = OverrideStatTooltipsFeature.HeatEfficiencyStat.BarValue(mechDef);
            MechStatUtils.SetStatValues(value, ref currentValue, ref maxValue);
            __runOriginal = false;
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
