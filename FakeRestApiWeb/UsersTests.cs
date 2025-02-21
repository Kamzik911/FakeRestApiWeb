namespace FakeRestApiWeb
{
    [TestClass]
    public class UsersTests
    {
        UsersMethods usersMethods = new UsersMethods();

        [TestMethod]
        public void GetAllUsers_ShouldPass()
        {
            usersMethods.GetAllUsers();
        }

        [TestMethod]
        public void CreateUser_ShouldPass()
        {
            usersMethods.CreateUser();
        }

        [DataTestMethod]
        [DataRow(1, "User 1")]
        [DataRow(2, "User 2")]
        public void GetUserId_ShouldPass(int userId, string userName)
        {
            usersMethods.GetUserId(userId, userName);
        }
    }
}
