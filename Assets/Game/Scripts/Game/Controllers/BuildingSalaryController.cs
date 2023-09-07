using System;
using DG.Tweening;
using Game.Core.Configurations;
using Game.Data;
using UniRx;

namespace Game.Controllers
{
    public class BuildingSalaryController : IDisposable
    {
        private readonly GameDataModel gameDataModel;
        private readonly LevelSettings levelSettings;
        
        private readonly CompositeDisposable compositeDisposables;
        private readonly CompositeDisposable salaryCompositeDisposables;

        private float secondsTimeToSalary = 0f;

        public BuildingSalaryController(GameDataModel gameDataModel, LevelSettings levelSettings)
        {
            this.gameDataModel = gameDataModel;
            this.levelSettings = levelSettings;

            compositeDisposables = new CompositeDisposable();
            salaryCompositeDisposables = new CompositeDisposable();

            InitializeOfflineSalary();
            
            gameDataModel.CurrentProgressIndex.Subscribe(OnBuildingChanged).AddTo(compositeDisposables);
        }

        private void InitializeOfflineSalary()
        {
            DateTime now = DateTime.Now;
            DateTime last = gameDataModel.LastSessionDate;
            TimeSpan offlineTime = now - last;
            int seconds = offlineTime.Seconds;
            
            BuildingParameter buildingParameter = levelSettings.BuildingParameters[gameDataModel.CurrentProgressIndex.Value];
            int coins = buildingParameter.CoinsPerSeconds * seconds;
            
            OnGiveSalary(coins);
        }

        private void OnBuildingChanged(int buildingIndex)
        {
            salaryCompositeDisposables.Clear();
            DOTween.Kill(this);
            
            var buildingParameter = levelSettings.BuildingParameters[buildingIndex];

            DOVirtual.DelayedCall(1f, () =>
            {
                secondsTimeToSalary++;
                if (secondsTimeToSalary >= buildingParameter.SalaryDelay)
                {
                    OnGiveSalary((int)(buildingParameter.CoinsPerSeconds * secondsTimeToSalary));
                    secondsTimeToSalary = 0f;
                }
            }).SetLoops(-1).SetId(this);
        }

        private void OnGiveSalary(int coinsCount)
        {
            gameDataModel.CoinsCount.Value += coinsCount;
            gameDataModel.Save();
        }

        public void Dispose()
        {
            compositeDisposables?.Dispose();
            salaryCompositeDisposables?.Dispose();
        }
    }
}