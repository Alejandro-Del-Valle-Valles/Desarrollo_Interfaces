using Practica01.Core.Exceptions;
using Practica01.Core.Entity;
using Practica01.Core.Repository.Interfaces;

namespace Practica01.Core.Repository
{
    /// <summary>
    /// Repositorio de ventas que almacena la información en memoria
    /// </summary>
    internal class VentasMemoryRepository : IRepository<Venta, string>
    {
        private IDictionary<string, Venta> Ventas = new Dictionary<string, Venta>();

        /// <summary>
        /// Devuelve todas las ventas del diccionario
        /// </summary>
        /// <returns>ICollection de ventas</returns>
        public IList<Venta>? GetAll()
        {
            return Ventas.Values.ToList();
        }

        /// <summary>
        /// Devuelve la venta buscada por el mes, si no existe lanza una excepción.
        /// </summary>
        /// <param name="mes">string mes por el que se busca.</param>
        /// <returns>Venta si existe</returns>
        /// <exception cref="VentaNotFoundException">lanzado cuando no existe una venta con el mes buscado</exception>
        public Venta? GetById(string mes)
        {
            if (Ventas.ContainsKey(mes)) return Ventas[mes];
            throw new VentaNotFoundException();
        }

        /// <summary>
        /// Agrega al diccionario la venta si no existe una venta ya con ese mes.
        /// </summary>
        /// <param name="venta">Venta a insertar</param>
        /// <returns></returns>
        /// <exception cref="VentaAlreadyExistsException">lanzado si ya existe una venta con ese mes.</exception>
        public bool Insert(Venta venta) => !Ventas.TryAdd(venta.Mes, venta) ? throw new VentaAlreadyExistsException() : true;
        
        /// <summary>
        /// Actualiza la venta si existe.
        /// </summary>
        /// <param name="venta">Venta a actualizar</param>
        /// <returns>bool, true si se actualiza, false si no.</returns>
        /// <exception cref="VentaNotFoundException">Lanzado si la venta no existe.</exception>
        public bool Update(Venta venta)
        {
            if (!Ventas.ContainsKey(venta.Mes)) throw new VentaNotFoundException();
            Ventas[venta.Mes] = venta;
            return true;
        }

        /// <summary>
        /// Elimina la venta por el mes
        /// </summary>
        /// <param name="mes">string mes de la venta a eliminar</param>
        /// <returns>bool, true si la venta fue eliminada, false si no.</returns>
        /// <exception cref="VentaNotFoundException">Lanzado si la venta no existe</exception>
        public bool Delete(string mes) => !Ventas.Remove(mes) ? throw new VentaNotFoundException() : true;
        
    }
}
