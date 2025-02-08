using Microsoft.VisualBasic;
using System.Text.Json;

namespace FakeRestApiWeb
{
    public class Activities
    {
        public string id { get; set; } 
        public DateTime dueDate { get; set; }
        public bool completed { get; set; }
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
        }

        public void PostActivities()
        {
            var activitesObject = new
            {
                id = 0,
                title = "string",
                DueDate = DateTime.Now,
                completed = true
            };

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}", Method.Post).AddBody(activitesObject);
            var response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
            var jsonResponse = JObject.Parse(response.Content);
            Assert.IsNotNull(string.IsNullOrEmpty(jsonResponse["id"]?.ToString()));
            Assert.AreEqual(activitesObject.id, jsonResponse["id"]);
            Assert.IsTrue(activitesObject.completed);
        }

        public void GetActivitiesId()
        {
            var responseBody = new
            {
                id = 1,
                title = "Activity 1",
                completed = "false"
            };

            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}/1", Method.Get);
            var response = client.Execute(request);
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);            
            Assert.AreEqual(responseBody.id, jsonResponse["id"]);
            Assert.AreEqual(responseBody.title, jsonResponse["title"]);
            //Assert.AreEqual(responseBody.completed, jsonResponse["false"]);
            Console.WriteLine(response.Content);
        }

        public void PutActivitesId()
        {
            var requestBody = new
            {
                id = 0,
                title = "Test",   
                DueDate = DateTime.Now,
                completed = true
            };

            var putRequest = new RestRequest($"{endpoints.mainEndpoint}{endpoints.activitiesEndpoint}/0", Method.Put).AddBody(requestBody);
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
            Console.WriteLine(response.Content);
            
        }
    }
}
