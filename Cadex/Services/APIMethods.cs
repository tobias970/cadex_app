using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Services
{
    public class APIMethods
    {
        public JObject result;

        public APIMethods()
        {

        }

        /*Bruges til at sende brugernavn og kodeord til API'en som validere og hvis det er rigtigt 
        modtager den en token som bliver gemt i variablen "key".*/
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

        public bool validatekey(string key)
        {
            Console.WriteLine("TOKENVALID : " + key);
            //Den nye temperatur som sendes til APIen.
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

        public object HentProdukter()
        {

            APICustomRequest http = new APICustomRequest("https://api.cadex.dk/");

            //Sender data til følgende API endpoint.
            JObject json = http.SendData("product/getAll/img/3", new { }, Method.GET);
            //Console.WriteLine(json);

            result = (JObject)json.SelectToken("result");

            return result;
        }

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
            //Den nye temperatur som sendes til APIen.
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

        public bool OpretProdukt(string token, string overskrift, string beskrivelse, string pris)
        {
            //Den nye temperatur som sendes til APIen.
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
