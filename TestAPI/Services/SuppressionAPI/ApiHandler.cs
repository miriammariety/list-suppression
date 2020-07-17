using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Services.SuppressionAPI
{
    public class ApiHandler
    {
        public ApiService Service;

        public ApiHandler()
        {
            Service = new ApiService();
        }

        public void GetSuppressedList()
        {
            // Call Service.GetResponse()
            // Implement download created file from json to csv


        }
    }
}