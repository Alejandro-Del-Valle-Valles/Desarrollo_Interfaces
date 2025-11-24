using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;

namespace AgendaBienestar.Service
{
    internal class RegisterService(IGenericCrud<Register, Guid> repositoryCrud) : IGenericService<Register, Guid>
    {

        /// <summary>
        /// Create the Register in the Repository.
        /// </summary>
        /// <param name="obj">Register to create</param>
        /// <returns>Result, Success or Failure if something went wrong.</returns>
        public Result Create(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Insert(obj);
            }
            catch (IOException ex)
            {
                result = Result.Failure(ex);
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Update the Register from the Repository.
        /// </summary>
        /// <param name="obj">Register to update</param>
        /// <returns>Result, Success or Failure if something went wrong.</returns>
        public Result Modify(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Update(obj);
            }
            catch (IOException ex)
            {
                result = Result.Failure(ex);
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Delete the Register from the Repository.
        /// </summary>
        /// <param name="obj">Register to delete</param>
        /// <returns>Result, Success or Failure if something went wrong.</returns>
        public Result Delete(Guid id)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                result = repositoryCrud.Delete(id);
            }
            catch (IOException ex)
            {
                result = Result.Failure(ex);
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Get a Register from the Repository if exists.
        /// </summary>
        /// <param name="id">Guid Id of the searched Register.</param>
        /// <returns>Result, Success with the data or Failure with null data if something went wrong.</returns>
        public Result<Register?> GetById(Guid id)
        {
            Result<Register?> register = Result<Register?>.Failure(null, new Exception());
            try
            {
                register = repositoryCrud.GetById(id);
            }
            catch (IOException ex)
            {
                register = Result<Register?>.Failure(null, ex);
            }
            catch (Exception ex)
            {
                register = Result<Register?>.Failure(null, ex);
            }
            return register;
        }

        /// <summary>
        /// Get all Registers from the Repository.
        /// </summary>
        /// <returns>Result, Success with the data or Failure with null data if something went wrong.</returns>
        public Result<IEnumerable<Register>?> GetAll()
        {
            Result<IEnumerable<Register>?> registers = Result<IEnumerable<Register>?>.Failure(null, new Exception());
            try
            {
                registers = repositoryCrud.GetAll();
            }
            catch (IOException ex)
            {
                registers = Result<IEnumerable<Register>?>.Failure(null, ex);
            }
            catch (Exception ex)
            {
                registers = Result<IEnumerable<Register>?>.Failure(null, ex);
            }
            return registers;
        }
    }
}
