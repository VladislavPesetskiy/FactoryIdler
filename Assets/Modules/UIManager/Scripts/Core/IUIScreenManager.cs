using System;
using Cysharp.Threading.Tasks;
using Modules.UIManager.Panels;

namespace Modules.UIManager.Core
{
    public interface IUIScreenManager
    {
        bool IsPanelShowed<TPanel>() where TPanel : UIPanel;

        void ShowUIPanel<TPanel>(UIContext context = null, bool instant = false, Action<TPanel> panelShowed = null)
            where TPanel : UIPanel;

        UniTask<TPanel> ShowUIPanelAsync<TPanel>(UIContext context = null, bool instant = false) where TPanel : UIPanel;

        void UpdateUIPanel<TPanel>(UIContext context);
        
        void HideUIPanel<TPanel>(bool instant = false, Action panelHidden = null) where TPanel : UIPanel;
        
        UniTask HideUIPanelAsync<TPanel>(bool instant = false) where TPanel : UIPanel;

        bool TryGetUIPanel<TPanel>(out TPanel panel) where TPanel : UIPanel;
    }
}