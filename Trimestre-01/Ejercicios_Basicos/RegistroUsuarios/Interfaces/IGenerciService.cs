using RegistroUsuarios.Model;

namespace RegistroUsuarios.Interfaces
{
    internal interface IGenerciService<T, ID>
    {
        public Result Create(T obj);
        public Result Update(T obj);
        public Result Delete(ID id);
        public Result<T?> GetById(ID id);
        public Result<IEnumerable<T>?> GetAll();
    }
}
