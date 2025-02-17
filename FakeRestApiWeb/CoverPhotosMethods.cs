using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FakeRestApiWeb
{
    static class CoverPhotos
    {
        static int id { get; set; }
        static int idBook { get; set; }
        static string url { get; set; }
    }

    public class CoverPhotosMethods
    {        

        RestClient client = new RestClient();
        Endpoints endpoints = new Endpoints();

        public void GetAllCoverPhotos()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}", Method.Get);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var arrayResponse = JArray.Parse(response.Content);
            var firstCoverPhoto = arrayResponse[0];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(JTokenType.Integer, firstCoverPhoto["id"]?.Type);
            Assert.AreEqual(JTokenType.Integer, firstCoverPhoto["idBook"]?.Type);
            Assert.AreEqual(JTokenType.String, firstCoverPhoto["url"]?.Type);            
            Console.WriteLine(firstCoverPhoto);
        }        

        public void CreateCoverPhoto()
        {
            var objectBody = new
            {
                id = 0,
                idBook = 0,
                url = "www.seznam.cz"
            };
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}", Method.Post).AddBody(objectBody);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(objectBody.id, jsonResponse["id"]);
            Assert.AreEqual(objectBody.idBook, jsonResponse["idBook"]);
            Assert.AreEqual(objectBody.url, jsonResponse["url"]);
            Assert.AreEqual(JTokenType.Integer, jsonResponse?["id"]?.Type);
            Assert.AreEqual(JTokenType.Integer, jsonResponse?["idBook"]?.Type);
            Assert.AreEqual(JTokenType.String, jsonResponse?["url"]?.Type);
            Console.WriteLine(jsonResponse);
        }

        public void GetCoverPhotosBookId()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosBookId}/1", Method.Get);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JArray.Parse(response.Content).First();            

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, jsonResponse["id"]);
            Assert.AreEqual(1, jsonResponse["idBook"]);            
            Console.WriteLine(jsonResponse);
        }        
    }

    public class CoverPhotosBooks
    {
        public int id { get; set; }
        public int idBook { get; set; }
        public string url { get; set; }
    }
}