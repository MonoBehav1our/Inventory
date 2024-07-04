namespace Model.Inventory
{
    public class OutwearItem : Item, IEquipmentItem
    {
        public int ArmorValue { get; private set; }

        public OutwearItem(OutwearItemInfo so) : base(so) 
        {
            this.ArmorValue = so.ArmorValue;
        }
    }
}
