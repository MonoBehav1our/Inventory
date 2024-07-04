using UnityEngine;

namespace View.Inventory
{
    public class InventoryGridView : MonoBehaviour
    {
        [field: SerializeField] public EquipmentInventorySlotView HeadSlotView { get; private set; }
        [field: SerializeField] public EquipmentInventorySlotView OutwearSlotView { get; private set; }

        [field: SerializeField] public InventorySlotView[] MainSlotsViews { get; private set; }
    }
}