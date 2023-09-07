using System;
using Game.Core.Components;
using UnityEngine;

namespace Game.Core.Configurations
{
    [Serializable]
    public struct BuildingParameter
    {
        [field: SerializeField]
        public BuildingView BuildingView { get; private set; }
        
        [field: SerializeField]
        public int CoinsPerSeconds { get; private set; }
        
        [field: SerializeField]
        public float SalaryDelay { get; private set; }
        
        [field: SerializeField]
        public int BuildingCost { get; private set; }
    }
}