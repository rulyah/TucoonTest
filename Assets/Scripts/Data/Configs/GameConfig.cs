using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int baseProfitPerSecond;
        [SerializeField] private int coinsToReputationExchangeRate;
        [SerializeField] private List<CraftingData> craftingData;
        
        public int BaseProfitPerSecond => baseProfitPerSecond;
        public int CoinsToReputationExchangeRate => coinsToReputationExchangeRate;
        public IReadOnlyList<CraftingData> CraftingData => craftingData;
        
    }

    [Serializable]
    public struct CraftingData
    {
        [SerializeField] private int coinsCost;
        [SerializeField] private int reputationRequired;
        
        public int CoinsCost => coinsCost;
        public int ReputationRequired => reputationRequired;
    }
}