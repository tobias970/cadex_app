using System;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Cadex.Services
{
    public class APICustomRequest
    {
        private RestClient client;

        
        //APICustomRequest() bruges til at sætte vores RestClient client  
        
        public APICustomRequest(string baseURL)
        {
            // Opretter RestClient
            client = new RestClient(baseURL);
        }

        
        //SendData() bruges til at sende data over RestRequest.
        public JObject SendData(string endpoint, Object obj, Method method)
        {
            // Ignorere SSL self signed certifikat.
            ServicePointManager.ServerCertificateValidationCallback += (sender, certifcate, chain, errors) => true;

            // Opretter RestRequest.
            var request = new RestRequest(endpoint, method);

            // Sætter Content-type header.
            request.AddHeader("Content-type", "application/json");

            // Sætter JSON body.
            request.AddJsonBody(obj);

            Console.WriteLine("obj : " + obj);

            // Eksekvere requesten.
            IRestResponse response = client.Execute(request);

            Console.WriteLine("response : " + response.Content);

            // Konvertere variablen response til JSON.
            JObject jsonContent = JObject.Parse(response.Content);

            client = null;

            Console.WriteLine("jsonContent : " + jsonContent);

            // Returnere JSON objektet.
            return jsonContent;
        }

        public JObject SendData(string endpoint, Object obj, Method method, string token)
        {
            // Ignorere SSL self signed certifikat.
            ServicePointManager.ServerCertificateValidationCallback += (sender, certifcate, chain, errors) => true;

            // Opretter RestRequest.
            var request = new RestRequest(endpoint, method);

            // Sætter Content-type header.
            request.AddHeader("Content-type", "application/json");

            // Sætter Token.
            request.AddHeader("Authorization", "Bearer " + token);

            // Sætter JSON body.
            request.AddJsonBody(obj);

            // Eksekvere requesten.
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            // Konvertere variablen response til JSON.
            JObject jsonContent = JObject.Parse(response.Content);

            // Returnere JSON objektet.
            return jsonContent;
        }
    }
}
