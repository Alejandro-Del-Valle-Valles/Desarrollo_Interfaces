using Tienda.Interfaces;
using Tienda.Model;
using Tienda.Exceptions;

namespace Tienda.Service
{
    class CustomerService(ICrud<Customer, string> customerCrud) : IGenericService<Customer, string>
    {
        public bool Create(Customer obj)
        {
            throw new NotImplementedException();
        }

        public bool Modify(Customer obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetALl()
        {
            throw new NotImplementedException();
        }
    }
}
