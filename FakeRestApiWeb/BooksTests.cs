namespace FakeRestApiWeb
{
    [TestClass]
    public class BooksTests
    {
        BooksMethods bookMethods = new BooksMethods();

        [TestMethod]
        public void GetAllBooks_ShouldPass()
        {
            bookMethods.GetAllBooks();
        }
    }
}
