namespace FakeRestApiWeb
{
    public class Books
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public string excerpt { get; set; }
        public DateTime publishDate { get; set; }
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
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();            
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

        public void CreateBook()
        {
            var objectSchema = new
            {
                id = 1,
                title = "Lord of the shoots",
                description = "Lord",
                pageCount = 1,
                excerpt = "Seti",
                publishDate = DateTime.Now,
            };
            DateTime later = DateTime.Now + TimeSpan.FromHours(0);

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}", Method.Post).AddBody(objectSchema);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JsonConvert.DeserializeObject<Books>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, jsonResponse.id);
            Assert.AreEqual("Lord of the shoots", jsonResponse.title);
            Assert.AreEqual("Lord", jsonResponse.description);
            Assert.AreEqual(1, jsonResponse.pageCount);
            Assert.AreEqual("Seti", jsonResponse.excerpt);
            Assert.IsNotNull(jsonResponse.publishDate);
        }

        public void GetBookId()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}/1", Method.Get);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, jsonResponse["id"]);
            Assert.AreEqual(JTokenType.Integer, jsonResponse["id"]?.Type);
        }

        public void UpdateBookId()
        {
            var bookBody = new
            {
                id = 2,
                title = "Lord of the shits",
                description = "Lorder",
                pageCount = 5,
                excerpt = "Setis",
                publishDate = DateTime.Now,
            };

            DateTime now = DateTime.Now;
            DateTime later = now + TimeSpan.FromHours(1.0);

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}/1", Method.Put).AddBody(bookBody);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var jsonResponse = JsonConvert.DeserializeObject<Books>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(2, jsonResponse.id);
            Assert.AreEqual("Lord of the shits", jsonResponse.title);
            Assert.AreEqual("Lorder", jsonResponse.description);
            Assert.AreEqual(5, jsonResponse.pageCount);
            Assert.AreEqual("Setis", jsonResponse.excerpt);
        }
        
        public void DeleteBookId()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}/2", Method.Delete);
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
