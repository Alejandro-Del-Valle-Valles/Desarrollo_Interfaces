namespace RegistroUsuarios.Extensions
{
    internal static class StringExtensions
    {
        public static string Capitalize(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                text = text.Trim().First().ToString().ToUpper() + text.Substring(1);
            
            return text;
        }
    }
}
