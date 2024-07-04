using UnityEngine;

namespace Model.Inventory.Saves
{
    public static class InventorySaver
    {
        private const string KEY1 = "INVENTORY_DATA";

        public static void Save(InventoryGridData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(KEY1, json);
        }

        public static InventoryGridData Load()
        {
            if (PlayerPrefs.HasKey(KEY1))
            {
                string save = PlayerPrefs.GetString(KEY1);
                InventoryGridData data = JsonUtility.FromJson<InventoryGridData>(save);
                return data;
            }
            else
            {
                InventoryGrid inventory = new InventoryGrid();
                var data = new InventoryGridData(inventory);
                Save(data);

                return data;
            }
        }
    }
}