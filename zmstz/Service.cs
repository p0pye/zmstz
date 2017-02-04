using System;
using System.Xml;
using System.Globalization;

namespace zmstz
{
	/*
	 * Услуга
	 */
	public class Service
	{
		//обязательные поля
		private string id;
		private string lpu;
		private int profile;
		private int child;
		private DateTime date_start;
		private DateTime date_end;
		private string diagnosis;
		private string code;
		private double count;
		private double sum;
		private int md_specialisztion;
		private string code_md;

		//условно-обязательные
		private string lpu_1;
		private int code_lpu_1;
		private string intervention_type;
		private double tariff;
		private int notfull;
		private string comment;


		//свойства
		public string Id
		{
			get { return id; }
		}
		public string Lpu
		{
			get { return lpu; }
		}
		public int Profile
		{
			get { return profile; }
		}
		public int Child
		{
			get { return child; }
		}
		public DateTime Date_start
		{
			get { return date_start; }
		}
		public DateTime Date_end
		{
			get { return date_end; }
		}
		public string Diagnosis
		{
			get { return diagnosis; }
		}
		public string Code
		{
			get { return code; }
		}
		public double Count
		{
			get { return count; }
		}
		public double Sum
		{
			get { return sum; }
		}
		public int Md_specialisztion
		{
			get { return md_specialisztion; }
		}
		public string Code_md
		{
			get { return code_md; }
		}
		public string Lpu_1
		{
			get { return lpu_1; }
		}
		public int Code_lpu_1
		{
			get { return code_lpu_1; }
		}
		public string Intervention_type
		{
			get { return intervention_type; }
		}
		public double Tariff
		{
			get { return tariff; }
		}
		public int Notfull
		{
			get { return notfull; }
		}
		public string Comment
		{
			get { return comment; }
		}

		public Service (XmlNode node)
		{
			id = node.SelectSingleNode ("IDSERV")?.InnerText;
			lpu = node.SelectSingleNode ("LPU")?.InnerText;
			profile = Utils.parse_int (node.SelectSingleNode ("PROFIL")?.InnerText);
			child = Utils.parse_int (node.SelectSingleNode ("DET")?.InnerText);
			string buf = node.SelectSingleNode ("DATE_IN")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out date_start);
			buf = node.SelectSingleNode ("DATE_OUT")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out date_end);
			diagnosis = node.SelectSingleNode ("DS")?.InnerText;
			code = node.SelectSingleNode ("CODE_USL")?.InnerText;
			count = Utils.parse_double (node.SelectSingleNode ("KOL_USL")?.InnerText);
			sum = Utils.parse_double (node.SelectSingleNode ("SUMV_USL")?.InnerText);
			md_specialisztion = Utils.parse_int (node.SelectSingleNode ("PRVS")?.InnerText);
			code_md = node.SelectSingleNode ("CODE_MD")?.InnerText;

			lpu_1 = node.SelectSingleNode ("LPU_1")?.InnerText;
			code_lpu_1 = Utils.parse_int (node.SelectSingleNode ("PODR")?.InnerText);
			intervention_type = node.SelectSingleNode ("VID_VME")?.InnerText;
			tariff = Utils.parse_double (node.SelectSingleNode ("TARIF")?.InnerText);
			notfull = Utils.parse_int (node.SelectSingleNode ("NPL")?.InnerText);
			comment = node.SelectSingleNode ("COMENTU")?.InnerText;
		}


        public string generate_sql_insert_query (long incedent_id)
        {
            //Если запись уже существует - пропускаем
            string result = string.Format (
                @"if not exists (select 1 from Service where IDSERV='{0}')
                BEGIN
                    insert into Service values ('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}',{13},'{14}',{15},{16},'{17}',{18})
                END; ", id, lpu, profile, child, date_start.ToString ("dd.MM.yyyy"), date_end.ToString ("dd.MM.yyyy"), diagnosis, code, count.ToString (CultureInfo.InvariantCulture), sum.ToString (CultureInfo.InvariantCulture), 
                md_specialisztion, code_md, lpu_1, code_lpu_1, intervention_type, tariff.ToString (CultureInfo.InvariantCulture), notfull, comment, incedent_id);
            return result;
        }

    }
}

