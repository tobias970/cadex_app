using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class AuthModel
    {
        public JObject result;

        //Metoden HentNyNoegle bruges til at kalde API'en for at få en token som bruges af andre API kald. 
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

            //Gemmer Json resultatet i en variabel.
            result = (JObject)json.SelectToken("result");

            //Tager tokenen fra json objektet "result" og gemmer den i variablen "key". 
            key = (string)result["token"];

            //Retunere noeglen.
            return key;
        }
    }
}
