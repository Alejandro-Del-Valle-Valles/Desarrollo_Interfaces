using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;

namespace ModoConectado.Service
{
    class EmployeeService(ICrudRepository<Employee, int> _repository) : IService<Employee, int>
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
        /// Get all employees from the repository
        /// </summary>
        /// <returns>Task with Result with the Employees if Success when all goes great or Failure without Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetAll()
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetAll().Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Search and return the employee in the repository by his ID
        /// </summary>
        /// <param name="id">int id of the searched Department</param>
        /// <returns>Task with Result with the searched Employee if Success when all goes great or Failure without the Employee if something went wrong</returns>
        public Task<Result<Employee?>> GetById(int id)
        {
            return Task.Run(() =>
            {
                Result<Employee?> result;
                try
                {
                    result = _repository.GetById(id).Result;
                }
                catch (Exception ex)
                {
                    result = Result<Employee?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Insert the new Employee into the repository.
        /// </summary>
        /// <param name="obj">Employee to insert</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Save(Employee obj)
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
        /// Update the Employee from the repository if exists.
        /// </summary>
        /// <param name="obj">DEmployee to update</param>
        /// <returns>Task with Result Success if all goes great or Failure if something went wrong</returns>
        public Task<Result> Update(Employee obj)
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
        /// Delete the Employee from the repository if exists
        /// </summary>
        /// <param name="id">int id of the employee</param>
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
