using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Json;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json;
using RestService.Models;
using Attribute = RestService.Models.Attribute;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:58033/api/Objects";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url + "/10"));
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine(sr.ReadToEnd()); 
            }

            request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/json";


            var obj = new ObjectItem(1, "Насос");
            obj.Attributes.Add(new Attribute("Марка", AttributeType.String, "ЭЦРЗ15"));
            obj.Attributes.Add(new Attribute("Масса", AttributeType.Float, 12.4));
            obj.Attributes.Add(new Attribute("Дата установки", AttributeType.DateTime, DateTime.Now));

            var dts = JsonConvert.SerializeObject(obj);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(dts);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                Console.WriteLine(result);
            }

            Console.Read();
        }
    }
}
