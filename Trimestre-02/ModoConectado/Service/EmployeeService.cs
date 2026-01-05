using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;

namespace ModoConectado.Service
{
    class EmployeeService : IService<Employee, int>
    {

        private readonly ICrudRepository<Employee, int> _repository;

        private static readonly Lazy<EmployeeService> _instance = new(() => new EmployeeService());

        public static EmployeeService Instance = _instance.Value;

        private EmployeeService()
        {
            _repository = new EmployeeRepository();
        }

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
