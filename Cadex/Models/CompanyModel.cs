using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class CompanyModel
    {
        public JObject result;

        public (string, string, string, string) HentVirkInfo()
        {
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("company/information", new { }, Method.GET);
            //Console.WriteLine(json);

            JArray virkinfoResult = (JArray)json.SelectToken("result");

            string title = (string)virkinfoResult[0]["title"];
            string desc = (string)virkinfoResult[0]["content"];
            string tlf = (string)virkinfoResult[0]["email"];
            string mail = (string)virkinfoResult[0]["phone_number"];

            return (title, desc, tlf, mail);
        }
    }
}
