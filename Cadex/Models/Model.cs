﻿using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class Model
    {
        public bool validatekey(string key)
        {
            Console.WriteLine("TOKENVALID : " + key);

            var data = new
            {
                token = key
            };

            Console.WriteLine("DATA : " + data);

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("auth/validate", data, Method.POST);
            Console.WriteLine("VALID : " + json);

            bool status = (bool)json.SelectToken("status");

            return status;
        }
    }
}
