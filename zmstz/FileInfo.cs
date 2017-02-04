using System;
using System.Xml;
using System.Globalization;

namespace zmstz
{

	/*
	 * заголовок файла
	 */

	public class FileInfo 
	{
		//путь для чтения
		public const string HEADER_PATH = "/ZL_LIST/ZGLV/";
		//требуемая версия формата
		public const string REQUIRED_FORMAT_VERSION = "1.0";

		private string format_version;
		private DateTime file_date;
		private string file_name;
		private int records_count;

		//свойства
		public string FormatVersion 
		{
			get { return format_version; }
		}
		public DateTime FileDate
		{
			get { return file_date; }
		}
		public string FileName 
		{
			get { return file_name; }
		}
		public int RecordsCount 
		{
			get { return records_count; }
		}

		public FileInfo (XmlDocument xml) 
		{
			format_version = xml.DocumentElement.SelectSingleNode (HEADER_PATH+"VERSION")?.InnerText;
			string buf = xml.DocumentElement.SelectSingleNode (HEADER_PATH+"DATA")?.InnerText;
			DateTime.TryParseExact (buf, "yyyy-MM-dd", null, DateTimeStyles.None, out file_date);
			file_name = xml.DocumentElement.SelectSingleNode (HEADER_PATH+"FILENAME")?.InnerText;
			records_count = Utils.parse_int (xml.DocumentElement.SelectSingleNode (HEADER_PATH + "SD_Z")?.InnerText);
		}

		//проверка корректности заголовка
		public bool is_header_correct ()
		{
			return (format_version != null || format_version != REQUIRED_FORMAT_VERSION 
				|| file_name == null || file_name[0]!='H');
		}

		//вывод инф. о файле
		public void display_file_summary ()
		{
			Console.WriteLine ("Xml file summary info:\n============================================================");
			Console.WriteLine ("\tVersion: "+format_version);
			Console.WriteLine ("\tFile date: {0:dd.MM.yyyy}",file_date);
			Console.WriteLine ("\tOriginal file name: " + file_name);
			Console.WriteLine ("\tIncedents count: " + records_count+"\n");
		}
	}
}

