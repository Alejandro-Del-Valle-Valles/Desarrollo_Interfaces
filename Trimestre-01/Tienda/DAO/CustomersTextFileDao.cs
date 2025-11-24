using System.Text;
using Tienda.Extensions;
using Tienda.Model;
using Tienda.Interfaces;

namespace Tienda.DAO
{
    class CustomersTextFileDao : ICrud<Customer, string>
    {
        private const string FilePath = "../../../../Files/datosClientes.txt";

        /// <summary>
        /// Insert into the file the new Customer if not exist. Also order the data
        /// </summary>
        /// <param name="obj">Customer to insert</param>
        /// <returns>boo, true if it was added, false otherwise.</returns>
        public async Task<bool> Insert(Customer obj)
        {
            bool isAdded = false;
            List<Customer> customers = GetAll().Result.ToList();
            if (!customers.Contains(obj))
            {
                customers.Add(obj);
                File.WriteAllText(FilePath, OrderAndCreateStringData(customers));
                isAdded = true;
            }
            return isAdded;
        }

        /// <summary>
        /// Update a customer from the file and order the file.
        /// </summary>
        /// <param name="obj">Customer to update.</param>
        /// <returns>bool, if it was updated, false otherwise.</returns>
        public async Task<bool> Update(Customer obj)
        {
            bool isUpdated = false;
            List<Customer> customers = GetAll().Result.ToList();
            if (customers.Contains(obj))
            {
                customers.Remove(obj);
                customers.Add(obj);
                File.WriteAllText(FilePath, OrderAndCreateStringData(customers));
                isUpdated = true;
            }
            return isUpdated;
        }

        /// <summary>
        /// Delete a customer from the file if exists. It also ordered the file.
        /// </summary>
        /// <param name="id">string email of the customer</param>
        /// <returns>bool, true if it was deleted, false otherwise.</returns>
        public async Task<bool> Delete(string email)
        {
            bool isDeleted = false;
            List<Customer> customers = GetAll().Result.ToList();
            Customer? customerToDelete = customers.FirstOrDefault(c => c.Email == email.ToLower());
            if (customerToDelete != null)
            {
                isDeleted = customers.Remove(customerToDelete);
                File.WriteAllText(FilePath, OrderAndCreateStringData(customers));
            }
            return isDeleted;
        }

        /// <summary>
        /// Get the searched costumer by his email if exist or null if it doesn't.
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>Customer that can be null.</returns>
        public async Task<Customer?> GetById(string email) => GetAll().Result.FirstOrDefault(c => c.Email == email);

        /// <summary>
        /// Obtain all customers from the file.
        /// </summary>
        /// <returns>IEnumerable with all customers or empty if the file doesn't have any</returns>
        public async Task<IEnumerable<Customer>> GetAll()
        {
            IList<Customer> customers = new List<Customer>();
            using (var sr = new StreamReader(FilePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split('#');
                    customers.Add(new(data[0], data[1], data[2], data[3], data[4], bool.Parse(data[5])));
                }
            }
            return customers;
        }

        /// <summary>
        /// Order the list and create a string with the data. One line for each customer.
        /// </summary>
        /// <param name="customers">List of customers</param>
        /// <returns>string with the data</returns>
        private string OrderAndCreateStringData(List<Customer> customers)
        {
            customers.Order();
            StringBuilder sb = new();
            customers.ForEach(c => sb.AppendLine($"{c.Name}#{c.Surname}#{c.City}#{c.Email}#{c.Comment}#{c.IsVip}"));
            return sb.ToString();
        }
    }
}
