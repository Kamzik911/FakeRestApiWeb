namespace FakeRestApiWeb
{
    public class Endpoints
    {
        //Main endpoint
        public string mainEndpoint = "https://fakerestapi.azurewebsites.net/";

        //Activities
        public string activitiesEndpoint = "api/v1/Activities";
        public int zeroActivityId = 0;
        public int firstActivityId = 1;

        //Authors
        public string authorsEndpoint = "api/v1/Authors";
        public string firstName1 = "First Name 1";
        public string lastName1 = "Last Name 1";
               
        //Books
        public string authorBooksEndpoint = "api/v1/Authors/authors/books";
        public string booksEndpoint = "api/v1/Books";        

        //CoverPhotos
        public string coverPhotosEndpoint = "api/v1/CoverPhotos";
    }
}
