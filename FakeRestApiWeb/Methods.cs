using Microsoft.VisualBasic;
using System.Text.Json;

namespace FakeRestApiWeb
{
    public class Activities
    {
        public string id { get; set; } 
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
    }
}
