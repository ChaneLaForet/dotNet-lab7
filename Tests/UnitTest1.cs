using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class UnitTest1
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetupDriver()
        {
            _driver = new ChromeDriver("C:\\Users\\HP\\Documents\\chrome-driver");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }

        [Test]
        public void MovieTitleExists()
        {
            _driver.Url = "http://localhost:4200/movies";

            try
            {
                _driver.FindElement(By.XPath("//ion-title"));
                Assert.Pass("Movie title found.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Movies title not found.");
            }
        }

        [Test]
        public void MovieTitleExists2()
        {
            _driver.Url = "http://localhost:4200/movies";
            bool foundTitle = false;

            foreach (var ionLabel in _driver.FindElements(By.TagName("ion-label")))
            {
                Console.WriteLine(ionLabel.Text);
                if (ionLabel.Text == "The Shawshank Redemption")
              //if (ionLabel.Text == "title")
                    {
                    foundTitle = true;
                    break;
                }

            }
            Assert.IsTrue(foundTitle);
        }

        [Test]
        public void MovieTitleInMovieDetailsExists()
        {
            _driver.Url = "http://localhost:4200/movies";
            bool foundTitle = false;

            //var arrow = _driver.FindElement(By.TagName("ion-icon"));
            var arrow = _driver.FindElement(By.XPath("//*[@id='content1']/app-movies/ion-content/ion-list/ion-item[1]/div"));   //functioneaza
            //var arrow = _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-movies/ion-content/ion-list/ion-item[1]/div"));
            //var arrow = _driver.FindElement(By.ClassName("ionicon-fill-none"));
            Thread.Sleep(300);
            arrow.Click();

            try
            {
                foreach (var span in _driver.FindElements(By.TagName("span")))
                {
                    if (span.Text == "Title") {
                        foundTitle = true;
                        break;
                    }
                }
            }catch (NoSuchElementException)
            {
                Assert.Fail("The Title field was not found.");
            }
            Assert.IsTrue(foundTitle);
        }
    }
}