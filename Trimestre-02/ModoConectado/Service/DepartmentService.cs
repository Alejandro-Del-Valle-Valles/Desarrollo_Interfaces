using ModoConectado.Interfaces;
using ModoConectado.Model;

namespace ModoConectado.Service
{
    sealed class DepartmentService(ICrudRepository<Department, int> _repository) : IService<Department, int>
    {
        /// <summary>
        /// Initialize the repository (Create files, tables, etc)
        /// </summary>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> InitializeRepository()
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    result = _repository.InitializeRepository().Result;
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get all departments from the repository
        /// </summary>
        /// <returns>Task with Result with the Departments if Success when all goes great or Failure without Departments if something went wrong</returns>
        public Task<Result<IEnumerable<Department>?>> GetAll()
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Department>?> result;
                try
                {
                    result = _repository.GetAll().Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Department>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Search and return the department from the repository by his ID
        /// </summary>
        /// <param name="id">int id of the searched Department</param>
        /// <returns>Task with Result with the searched Department if Success when all goes great or Failure without the Department if something went wrong</returns>
        public Task<Result<Department?>> GetById(int id)
        {
            return Task.Run(() =>
            {
                Result<Department?> result;
                try
                {
                    result = _repository.GetById(id).Result;
                }
                catch (Exception ex)
                {
                    result = Result<Department?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Insert the new Department into the repository.
        /// </summary>
        /// <param name="obj">Department to insert</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Save(Department obj)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    result = _repository.Save(obj).Result;
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Update the Department from the repository if exists.
        /// </summary>
        /// <param name="obj">Department to update</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Update(Department obj)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    result = _repository.Update(obj).Result;
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Delete the Department from the repository if exists
        /// </summary>
        /// <param name="id">int id of the department</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Delete(int id)
        {
            return Task.Run(() =>
            {
                Result result;
                try
                {
                    result = _repository.Delete(id).Result;
                }
                catch (Exception ex)
                {
                    result = Result.Failure(ex);
                }

                return result;
            });
        }
    }
}
