namespace Practica01.Core.Repository.Interfaces
{
    /// <summary>
    /// Contiene los métodos básicos que debe contener un repositorio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    internal interface IRepository<T, ID>
    {
        IList<T>? GetAll();
        T? GetById(ID id);
        bool Insert(T data);
        bool Update(T data);
        bool Delete(ID id);
    }
}
