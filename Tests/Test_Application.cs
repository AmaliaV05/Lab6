using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    class Test_Application
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupDriver()
        {
            _driver = new ChromeDriver("C:/Users/Asus/Downloads/");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }

        [Test]
        public void LoginIsSuccessful()
        {
            _driver.Url = "https://localhost:5001/Identity/Account/Login";

            var email = _driver.FindElement(By.XPath("//*[@id='Input_Email']"));
            email.SendKeys("test2@test.com");

            var password = _driver.FindElement(By.XPath("//*[@id='Input_Password']"));
            password.SendKeys("Gintama10!");

            System.Threading.Thread.Sleep(2000);

            var loginButton = _driver.FindElement(By.XPath("//*[@id='account']/div[5]/button"));
            loginButton.Click();

            System.Threading.Thread.Sleep(2000);

            string actualUrl = "http://localhost:5001";
            string expectedUrl = _driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void RegistrationIsSuccessful()
        {
            _driver.Url = "https://localhost:5001/Identity/Account/Register";

            var email = _driver.FindElement(By.XPath("//*[@id='Input_Email']"));
            email.SendKeys("test3@test.com");

            var password = _driver.FindElement(By.XPath("//*[@id='Input_Password']"));
            password.SendKeys("e5h4q5jAAdfr4556**");

            var confirmPassword = _driver.FindElement(By.XPath("//*[@id='Input_ConfirmPassword']"));
            confirmPassword.SendKeys("e5h4q5jAAdfr4556**");

            System.Threading.Thread.Sleep(2000);

            var registerButton = _driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[1]/form/button"));
            registerButton.Click();

            System.Threading.Thread.Sleep(2000);

            string actualUrl = "https://localhost:5001/Identity/Account/RegisterConfirmation?email=test3@test.com&returnUrl=%2F";
            string expectedUrl = _driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

    }
}
