using Game.UI.Components;
using Modules.UIManager.Panels;
using UnityEngine;

namespace Game.UI.Loading
{
    public class GameLoadingPanel : AnimatedPanel
    {
        [SerializeField]
        private ProgressBar progressBar;

        public override void Initialize(UIContext panelContext = default(UIContext))
        {
            base.Initialize(panelContext);
            progressBar.SetNormalizedProgress(0, true);
        }

        public void SetProgress(float progressValue, bool instant = false)
        {
            progressBar.SetNormalizedProgress(progressValue, instant);
        }
    }
}
