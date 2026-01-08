using ModoConectado.Exceptions;
using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Service;

namespace ModoConectado.Repository
{
    class DepartmentRepository : ICrudRepository<Department, int>
    {
        /// <summary>
        /// Create the table of Departments if not exists.
        /// <remarks>EXECUTE BEFORE EMPLOYEES</remarks>
        /// </summary>
        /// <returns>Result Success if all goes great or Failure with the exception if something went wrong.</returns>
        public async Task<Result> InitializeRepository()
        {
            return await Task.Run(() =>
            {
                Result result;
                try
                {
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              CREATE TABLE IF NOT EXISTS Departamento(
                                              id_dep INTEGER PRIMARY KEY AUTOINCREMENT,
                                              nombre TEXT NOT NULL,
                                              localizacion TEXT NOT NULL)
                                              """;
                        command.ExecuteNonQuery();
                    }
                    result = Result.Success();
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get all departments from the DB
        /// </summary>
        /// <returns>Task with Result with the Departments if Success when all goes great or Failure without Departments if something went wrong</returns>
        public Task<Result<IEnumerable<Department>?>> GetAll()
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Department>?> result;
                try
                {
                    IList<Department>? departments = new List<Department>();
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              SELECT id_dep, nombre, localizacion
                                              FROM Departamento;
                                              """;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departments.Add(new Department
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Localization = reader.GetString(2)
                                });
                            }
                        }
                    }
                    result = Result<IEnumerable<Department>?>.Success(departments);
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Department>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Search and return the department from the DB by his ID
        /// </summary>
        /// <param name="id">int id of the searched Department</param>
        /// <returns>Task with Result with the searched Department if Success when all goes great or Failure without the Department if something went wrong</returns>
        public Task<Result<Department?>> GetById(int id)
        {
            return Task.Run(() =>
            {
                Result<Department?> result;
                try
                {
                    Department? department = null;
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              SELECT id_dep, nombre, localizacion
                                              FROM Departamento
                                              WHERE id_dep = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                department = new Department()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Localization = reader.GetString(2)
                                };
                            }
                        }
                    }
                    result = Result<Department?>.Success(department);
                }
                catch (Exception ex)
                {
                    result = Result<Department?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Insert the new Department into the B.
        /// </summary>
        /// <param name="obj">Department to insert</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Save(Department obj)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              INSERT INTO Departamento(nombre, localizacion)
                                              VALUES(@name, @localization);
                                              """;
                        command.Parameters.AddWithValue("@name", obj.Name);
                        command.Parameters.AddWithValue("@localization", obj.Localization);

                        if (command.ExecuteNonQuery() > 0) result = Result.Success();
                        else result = Result.Failure(new ObjectNotInsertedUpdatedDeletedException($"El departamento {obj.Name} no ha sido insertado."));
                    }
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }
                return result;
            });
        }

        /// <summary>
        /// Update the Department from the DB if exists.
        /// </summary>
        /// <param name="obj">Department to update</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Update(Department obj)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              UPDATE Departamento
                                              SET nombre = @name, localizacion = @localization
                                              WHERE id_dep = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", obj.Id);
                        command.Parameters.AddWithValue("@name", obj.Name);
                        command.Parameters.AddWithValue("@localization", obj.Localization);

                        if (command.ExecuteNonQuery() > 0) result = Result.Success();
                        else result = Result.Failure(new ObjectNotInsertedUpdatedDeletedException(
                                $"El departamento {obj.Name} no ha sido actualizado. Comprueba que exista primero."));
                    }
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Delete the Department from the DB if exists
        /// </summary>
        /// <param name="id">int id of the department</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Delete(int id)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              DELETE FROM Departamento
                                              WHERE id_dep = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", id);

                        if (command.ExecuteNonQuery() > 0) result = Result.Success();
                        else result = Result.Failure(new ObjectNotInsertedUpdatedDeletedException(
                                $"El departamento con ID: {id} no ha sido eliminado."));
                    }
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }
    }
}
