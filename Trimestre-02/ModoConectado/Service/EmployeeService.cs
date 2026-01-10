using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;

namespace ModoConectado.Service
{
    class EmployeeService(ICrudEmployeeRepository _repository) : IServiceEmployee
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
        /// Get all employees from the specified department
        /// </summary>
        /// <param name="id">int ID of the department</param>
        /// <returns>Task with Result with the Employees if Success when all goes great or Failure without Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetAllByDepartmentId(int id)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetAllByDepartmentId(id).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get from the repository the employees searched by his surname.
        /// </summary>
        /// <param name="surname">float commission</param>
        /// <returns>Task with Result with the searched Employees if Success when all goes great or Failure without the Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetBySurname(string surname)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetBySurname(surname).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get from the repository the employees searched by his craft.
        /// </summary>
        /// <param name="craft">float commission</param>
        /// <returns>Task with Result with the searched Employees if Success when all goes great or Failure without the Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetByCraft(string craft)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetByCraft(craft).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get from the repository the employees searched by his salary.
        /// </summary>
        /// <param name="salary">float commission</param>
        /// <returns>Task with Result with the searched Employees if Success when all goes great or Failure without the Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetBySalary(float salary)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetBySalary(salary).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get from the repository the employees searched by his commission.
        /// </summary>
        /// <param name="commission">float commission</param>
        /// <returns>Task with Result with the searched Employees if Success when all goes great or Failure without the Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetByCommission(float commission)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetByCommission(commission).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Get from the repository the employees searched by his registration date.
        /// </summary>
        /// <param name="date">string with the date</param>
        /// <returns>Task with Result with the searched Employees if Success when all goes great or Failure without the Employees if something went wrong</returns>
        public Task<Result<IEnumerable<Employee>?>> GetByRegistrationDate(string date)
        {
            return Task.Run(() =>
            {
                Result<IEnumerable<Employee>?> result;
                try
                {
                    result = _repository.GetByRegistrationDate(date).Result;
                }
                catch (Exception ex)
                {
                    result = Result<IEnumerable<Employee>?>.Failure(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// Search and return the employee from the repository by his ID
        /// </summary>
        /// <param name="id">int id of the searched Employee</param>
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
