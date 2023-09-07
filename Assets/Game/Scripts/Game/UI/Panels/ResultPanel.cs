using System;
using Modules.UIManager.Panels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
    public class ResultPanel : AnimatedPanel
    {
        [SerializeField]
        private Button nextButton;

        public ISubject<Unit> EventNextButton;

        public override void Initialize(UIContext panelContext = default(UIContext))
        {
            base.Initialize(panelContext);
            EventNextButton = new Subject<Unit>();
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            nextButton.onClick.RemoveListener(OnNextButtonClick);
        }

        public override void Show(bool instant = false, Action onShowed = null)
        {
            base.Show(instant, onShowed);
            
            nextButton.onClick.AddListener(OnNextButtonClick);
        }

        private void OnNextButtonClick()
        {
            nextButton.interactable = false;
            EventNextButton.OnNext(Unit.Default);
        }
    }
}