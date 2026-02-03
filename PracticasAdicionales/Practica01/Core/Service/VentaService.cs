using Practica01.Core.Entity;
using Practica01.Core.Enums;
using Practica01.Core.Factories;
using Practica01.Core.Helper;
using Practica01.Core.Repository.Interfaces;

namespace Practica01.Core.Service
{
    internal static class VentaService
    {

        private static readonly IRepository<Venta, string> _ventaRepository = RepositoryFactory.CreateRepository(RepositoryType.Memoria);

        /// <summary>
        /// Devuelve todas las ventas del repositorio
        /// </summary>
        /// <returns>Result con IList de Ventas si no hay fallos</returns>
        public static Result<IList<Venta>?> GetAll()
        {
            Result<IList<Venta>?> result;
            try
            {
                IList<Venta>? ventas = _ventaRepository.GetAll();
                result = Result<IList<Venta>?>.Success(ventas);
            }
            catch (Exception ex)
            {
                result = Result<IList<Venta>?>.Failure(ex);
            }

            return result;
        }

        /// <summary>
        /// Devuelve una venta en base al mes buscado
        /// </summary>
        /// <param name="mes">string mes de búsqueda</param>
        /// <returns>Result con la venta si no hay fallos</returns>
        public static Result<Venta?> GetByMes(string mes)
        {
            Result<Venta?> result;
            try
            {
                Venta? venta = _ventaRepository.GetById(mes);
                result = Result<Venta?>.Success(venta);
            }
            catch (Exception ex)
            {
                result = Result<Venta?>.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Inserta la venta si no existe ya.
        /// </summary>
        /// <param name="venta">Venta a insertar</param>
        /// <returns>Result si ha sido exitoso o no</returns>
        public static Result Insert(Venta venta)
        {
            Result result;
            try
            {
                bool insertado = _ventaRepository.Insert(venta);
                result = insertado ? Result.Success() : throw new Exception("Venta no guardada");
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Actualiza una venta si existe
        /// </summary>
        /// <param name="venta">Venta a actualizar</param>
        /// <returns>Result si la actalización ha sido o no exitosa</returns>
        public static Result Update(Venta venta)
        {
            Result result;
            try
            {
                bool actualizado = _ventaRepository.Update(venta);
                result = actualizado ? Result.Success() : throw new Exception("Venta no actualizada");
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Elimina una venta si existe
        /// </summary>
        /// <param name="mes">Mes de la venta a eliminar</param>
        /// <returns>Result si la eliminación ha sido o no exitosa</returns>
        public static Result Delete(string mes)
        {
            Result result;
            try
            {
                bool eliminado = _ventaRepository.Delete(mes);
                result = eliminado ? Result.Success() : throw new Exception("Venta no eliminada");
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }
    }
}
