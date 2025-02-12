using RestSharp;
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

    public class ActivitiesMethods
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
    }
}
