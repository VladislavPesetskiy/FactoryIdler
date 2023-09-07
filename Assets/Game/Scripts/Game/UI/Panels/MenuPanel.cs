using System;
using Modules.UIManager.Panels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
    public class MenuPanel : AnimatedPanel
    {
        [SerializeField]
        private Button startGameButton;
        
        public ISubject<Unit> EventStartGame { get; private set; }

        public override void Initialize(UIContext panelContext = default(UIContext))
        {
            base.Initialize(panelContext);
            
            EventStartGame = new Subject<Unit>();
            
            startGameButton.interactable = true;
            startGameButton.onClick.AddListener(OnStartGameButtonClick);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            
            startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            startGameButton.interactable = false;
            EventStartGame.OnNext(Unit.Default);
        }
    }
}