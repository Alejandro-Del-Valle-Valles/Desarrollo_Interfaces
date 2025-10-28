using Tienda.Exceptions;
using Tienda.Model;
using Tienda.Interfaces;

namespace Tienda.DAO
{
    class CustomersTextFileDao : ICrud<Customer, string>
    {
        public bool Insert(Customer obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
