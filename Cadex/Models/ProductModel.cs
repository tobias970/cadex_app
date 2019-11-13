using System;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Models
{
    public class ProductModel
    {
        public JObject result;

        //Metode der bruges til at hente produkter.
        public object HentProdukter()
        {
            //Oprettelse af https request til API'en.
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/getAll/img/3", new { }, Method.GET);

            //Gemmer API resultatet i et json object.
            result = (JObject)json.SelectToken("result");

            return result;
        }

        //Metode der bruges til at oprette produkter.
        public (bool, int) OpretProdukt(string token, string overskrift, string beskrivelse, string pris)
        {
            //Data der sendes til API'en
            var data = new
            {
                title = overskrift,
                description = beskrivelse,
                price = pris
            };

            //Oprettelse af https request til API'en
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/create", data, Method.POST, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");

            //Gemmer API resultatet i et json object.
            result = (JObject)json.SelectToken("result");

            //Gemmer id'et for produktet
            int produktid = (int)result["id"];

            return (status, produktid);
        }

        //Metode der bruges til at updatere produkter.
        public bool UpdateProdukt(string token, int identity, string overskrift, string beskrivelse, string pris)
        {
            //Data der sendes til API'en
            var data = new
            {
                title = overskrift,
                content = beskrivelse,
                price = pris
            };

            //Oprettelse af https request til API'en
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/update/" + identity, data, Method.PUT, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");
            
            return status;
        }

        //Metode der bruges til at slette produkter.
        public bool SletProdukt(string token, string identity)
        {
            //Oprettelse af https request til API'en
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/delete/" + identity, new { }, Method.DELETE, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");
            
            return status;
        }

        public bool UploadBillede(string token, int produktid, string billede)
        {
            //Data der sendes til API'en
            var data = new
            {
                picture = billede
            };

            //Oprettelse af https request til API'en
            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/uploadImage/" + produktid + "/true", data, Method.POST, token);

            //Gemmer statussen fra API'en
            bool status = (bool)json.SelectToken("status");

            return status;
        }
    }
}
