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
            Assert.IsTrue(firstCoverPhoto["url"]?.Contains("http"), "Partial url isn't match");
            Console.WriteLine(firstCoverPhoto);
        }        
    }
}
