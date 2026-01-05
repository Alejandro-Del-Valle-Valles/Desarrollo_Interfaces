using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;

namespace ModoConectado.Service
{
    sealed class DepartmentService : ICrudRepository<Department, int>
    {

        private readonly ICrudRepository<Department, int> _repository;

        private static readonly Lazy<DepartmentService> _instance = new(() => new DepartmentService());

        public static DepartmentService Instance = _instance.Value;

        private DepartmentService()
        {
            _repository = new DepartmentRepository();
        }

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
