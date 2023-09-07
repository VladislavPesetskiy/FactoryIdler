using System;

namespace Modules.Storage.Commands
{
    public abstract class AbstractStorageCommand : IStorageCommand
    {
        public bool IsCompleted { get; private set; } = false;
        public event Action<IStorageCommand, bool> Completed;
        public abstract void Execute();

        protected void OnCompleted(bool result)
        {
            IsCompleted = true;
            Completed?.Invoke(this, result);
        }
    }
}