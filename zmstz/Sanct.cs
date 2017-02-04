using System.Globalization;
using System.Xml;

namespace zmstz
{
	/*
	 * Санкция
	 */
	public class Sanct
	{
		//обязательные поля
		private string code;
		private double sum;
		private int type;
		private int reason;
		private int source;

		//условно-обязательные
		private string comment;

		//свойства
		public string Code
		{
			get { return code; }
		}
		public double Sum
		{
			get { return sum; }
		}
		public int Type
		{
			get { return type; }
		}
		public int Reason
		{
			get { return reason; }
		}
		public int Source
		{
			get { return source; }
		}
		public string Comment
		{
			get { return comment; }
		}

		public Sanct (XmlNode node)
		{
			code = node.SelectSingleNode ("S_CODE")?.InnerText;
			sum = Utils.parse_double (node.SelectSingleNode ("S_SUM")?.InnerText);
			type = Utils.parse_int (node.SelectSingleNode ("S_TIP")?.InnerText);
			reason = Utils.parse_int (node.SelectSingleNode ("S_OSN")?.InnerText);
			source = Utils.parse_int (node.SelectSingleNode ("S_IST")?.InnerText);
			comment = node.SelectSingleNode ("S_COM")?.InnerText;
		}

        public string generate_sql_insert_query (long incident_id)
        {
            //Если запись уже существует - пропускаем
            string result = string.Format (
                @"if not exists (select 1 from Sanct where S_CODE='{0}')
                BEGIN
                    insert into Sanct values ('{0}',{1},{2},{3},{4},'{5}',{6})
                END; ", code, sum.ToString (CultureInfo.InvariantCulture), type, reason, source, comment, incident_id);

            return result;
        }

    }
}

