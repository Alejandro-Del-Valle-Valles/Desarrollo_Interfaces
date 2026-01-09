using ModoConectado.Model;

namespace ModoConectado.Interfaces
{
    internal interface IServiceEmployee : IService<Employee, int>
    {
        Task<Result<IEnumerable<Employee>?>> GetAllByDepartmentId(int id);
    }
}
