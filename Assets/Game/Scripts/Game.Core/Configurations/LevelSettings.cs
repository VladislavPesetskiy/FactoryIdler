using System;
using UnityEngine;

namespace Game.Core.Configurations
{
    [Serializable]
    public class LevelSettings
    {
        [field: SerializeField]
        public BuildingParameter[] BuildingParameters { get; private set; }
    }
}