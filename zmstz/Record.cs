using System;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;

namespace zmstz
{
	/*
	 * запись об оказании мед. помощи
	 */

	public class Record {
		//обязательные поля
		private long no;
		private int modified;
		private Patient patient;
		private List<Incident> incidents;

		//свойства
		public long No 
		{
			get { return no; }
		}
		public int Modified
		{
			get { return modified; }
		}
		public Patient Patient 
		{
			get { return patient; }
		}
		public List<Incident> Incidents
		{
			get { return incidents; }
		}

		public Record (XmlNode node) 
		{
			no = Utils.parse_int (node.SelectSingleNode ("N_ZAP")?.InnerText);
			modified = Utils.parse_int (node.SelectSingleNode ("PR_NOV")?.InnerText);
			patient = new Patient (node.SelectSingleNode ("PACIENT"));
			incidents = new List<Incident> ();
			XmlNodeList list = node.SelectNodes ("SLUCH");
			foreach (XmlNode element in list) {
				incidents.Add (new Incident (element));
			}
		}

        public string generate_sql_insert_query ()
        {
            //Если запись уже существует - пропускаем
            string result = string.Format (
                @"if not exists (select 1 from Record where N_ZAP='{0}')
                BEGIN
                    insert into Record values ({0},{1},'{2}')
                END; ",
                no, modified, patient.Id);
            return result;
        }

        public int process_to_db (SqlDbProvider provider)
        {
            var incidents_query = "";
            foreach (var i in incidents) {
                incidents_query += i.generate_sql_insert_query (no);
            }

            var record_sql_query =
                generate_sql_insert_query ()
                + patient.generate_sql_insert_query ()
                + incidents_query;

            return provider.execute_query (record_sql_query);
        }
	}
}

