using System;
using UnityEngine;

namespace Modules.UIManager.Panels
{
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract bool IsActive { get; }
        public abstract string LayerID { get; }

        public abstract void Initialize(UIContext panelContext = null);
        public abstract void DeInitialize();
        public abstract void Show(bool instant = false, Action onShowed = null);
        public abstract void UpdatePanel(UIContext newPanelContext = null);
        public abstract void Hide(bool instant = false, Action onHidden = null);
    }

    public abstract class UIPanel<TPanelContext> : UIPanel where TPanelContext : UIContext
    {
        public TPanelContext PanelContext { get; private set; }
        
        public override void Initialize(UIContext panelContext = null)
        {
            PanelContext = (TPanelContext)panelContext;
        }

        public override void UpdatePanel(UIContext panelContext = null)
        {
            PanelContext = (TPanelContext)panelContext;;
        }
    }
}