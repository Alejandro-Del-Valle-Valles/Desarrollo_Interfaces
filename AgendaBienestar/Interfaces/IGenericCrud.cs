namespace AgendaBienestar.Interfaces
{
    internal interface IGenericCrud<T, ID>
    {
        public bool Insert(T obj);
        public bool Update(T obj);
        public bool Delete(ID obj);
        public T? GetById(ID id);
        public IEnumerable<T> GetAll();
    }
}
