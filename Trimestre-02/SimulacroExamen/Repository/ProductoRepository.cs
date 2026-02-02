using Microsoft.Data.Sqlite;
using SimulacroExamen.Entity;

namespace SimulacroExamen.Repository
{
    public static class ProductoRepository
    {

        private static readonly string DB_NAME = "Productos.db3";

        public static string GetRuta() => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        public static SqliteConnection GetConexion() => new SqliteConnection($"Data Source={GetRuta()}");

        public static async Task InicializarBaseDatos()
        {
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      CREATE TABLE IF NOT EXISTS Productos(
                                      nombre TEXT PRIMARY KEY,
                                      descripcion TEXT NOT NULL,
                                      precio DECIMAL
                                      )
                                      """;
                await comando.ExecuteNonQueryAsync();
            }
        }

        public static async Task<IList<Producto>> GetAll()
        {
            IList<Producto> productos = new List<Producto>();
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      SELECT nombre, descripcion, precio
                                      FROM Productos
                                      """;
                using (var lector = await comando.ExecuteReaderAsync())
                {
                    while (await lector.ReadAsync())
                    {
                        productos.Add(new(lector.GetString(0), lector.GetString(1), lector.GetFloat(2)));
                    }
                }
            }
            return productos;
        }

        public static async Task<Producto?> GetByNombre(string nombre)
        {
            Producto? producto = null;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      SELECT nombre, descripcion, precio
                                      FROM Productos
                                      WHERE nombre = @nombre
                                      """;
                comando.Parameters.AddWithValue("@nombre", nombre);
                using (var lector = await comando.ExecuteReaderAsync())
                {
                    while (await lector.ReadAsync())
                    {
                        producto = new(lector.GetString(0), lector.GetString(1), lector.GetFloat(2));
                    }
                }
            }
            return producto;
        }

        public static async Task<bool> Insert(Producto producto)
        {
            bool insertado = false;
            if (await GetByNombre(producto.Nombre) != null) return insertado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      INSERT INTO Productos (nombre, descripcion, precio)
                                      VALUES (@nombre, @descripcion, @precio)
                                      """;
                comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                insertado = comando.ExecuteNonQuery() > 0;
            }
            return insertado;
        }

        public static async Task<bool> Update(Producto producto)
        {
            bool actualizado = false;
            if (await GetByNombre(producto.Nombre) == null) return actualizado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      UPDATE Productos
                                      SET descripcion = @descripcion, precio = @precio
                                      WHERE nombre = @nombre
                                      """;
                comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                actualizado = comando.ExecuteNonQuery() > 0;
            }
            return actualizado;
        }

        public static async Task<bool> Delete(string nombre)
        {
            bool eliminado = false;
            if (await GetByNombre(nombre) == null) return eliminado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      DELETE FROM Productos
                                      WHERE nombre = @nombre
                                      """;
                comando.Parameters.AddWithValue("@nombre", nombre);
                eliminado = comando.ExecuteNonQuery() > 0;
            }
            return eliminado;
        }
    }
}
