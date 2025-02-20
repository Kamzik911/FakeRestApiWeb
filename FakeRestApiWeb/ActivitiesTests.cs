using System.Data;

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

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void ACreateActivity_ShouldPass(bool cTrueFalse)
        {
            methods.CreateActivity(cTrueFalse);
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
