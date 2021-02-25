using System;

namespace Discord.Core.Utils
{
    /// <summary>
    /// <see cref="DateTime"/> extention functions.
    /// </summary>
    public static class DateExtentions
    {
        /// <summary>
        /// Formats date like: | 
        ///     Today, 10:42 | 
        ///     Yesterday, 19:15, | 
        ///     25.02.2021
        /// </summary>
        /// <param name="date">This <see cref="DateTime"/></param>
        /// <returns>Formatted string.</returns>
        public static string Formatted(this DateTime date)
        {
            var hourMinute = date.ToString("t"); // hh:mm
            var dayMonthYear = date.ToString("d"); // dd.mm.yyyy

            if (date.Day == DateTime.Now.Day)
                return $"Сегодня, в {hourMinute}";
            else if (date.Day == DateTime.Now.Day - 1)
                return $"Вчера, в {hourMinute}";
            else return dayMonthYear;
        }
    }
}
