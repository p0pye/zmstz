using System;
using System.Globalization;

namespace zmstz
{
	/*
	 * функции для внешнего использования
	 */
	public class Utils
	{
		public static double parse_double (string value)
		{
			double result;
			try {
				result = double.Parse (value, CultureInfo.InvariantCulture);
			} catch (ArgumentNullException) {
				result = 0;
			}
			return result;
		}

		public static int parse_int (string value)
		{
			int result;
			try {
				result = int.Parse (value);
			} catch (ArgumentNullException) {
				result = 0;
			}
			return result;
		}

		public static long parse_long (string value)
		{
			long result;
			try {
				result = long.Parse (value);
			} catch (ArgumentNullException) {
				result = 0;
			}
			return result;
		}

		public Utils ()
		{
		}
	}
}

