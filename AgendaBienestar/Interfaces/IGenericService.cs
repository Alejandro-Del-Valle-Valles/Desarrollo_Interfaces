namespace AgendaBienestar.Interfaces
{
    internal interface IGenericService<T, ID>
    {
        public bool Create(T obj);
        public bool Modify(T obj);
        public bool Delete(ID id);
        public T? GetById(ID id);
        public IEnumerable<T> GetAll();
    }
}
