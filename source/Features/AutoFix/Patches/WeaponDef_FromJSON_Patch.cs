﻿using System;
using System.Linq;
using BattleTech;
using CustomComponents;
using Harmony;

namespace MechEngineer.Features.AutoFix.Patches
{
    [HarmonyPatch(typeof(WeaponDef), nameof(Weapon.FromJSON))]
    public static class WeaponDef_FromJSON_Patch
    {
        public static void Postfix(WeaponDef __instance)
        {
            try
            {
                var def = __instance;

                if (def.ComponentTags.IgnoreAutofix())
                {
                    return;
                }

                var changes = AutoFixerFeature.settings.AutoFixWeaponDefSlotsChanges;
                if (changes == null)
                {
                    return;
                }
                
                foreach (var change in changes.Where(x => x.Type == def.WeaponSubType))
                {
                    if (change.SlotChange != null)
                    {
                        var newValue = change.SlotChange.Change(def.InventorySize);
                        if (!newValue.HasValue)
                        {
                            return;
                        }
                        Traverse.Create(def).Property("InventorySize").SetValue(newValue);
                    }

                    if (change.TonnageChange != null)
                    {
                        var newValue = change.TonnageChange.Change(def.Tonnage);
                        if (!newValue.HasValue)
                        {
                            return;
                        }
                        Traverse.Create(def).Property("Tonnage").SetValue(newValue);
                    }
                }

                if (AutoFixerFeature.settings.AutoFixWeaponDefSplitting && !def.Is<DynamicSlots.DynamicSlots>())
                {
                    var threshold = AutoFixerFeature.settings.AutoFixWeaponDefSplittingLargerThan;
                    if (def.InventorySize >= threshold)
                    {
                        var slot = new DynamicSlots.DynamicSlots
                        {
                            InnerAdjacentOnly = true,
                            ReservedSlots = def.InventorySize - 1
                        };
                        def.AddComponent(slot);
                        Traverse.Create(def).Property("InventorySize").SetValue(1);
                    }
                }
            }
            catch (Exception e)
            {
                Control.mod.Logger.LogError(e);
            }
        }
    }
}
