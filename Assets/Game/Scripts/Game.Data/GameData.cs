using System;
using UniRx;

namespace Game.Data
{
    [Serializable]
    public class GameData
    {
        public IntReactiveProperty CurrentLevelIndexReactive;
        public IntReactiveProperty CurrentProgressIndex;
        public IntReactiveProperty CoinsCountReactive;
        public DateTime? LastSessionDate;
    }
}