using OpenQA.Selenium;

namespace Exercise
{
    internal class Helper
    {
        public void Login(IWebDriver driver, string UserName, string Password)
        {
            //Enter username
            IWebElement username = driver.FindElement(By.Name("username")); //By.XPath("/html/body/form/input[1]");
            username.SendKeys(UserName);

            //Enter the password
            IWebElement password = driver.FindElement(By.Name("password")); //By.XPath("/html/body/form/input[2]");
            password.SendKeys(Password);

            //Click the login button to login
            IWebElement LoginButton = driver.FindElement(By.XPath("/html/body/form/input[3]"));
            LoginButton.Click();
        }
    }
}