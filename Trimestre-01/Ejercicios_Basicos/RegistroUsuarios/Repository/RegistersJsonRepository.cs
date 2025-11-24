using RegistroUsuarios.Interfaces;
using RegistroUsuarios.Model;
using System.Text.Json;

namespace RegistroUsuarios.Repository
{
    internal class RegistersJsonRepository : IGenericCrud<Register, string>
    {
        private static readonly string JsonDirectory = FileSystem.AppDataDirectory;
        private static readonly string JsonPath = Path.Combine(JsonDirectory, "registers.json");
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Add to the Json the new Register if not exists.
        /// </summary>
        /// <param name="obj">Register to save.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Insert(Register obj)
        {
            Result result = Result.Failure(new Exception($"Ya existe un usuario con el correo {obj.Email}"));
            try
            {
                List<Register> listToSave;
                if (File.Exists(JsonPath))
                {
                    var getResult = GetAll();
                    if (getResult.IsSuccess && getResult.Value != null) listToSave = getResult.Value.ToList();
                    else listToSave = new List<Register>();
                }
                else listToSave = new List<Register>();

                if(!listToSave.Contains(obj))
                {
                    listToSave.Add(obj);
                    string json = JsonSerializer.Serialize(listToSave, Options);
                    File.WriteAllText(JsonPath, json);
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Update the data of a Register if exists.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Update(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                if (!File.Exists(JsonPath)) throw new Exception("No existe un fcihero de donde se pueda actualizar el registro.");
                if (GetAll().IsSuccess)
                {
                    IEnumerable<Register>? registers = GetAll().Value;
                    if (registers != null)
                    {
                        int index = registers.ToList().FindIndex(r => r.Email == obj.Email);
                        if (index != -1)
                        {
                            registers.ToList()[index] = obj;
                            string json = JsonSerializer.Serialize(registers, Options);
                            File.WriteAllText(JsonPath, json);
                            result = Result.Success();
                        }
                        else throw new Exception("El registro que se trata de actualizar no existe.");
                    }
                    else throw new Exception("No existen registros.");
                }
                else throw new Exception("No se pudo acceder al fichero.");
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Delete a register form the json if exists.
        /// </summary>
        /// <param name="id">Email of the register to delete.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Delete(string id)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                if (!File.Exists(JsonPath)) throw new Exception("No existe un fcihero de donde se pueda eliminar el registro.");
                if (GetAll().IsSuccess)
                {
                    IEnumerable<Register>? registers = GetAll().Value;
                    if (registers != null)
                    {
                        int index = registers.ToList().FindIndex(r => r.Email == id);
                        if (index != -1)
                        {
                            registers.ToList().RemoveAt(index);
                            string json = JsonSerializer.Serialize(registers, Options);
                            File.WriteAllText(JsonPath, json);
                            result = Result.Success();
                        }
                        else throw new Exception("El registro que se trata de eliminar no existe.");
                    }
                    else throw new Exception("No existen registros.");
                }
                else throw new Exception("No se pudo acceder al fichero.");
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result<Register?> GetById(string id)
        {
            Result<Register?> result = Result<Register?>.Failure(new Exception("No existe un registro con el correo indicado."));
            try
            {
                IEnumerable<Register>? registers = GetAll().Value;
                Register? register = registers?.FirstOrDefault(r => r.Email.Equals(id));
                result = register == null
                    ? Result<Register?>.Failure(new Exception("No existe un registro con el correo indicado."))
                    : Result<Register?>.Success(register);
            }
            catch (Exception ex)
            {
                result = Result<Register?>.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// get all registers from the json.
        /// </summary>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result<IEnumerable<Register>?> GetAll()
        {
            Result<IEnumerable<Register>?> result = Result<IEnumerable<Register>?>.Failure(new Exception());
            try
            {
                string json = File.ReadAllText(JsonPath);
                IEnumerable<Register>? registers = JsonSerializer.Deserialize<IEnumerable<Register>>(json, Options);
                result = registers == null || !registers.Any()
                    ? Result<IEnumerable<Register>?>.Failure(new Exception("No existen registros.")) 
                    : Result<IEnumerable<Register>?>.Success(registers);
            }
            catch (Exception ex)
            {
                Result.Failure(ex);
            }
            return result;
        }
    }
}
