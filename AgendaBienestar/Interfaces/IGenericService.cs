using AgendaBienestar.Model;

namespace AgendaBienestar.Interfaces
{
    internal interface IGenericService<T, ID>
    {
        public Result Create(T obj);
        public Result Modify(T obj);
        public Result Delete(ID id);
        public Result<T?> GetById(ID id);
        public Result<IEnumerable<T>?> GetAll();
    }
}
