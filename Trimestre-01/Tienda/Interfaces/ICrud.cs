using Tienda.Model;

namespace Tienda.Interfaces
{
    /// <summary>
    /// Interface used for CRUD operations.
    /// </summary>
    /// <typeparam name="T">Object to act</typeparam>
    /// <typeparam name="ID">Type of the attribute that acts as PK of the object.</typeparam>
    interface ICrud<T, ID>
    {
        public Task<bool> Insert(T obj);
        public Task<bool> Update(T obj);
        public Task<bool> Delete(ID id);
        public Task<T?> GetById(ID id);
        public Task<IEnumerable<T>> GetAll();

    }
}
