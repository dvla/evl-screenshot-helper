using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVL.Web.Test.Shared;

namespace SampleTests.Web.Validation
{
    [TestClass]
    public class ClientSlideValidation : ScreenshotManager
    {
        [TestMethod]
        public void MissingFieldWarning()
        {
            Browser.Navigate().GoToUrl("https://www.vehicleenquiry.service.gov.uk");
           
            Browser.FindElementByName("Continue").Click();

            //Results in a screenshot stored in <ScreenshotRootDirectory>\SampleTests\Web\Validation\MissingFieldWarning.png
            TakeScreenshot();           
        }
    }
}
