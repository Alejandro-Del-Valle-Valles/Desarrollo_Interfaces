using NotasRapidas.Model;
using System.Text.Json;

namespace NotasRapidas.Repository
{
    internal static class NotesJsonRepository
    {
        private static readonly string JsonDirectory = FileSystem.AppDataDirectory;
        private static readonly string JsonPath = Path.Combine(JsonDirectory, "notes.json");
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Insert into the Json a new note. If the file not exists, create it.
        /// </summary>
        /// <param name="note">Note to insert</param>
        /// <returns>Result Success, or Failure if something wet wrong.</returns>
        public static Result Insert(Note note)
        {
            Result result;
            try
            {
                Result<IEnumerable<Note>?> getAllResult = GetAll();
                if (getAllResult.IsSuccess)
                {
                    List <Note> notes = getAllResult.Data?.ToList() ?? new List<Note>();
                    notes.Add(note);
                    string json = JsonSerializer.Serialize(notes, Options);
                    File.WriteAllText(JsonPath, json);
                    result = Result.Success();
                }
                else throw getAllResult.Exception ?? new Exception("Ha ocurrido una excepción indeterminada al obtener los datos.");
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Return a Result with a IEnumerable with all Note from the Json if the file exists, and it has notes.
        /// </summary>
        /// <returns>Result Success with all Notes or Failure if something went wrong.</returns>
        public static Result<IEnumerable<Note>?> GetAll()
        {
            Result<IEnumerable<Note>?> result;
            try
            {
                if (!File.Exists(JsonPath)) return Result<IEnumerable<Note>?>.Success(new List<Note>());

                string json = File.ReadAllText(JsonPath);
                if (string.IsNullOrWhiteSpace(json)) return Result<IEnumerable<Note>?>.Success(new List<Note>());
                
                IEnumerable<Note>? notes = JsonSerializer.Deserialize<IEnumerable<Note>>(json, Options);
                result = Result<IEnumerable<Note>>.Success(notes);
            }
            catch (Exception ex)
            {
                result = Result<IEnumerable<Note>?>.Failure(ex);
            }
            return result;
        }
    }
}
