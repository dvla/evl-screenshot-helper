using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVL.Web.Test.Shared;

namespace SampleTests.Web.Quick
{
    [TestClass]
    public class SimpleWebTests : ScreenshotManager
    {

        /// <summary>
        /// These tests show how by calling the TakeScreenshot() method is called, a folder structure matching the namespace will be created
        /// and a screenshot of the page, named after the method, will be stored in it.
        /// </summary>

        [TestMethod]
        public void GovUkHomePageTest()
        {
            Browser.Navigate().GoToUrl("https://www.gov.uk/get-vehicle-information-from-dvla");

            string actual = Browser.FindElementByTagName("h1").Text;
            Assert.AreEqual("Get vehicle information from DVLA", actual);

            //Results in a screenshot stored in <ScreenshotRootDirectory>\SampleTests\Web\Quick\GovUkHomePageTest.png
            TakeScreenshot();
        }

        [TestMethod]
        public void VesHomePageTest()
        {
            Browser.Navigate().GoToUrl("https://www.vehicleenquiry.service.gov.uk");

            string actual = Browser.FindElementByClassName("heading-large").Text;
            Assert.AreEqual("Enter the registration number of the vehicle", actual);

            //Results in a screenshot stored in <ScreenshotRootDirectory>\SampleTests\Web\Quick\VesHomePageTest.png
            TakeScreenshot();

            //In order to take two screenshots in the same method, call TakeScreenShot again, but provide your own filename
            Browser.FindElementByLinkText("Cymraeg").Click();

            TakeScreenshot("VesHomePageTest_Welsh.png");
        }
    }
}
