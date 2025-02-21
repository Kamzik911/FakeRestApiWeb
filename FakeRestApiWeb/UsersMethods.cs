using Microsoft.Extensions.DependencyModel;

namespace FakeRestApiWeb
{
    public class UsersMethods
    {
        RestClient client = new RestClient();
        Endpoints endpoints = new Endpoints();

        public void GetAllUsers()
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.usersMainEndpoint}", Method.Get);
            var response = client.ExecuteAsync(request).Result;
            var jsonResponse = JArray.Parse(response.Content).First();            

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, jsonResponse["id"]);
            Assert.AreEqual(JTokenType.Integer, jsonResponse["id"]?.Type);            
            Assert.AreEqual("User 1", jsonResponse["userName"]);
            Assert.AreEqual(JTokenType.String, jsonResponse["userName"]?.Type);
            Assert.AreEqual("Password1", jsonResponse["password"]);
            Assert.AreEqual(JTokenType.String, jsonResponse["password"]?.Type);
            Assert.IsTrue(jsonResponse?["password"]?.ToString().Contains("1"));
        }

        public void CreateUser()
        {
            var objectBody = new
            {
                id = 1,
                userName = "Vendula",
                password = "pass"
            };
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.usersMainEndpoint}", Method.Post).AddBody(objectBody);
            var response = client.ExecuteAsync(request).Result;
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(objectBody.id, jsonResponse["id"]);
            Assert.AreEqual(JTokenType.Integer, jsonResponse["id"]?.Type);
            Assert.AreEqual(objectBody.userName, jsonResponse["userName"]);
            Assert.AreEqual(JTokenType.String, jsonResponse["userName"]?.Type);
            Assert.AreEqual(objectBody.password, jsonResponse["password"]);
            Assert.AreEqual(JTokenType.String, jsonResponse["password"]?.Type);            
        }

        public void GetUserId(int userId, string userName)
        {
            var request = new RestRequest($"{endpoints.mainEndpoint}{endpoints.usersMainEndpoint}/{userId}", Method.Get);
            var response = client.ExecuteAsync(request).Result;
            var jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(JTokenType.Integer, jsonResponse["id"]?.Type);
            Assert.AreEqual(userId, jsonResponse["id"]);
            Assert.AreEqual(userName, jsonResponse["userName"]);
            Console.WriteLine(jsonResponse);
        }
    }
}
