﻿using System;
using BattleTech;
using Harmony;

namespace MechEngineer.Features.ArmorStructureChanges.Patches;

[HarmonyPatch(typeof(Mech), nameof(Mech.ArmorMultiplier), MethodType.Getter)]
public static class Mech_ArmorMultiplier_Getter_Patch
{
    public static void Postfix(Mech __instance, ref float __result)
    {
        try
        {
            __result *= ArmorStructureChangesFeature.GetArmorFactorForMech(__instance);
        }
        catch (Exception e)
        {
            Control.Logger.Error.Log(e);
        }
    }
}