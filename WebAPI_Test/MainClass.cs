using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebAPI_Test
{
    public static class MainClass
    {
        public static string GetResponse()
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
    }
}