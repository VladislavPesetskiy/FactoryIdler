using System.Collections.Generic;
using System.Linq;
using Game.Configurations;
using Game.Core.Levels;
using Game.Data;
using Game.Levels;

namespace Game.Providers
{
    public class PlayerLevelsProvider : ILevelsProvider
    {
        public PlayerLevelsProvider(GameDataModel gameDataModel, LevelsConfig levelsConfig)
        {
            this.gameDataModel = gameDataModel;
            this.levelsConfig = levelsConfig;
        }
        
        private readonly GameDataModel gameDataModel;
        private readonly LevelsConfig levelsConfig;

        public LevelData GetLevelDataByCurrentLevelNumber()
        {
            var currentLevelIndex = gameDataModel.CurrentLevelIndex;
            var levelsCount = levelsConfig.LevelsData.Count;
            if (currentLevelIndex >= levelsCount)
            {
                var repeatLevels = GetRepeatLevels();
                var repeatIndex = (currentLevelIndex - levelsCount) % repeatLevels.Count;
                var repeatLevelData = repeatLevels[repeatIndex];
                return repeatLevelData;
            }

            var levelData = levelsConfig.LevelsData.ElementAt(currentLevelIndex);
            return levelData;   
        }

        private List<LevelData> GetRepeatLevels()
        {
            return levelsConfig.LevelsData.Where(t => t.IsRepeat).ToList();
        }
    }
}