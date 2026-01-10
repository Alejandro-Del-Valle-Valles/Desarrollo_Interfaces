using ModoConectado.Model;

namespace ModoConectado.Interfaces
{
    internal interface IServiceEmployee : IService<Employee, int>
    {
        Task<Result<IEnumerable<Employee>?>> GetAllByDepartmentId(int id);
        Task<Result<IEnumerable<Employee>?>> GetBySurname(string surname);
        Task<Result<IEnumerable<Employee>?>> GetByCraft(string craft);
        Task<Result<IEnumerable<Employee>?>> GetBySalary(float salary);
        Task<Result<IEnumerable<Employee>?>> GetByCommission(float commission);
        Task<Result<IEnumerable<Employee>?>> GetByRegistrationDate(string date);
    }
}
