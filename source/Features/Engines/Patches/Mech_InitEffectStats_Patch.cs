﻿using System;
using BattleTech;
using Harmony;
using MechEngineer.Features.Engines.StaticHandler;

namespace MechEngineer.Features.Engines.Patches;

[HarmonyPatch(typeof(Mech), nameof(Mech.InitEffectStats))]
public static class Mech_InitEffectStats_Patch
{
    [HarmonyPrefix]
    public static void Prefix(Mech __instance)
    {
        try
        {
            Jumping.InitEffectStats(__instance);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }

    [HarmonyPostfix]
    public static void Postfix(Mech __instance)
    {
        try
        {
            EngineMisc.OverrideInitEffectStats(__instance);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
