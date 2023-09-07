using System;
using Modules.Storage.Models;

namespace Modules.Storage.Storage
{
    public interface IStorage
    {
        IObservable<bool> HasProperty(string propertyName);
        IObservable<bool> Load<T>(T loadingObject, string keyName) where T : IPersistence;
        void Save<T>(T savingObject, string keyName) where  T : IPersistence;
    }
}