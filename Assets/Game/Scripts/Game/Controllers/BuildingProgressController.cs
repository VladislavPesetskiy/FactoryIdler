using System;
using Game.Core.Components;
using Game.Core.Configurations;
using Game.Data;
using UniRx;
using UnityEngine;

namespace Game.Controllers
{
    public class BuildingProgressController : IDisposable
    {
        private readonly GameDataModel gameDataModel;
        private readonly LevelSettings levelSettings;
        private readonly Transform buildingRoot;

        private BuildingView currentBuildingView;
        private CompositeDisposable compositeDisposable;

        public BuildingProgressController(GameDataModel gameDataModel, LevelSettings levelSettings, Transform buildingRoot)
        {
            this.gameDataModel = gameDataModel;
            this.levelSettings = levelSettings;
            this.buildingRoot = buildingRoot;

            compositeDisposable = new CompositeDisposable();
            gameDataModel.CoinsCount.Subscribe(_ => UpdateData()).AddTo(compositeDisposable);
            
            var buildingView = levelSettings.BuildingParameters[gameDataModel.CurrentProgressIndex.Value].BuildingView;
            SetBuilding(buildingView);
        }

        public void UpgradeBuilding()
        {
            var nextCost = levelSettings.BuildingParameters[gameDataModel.CurrentProgressIndex.Value + 1].BuildingCost;
            gameDataModel.CoinsCount.Value -= nextCost;

            gameDataModel.CurrentProgressIndex.Value++;
            
            var buildingView = levelSettings.BuildingParameters[gameDataModel.CurrentProgressIndex.Value].BuildingView;
            SetBuilding(buildingView);
        }

        private void UpdateData()
        {
            var currentIndex = gameDataModel.CurrentProgressIndex.Value;
            var fullUpgrade = currentIndex + 1 >= levelSettings.BuildingParameters.Length;
            
            gameDataModel.UpgradeFullReactive.Value = fullUpgrade;

            if (fullUpgrade == false)
            {
                var nextCost = levelSettings.BuildingParameters[currentIndex + 1].BuildingCost;
                var availableUpgrade = gameDataModel.CoinsCount.Value >= nextCost;
                
                gameDataModel.UpgradeAvailableReactive.Value = availableUpgrade;
                gameDataModel.NextCostReactive.Value = nextCost;
            }
        }

        private void SetBuilding(BuildingView buildingView)
        {
            if (currentBuildingView != null)
            {
                GameObject.Destroy(currentBuildingView.gameObject);
            }

            currentBuildingView = GameObject.Instantiate(buildingView, buildingRoot);
        }

        public void Dispose()
        {
            compositeDisposable?.Dispose();
        }
    }
}