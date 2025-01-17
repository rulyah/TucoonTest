using System.Collections.Generic;
using Data;
using Data.Configs;
using InventorySystem;
using UnityEngine;

namespace CraftingSystem
{
    public class Crafting
    {
        private IReadOnlyList<CraftingData> _craftingData;
        
        public int CraftedPermitsCount { get; private set; }

        public Crafting(IReadOnlyList<CraftingData> craftingData) => _craftingData = craftingData;
        
        public void Init(int currentCraftingIndex) => CraftedPermitsCount = currentCraftingIndex;
    
        public bool CraftBuildingPermit(Inventory inventory)
        {
            var craftingData = CraftedPermitsCount > _craftingData.Count - 1
                ? _craftingData[^1]
                : _craftingData[CraftedPermitsCount];

            if (!inventory.Items.TryGetValue(ItemType.Reputation, out int repCount) || repCount < craftingData.ReputationRequired)
            {
                Debug.LogError($"Can't craft building permit. Rep: {repCount}/{craftingData.ReputationRequired}.");
                return false;
            }
            
            if (!inventory.TryRemoveItem(ItemType.Coins, craftingData.CoinsCost))
            {
                Debug.LogError("Can't craft building permit. " +
                               $"Coins: {inventory.GetItemCount(ItemType.Coins)}/{craftingData.CoinsCost}.");
                
                return false;
            }
            
            CraftedPermitsCount++;
            inventory.AddItem(ItemType.BuildingPermit, 1);
            Debug.Log("Crafted building permit.");
            return true;
        }
    }
}