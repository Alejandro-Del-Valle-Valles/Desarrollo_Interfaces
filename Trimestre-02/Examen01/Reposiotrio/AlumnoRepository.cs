using Examen01.entity;
using Microsoft.Data.Sqlite;

namespace Examen01.Reposiotrio
{
    public static class AlumnoRepository
    {
        private static readonly string DB_NAME = "Alumnos.db3";

        private static string GetRuta() => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        private static SqliteConnection GetConexion() => new SqliteConnection($"Data Source={GetRuta()}");

        public static async Task IniciarBaseDatos()
        {
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      CREATE TABLE IF NOT EXISTS Alumnos(
                                      id INTEGER PRIMARY KEY AUTOINCREMENT,
                                      nombre TEXT NOT NULL,
                                      nota_media DECIMAL CHECK(nota_media >= 0 AND nota_media <= 10),
                                      fecha_nacimiento DATE
                                      )
                                      """;
                await comando.ExecuteNonQueryAsync();
                comando.CommandText = """
                                      INSERT INTO Alumnos (id, nombre, nota_media, fecha_nacimiento)
                                      VALUES 
                                          (1, 'Liam Smith', 8.5, '2000-01-15'),
                                          (2, 'Emma Johnson', 9.2, '1999-11-22'),
                                          (3, 'Noah Williams', 7.0, '2001-02-10'),
                                          (4, 'Olivia Brown', 6.4, '2000-05-30'),
                                          (5, 'William Jones', 8.1, '1998-12-12'),
                                          (6, 'Sophia Garcia', 9.8, '2002-03-25'),
                                          (7, 'James Miller', 5.5, '2001-07-08'),
                                          (8, 'Isabella Davis', 7.9, '1999-09-14'),
                                          (9, 'Benjamin Rodriguez', 6.8, '2000-10-01'),
                                          (10, 'Charlotte Martinez', 8.7, '2001-04-19'),
                                          (11, 'Lucas Hernandez', 4.5, '1998-06-22'),
                                          (12, 'Mia Lopez', 9.1, '2002-01-11'),
                                          (13, 'Alexander Gonzalez', 7.3, '2000-08-30'),
                                          (14, 'Evelyn Wilson', 8.0, '1999-03-05'),
                                          (15, 'Michael Anderson', 6.2, '2001-11-20'),
                                          (16, 'Harper Thomas', 9.5, '2002-05-12'),
                                          (17, 'Daniel Taylor', 5.9, '2000-02-28'),
                                          (18, 'Abigail Moore', 7.6, '1998-10-10'),
                                          (19, 'Henry Jackson', 8.4, '2001-09-09'),
                                          (20, 'Ella Martin', 9.0, '2002-07-15'),
                                          (21, 'Sebastian Lee', 6.7, '2000-12-01'),
                                          (22, 'Scarlett Perez', 8.9, '1999-01-20'),
                                          (23, 'Jack Thompson', 7.2, '2001-03-18'),
                                          (24, 'Aria White', 5.1, '1998-08-25'),
                                          (25, 'Samuel Harris', 8.3, '2002-09-30'),
                                          (26, 'Grace Sanchez', 9.6, '2001-06-14'),
                                          (27, 'David Clark', 6.0, '2000-04-04'),
                                          (28, 'Chloe Ramirez', 7.8, '1999-12-31'),
                                          (29, 'Joseph Lewis', 4.9, '1998-05-05'),
                                          (30, 'Victoria Robinson', 8.2, '2002-08-22')
                                      ON CONFLICT(Id) DO NOTHING;
                                      """;
                await comando.ExecuteNonQueryAsync();
            }
        }

        public static async Task<IList<Alumno>> GetAll()
        {
            IList<Alumno> alumnos = new List<Alumno>();
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      SELECT id, nombre, nota_media, fecha_nacimiento
                                      FROM Alumnos;
                                      """;
                using (var lector = await comando.ExecuteReaderAsync())
                {
                    while (await lector.ReadAsync())
                    {
                        alumnos.Add(new(lector.GetInt32(0), lector.GetString(1), lector.GetFloat(2),
                            lector.GetString(3)));
                    }
                }
            }
            return alumnos;
        }

        public static async Task<Alumno?> GetById(int id)
        {
            Alumno alumno = null;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      SELECT id, nombre, nota_media, fecha_nacimiento
                                      FROM Alumnos
                                      WHERE id = @id
                                      """;
                comando.Parameters.AddWithValue("@id", id);
                using (var lector = await comando.ExecuteReaderAsync())
                {
                    while (await lector.ReadAsync())
                    {
                        alumno = new(lector.GetInt32(0), lector.GetString(1), lector.GetFloat(2),
                            lector.GetString(3));
                    }
                }
            }
            return alumno;
        }

        public static async Task<bool> Insert(Alumno alumno)
        {
            bool resultado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      INSERT INTO Alumnos (id, nombre, nota_media, fecha_nacimiento)
                                      VALUES(@id, @nombre, @nota, @fecha)
                                      """;
                comando.Parameters.AddWithValue("@id", alumno.Id);
                comando.Parameters.AddWithValue("@nombre", alumno.Nombre);
                comando.Parameters.AddWithValue("@nota", alumno.NotaMedia);
                comando.Parameters.AddWithValue("@fecha", alumno.FechaNacimiento);
                resultado = await comando.ExecuteNonQueryAsync() > 0;
            }

            return resultado;
        }

        public static async Task<bool> Update(Alumno alumno)
        {
            bool resultado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                if (await GetById(alumno.Id) == null) return false;
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      UPDATE Alumnos
                                      SET nombre = @nombre, nota_media = @nota, fecha_nacimiento = @fecha
                                      WHERE id = @id
                                      """;
                comando.Parameters.AddWithValue("@id", alumno.Id);
                comando.Parameters.AddWithValue("@nombre", alumno.Nombre);
                comando.Parameters.AddWithValue("@nota", alumno.NotaMedia);
                comando.Parameters.AddWithValue("@fecha", alumno.FechaNacimiento);
                resultado = await comando.ExecuteNonQueryAsync() > 0;
            }
            return resultado;
        }

        public static async Task<bool> Delete(int id)
        {
            bool resultado;
            using (var conexion = GetConexion())
            {
                await conexion.OpenAsync();
                if (await GetById(id) == null) return false;
                var comando = conexion.CreateCommand();
                comando.CommandText = """
                                      DELETE FROM Alumnos
                                      WHERE id = @id
                                      """;
                comando.Parameters.AddWithValue("@id", id);
                resultado = await comando.ExecuteNonQueryAsync() > 0;
            }
            return resultado;
        }
    }
}
