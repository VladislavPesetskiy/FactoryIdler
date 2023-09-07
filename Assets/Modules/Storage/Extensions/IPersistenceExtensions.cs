using Modules.Storage.Models;

namespace Modules.Storage.Extensions
{
    public static class PersistenceExtensions
    {
        public static string GetDefaultSaveKey(this IPersistence persistence)
        {
            return persistence.GetType().FullName;
        }
    }
}