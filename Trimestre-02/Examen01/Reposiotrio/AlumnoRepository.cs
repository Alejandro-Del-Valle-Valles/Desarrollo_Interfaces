

using Microsoft.Data.Sqlite;

namespace Examen01.Reposiotrio
{
    public static class AlumnoRepository
    {
        private static readonly string DB_NAME = "Alumnos.db3";

        public static string GetRuta() => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        public static SqliteConnection GetConexion() => new SqliteConnection($"Data Source={GetRuta()}");

        public static Task IniciarBaseDatos()
        {
            return Task.Run(() =>
            {
                using (var conexion = GetConexion())
                {
                    conexion.Open();
                    var comando = conexion.CreateCommand();
                    comando.CommandText = """
                                          CREATE TABLE IF NOT EXISTS Alumnos(
                                          id INTEGER PRIMARY KEY AUTOINCREMENT,
                                          nombre TEXT NOT NULL,
                                          nota_media DECIMAL CHECK(nota_media >= 0 AND nota_media <= 10),
                                          fecha_nacimiento DATE
                                          )
                                          """;
                    comando.ExecuteNonQuery();
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
                    comando.ExecuteNonQuery();
                }
            });
        }
    }
}
