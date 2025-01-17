using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Configs/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private int profitPerSecond;
        
        public int ProfitPerSecond => profitPerSecond;
    }
}