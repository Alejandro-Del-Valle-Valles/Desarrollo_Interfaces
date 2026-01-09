using ModoConectado.Model;

namespace ModoConectado.Interfaces
{
    internal interface ICrudEmployeeRepository : ICrudRepository<Employee, int>
    {
        Task<Result<IEnumerable<Employee>?>> GetAllByDepartmentId(int id);
    }
}
