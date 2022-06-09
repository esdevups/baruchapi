using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Datetime
{
    public static class Persiandate
    {
		public static string PersianDate(DateTime DateTime1)
		{
            PersianCalendar PersianCalendar1 = new PersianCalendar();
			return string.Format(@"{0}/{1}/{2}",
						 PersianCalendar1.GetYear(DateTime1),
						 PersianCalendar1.GetMonth(DateTime1),
						 PersianCalendar1.GetDayOfMonth(DateTime1));
		}
	}
}
