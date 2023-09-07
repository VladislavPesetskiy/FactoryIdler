using Game.Core.Levels;
using Game.Levels;

namespace Game.Providers
{
    public interface ILevelsProvider
    {
        LevelData GetLevelDataByCurrentLevelNumber();
    }
}