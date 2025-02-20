namespace FakeRestApiWeb
{
    [TestClass]
    public class CoverPhotosTests
    {
        CoverPhotosMethods coverPhotosMethod = new CoverPhotosMethods();
                
        [TestMethod]
        public void GetAllCoverPhotos_ShouldPass()
        {
            coverPhotosMethod.GetAllCoverPhotos();
        }

        [TestMethod]
        public void ACreateCoverPhoto_ShouldPass()
        {
            coverPhotosMethod.CreateCoverPhoto();
        }

        [TestMethod]
        public void GetCoverPhotosBookId_ShouldPass()
        {
            coverPhotosMethod.GetCoverPhotosBookId();
        }       
        
        [TestMethod]
        public void GetCoverPhodosId_ShouldPass()
        {
            coverPhotosMethod.GetCoverPhotosId();
        }

        [TestMethod]
        public void BPutCoverPhotosId_ShouldPass()
        {
            coverPhotosMethod.PutCoverPhotosId();
        }

        [TestMethod]
        public void CDeleteCroverPhotosId_ShouldPass()
        {
            coverPhotosMethod.DeleteCoverPhotosId();
        }
    }
}
