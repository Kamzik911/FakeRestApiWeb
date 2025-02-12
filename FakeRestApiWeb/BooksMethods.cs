using System.Collections.Immutable;

namespace FakeRestApiWeb
{
    public class Books
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string pageCount { get; set; }
    }

    public class BooksMethods
    {
        RestClient client = new RestClient();
        Endpoints endpoints = new Endpoints();

        public void GetAllBooks()
        {
            var arraySchema = new
            {
                id = 1,
                title = "string",
                description = "string",
                pageCount = 0,
                excerpt = "string",
                publishDate = DateTime.Now,
            };

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}", Method.Get);
            var response = client.Execute(request);            
            var arrayResponse = JArray.Parse(response.Content);
            var arrayResponseFirst = arrayResponse.First();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(arraySchema.id, arrayResponse.First?["id"]);
            Assert.AreEqual(JTokenType.Integer, arrayResponseFirst["id"]?.Type);
            Assert.AreEqual(JTokenType.String, arrayResponseFirst["title"]?.Type);
            Assert.AreEqual(JTokenType.String , arrayResponseFirst["description"]?.Type);
            Assert.AreEqual(JTokenType.Integer, arrayResponseFirst["pageCount"]?.Type);
            Assert.AreEqual(JTokenType.String, arrayResponseFirst["excerpt"]?.Type);
            Assert.AreEqual(JTokenType.Date, arrayResponseFirst["publishDate"]?.Type);
            Console.WriteLine(arrayResponse);
        }
    }
}
