using HelperServiceModels.Models;
using HelperServices.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewTask.Helpers;

namespace HelperServiceTests.Extensions
{
    [TestClass]
    public class ServiceHelperTests
    {
        [TestMethod]
        public void OpeningHours_NullService_ReturnEmptyString()
        {
            HelperServiceModel helperService = new HelperServiceModel();

            var result = ServiceHelpers.OpeningHours(helperService);
            Assert.AreEqual(result, string.Empty);

        }
    }
}
