using API.Readers;
using ChoETL;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace API
{
    public class ApiService
    {
        //public string Endpoint { get; set; }
        //public ApiService(string endpoint)
        //{
        //    Endpoint = endpoint;
        //}

        public string GetResponse()
        {
            string endpoint = "https://cat-fact.herokuapp.com/facts";
            var client = new RestClient(endpoint);
            //var parameters = "v1/employees";

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");

            var result = client.Execute(request);
            return result.Content;

        }

        public string ConvertToJson()
        {
            FileMapper mapper = new FileMapper();
            string path = mapper.MapFile("~/input_cut.csv");
            var csv = File.ReadAllText(path);

            StringBuilder sb = new StringBuilder();
            using (var p = ChoCSVReader.LoadText(csv)
                .WithFirstLineHeader()
                )
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }

            return sb.ToString();

        }

        public string ConvertToCSV()
        {
            return null;
        }
    }
}
