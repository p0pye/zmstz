using System;
using System.Xml;
using System.Collections.Generic;

namespace zmstz
{
	class MainClass
	{
		public static int Main (string[] args)
		{
            const string SERVER_DB_PATH = @"localhost\sqlexpress";
            const string DB_NAME = "zms";
            const string DB_USER = "sa";
            const string DB_PASSWORD = "test";
			string xml_file_name;

			if (args.Length < 1) {
				Console.WriteLine ("Usage: zmstz path_to_xml_file\n");
				return 0;
			}
			xml_file_name = args[0];
			
			Console.WriteLine ("Processing file {0} ...", xml_file_name);

			//основной документ
			XmlDocument xml_document = new XmlDocument ();
			//заголовок
			FileInfo xml_header;
			//счет
			Bill bill;
			//записи
			List<Record> records = new List<Record> ();
            //база
            SqlDbProvider db_provider;

            try
            {
                db_provider = new SqlDbProvider (SERVER_DB_PATH, DB_NAME, DB_USER, DB_PASSWORD);
            }
            catch (Exception e)
            {
                Console.WriteLine ("{0}\n****\nError while connecting to sql server! Exiting...", e.Message);
                return 3;
            }

            try {
				xml_document.Load (xml_file_name);
			} catch (Exception e) {
				Console.WriteLine ("{0}\n****\nError while reading xml-file! Exiting...", e.Message);
				return 1;
			}

			try {
				xml_header = new FileInfo (xml_document);
			} catch (Exception e) {
				Console.WriteLine ("{0}\n****\nError while reading file header! Exiting...", e.Message);
				return 2;
			}

			if (!xml_header.is_header_correct ()) {
				Console.WriteLine ("Unsupported file format! Exiting...");
				return 2;
			}
			xml_header.display_file_summary ();

			try {
				bill = new Bill (xml_document);
			} catch (Exception e) {
				Console.WriteLine ("{0}\n****\nError while reading bill information! Exiting...", e.Message);
				return 2;
			}
			bill.display_bill_summary ();
            //запись информации о счете в базу
            db_provider.execute_query (bill.generate_sql_insert_query ());

			if (xml_header.RecordsCount <= 0) {
				Console.WriteLine ("Nothing to do. Exiting...");
				return 0;
			}

            int total = 0;
			XmlNodeList list = xml_document.GetElementsByTagName ("ZAP");
			if (list.Count != xml_header.RecordsCount) {
				Console.WriteLine ("File corrupted! Exiting...");
				return 2;
			}

            Console.WriteLine ("Preparation for reading records...");
			foreach (XmlNode record in list) {
				total++;
				Console.CursorLeft = 0;
				try {
					records.Add (new Record (record));
				} catch (Exception) {
					Console.WriteLine ("Warning: record №{0} corrupted! Skipping...", total);
					continue;
				}
				Console.Write ("Readed {0} of {1} records", total, xml_header.RecordsCount);
			}
            Console.WriteLine ();
            total = 0;
            foreach (var record in records) {
                Console.CursorLeft = 0;
                total++;
                Console.Write ("Processing {0} of {1} records", total, records.Count);
                record.process_to_db (db_provider);
            }

            db_provider.close_connection ();
			Console.WriteLine ("\nDone.");

			return 0;
		}
	}
}
