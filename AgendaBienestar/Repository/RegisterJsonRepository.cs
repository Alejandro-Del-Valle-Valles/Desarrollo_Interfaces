using AgendaBienestar.Model;
using AgendaBienestar.Interfaces;

namespace AgendaBienestar.Repository
{
    internal class RegisterJsonRepository : IGenericCrud<Register, Guid>
    {
        public bool Insert(Register obj)
        {
            bool isInserted = false;

            return isInserted;
        }

        public bool Update(Register obj)
        {
            bool isUpdated = false;

            return isUpdated;
        }

        public bool Delete(Guid obj)
        {
            bool isDeleted = false;

            return isDeleted;
        }

        public Register? GetById(Guid id)
        {
            Register? register = null;

            return register;
        }

        public IEnumerable<Register> GetAll()
        {
            IEnumerable<Register> registers = new List<Register>();

            return registers;
        }
    }
}
