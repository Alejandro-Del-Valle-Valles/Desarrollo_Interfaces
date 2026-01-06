using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Service;

namespace ModoConectado.Repository
{
    class DepartmentRepository : ICrudRepository<Department, int>
    {
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

        public Task<Result<IEnumerable<Department>?>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Department?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(Department obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(Department obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
