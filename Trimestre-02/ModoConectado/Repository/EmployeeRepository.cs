using ModoConectado.Exceptions;
using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Service;

namespace ModoConectado.Repository
{
    class EmployeeRepository : ICrudEmployeeRepository
    {
        /// <summary>
        /// Create the table of Employees if not exists.
        /// <remarks>EXECUTE AFTER CREATE Departamento TABLE</remarks>
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
                                              CREATE TABLE IF NOT EXISTS Empleado(
                                              id_emp INTEGER PRIMARY KEY AUTOINCREMENT,
                                              apellido TEXT NOT NULL,
                                              oficio TEXT NOT NULL,
                                              salario REAL CHECK(salario > 0),
                                              comision REAL CHECK(comision > 0),
                                              fecha_alt TEXT NOT NULL,
                                              id_departamento INTEGER,
                                              FOREIGN KEY (id_departamento) REFERENCES Departamento(id_dep) ON DELETE SET NULL)
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
        /// Get all employees from the DB
        /// </summary>
        /// <returns>Task with Result with the Employees if Success when all goes great or Failure without Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetAll()
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    IList<Employee>? employees = new List<Employee>();
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              SELECT id_emp, apellido, oficio, salario, comision, fecha_alt, id_departamento
                                              FROM Empleado;
                                              """;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    Surname = reader.GetString(1),
                                    Craft = reader.GetString(2),
                                    Salary = reader.GetFloat(3),
                                    Commission = reader.GetFloat(4),
                                    RegistrationDate = reader.GetString(5),
                                    IdDepartment = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                    result = Result<IEnumerable<Employee>?>.Success(employees);
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get all employees from the specified department from DB
        /// </summary>
        /// <param name="id">int id of the department</param>
        /// <returns>Task with Result with the Employees if Success when all goes great or Failure without Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetAllByDepartmentId(int id)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    IList<Employee>? employees = new List<Employee>();
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              SELECT id_emp, apellido, oficio, salario, comision, fecha_alt, id_departamento
                                              FROM Empleado
                                              WHERE id_departamento = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    Surname = reader.GetString(1),
                                    Craft = reader.GetString(2),
                                    Salary = reader.GetFloat(3),
                                    Commission = reader.GetFloat(4),
                                    RegistrationDate = reader.GetString(5),
                                    IdDepartment = reader.GetInt32(6)
                                });
                            }
                        }
                    }

                    result = Result<IEnumerable<Employee>?>.Success(employees);
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Search and return the employee from the DB by his ID
        /// </summary>
        /// <param name="id">int id of the searched Employee</param>
        /// <returns>Task with Result with the searched Employee if Success when all goes great or Failure without the Employee if something went wrong</returns>
        public Task<Result<Employee?>> GetById(int id)
        {
            return Task.Run(() =>
            {
                Result<Employee?> result;
                try
                {
                    Employee? employee = null;
                    using (var connection = SqliteDbConnectionService.GetConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = """
                                              SELECT id_emp, apellido, oficio, salario, comision, fecha_alt, id_departamento
                                              FROM Empleado
                                              WHERE id_emp = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employee = new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    Surname = reader.GetString(1),
                                    Craft = reader.GetString(2),
                                    Salary = reader.GetFloat(3),
                                    Commission = reader.GetFloat(4),
                                    RegistrationDate = reader.GetString(5),
                                    IdDepartment = reader.GetInt32(6)
                                };
                            }
                        }
                    }
                    result = Result<Employee?>.Success(employee);
                }
                catch (Exception ex)
                {
                    result = Result<Employee?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Insert the new Employee into the DB
        /// </summary>
        /// <param name="obj">Employee to insert</param>
        /// /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Save(Employee obj)
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
                                              INSERT INTO Empleado(apellido, oficio, salario, comision, fecha_alt, id_departamento)
                                              VALUES(@surname, @craft, @salary, @commission, @reg_date, @id_dept);
                                              """;
                        command.Parameters.AddWithValue("@surname", obj.Surname);
                        command.Parameters.AddWithValue("@craft", obj.Craft);
                        command.Parameters.AddWithValue("@salary", obj.Salary);
                        command.Parameters.AddWithValue("@commission", obj.Commission);
                        command.Parameters.AddWithValue("@reg_date", obj.RegistrationDate);
                        command.Parameters.AddWithValue("@id_dept", obj.IdDepartment);
                        
                        if(command.ExecuteNonQuery() > 0) result = Result.Success();
                        else result = Result.Failure(new ObjectNotInsertedUpdatedDeletedException($"El empleado {obj.Surname} no ha sido insertado."));
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
        /// Update the Employee from the DB if exists.
        /// </summary>
        /// <param name="obj">Empoyee to update</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Update(Employee obj)
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
                                              UPDATE Empleado
                                              SET apellido = @surname, oficio = @craft, salario = @salary, comision = @commission, 
                                              fecha_alt = @reg_date, id_departamento = @id_dept
                                              WHERE id_emp = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", obj.Id);
                        command.Parameters.AddWithValue("@surname", obj.Surname);
                        command.Parameters.AddWithValue("@craft", obj.Craft);
                        command.Parameters.AddWithValue("@salary", obj.Salary);
                        command.Parameters.AddWithValue("@commission", obj.Commission);
                        command.Parameters.AddWithValue("@reg_date", obj.RegistrationDate);
                        command.Parameters.AddWithValue("@id_dept", obj.IdDepartment);

                        result = command.ExecuteNonQuery() > 0 
                            ? Result.Success() 
                            : Result.Failure(new ObjectNotInsertedUpdatedDeletedException($"El empleado {obj.Surname} no ha sido actualizado."));
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
        /// Delete the Employee from the DB if exists.
        /// </summary>
        /// <param name="id">int id of the Employee to delete.</param>
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
                                              DELETE FROM Empleado
                                              WEHRE id_emp = @id;
                                              """;
                        command.Parameters.AddWithValue("@id", id);
                        result = command.ExecuteNonQuery() > 0
                            ? Result.Success()
                            : Result.Failure(new ObjectNotInsertedUpdatedDeletedException($"El empleado con ID {id} no ha sido eliminado. Comprueba que exista dicho empleado."));
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
