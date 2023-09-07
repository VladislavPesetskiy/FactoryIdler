using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Providers
{
    public interface IAssetProvider
    {
        UniTask<TAsset> TryGetAsset<TAsset>(AssetReference assetReference);
    }
}