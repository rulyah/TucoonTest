using System.Collections.Generic;
using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(fileName = "BuildingsConfig", menuName = "Configs/BuildingsConfig")]
    public class BuildingsConfig : ScriptableObject
    {
        [SerializeField] private List<BuildingConfig> buildings;

        public IReadOnlyList<BuildingConfig> Buildings => buildings;
    }
}