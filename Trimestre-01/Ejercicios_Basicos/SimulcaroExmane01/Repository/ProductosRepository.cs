using SimulcaroExmane01.Model;
using System.Text.Json;

namespace SimulcaroExmane01.Repository
{
    internal static class ProductosRepository
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "productos.json");

        public static bool Guardar(Producto producto)
        {
            bool guardado;
            try
            {
                if (!File.Exists(_filePath)) File.Create(_filePath);
                List<Producto>? productos = GetProductos()?.ToList();
                if (productos == null) productos = new List<Producto>();
                productos.Add(producto);
                string json = JsonSerializer.Serialize(productos, Options);
                File.WriteAllText(_filePath, json);
                guardado = true;
            }
            catch (Exception ex)
            {
                guardado = false;
            }
            return guardado;
        }

        public static bool Eliminar(Producto producto)
        {
            bool eliminado;
            try
            {
                List<Producto>? productos = GetProductos()?.ToList();
                if (productos != null)
                {
                    eliminado = productos.Remove(producto);
                    string json = JsonSerializer.Serialize(productos, Options);
                    File.WriteAllText(_filePath, json);
                }
                else eliminado = false;
            }
            catch (Exception ex)
            {
                eliminado = false;
            }

            return eliminado;
        }

        public static IEnumerable<Producto>? GetProductos()
        {
            IEnumerable<Producto>? productos;
            try
            {
                if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0) return new List<Producto>();
                string json = File.ReadAllText(_filePath);
                productos = JsonSerializer.Deserialize<IEnumerable<Producto>>(json, Options);
            }
            catch (Exception ex)
            {
                productos = null;
            }

            return productos;
        }
    }
}
