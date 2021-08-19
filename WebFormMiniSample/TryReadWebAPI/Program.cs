using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace TryReadWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherDataReader.ReadData();
            Console.ReadLine();

            //WebClient client = new WebClient();
            //byte[] sourceByte = client.DownloadData("https://apiservice.mol.gov.tw/OdService/rest/datastore/A17010000J-000135-MOZ");
            //string jsonText = Encoding.UTF8.GetString(sourceByte);

            //Rootobject obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(jsonText);

            //foreach (var item in obj.result.records)
            //{
            //    Console.WriteLine(item.年度);
            //}

            //Console.WriteLine(jsonText);
            //Console.ReadLine();
        }
    }
}
