using System;
using Cysharp.Threading.Tasks;
using Modules.UIManager.Panels;

namespace Modules.UIManager.Commands
{
    public interface IPanelCommand : IDisposable
    {
        UniTask<TPanel> Execute<TPanel>() where TPanel : UIPanel;
    }
}