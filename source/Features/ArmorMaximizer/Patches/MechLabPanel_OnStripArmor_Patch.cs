﻿using System;
using System.Linq;
using BattleTech.UI;

namespace MechEngineer.Features.ArmorMaximizer.Patches;

[HarmonyPatch(typeof(MechLabPanel), nameof(MechLabPanel.OnStripArmor))]
public static class MechLabPanel_OnStripArmor_Patch
{
    [HarmonyPrefix]
    public static void Prefix(ref bool __runOriginal, MechLabPanel __instance, MechLabMechInfoWidget ___mechInfoWidget, MechLabItemSlotElement ___dragItem)
    {
        if (!__runOriginal)
        {
            return;
        }

        try
        {
            if (__instance.Initialized && ___dragItem == null && !LocationExtensions.ChassisLocationList.Any(location => __instance.GetLocationWidget(location).IsDestroyed))
            {
                ArmorMaximizerHandler.OnStripArmor(__instance);
                __runOriginal = false;
            }
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
