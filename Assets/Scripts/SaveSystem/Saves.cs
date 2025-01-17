using System.IO;
using Construction;
using Data;
using UnityEngine;

namespace SaveSystem
{
    public static class Saves
    {
        public static void Save()
        {
            SaveData saveData = new SaveData
            {
                coins = Core.Instance.Inventory.GetItemCount(ItemType.Coins),
                reputation = Core.Instance.Inventory.GetItemCount(ItemType.Reputation),
                buildingPermit = Core.Instance.Inventory.GetItemCount(ItemType.BuildingPermit),
                craftedPermitsCount = Core.Instance.Crafting.CraftedPermitsCount,
                currentBuildingIndex = BuildingsManager.Instance.CurrentBuildingIndex
            };
            
            string json = JsonUtility.ToJson(saveData);
            string path = Path.Combine(Application.dataPath, "save.json");
            using StreamWriter writer = new StreamWriter(path);
            writer.Write(json);
            Debug.Log("Game saved into save.json");
        }

        public static bool TryLoad()
        {
            string path = Path.Combine(Application.dataPath, "save.json");
            if (!File.Exists(path))
            {
                Debug.LogError("Save file not found.");
                return false;
            }

            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
                
            Core.Instance.Inventory.SetItemCount(ItemType.Coins, saveData.coins);
            Core.Instance.Inventory.SetItemCount(ItemType.Reputation, saveData.reputation);
            Core.Instance.Inventory.SetItemCount(ItemType.BuildingPermit, saveData.buildingPermit);
            Core.Instance.Crafting.Init(saveData.craftedPermitsCount);
            BuildingsManager.Instance.Init(saveData.currentBuildingIndex);

            return true;
        }
        
        public struct SaveData
        {
            public int coins;
            public int reputation;
            public int buildingPermit;
            public int currentBuildingIndex;
            public int craftedPermitsCount;
        }
    }
}