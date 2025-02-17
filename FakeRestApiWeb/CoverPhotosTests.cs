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
    }
}
