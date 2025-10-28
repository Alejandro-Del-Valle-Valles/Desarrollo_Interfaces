namespace Tienda.Extensions
{
    static class StringExtensions
    {
        /// <summary>
        /// Capitalize the first char of an array.
        /// </summary>
        /// <param name="text">string text to capitalize.</param>
        /// <returns>string capitalized.</returns>
        public static string Capitalize(this string text)
        {
            string capitalized = text;
            if (!string.IsNullOrEmpty(text.Trim()))
            {
                capitalized = text.Trim().First().ToString().ToUpper();
                if (text.Length > 1)  capitalized += text.Substring(1).ToLower();
            }
            return capitalized;
        }

        /// <summary>
        /// Capitalize each word of an array.
        /// </summary>
        /// <param name="text">string to capitalize.</param>
        /// <returns>string capitalized.</returns>
        public static string CapitalizeAll(this string text) => string.Join(" ",
            text.Split(' ')
            .Select(Capitalize)
            .ToArray());
    }
}
