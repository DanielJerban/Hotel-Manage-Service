using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.PersianDateTime;

namespace HMS.Library.Helpers
{
    public static class MyCalender
    {
        private static PersianDateTime persianDate = PersianDateTime.Now;
        public static string getMonthName()
        {
            return persianDate.MonthName;
        }

        public static string getYear()
        {
            return persianDate.Year.ToString();
        }

        public static int getCurrentDay()
        {
            return Convert.ToInt32(persianDate.PersianDayOfWeek);
        }

        /// <summary>
        /// Get the date of the current week
        /// </summary>
        /// <param name="index">the index of the week</param>
        /// <returns></returns>
        public static List<string> weekHeaderDate(int index = 0)
        {
            int currentDay = getCurrentDay();
            string saturday, sunday, monday, teusday, wendsday, tuersday, friday;

            switch (currentDay)
            {
                // شنبه
                case 0:
                    saturday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(3 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(4 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(5 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(6 + (7 * index)).Day.ToString();
                    break;

                // یک شنبه
                case 1:
                    saturday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(3 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(4 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(5 + (7 * index)).Day.ToString();
                    break;

                // دو شنبه
                case 2:
                    saturday = persianDate.AddDays(-2 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(3 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(4 + (7 * index)).Day.ToString();
                    break;

                // سه شنبه
                case 3:
                    saturday = persianDate.AddDays(-3 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(-2 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(3 + (7 * index)).Day.ToString();
                    break;

                // چهار شنبه
                case 4:
                    saturday = persianDate.AddDays(-4 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(-3 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(-2 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    break;

                // پنج شنبه
                case 5:
                    saturday = persianDate.AddDays(-5 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(-4 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(-3 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(-2 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    break;

                // جمعه
                case 6:
                    saturday = persianDate.AddDays(-6 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(-5 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(-4 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(-3 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(-2 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(-1 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    break;

                // شنبه
                default:
                    saturday = persianDate.AddDays(0 + (7 * index)).Day.ToString();
                    sunday = persianDate.AddDays(1 + (7 * index)).Day.ToString();
                    monday = persianDate.AddDays(2 + (7 * index)).Day.ToString();
                    teusday = persianDate.AddDays(3 + (7 * index)).Day.ToString();
                    wendsday = persianDate.AddDays(4 + (7 * index)).Day.ToString();
                    tuersday = persianDate.AddDays(5 + (7 * index)).Day.ToString();
                    friday = persianDate.AddDays(6 + (7 * index)).Day.ToString();
                    break;
            }

            List<string> headers = new List<string>
            {
                saturday,
                sunday,
                monday,
                teusday,
                wendsday,
                tuersday,
                friday
            };

            return headers;
        }
    }
}
