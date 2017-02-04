using System;
using System.Xml;
using System.Globalization;

namespace zmstz
{
	/*
	 * пациент
	 */

	public class Patient 
	{
		//обязательные поля
		private string id;
		private int polis_type;
		private string polis_no;
		private string newborn;
		//условно-обязательные
		private string polis_s;
		private string st_okato;
		private string smo;
		private string smo_ogrn;
		private string smo_okato;
		private string smo_name;
		private int inv_group;
		private int newborn_weight;

		//свойства 
		public string Id
		{
			get { return id; }
		}
		public int Polis_type
		{
			get { return polis_type; }
		}
		public string Polis_no
		{
			get { return polis_no; }
		}
		public string Neworn
		{
			get { return newborn; }
		}

		public string Polis_s
		{
			get { return polis_s; }
		}
		public string St_okato
		{
			get { return st_okato; }
		}
		public string Smo
		{
			get { return smo; }
		}
		public string Smo_ogrn
		{
			get { return smo_ogrn; }
		}
		public string Smo_okato
		{
			get { return smo_okato; }
		}
		public string Smo_name
		{
			get { return smo_name; }
		}
		public int Inv_group
		{
			get { return inv_group; }
		}
		public int Newborn_weight
		{
			get { return newborn_weight; }
		}

		public Patient (XmlNode node)
		{
			id = node.SelectSingleNode ("ID_PAC")?.InnerText;
			polis_type = Utils.parse_int (node.SelectSingleNode ("VPOLIS")?.InnerText);
			polis_no = node.SelectSingleNode ("NPOLIS")?.InnerText;
			newborn = node.SelectSingleNode ("NOVOR")?.InnerText;

			polis_s = node.SelectSingleNode ("SPOLIS")?.InnerText;
			st_okato = node.SelectSingleNode ("ST_OKATO")?.InnerText;
			smo = node.SelectSingleNode ("SMO")?.InnerText;
			smo_ogrn = node.SelectSingleNode ("SMO_OGRN")?.InnerText;
			smo_okato = node.SelectSingleNode ("SMO_OK")?.InnerText;
			smo_name = node.SelectSingleNode ("SMO_NAM")?.InnerText;
			inv_group = Utils.parse_int (node.SelectSingleNode ("INV")?.InnerText);
			newborn_weight = Utils.parse_int (node.SelectSingleNode ("VNOV_D")?.InnerText);
		}

        public string generate_sql_insert_query ()
        {
            //Если запись уже существует - пропускаем
            string result = string.Format (
                @"if not exists (select 1 from Patient where ID_PAC='{0}')
                BEGIN
                    insert into Patient values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11})
                END; ",
                id, polis_type, polis_no, newborn, polis_s, st_okato, smo, smo_ogrn, smo_okato, smo_name, inv_group, newborn_weight);
            return result;
        }

    }
}

