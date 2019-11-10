using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class NewsModel
    {
        public JObject result;

        //Metode er henter nyheder fra API'en
        public object HentNyheder(string token)
        {
            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/getAll", new { }, Method.GET, token);

            //Gemmer API resultatet i et json object.
            result = (JObject)json.SelectToken("result");

            return result;
        }

        //Metode der opretter nyheder ved at kalde API'en
        public bool OpretNyhed(string token, string overskrift, string beskrivelse)
        {
            //Data der sendes til API'en
            var data = new
            {
                title = overskrift,
                content = beskrivelse
            };

            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/create", data, Method.POST, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");
            
            return status;
        }

        //Metode der updatere nyheder ved at kalde API'en
        public bool UpdateNyhed(string token, int identity, string overskrift, string beskrivelse)
        {
            //Data der sendes til API'en
            var data = new
            {
                title = overskrift,
                content = beskrivelse
            };

            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/update/" + identity, data, Method.PUT, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");
            
            return status;
        }



        public bool SletNyhed(string token, string identity)
        {
            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/delete/" + identity, new { }, Method.DELETE, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");
            
            return status;
        }
    }
}
