using ModoConectado.Interfaces;
using ModoConectado.Model;

namespace ModoConectado.Repository
{
    class EmployeeRepository : ICrudRepository<Employee, int>
    {
        public Task<Result> InitializeRepository()
        {
            throw new NotImplementedException();
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
