using Controller.Inventory;

namespace Model.Inventory
{
    public class AmmoItem : Item
    {
        public AmmoType AmmoType { get; private set; }

        public AmmoItem(AmmoItemInfo so) : base(so) 
        {
            this.AmmoType = so.AmmoType;
        }
    }
}
