using System;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Services
{
    public class Validate
    {
        //Metode der bruges til at validere tokenen, som den stadig er valid.
        public bool validatekey(string key)
        {
            //Data der sendes til API'en
            var data = new
            {
                token = key
            };

            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("auth/validate", data, Method.POST);

            JArray afdelinger = (JArray)json["result"]["securityGroups"];

            foreach (string item in afdelinger)
            {
                string afdeling = item;

                if (afdeling == "IT_SG")
                {
                    AppSession.IT = true;
                }
            }

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");

            return status;
        }
    }
}
