using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class AuthModel
    {
        public JObject result;

        public string HentNyNoegle(string brugernavn, string kodeord)
        {
            string key = "";

            //Dataene der bliver sendt til API'en
            var data = new
            {
                username = brugernavn,
                password = kodeord

            };
            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("auth/authenticate", data, Method.POST);

            Console.WriteLine(json);

            //Finder relevante json data og gemmer det i en variabel.
            result = (JObject)json.SelectToken("result");

            Console.WriteLine("result : " + result);

            key = (string)result["token"];

            Console.WriteLine("key : " + key);

            //Retunere noeglen.
            return key;
        }
    }
}
