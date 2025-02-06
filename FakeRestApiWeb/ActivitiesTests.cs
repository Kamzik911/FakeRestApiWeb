using RestSharp;

namespace FakeRestApiWeb
{
    [TestClass]
    public class ActivitiesTests
    {
        Methods methods = new Methods();

        [TestMethod]
        public void GetActivities_ShouldPass()
        {
            methods.GetAllActivities();
        }

        [TestMethod]
        public void PostActivities_ShouldPass()
        {
            methods.PostActivities();
        }
    }
}
