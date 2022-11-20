﻿using System;
using BattleTech.UI;
using Harmony;

namespace MechEngineer.Features.Globals.Patches;

[HarmonyPatch(typeof(MechBayPanel), nameof(MechBayPanel.CloseMechBay))]
public static class MechBayPanel_CloseMechBay_Patch
{
    [HarmonyPostfix]
    public static void Postfix(MechBayPanel __instance)
    {
        try
        {
            Global.ActiveMechBayPanel = null;
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
