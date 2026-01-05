using System.Text;

namespace ModoConectado.Extenisons
{
    static class StringExtensions
    {

        /// <summary>
        /// Capitalizes the first letter of a string and ensures the rest is lowercase.
        /// </summary>
        /// <param name="text">The string to process.</param>
        /// <returns>A string with the first letter capitalized.</returns>
        public static string Capitalize(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string cleaned = text.Trim();

            if (cleaned.Length == 1)
                return cleaned.ToUpper();

            return char.ToUpper(cleaned[0]) + cleaned.Substring(1).ToLower();
        }

        /// <summary>
        /// Capitalizes the first letter of each word of the string and ensures the rest is lowercase.
        /// </summary>
        /// <param name="text">The string to process.</param>
        /// <returns>string with the first letter of each word capitalized.</returns>
        public static string CapitalizeAll(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string cleaned = text.Trim();
            string[] words = cleaned.Split(' ');

            if (words.Length == 1)
                return text.Capitalize();

            StringBuilder sb = new();
            foreach (string word in words)
                sb.AppendJoin(' ', word.Capitalize());

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Capitalizes the first letter of each word of and array of strings and ensures the rest is lowercase.
        /// </summary>
        /// <param name="text">The string array to process.</param>
        /// <returns>string with the first letter of each word capitalized.</returns>
        public static string CapitalizeAll(this string[] text)
        {
            string[] cleaned = text.Where(word => word != " ").ToArray();
            if (cleaned.Length == 1)
                return text[0].Capitalize();

            StringBuilder sb = new();
            foreach (string word in cleaned)
                sb.AppendJoin(' ', word.Capitalize());

            return sb.ToString().Trim();
        }

    }
}
