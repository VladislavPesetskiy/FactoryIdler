using System;
using System.Linq;
using Modules.Storage.Commands;
using Modules.Storage.Models;
using Modules.Storage.Storage;
using Zenject;

namespace Modules.Storage.Installers
{
    public class DataInstaller : Installer<DataInstaller>
    {
        public override void InstallBindings()
        {
            // install app data factory
            Container.BindFactory<IPersistence, LoadDataCommand, LoadDataCommand.Factory>();
            Container.BindFactory<IPersistence, SaveDataCommand, SaveDataCommand.Factory>();
            
            Container.Bind<IStorage>().To<Storage.Storage>().AsSingle();

            // install app data
            var autoBindType = typeof(IAutoBindDataModel);
            var targetAutoData = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && autoBindType.IsAssignableFrom(t));
            
            foreach (var autoData in targetAutoData)
            {
                DataModel.BindData(Container, autoData);
            }
        }
    }
}