namespace Tienda.Interfaces
{
    /// <summary>
    /// Interfaces used to create services.
    /// </summary>
    /// <typeparam name="T">object to act on.</typeparam>
    /// <typeparam name="ID">type of the PK of the object.</typeparam>
    interface IGenericService<T, ID>
    {
        public bool Create(T obj);
        public bool Modify(T obj);
        public bool Delete(ID id);
        public T? GetById(ID id);
        public IEnumerable<T> GetALl();
    }
}
