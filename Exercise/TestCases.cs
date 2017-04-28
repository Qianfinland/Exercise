using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Exercise
{
    internal class TestCases
    {
        private IWebDriver driver = new ChromeDriver();
        private Helper instanceHelper = new Helper();
        private string filePath = "file:///C:/Users/Qian.Zhou/Documents/2017/AutomationTestExercise/exercise/htmlFiles/";

        [SetUp]
        public void Initialize()
        {
            // go to the url
            driver.Navigate().GoToUrl(filePath + "index.html");
        }

        #region Test Login Function

        [Test(Description = "Positive case: Login with valid username and password")]
        [TestCase("admin", "1234")]
        public void LoginWithValidCredentials(string username, string password)
        {
            instanceHelper.Login(driver, username, password);

            //Check Login Page content
            //Check Login URL
            string currentUrl = driver.Url.ToString();
            Assert.That(currentUrl, Is.EqualTo(filePath + "main.html"));
            IWebElement H1Text = driver.FindElement(By.TagName("h1"));
            Assert.That(H1Text.Text, Is.EqualTo("Main Page"));
        }

        [Test(Description = "Negative case: Login with invalid credentials")]
        [TestCase("wrongUser", "1234")]
        [TestCase("admin", "wrongPassword")]
        [TestCase("wrongUser", "WrongPassword")]
        public void LoginWithInValidCredentials(string username, string password)
        {
            instanceHelper.Login(driver, username, password);

            //Check error page content
            //Check url
            string currentUrl = driver.Url.ToString();
            Assert.That(currentUrl, Is.EqualTo(filePath + "error.html"));

            //Check H1 Text
            IWebElement H1 = driver.FindElement(By.TagName("h1"));
            Assert.That(H1.Text, Is.EqualTo("Error Page"));
        }

        #endregion Test Login Function

        #region Test Logout Function

        [Test(Description = "Test logout")]
        public void Test_LogOut()
        {
            //Precondition: Login first
            instanceHelper.Login(driver, "admin", "1234");

            //Click the logout text to logout
            IWebElement Logout = driver.FindElement(By.LinkText("logout"));
            Logout.Click();

            //Check page content after logout
            string currentUrl = driver.Url.ToString();
            Assert.That(currentUrl, Is.EqualTo(filePath + "index.html"));

            //Check login hint message
            IWebElement TextMessage = driver.FindElement(By.XPath("/html/body/p"));
            Assert.That(TextMessage.Text, Is.EqualTo("To login enter user name and password and click the login button."));
        }

        #endregion Test Logout Function

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}