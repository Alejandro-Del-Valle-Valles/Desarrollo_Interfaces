using Microsoft.Data.Sqlite;

namespace ModoConectado.Service
{
    internal static class SqliteDbConnectionService
    {
        public static readonly string DB_NAME = "Empleados.db3";

        /// <summary>
        /// Return the path of the db file if the file exists.
        /// </summary>
        /// <returns>string path of the db file.</returns>
        public static string GetPath() => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);

        /// <summary>
        /// Return a connection to the DB.
        /// </summary>
        /// <returns></returns>
        public static SqliteConnection GetConnection() => new SqliteConnection($"Data Source={GetPath()}");
    }
}
