using Game.Controllers;
using Game.Core.Configurations;
using Game.Core.Levels;
using Game.Data;
using Game.UI.Panels;
using Modules.UIManager.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Levels
{
    public class GameLevel : Level
    {
        [SerializeField]
        public Transform buildingRoot;

        [Inject]
        private IUIScreenManager uiScreenManager;

        [Inject]
        private GameDataModel gameDataModel;

        private readonly CompositeDisposable compositeDisposables = new();
        
        private BuildingProgressController buildingProgressController;
        private BuildingSalaryController buildingSalaryController;

        public override async void Initialize(LevelSettings levelSettings)
        {
            base.Initialize(levelSettings);
            
            var panel = await uiScreenManager.ShowUIPanelAsync<GamePanel>();

            buildingProgressController = new BuildingProgressController(gameDataModel, levelSettings, buildingRoot);
            buildingSalaryController = new BuildingSalaryController(gameDataModel, levelSettings);
            
            panel.UpgradeButtonClick.Subscribe(_ => buildingProgressController.UpgradeBuilding()).AddTo(compositeDisposables);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            
            compositeDisposables.Clear();
            
            buildingSalaryController.Dispose();
            buildingProgressController.Dispose();
        }
    }
}