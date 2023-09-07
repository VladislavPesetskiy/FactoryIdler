using System.Collections.Generic;
using Game.Core.Levels;
using UnityEngine;

namespace Game.Configurations
{
    [CreateAssetMenu(fileName = nameof(LevelsConfig), menuName = "Configs/GameCore/"+nameof(LevelsConfig), order = 0)]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField]
        private List<LevelData> levelsData;

        public IReadOnlyCollection<LevelData> LevelsData => levelsData;
    }
}