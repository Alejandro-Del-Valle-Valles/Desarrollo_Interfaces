using Tienda.Interfaces;
using Tienda.Model;
using Tienda.Exceptions;
using Tienda.Enums;

namespace Tienda.Service
{
    class CustomerService(ICrud<Customer, string> customerCrud) : IGenericService<Customer, string>
    {
        /// <summary>
        /// Add into the repository (DB, File...) the new customer if not exists.
        /// </summary>
        /// <param name="customer">Customer to add.</param>
        /// <returns>bool, true if it was added, false otherwise.</returns>
        public async Task<ServiceResult> Create(Customer customer)
        {
            ServiceResult sr;
            try
            {
                if (await customerCrud.Insert(customer))
                {
                    sr = ServiceResult.Success();
                }
                else sr = ServiceResult.Failure(ServiceErrorType.UnknowException, "No se pudo guardar el cliente probablemente porque ya existe.");
            }
            catch (DirectoryNotFoundException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.DirectoryNotFoundException, ex.Message);
            }
            catch (IOException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.IOException, ex.Message);
            }
            catch (ArgumentException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.ArgumentException, ex.Message);
            }
            catch (Exception ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.UnknowException, ex.Message);
            }

            return sr;
        }

        /// <summary>
        /// Modify a customer from the repository (DB, File...) if exist.
        /// </summary>
        /// <param name="obj">Customer to modify.</param>
        /// <returns>boo, true if it was modified, false otherwise.</returns>
        public async Task<ServiceResult> Modify(Customer obj)
        {
            ServiceResult sr;
            try
            {
                if (await customerCrud.Update(obj))
                {
                    sr = ServiceResult.Success();
                }
                else sr = ServiceResult.Failure(ServiceErrorType.UnknowException, "No se pudo guardar el cliente probablemente porque no existe.");
            }
            catch (DirectoryNotFoundException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.DirectoryNotFoundException, ex.Message);
            }
            catch (IOException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.IOException, ex.Message);
            }
            catch (ArgumentException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.ArgumentException, ex.Message);
            }
            catch (Exception ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.UnknowException, ex.Message);
            }

            return sr;
        }

        /// <summary>
        /// Delete a customer from the Repository (DB, File, ...) if exists.
        /// </summary>
        /// <param name="email">string email of the Customer to delete.</param>
        /// <returns>bool, true if it was deleted, false otherwise.</returns>
        public async Task<ServiceResult> Delete(string email)
        {
            ServiceResult sr;
            try
            {
                if (await customerCrud.Delete(email))
                {
                    sr = ServiceResult.Success();
                }
                else sr = ServiceResult.Failure(ServiceErrorType.UnknowException, $"No se pudo eliminar el cliente probablemente porque no existe.{email}");
            }
            catch (DirectoryNotFoundException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.DirectoryNotFoundException, ex.Message);
            }
            catch (IOException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.IOException, ex.Message);
            }
            catch (ArgumentException ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.ArgumentException, ex.Message);
            }
            catch (Exception ex)
            {
                sr = ServiceResult.Failure(ServiceErrorType.UnknowException, ex.Message);
            }

            return sr;
        }

        /// <summary>
        /// Obtain the searched customer by his email if exists.
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>Customer or null if not exists.</returns>
        public async Task<ServiceResult<Customer>> GetById(string email)
        {
            ServiceResult<Customer> sr;
            try
            {
                sr = ServiceResult<Customer>.Success(customerCrud.GetById(email).Result);
            }
            catch (InvalidValueException ex)
            {
                sr = ServiceResult<Customer>.Failure(ServiceErrorType.InvalidValueException, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                sr = ServiceResult<Customer>.Failure(ServiceErrorType.DirectoryNotFoundException, ex.Message);
            }
            catch (IOException ex)
            {
                sr = ServiceResult<Customer>.Failure(ServiceErrorType.IOException, ex.Message);
            }
            catch (ArgumentException ex)
            {
                sr = ServiceResult<Customer>.Failure(ServiceErrorType.ArgumentException, ex.Message);
            }
            catch (Exception ex)
            {
                sr = ServiceResult<Customer>.Failure(ServiceErrorType.UnknowException, ex.Message);
            }

            return sr;
        }

        /// <summary>
        /// Obtain all customer from the repository (DB, File...) if there's any in it.
        /// </summary>
        /// <returns>IEnumerable of costumers. Can be empty.</returns>
        public async Task<ServiceResult<IEnumerable<Customer>>> GetAll()
        {
            ServiceResult<IEnumerable<Customer>> sr;
            try
            {
                IEnumerable<Customer> customers = customerCrud.GetAll().Result;
                sr = ServiceResult<IEnumerable<Customer>>.Success(customers);
            }
            catch (InvalidValueException ex)
            {
                sr = ServiceResult<IEnumerable<Customer>>.Failure(ServiceErrorType.InvalidValueException, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                sr = ServiceResult<IEnumerable<Customer>>.Failure(ServiceErrorType.DirectoryNotFoundException, ex.Message);
            }
            catch (IOException ex)
            {
                sr = ServiceResult<IEnumerable<Customer>>.Failure(ServiceErrorType.IOException, ex.Message);
            }
            catch (ArgumentException ex)
            {
                sr = ServiceResult<IEnumerable<Customer>>.Failure(ServiceErrorType.ArgumentException, ex.Message);
            }
            catch (Exception ex)
            {
                sr = ServiceResult<IEnumerable<Customer>>.Failure(ServiceErrorType.UnknowException, ex.Message);
            }

            return sr;
        }
    }
}
