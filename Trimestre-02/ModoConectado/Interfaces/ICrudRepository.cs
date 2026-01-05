using ModoConectado.Model;

namespace ModoConectado.Interfaces
{
    interface ICrudRepository<T, ID>
    {
        Task<Result> InitializeRepository();
        Task<Result<IEnumerable<T>?>> GetAll();
        Task<Result<T?>> GetById(ID id);
        Task<Result> Save(T obj);
        Task<Result> Update(T obj);
        Task<Result> Delete(ID id);
    }
}
