﻿using System;
using BattleTech;
using Harmony;

namespace MechEngineer.Features.OrderedStatusEffects.Patches;

[HarmonyPatch(typeof(StatCollection), nameof(StatCollection.ModifyStatistic))]
public static class StatCollection_ModifyStatistic_Patch
{
    [HarmonyPostfix]
    public static void Postfix(StatCollection __instance, string statName, int __result)
    {
        try
        {
            if (__result < 0)
            {
                return;
            }
            OrderedStatusEffectsFeature.Shared.ModifyStatisticPostfix(__instance, statName);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
