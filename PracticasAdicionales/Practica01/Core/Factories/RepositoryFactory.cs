using Practica01.Core.Entity;
using Practica01.Core.Enums;
using Practica01.Core.Repository;
using Practica01.Core.Repository.Interfaces;

namespace Practica01.Core.Factories
{
    internal static class RepositoryFactory
    {
        public static IRepository<Venta, string> CreateRepository(RepositoryType type)
        {
            return type switch
            {
                RepositoryType.Memoria => new VentasMemoryRepository(),
                _ => throw new ArgumentException("Invalid repository type")
            };
        }
    }
}
