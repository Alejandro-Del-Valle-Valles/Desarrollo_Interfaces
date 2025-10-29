using Tienda.Exceptions;
using Tienda.Model;
using Tienda.Interfaces;

namespace Tienda.DAO
{
    class CustomersTextFileDao : ICrud<Customer, string>
    {
        private const string FilePath = "../../../Files/datosClientes.txt";

        /// <summary>
        /// Insert into the file the new Customer if not exist.
        /// </summary>
        /// <param name="obj">Customer to insert</param>
        /// <returns>boo, true if it was added, false otherwise.</returns>
        public bool Insert(Customer obj)
        {
            bool isAdded = false;
            IList<Customer> customers = GetAll().ToList();
            if (!customers.Contains(obj))
            {
                string formatted = $"{obj.Name}#{obj.Surname}#{obj.City}#{obj.Email}#{obj.Comment}#{obj.IsVip}";
                File.AppendAllText(FilePath, formatted);
                isAdded = true;
            }
            return isAdded;
        }

        /// <summary>
        /// Update a customer from the file and order the file.
        /// </summary>
        /// <param name="obj">Customer to update.</param>
        /// <returns>bool, if it was updated, false otherwise.</returns>
        public bool Update(Customer obj)
        {
            bool isUpdated = false;
            List<Customer> customers = GetAll().ToList();
            if (!customers.Contains(obj))
            {
                customers.Add(obj);
                customers.Order();
                string data = string.Empty;
                customers.ForEach(c => data += $"{c.Name}#{c.Surname}#{c.City}#{c.Email}#{c.Comment}#{c.IsVip}\n");
                File.WriteAllText(FilePath, data);
                isUpdated = true;
            }
            return isUpdated;
        }

        /// <summary>
        /// Delete a customer from the file if exist. It also ordered the file.
        /// </summary>
        /// <param name="id">string email of the customer</param>
        /// <returns>bool, true if it was deleted, false otherwise.</returns>
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the searched costumer by his email if exist or null if it doesn't.
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>Customer that can be null.</returns>
        public Customer? GetById(string email) => GetAll().FirstOrDefault(c => c.Email == email);

        public IEnumerable<Customer> GetAll()
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
    }
}
