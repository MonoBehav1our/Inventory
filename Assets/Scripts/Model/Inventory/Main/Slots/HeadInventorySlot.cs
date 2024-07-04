
namespace Model.Inventory
{
    public class HeadInventorySlot : InventorySlot<HeaddressItem>
    {
        public HeaddressItem HeaddressItem => (HeaddressItem)Item;

        public override void SetItem(HeaddressItem item, int amount = 1) 
        {
            Item = item;
            _amount = amount;
        }
    }

    public class OutwearInventorySlot : InventorySlot<OutwearItem>
    {
        public OutwearItem HeaddressItem => (OutwearItem)Item;

        public override void SetItem(OutwearItem item, int amount = 1)
        {
            Item = item;
            _amount = amount;
        }
    }
}
