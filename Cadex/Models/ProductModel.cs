using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class ProductModel
    {
        public JObject result;

        public object HentProdukter()
        {


            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/getAll/img/3", new { }, Method.GET);
            //Console.WriteLine(json);

            result = (JObject)json.SelectToken("result");

            return result;
        }

        public bool OpretProdukt(string token, string overskrift, string beskrivelse, string pris)
        {
            var data = new
            {
                title = overskrift,
                description = beskrivelse,
                price = pris
            };

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/create", data, Method.POST, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }

        public bool UpdateProdukt(string token, int identity, string overskrift, string beskrivelse, string pris)
        {
            var data = new
            {
                title = overskrift,
                content = beskrivelse,
                price = pris
            };

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/update/" + identity, data, Method.PUT, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }

        public bool SletProdukt(string token, string identity)
        {
            //Den nye temperatur som sendes til APIen.

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/delete/" + identity, new { }, Method.DELETE, token);
            Console.WriteLine(json);

            bool status = (bool)json.SelectToken("status");
            Console.WriteLine("STATUSRE : " + status);

            return status;
        }
    }
}
