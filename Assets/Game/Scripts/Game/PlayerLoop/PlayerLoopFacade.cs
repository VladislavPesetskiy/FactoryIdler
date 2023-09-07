using Cysharp.Threading.Tasks;
using Game.Core.Configurations;
using Game.Core.Levels;
using Game.Data;
using Game.Providers;
using Game.UI.Panels;
using Modules.UIManager.Core;
using UniRx;

namespace Game.PlayerLoop
{
    public class PlayerLoopFacade : IPlayerLoopFacade
    {
        public PlayerLoopFacade
        (
            ILevelsProvider levelsProvider,
            ILevelLoader levelLoader,
            IUIScreenManager uiScreenManager,
            GameDataModel gameDataModel
        )
        {
            this.levelsProvider = levelsProvider;
            this.levelLoader = levelLoader;
            this.uiScreenManager = uiScreenManager;
            this.gameDataModel = gameDataModel;
        }

        private readonly ILevelsProvider levelsProvider;
        private readonly ILevelLoader levelLoader;
        private readonly IUIScreenManager uiScreenManager;
        private readonly GameDataModel gameDataModel;

        private CompositeDisposable compositeDisposables;

        public void Initialize()
        {
            compositeDisposables = new CompositeDisposable();
            
            EnterMenu();
        }

        private async void EnterMenu()
        {
            var menuPanel = await uiScreenManager.ShowUIPanelAsync<MenuPanel>();
            menuPanel.EventStartGame.Subscribe(OnStartLevel).AddTo(compositeDisposables);
        }

        private void OnStartLevel(Unit unit)
        {
            compositeDisposables.Clear();
            uiScreenManager.HideUIPanelAsync<MenuPanel>().Forget();
            StartLevel();
        }

        private void StartLevel()
        {
            var levelData = levelsProvider.GetLevelDataByCurrentLevelNumber();

            levelLoader.LoadLevel(levelData, level =>
            {
                level.EventLevelResult.Subscribe(OnLevelResult).AddTo(compositeDisposables);
            });
        }

        private async UniTaskVoid CompleteLevel()
        {
            gameDataModel.CurrentLevelIndex++;
            gameDataModel.Save();

            var resultPanel = await uiScreenManager.ShowUIPanelAsync<ResultPanel>();
            resultPanel.EventNextButton.Subscribe(OnNextLevel).AddTo(compositeDisposables);
        }

        private void FailLevel()
        {
            // TODO Show Fail panel
        }

        private void OnLevelResult(bool isWin)
        {
            compositeDisposables.Clear();

            if (isWin)
            {
                CompleteLevel().Forget();
            }
            else
            {
                FailLevel();
            }
        }

        private void OnNextLevel(Unit unit)
        {
            compositeDisposables.Clear();
            uiScreenManager.HideUIPanel<ResultPanel>();
            StartLevel();
        }
    }
}