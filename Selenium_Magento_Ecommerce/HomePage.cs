using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;

namespace Selenium_Magento_Ecommerce
{
    class HomePage
    {
        public IWebDriver webDriver;
        // constructor, passes in the webdriver that is initialised in Setup, in order to use it in the page class

        public HomePage(IWebDriver _webdriver)
        {
            webDriver = _webdriver;
        }

        public string baseURL = "https://magento.softwaretestingboard.com/";

        public string storeLogo = "[aria-label='store logo']";
        public string loginLinkCss = "li.authorization-link";
        public string shoppingCart = "a.action.showcart";

        public void NavigateToHomePage()
        {
            webDriver.Navigate().GoToUrl(baseURL);
            webDriver.Manage().Window.Maximize();
        }

        public void ClickLoginLink()
        {

            IWebElement loginLink = webDriver.FindElement(By.CssSelector(loginLinkCss));
            loginLink.Click();

        }

        public void verifyElementExists(string elementCss)
        {
            IList<IWebElement> elements = webDriver.FindElements(By.CssSelector(elementCss));
            Assert.That(elements.Count > 0);
        }

        public void VerifyHomePageURL()
        {
            string actualURL = webDriver.Url;
            Assert.That(actualURL, Is.EqualTo(baseURL));
        }




    }


}
