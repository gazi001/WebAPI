using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RM.Common.DotNetCode
{
    public class DateTimeHelper
    {
        public static string GetToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetToday(string format)
        {
            return DateTime.Now.ToString(format);
        }

        public static string GetDate(int i)
        {
            return DateTime.Now.AddDays((double)i).ToString("yyyy-MM-dd");
        }

        public static string GetNumberWeekDay(DateTime dt)
        {
            int y = dt.Year;
            int i = dt.Month;
            int d = dt.Day;
            if (i < 3)
            {
                i += 12;
                y--;
            }
            if (y % 400 == 0 || (y % 100 != 0 && y % 4 == 0))
            {
                d--;
            }
            else
            {
                d++;
            }
            return ((d + 2 * i + 3 * (i + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7).ToString();
        }

        public string GetChineseWeekDay(int y, int m, int d)
        {
            string[] weekstr = new string[]
			{
				"日",
				"一",
				"二",
				"三",
				"四",
				"五",
				"六"
			};
            if (m < 3)
            {
                m += 12;
                y--;
            }
            if (y % 400 == 0 || (y % 100 != 0 && y % 4 == 0))
            {
                d--;
            }
            else
            {
                d++;
            }
            return "星期" + weekstr[(d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7];
        }

        public static int GetDaysOfYear(int iYear)
        {
            return DateTimeHelper.IsRuYear(iYear) ? 366 : 365;
        }

        public static int GetDaysOfYear(DateTime dt)
        {
            return DateTimeHelper.IsRuYear(dt.Year) ? 366 : 365;
        }

        public static int GetDaysOfMonth(int iYear, int Month)
        {
            int days = 0;
            switch (Month)
            {
                case 1:
                    days = 31;
                    break;

                case 2:
                    days = (DateTimeHelper.IsRuYear(iYear) ? 29 : 28);
                    break;

                case 3:
                    days = 31;
                    break;

                case 4:
                    days = 30;
                    break;

                case 5:
                    days = 31;
                    break;

                case 6:
                    days = 30;
                    break;

                case 7:
                    days = 31;
                    break;

                case 8:
                    days = 31;
                    break;

                case 9:
                    days = 30;
                    break;

                case 10:
                    days = 31;
                    break;

                case 11:
                    days = 30;
                    break;

                case 12:
                    days = 31;
                    break;
            }
            return days;
        }

        public static int GetDaysOfMonth(DateTime dt)
        {
            int days = 0;
            int year = dt.Year;
            switch (dt.Month)
            {
                case 1:
                    days = 31;
                    break;

                case 2:
                    days = (DateTimeHelper.IsRuYear(year) ? 29 : 28);
                    break;

                case 3:
                    days = 31;
                    break;

                case 4:
                    days = 30;
                    break;

                case 5:
                    days = 31;
                    break;

                case 6:
                    days = 30;
                    break;

                case 7:
                    days = 31;
                    break;

                case 8:
                    days = 31;
                    break;

                case 9:
                    days = 30;
                    break;

                case 10:
                    days = 31;
                    break;

                case 11:
                    days = 30;
                    break;

                case 12:
                    days = 31;
                    break;
            }
            return days;
        }

        public static string GetWeekNameOfDay(DateTime dt)
        {
            string week = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;

                case DayOfWeek.Monday:
                    week = "星期一";
                    break;

                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;

                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;

                case DayOfWeek.Friday:
                    week = "星期五";
                    break;

                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
            }
            return week;
        }

        public static int GetWeekNumberOfDay(DateTime dt)
        {
            int week = 0;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = 7;
                    break;

                case DayOfWeek.Monday:
                    week = 1;
                    break;

                case DayOfWeek.Tuesday:
                    week = 2;
                    break;

                case DayOfWeek.Wednesday:
                    week = 3;
                    break;

                case DayOfWeek.Thursday:
                    week = 4;
                    break;

                case DayOfWeek.Friday:
                    week = 5;
                    break;

                case DayOfWeek.Saturday:
                    week = 6;
                    break;
            }
            return week;
        }

        public static int GetWeekAmount(int year)
        {
            DateTime end = new DateTime(year, 12, 31);
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static void WeekRange(int year, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            DateTime firstDay = new DateTime(year, 1, 1);
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);
            int dayDiff = -1 * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;
            firstDate = firstDay.AddDays((double)dayDiff).Date;
            lastDate = firstDay.AddDays((double)dayAdd).Date;
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays((double)addDays);
                lastDate = lastDate.AddDays((double)addDays);
            }
        }

        public static int DiffDays(DateTime dtfrm, DateTime dtto)
        {
            return (dtto.Date - dtfrm.Date).Days;
        }

        private static bool IsRuYear(int iYear)
        {
            return iYear % 400 == 0 || (iYear % 4 == 0 && iYear % 100 != 0);
        }

        public static DateTime ToDate(string strInput)
        {
            DateTime oDateTime;
            try
            {
                oDateTime = DateTime.Parse(strInput);
            }
            catch (Exception)
            {
                oDateTime = DateTime.Today;
            }
            return oDateTime;
        }

        public static string ToString(DateTime oDateTime, string strFormat)
        {
            string strDate;
            try
            {
                string text = strFormat.ToUpper();
                if (text != null)
                {
                    if (text == "SHORTDATE")
                    {
                        strDate = oDateTime.ToShortDateString();
                        goto IL_47;
                    }
                    if (text == "LONGDATE")
                    {
                        strDate = oDateTime.ToLongDateString();
                        goto IL_47;
                    }
                }
                strDate = oDateTime.ToString(strFormat);
            IL_47: ;
            }
            catch (Exception)
            {
                strDate = oDateTime.ToShortDateString();
            }
            return strDate;
        }

        public static int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }

        /// <summary>
        /// 判断时间是否在指定范围内
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="time_intervals"></param>
        /// <returns></returns>
       public static bool isLegalTime(DateTime dt, string time_intervals)
        {    //当前时间    
            int time_now = dt.Hour * 10000 + dt.Minute * 100 + dt.Second;
            //查看各个时间区间    
            string[] time_interval = time_intervals.Split(';');
            foreach (string time in time_interval)
            {
                //空数据直接跳过     
                if (string.IsNullOrWhiteSpace(time))
                { 
                    continue;
                }
                //一段时间格式：六个数字-六个数字     
                if (!Regex.IsMatch(time, "^[0-9]{6}-[0-9]{6}$"))
                {
                    Console.WriteLine("{0}： 错误的时间数据", time);
                }
                string timea = time.Substring(0, 6);
                string timeb = time.Substring(7, 6);
                int time_a, time_b;
                //尝试转化为整数     
                if (!int.TryParse(timea, out time_a))
                {
                    Console.WriteLine("{0}： 转化为整数失败", timea);
                }
                if (!int.TryParse(timeb, out time_b))
                {
                    Console.WriteLine("{0}： 转化为整数失败", timeb);
                }
                //如果当前时间不小于初始时间，不大于结束时间，返回true     
                if (time_a <= time_now && time_now <= time_b)
                {
                    return true;
                }
            }
            //不在任何一个区间范围内，返回false    
            return false;
        }

        /// <summary>
        /// 将14:00-20:00格式转换成140000-200000
        /// </summary>
        /// <param name="time_intervals"></param>
        /// <returns></returns>
       public static string TimeIntervalsConvert(string time_intervals)
       {
           string[] b = time_intervals.Replace(":","").Split('-');
           return b[0] + "00" + "-" + b[1] + "00";
       }
    }
}