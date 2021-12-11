using BattleTech;
using BattleTech.UI.Tooltips;

namespace MechEngineer.Features.OverrideDescriptions
{
    public interface IAdjustTooltipEquipment
    {
        void AdjustTooltipEquipment(TooltipPrefab_Equipment tooltip, MechComponentDef componentDef);
    }
}