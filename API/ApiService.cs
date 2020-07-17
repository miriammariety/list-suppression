using API.Readers;
using ChoETL;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        FileMapper Mapper { get; set; }
        
        public ApiService()
        {
            Mapper = new FileMapper();
        }

        public string GetResponse()
        {
            // Implement csv to json
            // Use json as parameter

            string endpoint = ConfigurationManager.AppSettings.Get("ApiEndpoint");
            var client = new RestClient(endpoint);
            var file = Mapper.MapFile("~/sample.json");
            var json = File.ReadAllText(file);

            Debug.WriteLine(json);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=d172b9aae6dec71b8de31867e09365e401594744964");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Accept", "application/json");

            var result = client.Execute(request);
            return result.Content;

        }

        public string ConvertToJson()
        {
            string path = Mapper.MapFile("~/input_cut.csv");
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

        public void ConvertToCSV(string json)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            var lines = new List<string>();
            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dt.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            string path = Mapper.MapFile("~/output.csv");

            File.WriteAllLines(path, lines);
        }
    }
}
