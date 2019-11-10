using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class NewsModel
    {
        public JObject result;

        public object HentNyheder(string token)
        {
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/getAll", new { }, Method.GET, token);
            //Console.WriteLine(json);

            result = (JObject)json.SelectToken("result");

            return result;
        }

        public bool OpretNyhed(string token, string overskrift, string beskrivelse)
        {
            var data = new
            {
                title = overskrift,
                content = beskrivelse
            };

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/create", data, Method.POST, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }


        public bool UpdateNyhed(string token, int identity, string overskrift, string beskrivelse)
        {
            var data = new
            {
                title = overskrift,
                content = beskrivelse
            };

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/update/" + identity, data, Method.PUT, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }



        public bool SletNyhed(string token, string identity)
        {
            //Den nye temperatur som sendes til APIen.

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("news/delete/" + identity, new { }, Method.DELETE, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }
    }
}
