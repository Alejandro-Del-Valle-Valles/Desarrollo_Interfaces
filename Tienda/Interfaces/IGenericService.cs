using Tienda.Model;

namespace Tienda.Interfaces
{
    /// <summary>
    /// Interfaces used to create services.
    /// </summary>
    /// <typeparam name="T">object to act on.</typeparam>
    /// <typeparam name="ID">type of the PK of the object.</typeparam>
    interface IGenericService<T, ID>
    {
        public Task<ServiceResult> Create(T obj);
        public Task<ServiceResult> Modify(T obj);
        public Task<ServiceResult> Delete(ID id);
        public Task<ServiceResult<T>> GetById(ID id);
        public Task<ServiceResult<IEnumerable<T>>> GetAll();
    }
}
