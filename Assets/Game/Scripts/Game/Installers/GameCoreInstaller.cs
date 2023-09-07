using Game.Core.Levels;
using Game.PlayerLoop;
using Game.Systems.PlayerInput;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameCoreInstaller : MonoInstaller<GameCoreInstaller>
    {
        [SerializeField]
        private GameCoreInitializer gameCoreInitializer;

        [SerializeField]
        private LevelLoader levelLoader;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameCoreInitializer>().FromInstance(gameCoreInitializer).AsSingle();
            Container.Bind<ILevelLoader>().To<LevelLoader>().FromInstance(levelLoader).AsSingle();
            
            Container.Bind<IPlayerLoopFacade>().To<PlayerLoopFacade>().AsSingle();
            Container.BindInterfacesTo<GameInput>().AsSingle();
        }
    }
}