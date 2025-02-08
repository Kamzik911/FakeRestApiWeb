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

        [TestMethod]
        public void GetActivitiesId_ShouldPass()
        {
            methods.GetActivitiesId();
        }

        [TestMethod]
        public void PutActivitesId_ShouldPass()
        {
            methods.PutActivitesId();
        }
    }
}
