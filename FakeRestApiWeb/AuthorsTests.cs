namespace FakeRestApiWeb
{
    [TestClass]
    public class AuthorsTests
    {
        AuthorMethods methods = new AuthorMethods();

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

        [TestMethod]
        public void GetIdBook_ShouldPass()
        {
            methods.GetIdBook();
        }
    }
}
