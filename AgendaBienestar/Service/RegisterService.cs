using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;

namespace AgendaBienestar.Service
{
    internal class RegisterService(IGenericCrud<Register, Guid> repositoryCrud) : IGenericService<Register, Guid>
    {
        //TODO: Crear una clase que encapsule el objeto a devolver y si fue devuelto o no con un booleano.
        public bool Create(Register obj)
        {
            bool isCreated = false;
            try
            {
                isCreated = repositoryCrud.Insert(obj);
            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {
                
            }
            return isCreated;
        }

        public bool Modify(Register obj)
        {
            bool isModified = false;
            try
            {
                isModified = repositoryCrud.Update(obj);
            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return isModified;
        }

        public bool Delete(Guid id)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = repositoryCrud.Delete(id);
            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return isDeleted;
        }

        public Register? GetById(Guid id)
        {
            Register? register = null;
            try
            {
                register = repositoryCrud.GetById(id);
            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return register;
        }

        public IEnumerable<Register> GetAll()
        {
            IEnumerable<Register> registers = new List<Register>();
            try
            {
                registers = repositoryCrud.GetAll();
            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return registers;
        }
    }
}
