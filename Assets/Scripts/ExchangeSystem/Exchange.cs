using Data;
using InventorySystem;
using UnityEngine;

namespace ExchangeSystem
{
    public class Exchange
    {
        private int _exchangeRate;
        
        public Exchange(int exchangeRate) => _exchangeRate = exchangeRate;
        
        public bool TryBuyReputation(int count, Inventory inventory)
        {
            if (!inventory.TryRemoveItem(ItemType.Coins, count * _exchangeRate))
            {
                Debug.LogError($"Can't buy {count} reputation. Not enough coins. Need {count * _exchangeRate} coins.");
                return false;
            }
            
            inventory.AddItem(ItemType.Reputation, count);
            return true;
        }
    }
}