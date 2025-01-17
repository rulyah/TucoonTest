using System.Collections;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Screens
{
    public class GameplayScreen : ScreenBase
    {
        [SerializeField] private TMP_Text txtMoney;
        [SerializeField] private TMP_Text txtReputation;
        [SerializeField] private TMP_Text txtPermits;
        [SerializeField] private Button btnCraftPermit;
        [SerializeField] private Button btnBuyReputation;
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Core.Instance != null);
            
            txtMoney.text = $"Coins: {Core.Instance.Inventory.GetItemCount(ItemType.Coins)}";
            txtReputation.text = $"Rep: {Core.Instance.Inventory.GetItemCount(ItemType.Reputation)}";
            txtPermits.text = $"Permits: {Core.Instance.Inventory.GetItemCount(ItemType.BuildingPermit)}";
            
            Core.Instance.Inventory.OnItemsChanged += OnItemsChanged;
            btnCraftPermit.onClick.AddListener(OnCraftPermitButtonClick);
            btnBuyReputation.onClick.AddListener(OnBuyReputationButtonClick);
        }

        public void NewGame() => Core.Instance.StartGame();
        
        public void LoadGame() => Core.Instance.LoadGame();

        private void OnDestroy()
        {
            Core.Instance.Inventory.OnItemsChanged -= OnItemsChanged;
            btnCraftPermit.onClick.RemoveListener(OnCraftPermitButtonClick);
            btnBuyReputation.onClick.RemoveListener(OnBuyReputationButtonClick);
        }
        
        private void OnCraftPermitButtonClick()
        {
            if (!Core.Instance.Crafting.CraftBuildingPermit(Core.Instance.Inventory))
            {
                Debug.LogError("Not enough resources to craft building permit.");
                return;
            }
            
            Debug.Log($"Crafted building permit. You have {Core.Instance.Inventory.Items[ItemType.BuildingPermit]} building permits.");
        }
        
        private void OnBuyReputationButtonClick()
        {
            if (!Core.Instance.Exchange.TryBuyReputation(1, Core.Instance.Inventory))
            {
                Debug.LogError("Not enough coins to buy reputation.");
                return;
            }
            
            Debug.Log($"Bought 1 reputation. You have {Core.Instance.Inventory.Items[ItemType.Reputation]} reputation.");
        }
        
        private void OnItemsChanged(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Coins:
                    txtMoney.text = $"Coins: {Core.Instance.Inventory.GetItemCount(ItemType.Coins)}";
                    break;
                case ItemType.Reputation:
                    txtReputation.text = $"Rep: {Core.Instance.Inventory.GetItemCount(ItemType.Reputation)}";
                    break;
                case ItemType.BuildingPermit:
                    txtPermits.text = $"Permits: {Core.Instance.Inventory.GetItemCount(ItemType.BuildingPermit)}";
                    break;
            }
        }
    }
}