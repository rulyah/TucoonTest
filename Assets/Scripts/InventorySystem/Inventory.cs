using System;
using System.Collections.Generic;
using Data;

namespace InventorySystem
{
    public class Inventory
    {
        private readonly Dictionary<ItemType, int> _items = new();
        
        public IReadOnlyDictionary<ItemType, int> Items => _items;
        
        public event Action<ItemType> OnItemsChanged; 
        
        public int GetItemCount(ItemType item) => _items.GetValueOrDefault(item, 0);
        
        public void AddItem(ItemType item, int count)
        {
            if (!_items.TryAdd(item, count))
                _items[item] += count;
            
            OnItemsChanged?.Invoke(item);
        }
        
        public void SetItemCount(ItemType item, int count)
        {
            _items[item] = count;
            OnItemsChanged?.Invoke(item);
        }
        
        public bool TryRemoveItem(ItemType item, int count)
        {
            if (_items.ContainsKey(item))
            {
                if (_items[item] >= count)
                {
                    _items[item] -= count;
                    
                    if (_items[item] == 0)
                        _items.Remove(item);
                    
                    OnItemsChanged?.Invoke(item);
                    return true;
                }
            }
            
            return false;
        }
    }
}