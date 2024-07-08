using System.IO;
using UnityEngine;

namespace Model.Inventory.Saves
{
    public static class InventorySaver
    {
        private static string PATH = Application.persistentDataPath + "/LOCAL_DATA.txt";

        public static void Save(InventoryGridData data)
        {
            string json = JsonUtility.ToJson(data);
            StreamWriter sw = new StreamWriter(PATH, false);
            sw.Write(json);
            sw.Close();
            sw.Dispose();
        }

        public static InventoryGridData Load()
        {
            if (File.Exists(PATH))
            {
                StreamReader streamReader = new StreamReader(PATH);
                string json = streamReader.ReadToEnd();

                InventoryGridData data = JsonUtility.FromJson<InventoryGridData>(json);
                return data;
            }
            else
            {
                InventoryGrid inventory = new InventoryGrid();
                var data = new InventoryGridData(inventory, true);
                Save(data);

                return data;
            }            
        }
    }
}