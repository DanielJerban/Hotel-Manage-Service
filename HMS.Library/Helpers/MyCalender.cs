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
                    saturday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(3 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(4 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(5 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(6 + (7 * index)).ToShortDateString();
                    break;

                // یک شنبه
                case 1:
                    saturday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(3 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(4 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(5 + (7 * index)).ToShortDateString();
                    break;

                // دو شنبه
                case 2:
                    saturday = persianDate.AddDays(-2 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(3 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(4 + (7 * index)).ToShortDateString();
                    break;

                // سه شنبه
                case 3:
                    saturday = persianDate.AddDays(-3 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(-2 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(3 + (7 * index)).ToShortDateString();
                    break;

                // چهار شنبه
                case 4:
                    saturday = persianDate.AddDays(-4 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(-3 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(-2 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    break;

                // پنج شنبه
                case 5:
                    saturday = persianDate.AddDays(-5 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(-4 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(-3 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(-2 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    break;

                // جمعه
                case 6:
                    saturday = persianDate.AddDays(-6 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(-5 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(-4 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(-3 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(-2 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(-1 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    break;

                // شنبه
                default:
                    saturday = persianDate.AddDays(0 + (7 * index)).ToShortDateString();
                    sunday = persianDate.AddDays(1 + (7 * index)).ToShortDateString();
                    monday = persianDate.AddDays(2 + (7 * index)).ToShortDateString();
                    teusday = persianDate.AddDays(3 + (7 * index)).ToShortDateString();
                    wendsday = persianDate.AddDays(4 + (7 * index)).ToShortDateString();
                    tuersday = persianDate.AddDays(5 + (7 * index)).ToShortDateString();
                    friday = persianDate.AddDays(6 + (7 * index)).ToShortDateString();
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

        public static string getYearFromHeader(int index = 0)
        {
            var weekHeader = weekHeaderDate(index);
            string date;

            switch (getCurrentDay())
            {
                // شنبه
                case 0:
                    date = weekHeader[0];
                    return date.Substring(0, 4);

                // یک شنبه
                case 1:
                    date = weekHeader[1];
                    return date.Substring(0, 4);

                // دو شنبه
                case 2:
                    date = weekHeader[2];
                    return date.Substring(0, 4);

                // سه شنبه
                case 3:
                    date = weekHeader[3];
                    return date.Substring(0, 4);

                // چهار شنبه
                case 4:
                    date = weekHeader[4];
                    return date.Substring(0, 4);

                // پنج شنبه
                case 5:
                    date = weekHeader[5];
                    return date.Substring(0, 4);

                // جمعه
                case 6:
                    date = weekHeader[6];
                    return date.Substring(0, 4);

                default:
                    date = weekHeader[0];
                    return date.Substring(0, 4);
            }
        }

        public static string getMonthFromHeader(int index = 0)
        {
            var weekHeader = weekHeaderDate(index);
            string date;

            switch (getCurrentDay())
            {
                // شنبه
                case 0:
                    date = weekHeader[0];
                    return date.Substring(5, 2);

                // یک شنبه
                case 1:
                    date = weekHeader[1];
                    return date.Substring(5, 2);

                // دو شنبه
                case 2:
                    date = weekHeader[2];
                    return date.Substring(5, 2);

                // سه شنبه
                case 3:
                    date = weekHeader[3];
                    return date.Substring(5, 2);

                // چهار شنبه
                case 4:
                    date = weekHeader[4];
                    return date.Substring(5, 2);

                // پنج شنبه
                case 5:
                    date = weekHeader[5];
                    return date.Substring(5, 2);

                // جمعه
                case 6:
                    date = weekHeader[6];
                    return date.Substring(5, 2);

                default:
                    date = weekHeader[0];
                    return date.Substring(5, 2);
            }
        }

        public static List<string> getDayFromHeader(int index = 0)
        {
            var weekHeader = weekHeaderDate(index);

            string saturday, sunday, monday, teusday, wendsday, tuersday, friday;

            saturday = weekHeader[0].Substring(8, 2);
            sunday = weekHeader[1].Substring(8, 2);
            monday = weekHeader[2].Substring(8, 2);
            teusday = weekHeader[3].Substring(8, 2);
            wendsday = weekHeader[4].Substring(8, 2);
            tuersday = weekHeader[5].Substring(8, 2);
            friday = weekHeader[6].Substring(8, 2);

            return new List<string>()
            {
                saturday,
                sunday,
                monday,
                teusday,
                wendsday,
                tuersday,
                friday
            };
        }

        /// <summary>
        /// Converts string month number to string month name
        /// </summary>
        public static string ConvertMonth(string monthInt)
        {
            string monthName;

            switch (monthInt)
            {
                case "۰۱":
                    monthName = "فروردین";
                    break;
                case "۰۲":
                    monthName = "اردیبهشت";
                    break;
                case "۰۳":
                    monthName = "خرداد";
                    break;
                case "۰۴":
                    monthName = "تیر";
                    break;
                case "۰۵":
                    monthName = "مرداد";
                    break;
                case "۰۶":
                    monthName = "شهریور";
                    break;
                case "۰۷":
                    monthName = "مهر";
                    break;
                case "۰۸":
                    monthName = "آبان";
                    break;
                case "۰۹":
                    monthName = "آذر";
                    break;
                case "۱۰":
                    monthName = "دی";
                    break;
                case "۱۱":
                    monthName = "بهمن";
                    break;
                case "۱۲":
                    monthName = "اسفند";
                    break;
                default:
                    monthName = "فروردین";
                    break;
            }

            return monthName;
        }
    }
}
