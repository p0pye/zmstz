using System;
using System.Xml;
using System.Collections.Generic;
using System.Globalization;

namespace zmstz
{
	public class Incident
	{
		//обязательные поля
		private long id;
		private int condition;
		private int help_type;
		private int help_form;
		private string lpu;
		private int profile;
		private int child;
		private string sick_no;
		private DateTime date_start;
		private DateTime date_end;
		private string diagnosis_1;
		private int treatment_result; //результат обращения
		private int disease_result; //исход заболевания
		private int doc_specialisation;
		private string doc_id;
		private int payment_code;
		private double sum;

		//условно-обязательные
		private string source_mo;
		private int direction;
		private string lpu_1; //подразделение
		private int code_lpu_1;
		private int supply; //признак поступления/перевода
		private string diagnosis_0;
		private List<string> diagnosis_2;
		private List<string> diagnosis_3;
		private int weight_at_birth;
		private int code_mes1;
		private int code_mes2;
		private int special_case; //особый случай
		private double ed_col;
		private double tariff;
		private int payment_type;
		private double sum_accepted;
		private double sum_sanct;
		private string comment;

		private List<Sanct> sancts;
		private List<Service> services;



		//свойства
		public long Id
		{
			get { return id; }
		}
		public int Condition
		{
			get { return condition; }
		}
		public int Help_type
		{
			get { return help_type; }
		}
		public int Help_form
		{
			get { return help_form; }
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
		public string Sick_no
		{
			get { return sick_no; }
		}
		public DateTime Date_start
		{
			get { return date_start; }
		}
		public DateTime Date_end
		{
			get { return date_end; }
		}
		public string Diagnosis_1
		{
			get { return diagnosis_1; }
		}
		public int Treatment_result
		{
			get { return treatment_result; }
		} //результат обращения
		public int Disease_result
		{
			get { return disease_result; }
		} //исход заболевания
		public int Doc_specialisation
		{
			get { return doc_specialisation; }
		}
		public string Doc_id
		{
			get { return doc_id; }
		}
		public int Payment_code
		{
			get { return payment_code; }
		}
		public double Sum
		{
			get { return sum; }
		}
		public string Source_mo
		{
			get { return source_mo; }
		}
		public int Direction
		{
			get { return direction; }
		}
		public string Lpu_1
		{
			get { return lpu_1; }
		} //подразделение
		public int Code_lpu_1
		{
			get { return code_lpu_1; }
		}
		public int Supply
		{
			get { return supply; }
		} //признак поступления/перевода
		public string Diagnosis_0
		{
			get { return diagnosis_0; }
		}
		public List<string> Diagnosis_2
		{
			get { return diagnosis_2; }
		}
		public List<string> Diagnosis_3
		{
			get { return diagnosis_3; }
		}
		public int Weight_at_birth
		{
			get { return weight_at_birth; }
		}
		public int Code_mes1
		{
			get { return code_mes1; }
		}
		public int Code_mes2
		{
			get { return code_mes2; }
		}
		public int Special_case
		{
			get { return special_case; }
		} //особый случай
		public double Ed_col
		{
			get { return ed_col; }
		}
		public double Tariff
		{
			get { return tariff; }
		}
		public int Payment_type
		{
			get { return payment_type; }
		}
		public double Sum_accepted
		{
			get { return sum_accepted; }
		}
		public double Sum_sanct
		{
			get { return sum_sanct; }
		}
		public string Comment
		{
			get { return comment; }
		}
		public List<Sanct> Sancts
		{
			get { return sancts; }
		}
		public List<Service> Services
		{
			get { return services; }
		}

		public Incident (XmlNode node)
		{
			id = Utils.parse_long (node.SelectSingleNode ("IDCASE")?.InnerText);
			condition = Utils.parse_int (node.SelectSingleNode ("USL_OK")?.InnerText);
			help_type = Utils.parse_int (node.SelectSingleNode ("VIDPOM")?.InnerText);
			help_form = Utils.parse_int (node.SelectSingleNode ("FOR_POM")?.InnerText);
			lpu = node.SelectSingleNode ("LPU")?.InnerText;
			profile = Utils.parse_int (node.SelectSingleNode ("PROFIL")?.InnerText);
			child = Utils.parse_int (node.SelectSingleNode ("DET")?.InnerText);
			sick_no = node.SelectSingleNode ("NHISTORY")?.InnerText;
			string buf = node.SelectSingleNode ("DATE_1")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out date_start);
			buf = node.SelectSingleNode ("DATE_2")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out date_end);
			diagnosis_1 = node.SelectSingleNode ("DS1")?.InnerText;
			treatment_result = Utils.parse_int (node.SelectSingleNode ("RSLT")?.InnerText);
			disease_result = Utils.parse_int (node.SelectSingleNode ("ISHOD")?.InnerText);
			doc_specialisation = Utils.parse_int (node.SelectSingleNode ("PRVS")?.InnerText);
			doc_id = node.SelectSingleNode ("IDDOKT")?.InnerText;
			payment_code = Utils.parse_int (node.SelectSingleNode ("IDSP")?.InnerText);
			sum = Utils.parse_double (node.SelectSingleNode ("SUMV")?.InnerText);

