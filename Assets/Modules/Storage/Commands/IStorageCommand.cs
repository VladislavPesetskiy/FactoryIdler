using System;

namespace Modules.Storage.Commands
{
    public interface IStorageCommand
    {
        event Action<IStorageCommand, bool> Completed;
        
        void Execute();
    }
}