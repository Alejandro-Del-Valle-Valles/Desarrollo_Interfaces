using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Service;

namespace ModoConectado.Repository
{
    class EmployeeRepository : ICrudRepository<Employee, int>
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

        public Task<Result<IEnumerable<Employee>?>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Employee?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(Employee obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(Employee obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
