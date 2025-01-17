using System.Collections;
using Construction;
using CraftingSystem;
using Data;
using Data.Configs;
using ExchangeSystem;
using InventorySystem;
using SaveSystem;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    
    private int _profitPerSecond;
    
    public static Core Instance { get; private set; }
    public Inventory Inventory { get; private set; }
    public Crafting Crafting { get; private set; }
    public Exchange Exchange { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        _profitPerSecond = gameConfig.BaseProfitPerSecond;
        
        Inventory = new Inventory();
        Exchange = new Exchange(gameConfig.CoinsToReputationExchangeRate);
        Crafting = new Crafting(gameConfig.CraftingData);
    }

    private void OnDestroy() => Saves.Save();

    private IEnumerator CoinsIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Inventory.AddItem(ItemType.Coins, _profitPerSecond);
            Debug.Log($"Coins {_profitPerSecond} (Total: {Inventory.Items[ItemType.Coins]})");
        }
    }
    
    public void StartGame()
    {
        StartCoroutine(CoinsIncome());
        BuildingsManager.Instance.Init(0);
    }

    public void LoadGame()
    {
        StartCoroutine(CoinsIncome());
        Saves.Load();
    }
    
    public void IncreaseProfit(int value) => _profitPerSecond += value;
}