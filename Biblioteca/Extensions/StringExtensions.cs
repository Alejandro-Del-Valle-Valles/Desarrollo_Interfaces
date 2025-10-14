namespace Biblioteca.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Trims and Capitalize the gave string. Only Capitalizes the first letter.
        /// If the string is only one character returns it in Upper Case.
        /// </summary>
        /// <param name="str">string to capitalize</param>
        /// <returns>string Capitalized</returns>
        public static string Capitalize(this string str)
        {
            string newString = string.Empty;
            if(!string.IsNullOrEmpty(str))
            {
                if (str.Trim().Length < 2) newString = str.ToUpper().Trim();
                else newString = $"{char.ToUpper(str.Trim()[0])}{str.Substring(1).ToLower().Trim()}";
            }
            return newString;
        }

        /// <summary>
        /// Trims and Capitalize each word of the string.
        /// If the string is only one character returns it in Upper Case.
        /// </summary>
        /// <param name="str">string to Capitalize</param>
        /// <returns>string Capitalized</returns>
        public static string CapitalizeAll(this string str)
        {
            string newString = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Trim().Length < 2) newString = str.ToUpper().Trim();
                else
                {
                    string[] words = str.Trim().Split(' ');
                    for(int i = 0; i < words.Length; i++) words[i] = words[i].Trim().Capitalize();
                    newString = string.Join(" ", words);
                }
            }
            return newString;
        }
    }
}
