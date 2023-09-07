using Modules.Storage.Extensions;
using Modules.Storage.Models;
using Modules.Storage.Storage;
using UniRx;
using Zenject;

namespace Modules.Storage.Commands
{
    public class LoadDataCommand : AbstractStorageCommand
    {
        private readonly IStorage storage;
        private readonly IPersistence data;
        private readonly SaveDataCommand.Factory saveDataFactory;

        public LoadDataCommand(IStorage storage, IPersistence data, SaveDataCommand.Factory saveDataFactory)
        {
            this.storage = storage;
            this.data = data;
            this.saveDataFactory = saveDataFactory;
        }

        public override void Execute()
        {
            storage
                .HasProperty(data.GetDefaultSaveKey())
                .Subscribe(result =>
                {
                    if (result)
                    {
                        LoadData();
                    }
                    else
                    {
                        var createDataCommand = saveDataFactory.Create(data);
                        createDataCommand.Completed += OnCreatedResult;
                        createDataCommand.Execute();
                    }
                });
        }

        private void OnCreatedResult(IStorageCommand storageCommand, bool result)
        {
            storageCommand.Completed -= OnCreatedResult;

            if (result)
            {
                LoadData(true);
            }
            else
            {
                OnCompleted(false);
            }
        }

        private void LoadData(bool justCreated = false)
        {
            storage.Load(data, data.GetDefaultSaveKey()).Subscribe((res) =>
            {
                data.JustCreate = justCreated;
                OnCompleted(res);
            });
        }

        public class Factory : PlaceholderFactory<IPersistence, LoadDataCommand>
        {
        }
    }
}