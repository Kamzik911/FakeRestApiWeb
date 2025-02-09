using System.Runtime.InteropServices;
using System.Text.Json;

namespace FakeRestApiWeb
{
    public class Activities
    {
        public int id { get; set; } 
        public string title { get; set; }
        public DateTime dueDate { get; set; }
        public bool completed { get; set; }        
    }

    public class Authors
    {
        public int id { get; set; }
        public int idBook { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Methods
    {
        RestClient client = new RestClient();
        Endpoints endpoints = new Endpoints();

        public void GetAllActivities()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}", Method.Get);
            var response = client.Execute(request);
            var jsonResponse = JArray.Parse(response.Content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Status code isn't 200");
            }
            Assert.AreEqual(JTokenType.Integer, jsonResponse.First?["id"]?.Type, "Id should be a number");
            Assert.AreEqual(JTokenType.String, jsonResponse.First?["title"]?.Type, "Title should be a number");
            Assert.AreEqual(JTokenType.Date, jsonResponse.First?["dueDate"]?.Type, "Date should be a number");
            Assert.AreEqual(JTokenType.Boolean, jsonResponse.First?["completed"]?.Type, "Completed should be true or false");
            Assert.IsNotNull(jsonResponse.First);
            Console.WriteLine(response.Content);
        }

        public void CreateActivity()
        {
            var responseBody = new
            {
                id = endpoints.zeroActivityId,
                title = "Activity 356",
                DueDate = DateTime.Now,
                completed = true
            };

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}", Method.Post).AddBody(responseBody);
            var response = client.Execute(request);
            var jsonResponse = JObject.Parse(response.Content);
            Assert.AreEqual(responseBody.id, jsonResponse["id"]);
            Assert.IsTrue(responseBody.completed);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }

        public void GetActivityId()
        {
            var responseBody = new
            {
                id = endpoints.firstActivityId,
                title = "Activity 1",                
                completed = false
            };

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}/{endpoints.firstActivityId}", Method.Get);
            var response = client.Execute(request);            
            var jsonResponse = JsonConvert.DeserializeObject<Activities>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);                        
            Assert.AreEqual(responseBody.id, jsonResponse?.id);
            Assert.AreEqual(responseBody.title, jsonResponse?.title);            
            Assert.AreEqual(responseBody.completed, jsonResponse?.completed);
            //Console.WriteLine(response.Content);
        }

        public void PutActivitesId()
        {
            var requestBody = new
            {
                id = endpoints.zeroActivityId,
                title = "Test",   
                dueDate = DateTime.Now,
                completed = true
            };

            var putRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}/{endpoints.zeroActivityId}", Method.Put).AddBody(requestBody);
            var response = client.Execute(putRequest);
            var data = JsonConvert.DeserializeObject<Activities>(response.Content);            

            if (HttpStatusCode.OK != response.StatusCode)
            {
                throw new Exception("Http status code doesn't match");
            }
            else if (requestBody.completed != data?.completed)
            {
                throw new Exception("Http status code doesn't match");

            }                
        }

        public void DeleteActivityId() 
        {
            var deleteRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}/{endpoints.zeroActivityId}", Method.Delete);
            var response = client.Execute(deleteRequest);

            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode);
        }

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

            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(objectBody.id, jsonResponse.id);
            Assert.AreEqual(objectBody.idBook, jsonResponse.idBook);
            Assert.AreEqual(objectBody.firstName, jsonResponse.firstName);
            Assert.AreEqual(objectBody.lastName, jsonResponse.lastName);
            Console.WriteLine(response.Content);
        }
    }
}
