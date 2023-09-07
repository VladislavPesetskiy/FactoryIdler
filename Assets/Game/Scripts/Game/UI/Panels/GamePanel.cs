using Game.Data;
using Modules.UIManager.Panels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Panels
{
    public class GamePanel : AnimatedPanel
    {
        [SerializeField]
        private Button upgradeButton;

        [SerializeField]
        private TextMeshProUGUI upgradeCostSource;

        [SerializeField]
        private CanvasGroup fullUpgradeCanvasGroup;

        [SerializeField]
        private TextMeshProUGUI coinsCountSource;

        [Inject]
        private GameDataModel gameDataModel;

        private readonly CompositeDisposable compositeDisposables = new();
        
        public readonly ReactiveCommand UpgradeButtonClick = new();

        public override void Initialize(UIContext panelContext = null)
        {
            base.Initialize(panelContext);
            
            upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
            
            gameDataModel.NextCostReactive.Subscribe(OnCostChanged).AddTo(compositeDisposables);
            gameDataModel.UpgradeAvailableReactive.Subscribe(OnUpgradeAvailableChanged).AddTo(compositeDisposables);
            gameDataModel.UpgradeFullReactive.Subscribe(OnUpgradeFull).AddTo(compositeDisposables);
            gameDataModel.CoinsCount.Subscribe(OnCoinsCountChanged).AddTo(compositeDisposables);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            
            compositeDisposables.Clear();
            upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
        }

        private void OnCostChanged(int cost)
        {
            upgradeCostSource.text = $"{cost}$";
        }

        private void OnCoinsCountChanged(int coins)
        {
            coinsCountSource.text = $"{coins}$";
        }

        private void OnUpgradeAvailableChanged(bool available)
        {
            upgradeButton.interactable = available;
        }

        private void OnUpgradeFull(bool isFull)
        {
            fullUpgradeCanvasGroup.alpha = isFull ? 1f : 0f;
            fullUpgradeCanvasGroup.interactable = isFull;
        }

        private void OnUpgradeButtonClick()
        {
            UpgradeButtonClick.Execute();
        }
    }
}