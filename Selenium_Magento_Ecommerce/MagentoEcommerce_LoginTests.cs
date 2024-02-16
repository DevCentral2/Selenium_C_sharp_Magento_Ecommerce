using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Selenium_Magento_Ecommerce
{
    public class Tests
    {
        string baseURL = "https://magento.softwaretestingboard.com/";

         IWebDriver webDriver;     //make this a member variable, so that it can be initialised in Setup() and is accessible to the Tests

        HomePage homePage;
        LoginPage loginPage;
        ShoppingPage shoppingPage;

        //bool HeadlessMode = false;

        [SetUp]
        public void Setup()
        {
            //if (HeadlessMode)  
            //{  InitialiseDriverHeadless();   }
            //else

            InitialiseDriver();
            InitialsePages();

    }

        public void InitialsePages()
        {
            homePage = new HomePage(webDriver);
            loginPage = new LoginPage(webDriver);
            shoppingPage = new ShoppingPage(webDriver);
        }

        public void InitialiseDriver()
        {     // Create a new instance of ChromeDriver
            webDriver = new ChromeDriver();
        }

        //public void InitialiseDriverHeadless()
        //{
        // // Set Chrome options to run in headless mode
        //ChromeOptions options = new ChromeOptions();
        //options.AddArgument("--headless"); // Add the headless argument
        //options.AddArgument("--disable-gpu"); // Disable GPU acceleration

        //// Create a new instance of ChromeDriver with options
        //IWebDriver webDriver = new ChromeDriver(options);
        //}

        public void NavigateToHomePage()
        {
            webDriver.Navigate().GoToUrl(baseURL);
            webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();    // Close the browser
        }


        [Test]
        public void HomePage_Navigation_VerifyCorrectURL()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();
        }

        [Test]
        public void LoginPage_VerifyPageElements()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();

            // Check presence of main site logo 
            homePage.verifyElementExists(homePage.storeLogo);
            // Check presence of Login link
            homePage.verifyElementExists(homePage.loginLinkCss);
            // Check presence of Shopping Cart link
            homePage.verifyElementExists(homePage.shoppingCart);
        }

        [Test]
        public void LoginPage_Navigation_VerifyCorrectURL()
        {
            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.VerifyLoginURL(baseURL);
        }

        [Test]
        public void LoginWithValidCredentialsSubmit_VerifySignedIn()
        {

            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(loginPage.validPassword);
            loginPage.SubmitLogin();
            loginPage.VerifySignedIn();

        }

        [Test]
        public void LoginWithValidCredentialsPressEnter_VerifySignedIn()
        {

            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(loginPage.validPassword);
            loginPage.PressEnter();
            loginPage.VerifySignedIn();

        }

        [Test]
        public void LoginWithInvalidPassword_VerifyNotSignedIn()
        {

            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterLoginDetails();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(loginPage.invalidPassword);
            loginPage.SubmitLogin();
            loginPage.VerifyNotSignedIn();

        }

        public void LoginWithInvalidPassword_VerifyInvalidPswdMsg()
        {

            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterLoginDetails();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(loginPage.invalidPassword);
            loginPage.SubmitLogin();
            loginPage.VerifyNotSignedIn();

        }

        [Test]
        public void LoginWithBlankPassword_VerifyNotSignedIn()
        {

            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(String.Empty);
            loginPage.SubmitLogin();
            loginPage.VerifyNotSignedIn();
        }

        [Test]
        public void LoginWithBlankPassword_VerifyCorrectMessage()
        {
            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.EnterUserName(loginPage.validUserName);
            loginPage.EnterPassword(String.Empty);
            loginPage.SubmitLogin();
            loginPage.VerifyRequiredPswdFieldErrorMessage();
        }
        [Test]

        public void LoginPage_ClickResetPswd_VerifyPswdResetForm()
        {
            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();
            loginPage.VerifyLoginURL(baseURL);

            loginPage.ClickPswdResetLink();
            loginPage.VerifyForgotPswdForm();

        }

        public void LoginPage_ClickResetPswd_VerifyPswdResetMsg()
        {
            homePage.NavigateToHomePage();
            homePage.ClickLoginLink();

            loginPage.VerifyLoginURL(baseURL);

            loginPage.ClickPswdResetLink();
            loginPage.VerifyForgotPswdForm();

            loginPage.EnterEmailAddress();
            loginPage.ClickPasswordReset();

            loginPage.VerifyPasswordResetMsg();
            loginPage.VeryifyPasswordResetMsgContainsEmailAddr();

        }

        string gender = "Women";
        string topsBottoms = "Tops";
        string typeOfClothing = "Tees";
        string itemToPurchase = "Desiree Fitness Tee";
        string size = "M";
        string colour = "Black";
        string itemPrice = "$24.00";

      [Test]

        public void ShoppingCart_AddItem_CorrectNumberOfItemsInCart()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();

            shoppingPage.SelectItemTypeFromPage(gender, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);


            //Verification

            //Assert number of items is blank before adding anything to the shopping cart
            Assert.That(shoppingPage.GetNumberOfItemsInShoppingCart().Equals(""), $"The number of items in shopping cart (before adding items) is expected to be ' ', but is currently {shoppingPage.GetNumberOfItemsInShoppingCart()}");

            shoppingPage.AddToCart();

            //Assert number of items is 1 after adding one item to the shopping cart
            Assert.That(shoppingPage.GetNumberOfItemsInShoppingCart(expectedNumberItems:"1").Equals("1"), $"The number of items in shopping cart (after adding items) is expected to be 1, but is currently {shoppingPage.GetNumberOfItemsInShoppingCart(expectedNumberItems: "1")}");
        }

        [Test]

        public void ShoppingCart_AddItem_ClickShoppingCartLink_NavigatesToShoppingCartPage()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();

            shoppingPage.SelectItemTypeFromPage(gender, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);
            shoppingPage.AddToCart();
            shoppingPage.ClickShoppingCartLink();

            //Verification

            // Assert whether the current URL contains the expected shopping cart page URL
            Assert.That(shoppingPage.WaitForShoppingCartPageUrl(), "The current URL is not the shopping cart URL.");

            // Assert that the page title element contains the text "Shopping Cart"
            Assert.That(shoppingPage.VerifyShoppingPageTitle(), "The page title does not contain 'Shopping Cart'.");
        }

        [Test]

        public void ShoppingCart_HoverMenus_AddItemToCart_NavigatesToShoppingPage()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();

            shoppingPage.SelectItemTypeFromMenus(gender, topsBottoms, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);
            shoppingPage.AddToCart();
            shoppingPage.ClickShoppingCartLink();

            //Verification

            // Assert whether the current URL contains the expected shopping cart page URL
            Assert.That(shoppingPage.WaitForShoppingCartPageUrl(), "The current URL is not the shopping cart URL.");

            // Assert that the page title element contains the text "Shopping Cart"
            Assert.That(shoppingPage.VerifyShoppingPageTitle(), "The page title does not contain 'Shopping Cart'.");
        }



        [Test]
        public void ShoppingCart_AddItem_CheckOrderTotalOnShoppingCartPage()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();

            shoppingPage.SelectItemTypeFromPage(gender, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);
            shoppingPage.AddToCart();
            shoppingPage.ClickShoppingCartLink();
      
            //Verification

            Assert.That(shoppingPage.GetTotalPriceShoppingCart(), Is.EqualTo(itemPrice), $"The Order Total is not correct. It it shows {shoppingPage.GetTotalPriceShoppingCart()} but should be {itemPrice}");

        }


        [Test]
        public void ShoppingCart_PlaceOrder_NavigatesToPaymentPage()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();
            homePage.ClickLoginLink();
            loginPage.LoginWithValidCredentials();

            shoppingPage.SelectItemTypeFromPage(gender, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);
            shoppingPage.AddToCart();

            //Verification

            shoppingPage.ClickShoppingCartLink();
            Assert.That(shoppingPage.WaitForShoppingCartPageUrl, "Not on Shopping Cart Page");

            shoppingPage.ClickProceedToCheckout();
            Assert.That(shoppingPage.WaitForShippingPageUrl, "Not on Shipping Page");

            shoppingPage.ClickNextButtonOnOrderPage();
            Assert.That(shoppingPage.WaitForPaymentPageUrl, "Not on Payment Page");

            shoppingPage.ClickPlaceOrder();
            Assert.That(shoppingPage.WaitForPaymentSuccessUrl, "Not on Payment Success Page");

        }

        [Test]
        public void ShoppingCart_PlaceOrder_VerifyCorrectItemOrdered()
        {

            homePage.NavigateToHomePage();
            homePage.VerifyHomePageURL();
            homePage.ClickLoginLink();
            loginPage.LoginWithValidCredentials();

            shoppingPage.SelectItemTypeFromPage(gender, typeOfClothing);
            shoppingPage.SelectItemChooseItemPage(itemToPurchase);
            shoppingPage.SelectItemSpecifics(size, colour);
            shoppingPage.AddToCart();

            shoppingPage.ClickShoppingCartLink();
            shoppingPage.WaitForShoppingCartPageUrl();

            shoppingPage.ClickProceedToCheckout();
            shoppingPage.WaitForShippingPageUrl();

            shoppingPage.ClickNextButtonOnOrderPage();
            shoppingPage.WaitForPaymentPageUrl();

            shoppingPage.ClickPlaceOrder();
            shoppingPage.WaitForPaymentSuccessUrl();

            //Verification

            shoppingPage.ClickOrderNumber();
            Assert.That(shoppingPage.VerifyCorrectItemOrdered(itemToPurchase));
        }

    }
}
