using System.Linq;
using Game.Core.Configurations;
using Game.Providers;
using Modules.Storage.Installers;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : Installer<GameInstaller>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ProjectContextRegistration()
        {
            ProjectContext.PreInstall += OnPreInstallProjectContext;
        }

        private static void OnPreInstallProjectContext()
        {
            var normInstallers = ProjectContext.Instance.NormalInstallers.ToList();
            normInstallers.Insert(0, new GameInstaller());
            ProjectContext.Instance.NormalInstallers = normInstallers;
            
            // Add SO installers
            var scInstallers = ProjectContext.Instance.ScriptableObjectInstallers.ToList();
            var coreLoaderConfig = CoreLoaderConfigs.Get();
            scInstallers.Add(coreLoaderConfig);
            ProjectContext.Instance.ScriptableObjectInstallers = scInstallers;

            // Add prefabs installers
            var prefabInstallers = ProjectContext.Instance.InstallerPrefabs.ToList();
            prefabInstallers.AddRange(coreLoaderConfig.PrefabInstallers);
            ProjectContext.Instance.InstallerPrefabs = prefabInstallers;
        }

        public override void InstallBindings()
        {
            // Install features
            
            DataInstaller.Install(Container);
            
            Container.Bind<ILevelsProvider>().To<PlayerLevelsProvider>().AsSingle();
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
        }
    }
}