			source_mo = node.SelectSingleNode ("NPR_MO")?.InnerText;
			direction = Utils.parse_int (node.SelectSingleNode ("EXTR")?.InnerText);
			lpu_1 = node.SelectSingleNode ("LPU_1")?.InnerText;
			code_lpu_1 = Utils.parse_int (node.SelectSingleNode ("PODR")?.InnerText);
			supply = Utils.parse_int (node.SelectSingleNode ("P_PER")?.InnerText); 
			diagnosis_0 = node.SelectSingleNode ("DS0")?.InnerText;
	
			XmlNodeList ds = node.SelectNodes ("DS2");
            diagnosis_2 = new List<string> ();
			foreach (XmlNode d in ds)
				diagnosis_2.Add (d.SelectSingleNode ("DS2")?.InnerText);

			ds = node.SelectNodes ("DS3");
            diagnosis_3 = new List<string> ();
			foreach (XmlNode d in ds)
				diagnosis_3.Add (d.SelectSingleNode ("DS3")?.InnerText);
	
			weight_at_birth = Utils.parse_int (node.SelectSingleNode ("VNOV_M")?.InnerText); 
			code_mes1 = Utils.parse_int (node.SelectSingleNode ("CODE_MES1")?.InnerText); 
			code_mes2 = Utils.parse_int (node.SelectSingleNode ("CODE_MES2")?.InnerText); 
			special_case = Utils.parse_int (node.SelectSingleNode ("OS_SLUCH")?.InnerText);
			ed_col = Utils.parse_double (node.SelectSingleNode ("ED_COL")?.InnerText); 
			tariff = Utils.parse_double (node.SelectSingleNode ("TARIF")?.InnerText); 
			payment_type = Utils.parse_int (node.SelectSingleNode ("OPLATA")?.InnerText); 
			sum_accepted = Utils.parse_double (node.SelectSingleNode ("SUMP")?.InnerText); 
			sum_sanct = Utils.parse_double (node.SelectSingleNode ("SANK_IT")?.InnerText); 
			comment = node.SelectSingleNode ("COMENTSL")?.InnerText;

			sancts = new List<Sanct> ();
			XmlNodeList list = node.SelectNodes ("SANK");
			foreach (XmlNode element in list) 
				sancts.Add (new Sanct (element));

			services = new List<Service> ();
			list = node.SelectNodes ("USL");
			foreach (XmlNode element in list)
				services.Add (new Service (element));
		}

        public string generate_sql_insert_query (long record_no)
        {
            string ds2="", ds3="";
            foreach (var d in diagnosis_2)
                ds2 += d+"|";
            foreach (var d in diagnosis_3)
                ds3 += d + "|";
            //Если запись уже существует - пропускаем
            string result = string.Format (
                @"if not exists (select 1 from Incident where IDCASE='{0}')
                BEGIN
                    insert into Incident values ({0},{1},{2},{3},'{4}',{5},{6},'{7}','{8}','{9}','{10}',{11},{12},{13},'{14}',{15},{16},
                    '{17}',{18},'{19}',{20},{21},'{22}','{23}','{24}',{25},{26},{27},{28},{29},{30},{31},{32},{33},'{34}', {35})
                END; ",
            id, condition, help_type, help_form, lpu, profile, child, sick_no, date_start.ToString ("dd.MM.yyyy"), date_end.ToString ("dd.MM.yyyy"), 
            diagnosis_1, treatment_result, disease_result, doc_specialisation, doc_id, payment_code, sum.ToString (CultureInfo.InvariantCulture), source_mo, direction, lpu_1, 
            code_lpu_1, supply, diagnosis_0, ds2, ds3, weight_at_birth, code_mes1, code_mes2, special_case, ed_col.ToString (CultureInfo.InvariantCulture), tariff, payment_type, sum_accepted, sum_sanct, comment, record_no);

            foreach (var sanct in sancts) {
                result += sanct.generate_sql_insert_query (id);
            }

            foreach (var service in services) {
                result += service.generate_sql_insert_query (id);
            }

            return result;
        }

    }
}

