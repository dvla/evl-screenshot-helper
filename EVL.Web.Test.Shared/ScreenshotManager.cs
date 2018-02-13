using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.IO;
using OpenQA.Selenium.Remote;
using System.Drawing.Imaging;

namespace EVL.Web.Test.Shared
{
    [TestClass]
    public class ScreenshotManager

    {
        private const string DEFAULT_IMAGE_EXTENSION = ".png";
        private const string DEFAULT_FOLDER_SEPARATOR = "\\";

        public RemoteWebDriver Browser { get; set; }
        private String ScreenshotRootDirectory { get; set; }

        [TestInitialize]
        public void baseInitialize()
        {
            GetApplicationSettings();

            Browser = new FirefoxDriver();
        }

        [TestCleanup]
        public void baseCleanUp()
        {
            Browser.Close();
            Browser.Dispose();
        }

        /// <summary>
        /// Take a screenshot of the current browser in PNG format
        /// </summary>
        /// <remarks>Uses the current test method name and namespace as the filename and folder structure</remarks>
        public void TakeScreenshot()
        {
            StackTrace stackTrace = new StackTrace();
            string callingMethodName = stackTrace.GetFrame(1).GetMethod().Name;
            string folderStructure = BuildFolderStruture();
            string fileName = callingMethodName + DEFAULT_IMAGE_EXTENSION;

            TakeScreenshot(folderStructure, fileName, ImageFormat.Png);

        }

        /// <summary>
        /// Take a screenshot of the current browser
        /// </summary>
        /// <remarks>Uses the current test namespace as the folder structure</remarks>
        /// <param name="fileName">The filename of the screenshot (With Extension)</param>
        public void TakeScreenshot(String fileName)
        {
            string folderStructure = BuildFolderStruture();
            ImageFormat imageFormat = Utilities.DetermineImageFormat(fileName);
            TakeScreenshot(folderStructure, fileName,imageFormat);

        }

        /// <summary>
        /// Take a screenshot of the current browser.
        /// </summary>
        ///<param name="folderStructure">The directory in which to store the screenshots</param>
        ///<param name="fileName">The filename of the screenshot (With Extension)</param>
        ///<param name="imageFormat">The image format to use</param>
        public void TakeScreenshot(string folderStructure, string fileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            string fullFilePath = string.Format("{0}{1}", folderStructure, fileName);

            Browser.GetScreenshot().SaveAsFile(fullFilePath, imageFormat);
        }


        /// <summary>
        /// Generates the folder structure, based on the full namespace of the calling test
        /// </summary>
        /// <returns></returns>
        private string BuildFolderStruture()
        {
            StackTrace stackTrace = new StackTrace();
            string callingMethodNamespace = stackTrace.GetFrame(2).GetMethod().ReflectedType.FullName;
            string folderStruture = ScreenshotRootDirectory + callingMethodNamespace.Replace(".", DEFAULT_FOLDER_SEPARATOR) + DEFAULT_FOLDER_SEPARATOR;

            CreateFolderStructure(folderStruture);

            return folderStruture;
        }

        /// <summary>
        /// If it doesn't exist, creates the folder structure to store the screenshots
        /// </summary>
        /// <param name="folderStructure"></param>
        private void CreateFolderStructure(string folderStructure)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(folderStructure);
            if (!file.Directory.Exists) {
                file.Directory.Create();
            }
        }


        /// <summary>
        /// Loads settings from App.Config
        /// </summary>
        private void GetApplicationSettings()
        {

            string keyName = "";

            try
            {
                keyName = "ScreenshotRootDirectory";
                ScreenshotRootDirectory = ConfigurationManager.AppSettings[keyName].ToString();

             }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(string.Format("{0} not found in App.Config",keyName));
            }
        }

    }

}
