using System.Threading;
using Cysharp.Threading.Tasks;
using Modules.UIManager.Panels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.UIManager.Commands
{
    public class LoadPanelCommand : IPanelCommand
    {
        private AsyncOperationHandle<GameObject> loadPanelHandler;
        private CancellationTokenSource cancellationToken; 
        
        public readonly string PanelGuid;
        public bool IsDisposed { get; private set; }

        public LoadPanelCommand(string panelGuid)
        {
            PanelGuid = panelGuid;
            cancellationToken = new CancellationTokenSource();
        }
        
        public async UniTask<TPanel> Execute<TPanel>() where TPanel : UIPanel
        {
            loadPanelHandler = Addressables.LoadAssetAsync<GameObject>(new AssetReference(PanelGuid));
            var task = loadPanelHandler.ToUniTask(cancellationToken: cancellationToken.Token);
            return (await task).GetComponent<TPanel>();
        }
        
        public void Dispose()
        {
            if(IsDisposed) return;
            
            cancellationToken?.Cancel();
            cancellationToken?.Dispose();
            
            if (loadPanelHandler.IsValid())
            {
                Addressables.Release(loadPanelHandler);
            }

            IsDisposed = true;
        }
    }
}