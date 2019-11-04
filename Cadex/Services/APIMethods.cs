using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cadex.Services
{
    public class APIMethods
    {
        public JObject result;

        public string tempvalue;
        public int[] rumarray;
        public string temperaturvalue;
        public string humidityvalue;


        public APIMethods()
        {

        }

        /*Bruges til at sende brugernavn og kodeord til API'en som validere og hvis det er rigtigt 
        modtager den en token som bliver gemt i variablen "key".*/
        public string HentNyNoegle(string brugernavn, string kodeord)
        {
            string key;

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


            validatekey(key);

            
            //Retunere noeglen.
            return key;
        }

        public void validatekey(string key)
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










        //Henter standard temperatur fra databasen via. APIen.
        public void HentStandardTemperatur(string token)
        {
            try
            {
                //Oprettelse af https request til API'en
                APICustomRequest http = new APICustomRequest("https://api.indeklima.local");

                //Sender data til følgende API endpoint.
                JObject json = http.SendData("api/temperature/getDefaultTemperature", new { }, Method.POST, token);
                Console.WriteLine(json);

                //Finder relevante json data og gemmer det i en variabel.
                tempvalue = (string)json.SelectToken("temperature");

                //Udskriver variablen.
                Console.WriteLine(tempvalue);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Bruges til at sætte en ny standard temperatur for alle rum. 
        public void SetStandardTemp(string token, int temp, int format)
        {

            //Den nye temperatur som sendes til APIen.
            var data = new
            {
                temperature = temp,
                format = format
            };

            try
            {
                APICustomRequest http = new APICustomRequest("https://api.indeklima.local");

                //Sender data til følgende API endpoint.
                JObject json = http.SendData("api/temperature/setDefaultTemperature", data, Method.POST, token);
                Console.WriteLine(json);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Henter rum numre og smider det i et array.
        public void HentRum(string token)
        {

            try
            {
                APICustomRequest http = new APICustomRequest("https://api.indeklima.local");

                //Sender data til følgende API endpoint.
                JObject json = http.SendData("api/location/getRooms", new { }, Method.POST, token);
                Console.WriteLine(json);

                //Finder relevante json data og gemmer det i et array.
                JArray rum = (JArray)json.SelectToken("rooms");

                //Laver det om til et public int array, som bruges af andre klasser.
                rumarray = rum.ToObject<int[]>();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Henter rum temperatur for de enkelte rum.
        public void HentRumTemperatur(string token, int nr)
        {

            var data = new
            {
                rumnr = nr

            };

            try
            {
                APICustomRequest http = new APICustomRequest("https://api.indeklima.local");

                //Sender data til følgende API endpoint.
                JObject json = http.SendData("api/temperature/getRoomTemperature", data, Method.POST, token);
                Console.WriteLine(json);

                //Finder relevante json data og gemmer det i variabler.
                temperaturvalue = (string)json.SelectToken("room.temperature");
                Console.WriteLine(temperaturvalue);

                humidityvalue = (string)json.SelectToken("room.humidity");
                Console.WriteLine(humidityvalue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
