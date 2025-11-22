namespace AgendaBienestar.Extensions
{
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// Checks if a DateTime object falls within the immediately preceding week (Last Week).
        /// Assumes Monday is the start of the week.
        /// </summary>
        /// <param name="targetDate">The date to check from a list of objects.</param>
        /// <returns>True if the date is in the last week, otherwise false.</returns>
        public static bool IsInLastWeek(this DateTime targetDate)
        {
            DateTime today = DateTime.Today;
            DateTime normalizedTargetDate = targetDate.Date;

            int daysSinceStartOfWeek = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime currentWeekStart = today.AddDays(-daysSinceStartOfWeek);

            DateTime lastWeekStart = currentWeekStart.AddDays(-7);
            DateTime lastWeekEnd = currentWeekStart.AddDays(-1);

            return (normalizedTargetDate >= lastWeekStart && normalizedTargetDate <= lastWeekEnd);
        }
    }
}
