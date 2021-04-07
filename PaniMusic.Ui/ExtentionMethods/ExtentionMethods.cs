using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PaniMusic.Ui.ExtentionMethods
{
    public static class ExtentionMethods
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1:00}/{2:00}",
                                 persianCalendar.GetYear(dateTime),
                                persianCalendar.GetMonth(dateTime),
                                persianCalendar.GetDayOfMonth(dateTime));
        }
    }
}
