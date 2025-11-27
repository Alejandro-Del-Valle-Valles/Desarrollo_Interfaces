using System.Globalization;
using Tareas.Model;

namespace Tareas.Service
{
    internal static class RepositoryService
    {
        private static readonly string TaskDirectory = FileSystem.AppDataDirectory;
        private static readonly string TaskPath = Path.Combine(TaskDirectory, "taskMAUI.txt");
        private static readonly char SEPARATOR = '#';

        public static Result SaveTask(Exercise exercise)
        {
            Result result = Result.Failure(new Exception("No se ha podido gaurdar la tarea"));
            try
            {
                if (!File.Exists(TaskPath)) File.Create(TaskPath);
                using StreamWriter sw = new(TaskPath);
                sw.WriteLine(exercise.ToString());
                result = Result.Success();
            }
            catch (Exception ex)
            {
                result = Result.Failure(ex);
            }
            return result;
        }

        public static Result<IEnumerable<Exercise>?> GetAllTask()
        {
            Result<IEnumerable<Exercise>?> result = Result<IEnumerable<Exercise>?>.Failure(new Exception("No se ha podido gaurdar la tarea"));
            try
            {
                if (!File.Exists(TaskPath)) throw new Exception("No existe el fichero de tareas.");
                IList<Exercise> exercises = new List<Exercise>();
                using StreamReader sr = new(TaskPath);
                string? line;
                string[] data;
                Exercise exercise;
                while ((line = sr.ReadLine()) != null)
                {
                    data = line.Split(SEPARATOR);
                    if (data.Length != 3) throw new Exception("Error de formato en el fichero.");
                    exercise = new(data[0], data[1], DateParse(data[2]));
                    exercises.Add(exercise);
                }
                result = Result<IEnumerable<Exercise>>.Success(exercises);
            }
            catch (Exception ex)
            {
                result = Result<IEnumerable<Exercise>?>.Failure(ex);
            }
            return result;
        }

        /// <summary>
        /// Parse to DateTime from string with the format dd/MM/yyyy
        /// </summary>
        /// <param name="data">string with the date</param>
        /// <returns>DateTime parsed or now if fails.</returns>
        private static DateTime DateParse(string data)
        {
            DateTime date;
            bool success = DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            return success
                ? date
                : DateTime.Now;
        }
    }
}
