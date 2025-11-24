using RegistroUsuarios.Model;
using RegistroUsuarios.Interfaces;

namespace RegistroUsuarios.Service
{
    internal class RegistersService(IGenericCrud<Register, string> repositoryCrud) : IGenerciService<Register, string>
    {
        /// <summary>
        /// Insert into the repository the new register if it don't exist.
        /// </summary>
        /// <param name="obj">Register to insert.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Create(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Insert(obj);
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Update from the repository the register if exist.
        /// </summary>
        /// <param name="obj">Register to update.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Update(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Update(obj);
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Delete from the repository the register if exist.
        /// </summary>
        /// <param name="id">Email of the register to delete.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Delete(string id)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Delete(id);

            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Get from the repository the register by his email if exist.
        /// </summary>
        /// <param name="id">Email of the register to obtain.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result<Register?> GetById(string id)
        {
            Result<Register?> result = Result<Register?>.Failure(new Exception());
            try
            {
                result = repositoryCrud.GetById(id);
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Get from the repository all register if there's any.
        /// </summary>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result<IEnumerable<Register>?> GetAll()
        {
            Result<IEnumerable<Register>?> result = Result<IEnumerable<Register>?>.Failure(new Exception());
            try
            {
                result = repositoryCrud.GetAll();
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }
    }
}
