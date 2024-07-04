namespace Model.Inventory
{
    public class HeaddressItem : Item, IEquipmentItem
    {
        public int ArmorValue { get; private set; }

        public HeaddressItem(HeaddressItemInfo so) : base(so) 
        {
            this.ArmorValue = so.ArmorValue;
        }
    }
}
