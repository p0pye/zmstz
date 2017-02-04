using System;
using System.Xml;
using System.Globalization;

namespace zmstz
{
	/*
	 * счет
	 */
	public class Bill 
	{
		//путь для чтения
		public const string BILL_PATH = "/ZL_LIST/SCHET/";
		//обязательные поля
		private int code;
		private string code_mo;
		private int year;
		private int month;
		private string bill_no;
		private DateTime bill_date;
		private double sum;
		//условно-обязательные
		private string payer;
		private string comment;
		private double sum_accepted;
		private double sanct_mek;
		private double sanct_mee;
		private double sanct_ekmp;

		//свойства
		public int Code
		{
			get { return code; }
		}
		public string Code_mo
		{
			get { return code_mo; }
		}
		public int Year
		{
			get { return year; }
		}
		public int Month
		{
			get { return Month; }
		}
		public string Bill_no
		{
			get { return bill_no; }
		}
		public DateTime Bill_date
		{
			get { return bill_date; }
		}
		public double Sum
		{
			get { return sum; }
		}
		public string Payer
		{
			get { return payer; }
		}
		public string Comment
		{
			get { return comment; }
		}
		public double Sum_accepted
		{
			get { return sum_accepted; }
		}
		public double Sanct_mek
		{
			get { return sanct_mek; }
		}
		public double Sanct_mee
		{
			get { return sanct_mee; }
		}
		public double Sanct_ekmp
		{
			get { return sanct_ekmp; }
		}

		public Bill (XmlDocument xml) 
		{
			code = Utils.parse_int (xml.DocumentElement.SelectSingleNode (BILL_PATH+"CODE")?.InnerText);
			code_mo = xml.DocumentElement.SelectSingleNode (BILL_PATH+"CODE_MO")?.InnerText;
			year = Utils.parse_int (xml.DocumentElement.SelectSingleNode (BILL_PATH+"YEAR")?.InnerText);
			month = Utils.parse_int (xml.DocumentElement.SelectSingleNode (BILL_PATH+"MONTH")?.InnerText);
			bill_no = xml.DocumentElement.SelectSingleNode (BILL_PATH+"NSCHET")?.InnerText;
			string buf = xml.DocumentElement.SelectSingleNode (BILL_PATH+"DSCHET")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out bill_date);
			sum = Utils.parse_double (xml.DocumentElement.SelectSingleNode (BILL_PATH+"SUMMAV")?.InnerText);

			payer = xml.DocumentElement.SelectSingleNode (BILL_PATH+"PLAT")?.InnerText;
			comment = xml.DocumentElement.SelectSingleNode (BILL_PATH+"COMENTS")?.InnerText;
			sum_accepted = Utils.parse_double (xml.DocumentElement.SelectSingleNode (BILL_PATH + "SUMMAP")?.InnerText);
			sanct_mek = Utils.parse_double (xml.DocumentElement.SelectSingleNode (BILL_PATH + "SANK_MEK")?.InnerText);
			sanct_mee = Utils.parse_double (xml.DocumentElement.SelectSingleNode (BILL_PATH + "SANK_MEE")?.InnerText);
			sanct_ekmp = Utils.parse_double (xml.DocumentElement.SelectSingleNode (BILL_PATH + "SANK_EKMP")?.InnerText);
		}

		//вывод информации о счете
		public void display_bill_summary () 
		{
			Console.WriteLine ("Bill summary info:\n============================================================");
			Console.WriteLine ("\tCode: " + code);
			Console.WriteLine ("\tCode MO: " + code_mo);
			Console.WriteLine ("\tReporting year: " + year);
			Console.WriteLine ("\tReporting month: " + month);
			Console.WriteLine ("\tBill no: " + bill_no);
			Console.WriteLine ("\tDate: {0:dd.MM.yyyy}",bill_date);
			Console.WriteLine ("\tSum: {0:0.00}\n", sum);
		}

		public string generate_sql_insert_query () {
            //Если запись уже существует - пропускаем
            string result = string.Format(
                @"if not exists (select 1 from Bill where CODE='{0}')
                BEGIN
                    insert into Bill values ({0},'{1}',{2},{3},'{4}','{5}',{6},{7},'{8}',{9},{10},{11},{12})
                END; ",
				code, code_mo, year, month, bill_no, bill_date.ToString ("dd.MM.yyyy"), sum.ToString (CultureInfo.InvariantCulture), payer, comment,
				sum_accepted.ToString (CultureInfo.InvariantCulture), sanct_mek.ToString (CultureInfo.InvariantCulture), 
                sanct_mee.ToString (CultureInfo.InvariantCulture), sanct_ekmp.ToString (CultureInfo.InvariantCulture));
			return result;
		}
	}
}

