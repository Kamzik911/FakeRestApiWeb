using System.Globalization;
using System.Threading.Tasks;

namespace FakeRestApiWeb
{
    public class CoverPhotosMethods
    {        
        public int bookId { get; set; }

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
                idBook = 4,
                url = "www.seznam.cz"
            };
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}", Method.Post).AddBody(objectBody);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JObject.Parse(response.Content);
            

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);               
            Assert.AreEqual(objectBody.idBook, jsonResponse["idBook"]);
            Assert.AreEqual(objectBody.url, jsonResponse["url"]);
            Assert.AreEqual(JTokenType.Integer, jsonResponse?["id"]?.Type);
            Assert.AreEqual(JTokenType.Integer, jsonResponse?["idBook"]?.Type);
            Assert.AreEqual(JTokenType.String, jsonResponse?["url"]?.Type);
            Console.WriteLine(jsonResponse);
        }

        public void PutCoverPhotosId()
        {
            var bookBody = new
            {                
                idBook = 4,
                url = "www.seznam.cz"
            };
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}/0", Method.Put).AddBody(bookBody);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(0, jsonResponse["id"], "Id doesn't match");
            Assert.AreEqual(4, jsonResponse["idBook"], "IdBook doesn't match");
            Assert.AreEqual(bookBody.url, jsonResponse["url"], "Url doesn't match");
            Console.WriteLine(jsonResponse);
        }
        public void DeleteCoverPhotosId()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}/0", Method.Delete);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();

            if (HttpStatusCode.OK != response.StatusCode)
            {
                throw new Exception("Status code is not 200");
            }
            
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

        public void GetCoverPhotosId()
        {
            int bookNotFound = 0;
            int firstBook = 1;
                        
            {
                var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}/{bookNotFound}", Method.Get);
                var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
                var jsonResponse = JObject.Parse(response.Content);


                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                Assert.IsTrue(jsonResponse["type"].ToString().Contains("https"));
                Assert.IsTrue(jsonResponse["title"].ToString().Contains("Not Found"));
                Assert.IsTrue(jsonResponse["status"].ToString().Equals("404"), "Statuc number doesn't match");
                Assert.IsTrue(jsonResponse["traceId"].ToString().Contains("00-"));                
            }
            {                
                var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.coverPhotosEndpoint}/{firstBook}", Method.Get);
                var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
                var jsonResponse = JsonConvert.DeserializeObject<CoverPhotosBooks>(response.Content);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(firstBook, jsonResponse.id);
                Assert.AreEqual(firstBook, jsonResponse.idBook);
                Assert.IsTrue(jsonResponse.url.Contains("https://"), "Url in json body doesn't contains \"http\"");                
            }
        }
    }

    public class CoverPhotosBooks
    {
        public int id { get; set; }
        public int idBook { get; set; }
        public string ?url { get; set; }
    }
}