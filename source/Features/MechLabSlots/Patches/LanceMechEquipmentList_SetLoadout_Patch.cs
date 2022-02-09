using System;
using System.Collections.Generic;
using BattleTech;
using BattleTech.Data;
using BattleTech.UI;
using BattleTech.UI.TMProWrapper;
using Harmony;
using UnityEngine;

namespace MechEngineer.Features.MechLabSlots.Patches
{
    [HarmonyPatch(
        typeof(LanceMechEquipmentList),
        nameof(LanceMechEquipmentList.SetLoadout),
        typeof(LocalizableText), typeof(UIColorRefTracker), typeof(Transform), typeof(ChassisLocations)
    )]
    public static class LanceMechEquipmentList_SetLoadout_Patch
    {
        public static void Postfix(
            LocalizableText headerLabel,
            ChassisLocations location,
            MechDef ___activeMech,
            DataManager ___dataManager,
            List<GameObject> ___allComponents
            )
        {
            try
            {
                CustomWidgetsFixLanceMechEquipment.SetLoadout(
                    headerLabel,
                    location,
                    ___activeMech,
                    ___dataManager,
                    ___allComponents
                );
            }
            catch (Exception e)
            {
                Control.Logger.Error.Log(e);
            }
        }
    }
}