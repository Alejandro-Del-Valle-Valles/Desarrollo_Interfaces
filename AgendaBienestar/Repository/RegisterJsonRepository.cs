using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;
using System.Linq;
using System.Text.Json;

namespace AgendaBienestar.Repository
{
    internal class RegisterJsonRepository : IGenericCrud<Register, Guid>
    {
        private const string JsonDirectory = "../../../Resources/Files";
        private const string JsonPath = JsonDirectory + "/registers.json";

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Insert into the Json the new Register. Also create the file if not exists.
        /// </summary>
        /// <param name="obj">Register to insert.</param>
        /// <returns>Result Success or Failure if something went wrong.</returns>
        public Result Insert(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                Directory.CreateDirectory(JsonPath);
                if (!File.Exists(JsonPath)) File.Create(JsonPath);
                if (GetAll().IsSuccess)
                {
                    IEnumerable<Register>? registers = GetAll().Data;
                    if (registers != null)
                    {
                        registers.Append(obj);
                        string json = JsonSerializer.Serialize(registers, Options);
                        File.WriteAllText(JsonPath, json);
                    }
                    else
                    {
                        registers = new List<Register>();
                        registers.Append(obj);
                        string json = JsonSerializer.Serialize(registers, Options);
                        File.WriteAllText(JsonPath, json);
                    }
                    result = Result.Success();
                }
                else throw new Exception("No se pudo acceder al fichero.");
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Update the data of a register if exists.
        /// </summary>
        /// <param name="obj">Register to update</param>
        /// <returns>Result success or Failure if something went wrong.</returns>
        public Result Update(Register obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                if (!File.Exists(JsonPath)) throw new Exception("No existe un fcihero de donde se pueda actualizar el registro.");
                if (GetAll().IsSuccess)
                {
                    IEnumerable<Register>? registers = GetAll().Data;
                    if (registers != null)
                    {
                        int index = registers.ToList().FindIndex(r => r.Id == obj.Id);
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
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Delete a Register if exists.
        /// </summary>
        /// <param name="obj">Guid (Id) of the Register to delete.</param>
        /// <returns>Result success or Failure if something went wrong.</returns>
        public Result Delete(Guid obj)
        {
            Result result = Result.Failure(new Exception());
            try
            {
                if (!File.Exists(JsonPath)) throw new Exception("No existe un fcihero de donde se pueda eliminar el registro.");
                if (GetAll().IsSuccess)
                {
                    IEnumerable<Register>? registers = GetAll().Data;
                    if (registers != null)
                    {
                        int index = registers.ToList().FindIndex(r => r.Id == obj);
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
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Get a Register from the Json by his ID.
        /// </summary>
        /// <param name="id">Guid id of the searched Register.</param>
        /// <returns>Result success with the data or Failure with null data if something went wrong.</returns>
        public Result<Register?> GetById(Guid id)
        {
            Result<Register?> result = Result<Register?>.Failure(null, new Exception());
            try
            {
                string json = File.ReadAllText(JsonPath);
                IEnumerable<Register>? deserialized = JsonSerializer.Deserialize<IEnumerable<Register>>(json, Options);
                Register? register = deserialized?.ToList().Find(r => r.Id == id) ?? null;
                result = register != null
                    ? Result<Register>.Success(register)
                    : Result<Register>.Failure(null, new Exception("El resgitros buscado no existe."));
            }
            catch (Exception ex)
            {
                result = Result<Register?>.Failure(null, ex);
            }
            return result;
        }

        public Result<IEnumerable<Register>?> GetAll()
        {
            Result<IEnumerable<Register>?> registers = Result<IEnumerable<Register>?>.Failure(null, new Exception());
            try
            {
                string json = File.ReadAllText(JsonPath);
                IEnumerable<Register>? deserialized = JsonSerializer.Deserialize<IEnumerable<Register>>(json, Options);
                registers = Result<IEnumerable<Register>?>.Success(deserialized);
            }
            catch (Exception ex)
            {
                registers = Result<IEnumerable<Register>?>.Failure(null, ex);
            }
            return registers;
        }
    }
}
