using System;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;		//outsite liberary to transfer from json to xml

namespace Work_KoboDownloader
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Downloading...Please wait...");
			WebClient wc = new WebClient();

			wc.Credentials = new NetworkCredential("username", "password");

			//download data in excel format
			//wc.DownloadFile("https://kc.humanitarianresponse.info/api/v1/data/******.xlsx", "D:\\Downloads\\newName.xlsx");

			//take data in stream
			Stream str = wc.OpenRead("https://kc.humanitarianresponse.info/api/v1/data/******");  // ****** - number of the form, 

			StreamReader sr = new StreamReader(str);
			string json = "";
			string sTemp;
			while ((sTemp = sr.ReadLine()) != null)
				json += sTemp;

			//chreate a json file
			FileInfo fs = new FileInfo("data.json");
			using (StreamWriter streamWriter = fs.CreateText())
			{
				streamWriter.Write(json);
			}

			//prepare to create a xml file
			json = "{'?xml':{'@version': '1.0','@standalone': 'no'},'root': { raws : " + json + "} }";
			json = json.Replace('\"', '\'');
			//Console.WriteLine(json);

			//create a xml file
			XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json);
			doc.Save("Test.xml");

			Console.WriteLine("Download complete!");
			Console.ReadLine();
		}
	}
}
