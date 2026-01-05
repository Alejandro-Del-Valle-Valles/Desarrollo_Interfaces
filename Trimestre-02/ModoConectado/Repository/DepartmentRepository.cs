using ModoConectado.Interfaces;
using ModoConectado.Model;

namespace ModoConectado.Repository
{
    class DepartmentRepository : ICrudRepository<Department, int>
    {
        public Task<Result> InitializeRepository()
        {
            throw new NotImplementedException();
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
