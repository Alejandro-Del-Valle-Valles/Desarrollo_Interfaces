namespace Tienda.Interfaces
{
    /// <summary>
    /// Interface used for CRUD operations.
    /// </summary>
    /// <typeparam name="T">Object to act</typeparam>
    /// <typeparam name="ID">Type of the attribute that acts as PK of the object.</typeparam>
    interface ICrud<T, ID>
    {
        public bool Insert(T obj);
        public bool Update(T obj);
        public bool Delete(ID id);
        public T? GetById(ID id);
        public IEnumerable<T> GetAll();

    }
}
