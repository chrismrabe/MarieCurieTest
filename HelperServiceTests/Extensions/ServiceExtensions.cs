using HelperServiceModels.Models;
using HelperServices.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperServiceTests.Extensions
{
    [TestClass]
    public class ServiceExtensionsTests
    {
        [TestMethod]
        public void OpeningHours_NullService_ReturnEmptyString()
        {
            HelperServiceModel helperService = new HelperServiceModel();

            var result = ServiceExtensions.OpeningHours(helperService);
            Assert.AreEqual(result, string.Empty);

        }
    }
}
