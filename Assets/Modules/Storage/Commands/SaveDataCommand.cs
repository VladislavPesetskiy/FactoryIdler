using Modules.Storage.Extensions;
using Modules.Storage.Models;
using Modules.Storage.Storage;
using Zenject;

namespace Modules.Storage.Commands
{
    public class SaveDataCommand : AbstractStorageCommand
    {
        private readonly IStorage storage;
        private readonly IPersistence data;

        public SaveDataCommand(IStorage storage, IPersistence data)
        {
            this.storage = storage;
            this.data = data;
        }

        public override void Execute()
        {
            storage.Save(data, data.GetDefaultSaveKey());
            OnCompleted(true);
        }

        public class Factory : PlaceholderFactory<IPersistence, SaveDataCommand>
        {
        }
    }
}