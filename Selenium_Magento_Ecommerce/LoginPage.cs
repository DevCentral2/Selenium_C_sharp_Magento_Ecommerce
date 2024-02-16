using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium_Magento_Ecommerce
{
    public class LoginPage
    {

        public IWebDriver webDriver;

        // constructor, passes in the webdriver that is initialised in Setup, in order to use it in the page class
        public LoginPage(IWebDriver _webdriver)

        { webDriver = _webdriver; }


        public string validUserName = "grand.quivara@gmail.com";
        public string validPassword = "EcommercePracticeSite22";
        public string invalidPassword = "InvalidPassword";
        public string sampleEmailAddrCss = "PasswordResetEmail@Test.com";

        string userNameFieldCss = "#email.input-text";
        string passwordFieldCss = "#pass.input-text";
        string signInbutton = "#send2.action.login.primary";
        string signedInLinkCss = "span.logged-in";

        string emailErrorMsg = "#email-error";
        string passwordErrorMsg = "#pass-error";
        string signInerrorMsg = ".message-error.error.message";

        string forgotPasswordLink = ".action.remind";
        string forgotPasswordFormCss = ".form.password.forget";
        string userNameorPswdLoginError = "div.message-error.error.message";

        string pswdResetButtonCss = ".action.submit.primary";
        string pswdResetEmailAddrFieldCss = "#email_address";


        //url
        public string loginPagePartURL = "customer/account/login/";

        // messages
        string incorrectSigninMsg = "The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.";
        string loginAndPswdRequiredMsg = "A login and a password are required.";
        string pswdResetLinkSentMsg = "you will receive an email with a link to reset your password.";

        // methods

        public void LoginWithValidCredentials()
        {
            EnterUserName(validUserName);
            EnterPassword(validPassword);
            SubmitLogin();
            VerifySignedIn();
        }

        public void VerifyLoginURL(string baseURL)
        {
            string actualURL = webDriver.Url;
            string expectedURL = baseURL + loginPagePartURL;
            Assert.That(actualURL.Contains(expectedURL));
        }

        public void EnterUserName(string userName)
        {
            // Find the username and password input fields and enter credentials
            IWebElement usernameField = webDriver.FindElement(By.CssSelector(userNameFieldCss));
            usernameField.SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            IWebElement passwordField = webDriver.FindElement(By.CssSelector(passwordFieldCss));
            passwordField.SendKeys(password);

        }

        public void EnterLoginDetails()
        {

            // Find the username and password input fields and enter credentials
            IWebElement usernameField = webDriver.FindElement(By.CssSelector(userNameFieldCss));
            usernameField.SendKeys(validUserName);

            IWebElement passwordField = webDriver.FindElement(By.CssSelector(passwordFieldCss));
            passwordField.SendKeys(validPassword);

        }

        public void SubmitLogin()
        {
            // Find and click the login button
            IWebElement loginButton = webDriver.FindElement(By.CssSelector(signInbutton));
            loginButton.Click();
        }

        public void ClickPswdResetLink()
        {
            // Find and click the login button
            IWebElement pswdResetLink = webDriver.FindElement(By.CssSelector(forgotPasswordLink));
            pswdResetLink.Click();
        }

        public void PressEnter()
        {
            IWebElement passwordField = webDriver.FindElement(By.CssSelector(passwordFieldCss));
            passwordField.SendKeys(Keys.Enter);

        }

        public void EnterEmailAddress()
        {
            IWebElement pswdResetEmailAddrField = webDriver.FindElement(By.CssSelector(pswdResetEmailAddrFieldCss));
            pswdResetEmailAddrField.SendKeys(sampleEmailAddrCss);
        }

        public void ClickPasswordReset()
        {
            IWebElement pswdResetButton = webDriver.FindElement(By.CssSelector(pswdResetButtonCss));
            pswdResetButton.Click(); 
        }

        public void VerifySignedIn()
        {

            IList<IWebElement> signedInLink = webDriver.FindElements(By.CssSelector(signedInLinkCss));
            Assert.That(signedInLink.Count > 0);
        }

        public void VerifyNotSignedIn()
        {
            IList<IWebElement> signedInLink = webDriver.FindElements(By.CssSelector(signedInLinkCss));
            Assert.That(signedInLink.Count == 0);
        }

        public void VerifyIncorrectSignInMessage()
        {
            IList<IWebElement> signedInLinks = webDriver.FindElements(By.CssSelector(incorrectSigninMsg));
            Assert.That(signedInLinks.Count > 0);
        }

        public void VerifyLoginAndPswdRequiredMessage()
        {
            IList<IWebElement> logindPswdRequired = webDriver.FindElements(By.CssSelector(loginAndPswdRequiredMsg));
            Assert.That(logindPswdRequired.Count > 0);

        }

        public void VerifyRequiredPswdFieldErrorMessage()
        {
            IList<IWebElement> requiredPswdField = webDriver.FindElements(By.CssSelector(passwordErrorMsg));
            Assert.That(requiredPswdField.Count > 0);

        }

        public void VerifyForgotPswdForm()
        {
            IList<IWebElement> forgotPasswordForm =  webDriver.FindElements(By.CssSelector(forgotPasswordFormCss));
            Assert.That(forgotPasswordForm.Count > 0);
        }

        public void VerifyPasswordResetMsg()
        {
            IList<IWebElement> pswdResetLinkSentMsgs = webDriver.FindElements(By.CssSelector(pswdResetLinkSentMsg));
            Assert.That(pswdResetLinkSentMsgs.Count > 0);
        }

        public void VeryifyPasswordResetMsgContainsEmailAddr()
        {
            IList<IWebElement> sampleEmailAddr = webDriver.FindElements(By.CssSelector(sampleEmailAddrCss));
            Assert.That(sampleEmailAddr.Count > 0);
        }

    }
}
