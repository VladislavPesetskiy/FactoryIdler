using System;
using Modules.Storage.Models;
using UniRx;
using UnityEngine;

namespace Game.Data
{
    public class GameDataModel : DataModel<GameData>, IAutoBindDataModel
    {
        public IReadOnlyReactiveProperty<int> CurrentLevelIndexReactive => Data.CurrentLevelIndexReactive ??= new IntReactiveProperty(0);
        public IntReactiveProperty CurrentProgressIndex => Data.CurrentProgressIndex ??= new IntReactiveProperty(0);
        public IntReactiveProperty CoinsCount => Data.CoinsCountReactive ??= new IntReactiveProperty(0);

        public DateTime LastSessionDate => Data.LastSessionDate ??= DateTime.Now;

        public readonly IntReactiveProperty NextCostReactive = new();
        public readonly BoolReactiveProperty UpgradeAvailableReactive = new();
        public readonly BoolReactiveProperty UpgradeFullReactive = new();

        public int CurrentLevelIndex
        {
            get => CurrentLevelIndexReactive.Value;
            set 
            {
                if (value < 0)
                {
                    Debug.LogError($"You try set level {value}. Level index can't be negative!");
                    return;
                }
                
                Data.CurrentLevelIndexReactive.Value = value;
            }
        }
    }
}