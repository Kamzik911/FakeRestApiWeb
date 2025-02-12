namespace FakeRestApiWeb
{
    public class AuthorMethods
    {
        RestClient client = new RestClient();
        Endpoints endpoints = new Endpoints();

        public void GetAllAuthors()
        {
            var getRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.authorsEndpoint}", Method.Get);
            var response = client.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject<List<Authors>>(response.Content);
            var firstAuthor = jsonResponse[0];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(firstAuthor.id);
            Assert.IsNotNull(firstAuthor.idBook);
            Assert.IsNotNull(firstAuthor.firstName);
            Assert.IsNotNull(firstAuthor.lastName);
            Assert.AreEqual(1, firstAuthor.id);

        }       

        public void CreateAuthor()
        {
            var objectBody = new
            {
                id = 0,
                idBook = 0,
                firstName = "Junk",
                lastName = "Junkovi"
            };
            var getRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.authorsEndpoint}", Method.Post).AddBody(objectBody);
            var response = client.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject<Authors>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(objectBody.id, jsonResponse.id);
            Assert.AreEqual(objectBody.idBook, jsonResponse.idBook);
            Assert.AreEqual(objectBody.firstName, jsonResponse.firstName);
            Assert.AreEqual(objectBody.lastName, jsonResponse.lastName);
            Console.WriteLine(response.Content);
        }

        public void GetIdBook()
        {
            var getRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.booksEndpoint}/{endpoints.idBook1}", Method.Get);
            var response = client.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject<List<AuthorBooks>>(response.Content);
            var jsonFirst = jsonResponse[0];
            var jsonSecond = jsonResponse[1];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(1, jsonFirst.id);
            Assert.AreEqual(1, jsonFirst.idBook);
            Assert.AreEqual("First Name 1", jsonFirst.firstName);
            Assert.AreEqual("Last Name 1", jsonFirst.lastName);

            Assert.AreEqual(2, jsonSecond.id, "Id 2 doesn't match");
            Assert.AreEqual(1, jsonSecond.idBook, "Id book 1 doesn't match");
            Assert.AreEqual("First Name 2", jsonSecond.firstName, "First name 2 doesn't match");
            Assert.AreEqual("Last Name 2", jsonSecond.lastName, "Last name 2 doesn't match");
            Console.WriteLine(response);
        }

        public void GetAuthorId()
        {
            var getRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.authorsEndpoint}/1", Method.Get);
            var response = client.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject<Authors>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(1, jsonResponse.id);
            Assert.AreEqual(1, jsonResponse.idBook);
            Assert.AreEqual(endpoints.firstName1, jsonResponse.firstName);
            Assert.AreEqual(endpoints.lastName1, jsonResponse.lastName);
            Console.WriteLine(response.Content);
        }

        public void PutAuthorId()
        {
            var authorObject = new
            {
                id = 1,
                idBook = 1,
                firstName = "Author",
                lastName = "new"
            };

            var putRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.authorsEndpoint}/1", Method.Put).AddBody(authorObject);
            var response = client.Execute(putRequest);            
            var jsonResponse = JsonConvert.DeserializeObject<Authors>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(1, jsonResponse.id);
            Assert.AreEqual (1, jsonResponse.idBook);
            Assert.AreEqual("Author", jsonResponse.firstName);
            Assert.AreEqual("new", jsonResponse.lastName);
            Console.WriteLine(response.Content);
        }

        public void DeleteAuthorId() 
        {
            var putRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.authorsEndpoint}/1", Method.Delete);
            var response = client.Execute(putRequest);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);            
        }
    }

    public class Authors
    {
        public int id { get; set; }
        public int idBook { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class AuthorBooks
    {
        public int id { get; set; }
        public int idBook { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}

