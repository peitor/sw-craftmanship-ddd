using System.Collections.Generic;
using System.Linq;

namespace BowlingKata.Test
{
    public static class Config
    {
        public static string ConnectionString { get; set; } = "(default)";
    }

    /// <summary>
    /// This is not a real database.
    /// But it has all the pain that real databases have :)
    /// </summary>
    public static class Database
    {
        static Dictionary<string, List<DatabaseMetadata>> clientToDatabaseObjects = new Dictionary<string, List<DatabaseMetadata>>();

        public static void Add<T>(string tablename, T thing) where T:DatabaseMetadata
        {
            if (!clientToDatabaseObjects.ContainsKey(Config.ConnectionString))
            {
                clientToDatabaseObjects[Config.ConnectionString] = new List<DatabaseMetadata>();
            }
            clientToDatabaseObjects[Config.ConnectionString].Add(thing);
        }

        public static T[] GetAll<T>(string tablename) where T:DatabaseMetadata
        {
            if (!clientToDatabaseObjects.ContainsKey(Config.ConnectionString))
            {
                clientToDatabaseObjects[Config.ConnectionString] = new List<DatabaseMetadata>();
            }
            return clientToDatabaseObjects[Config.ConnectionString].Cast<T>().ToArray();
        }
    }


    public abstract class DatabaseMetadata
    {
    }

    public class DatabaseMetadata<DataType> : DatabaseMetadata // where DataType : struct
    {
    }
}