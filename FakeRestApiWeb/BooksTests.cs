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

        [TestMethod]
        public void CreateBook_ShouldPass()
        {
            bookMethods.CreateBook();
        }

        [TestMethod]
        public void GetBookId_ShouldPass()
        {
            bookMethods.GetBookId();
        }

        [TestMethod]
        public void UpdateBookId_ShouldPass()
        {
            bookMethods.UpdateBookId();
        }

        [TestMethod]
        public void DeleteBookId_ShouldPass() 
        {
            bookMethods.DeleteBookId();
        }
    }
}
