namespace FakeRestApiWeb
{
    [TestClass]
    public class AuthorsTests
    {
        Methods methods = new Methods();

        [TestMethod]
        public void GetAllAuthors_ShouldPass()
        {
            methods.GetAllAuthors();
        }

        [TestMethod]
        public void GetAuthorId_ShouldPass()
        {
            methods.GetAuthorId();
        }

        [TestMethod]
        public void CreateAuthor_ShouldPass()
        {
            methods.CreateAuthor();
        }
    }
}
