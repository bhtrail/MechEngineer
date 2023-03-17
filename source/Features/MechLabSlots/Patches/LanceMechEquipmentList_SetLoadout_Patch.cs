﻿using System;
using BattleTech.UI;

namespace MechEngineer.Features.MechLabSlots.Patches;

[HarmonyPatch(
    typeof(LanceMechEquipmentList),
    nameof(LanceMechEquipmentList.SetLoadout),
    new Type[0]
)]
public static class LanceMechEquipmentList_SetLoadout_Patch
{
    [HarmonyPriority(Priority.High)]
    [HarmonyPrefix]
    public static void Prefix(ref bool __runOriginal, LanceMechEquipmentList __instance)
    {
        if (!__runOriginal)
        {
            return;
        }

        try
        {
            CustomWidgetsFixLanceMechEquipment.SetLoadout_LabelDefaults(__instance);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }

    [HarmonyPriority(Priority.Low)]
    [HarmonyPostfix]
    public static void Postfix(LanceMechEquipmentList __instance)
    {
        try
        {
            CustomWidgetsFixLanceMechEquipment.SetLoadout_LabelOverrides(__instance);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
