using RestSharp;

namespace FakeRestApiWeb
{
    [TestClass]
    public class ActivitiesTests
    {
        ActivitiesMethods methods = new ActivitiesMethods();

        [TestMethod]
        public void GetActivities_ShouldPass()
        {
            methods.GetAllActivities();
        }

        [TestMethod]
        public void ACreateActivity_ShouldPass()
        {
            methods.CreateActivity();
        }

        [TestMethod]
        public void BGetActivityId_ShouldPass()
        {
            methods.GetActivityId();
        }

        [TestMethod]
        public void PutActivitesId_ShouldPass()
        {
            methods.PutActivitesId();
        }

        [TestMethod]
        public void CDeleteActivitesId_ShouldPas()
        {
            methods.DeleteActivityId();
        }
    }
}
