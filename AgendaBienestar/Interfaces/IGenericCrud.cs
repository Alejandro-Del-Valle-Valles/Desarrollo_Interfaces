using AgendaBienestar.Model;

namespace AgendaBienestar.Interfaces
{
    internal interface IGenericCrud<T, ID>
    {
        public Result Insert(T obj);
        public Result Update(T obj);
        public Result Delete(ID obj);
        public Result<T?> GetById(ID id);
        public Result<IEnumerable<T>?> GetAll();
    }
}
