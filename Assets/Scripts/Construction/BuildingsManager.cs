using System.Collections.Generic;
using Data;
using Data.Configs;
using InventorySystem;
using UnityEngine;

namespace Construction
{
    public class BuildingsManager : MonoBehaviour
    {
        [SerializeField] private List<Building> buildings;
        [SerializeField] private BuildingsConfig buildingsConfig;

        public static BuildingsManager Instance { get; private set; }
        public int CurrentBuildingIndex { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void Init(int currentBuildingIndex)
        {
            CurrentBuildingIndex = currentBuildingIndex;
            var index = Mathf.Clamp(CurrentBuildingIndex, 0, buildingsConfig.Buildings.Count);
            
            for (var i = 0; i < index; i++)
            {
                var building = buildings.Find(n => n.Config == buildingsConfig.Buildings[i]);
                building.Build();
            }
            
            SetNextBuilding();
        }
        
        public bool TryBuildBuilding(Building building)
        {
            if (!Core.Instance.Inventory.Items.TryGetValue(ItemType.BuildingPermit, out int permitsCount) || permitsCount == 0)
            {
                Debug.LogError("Not enough building permits.");
                return false;
            }
            
            Core.Instance.Inventory.TryRemoveItem(ItemType.BuildingPermit, 1);
            Core.Instance.IncreaseProfit(building.Config.ProfitPerSecond);

            building.Build();
            CurrentBuildingIndex++;
            SetNextBuilding();
            
            return true;
        }

        void SetNextBuilding()
        {
            if(CurrentBuildingIndex >= buildingsConfig.Buildings.Count)
                return;
            
            var building = buildings.Find(n => n.Config == buildingsConfig.Buildings[CurrentBuildingIndex]);
            building.SetAsCurrentBuilding();
        }
    }
}