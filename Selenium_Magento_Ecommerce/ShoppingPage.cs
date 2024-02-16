using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

namespace Selenium_Magento_Ecommerce
{

    class ShoppingPage
    {


        private IWebDriver webDriver;
        private WebDriverWait wait;

        const int waitTimeforWebDriverWait = 10;

        // Constructor
        public ShoppingPage(IWebDriver _driver)
        {
            this.webDriver = _driver;
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitTimeforWebDriverWait));
        }

        public string shippingPageLogo = "Shipping";
        public string shoppingCartLogo = "Shopping Cart";
        public string searchEntireStore = "Search entire store here";
        public string quantityField = "Qty['spinbutton']";

        // confirmation or warning messages
        public string thankYouForYourPurchase = "Thank you for your purchase!";
        public string quantityExceedsMaxPurchase = "The maximum you may purchase is 10000.";
        public string quantityExceededInShoppingCart = "The requested qty exceeds the maximum qty allowed in shopping cart";
        public string quantityNotAvailable = "The requested qty is not available";
        public string youHaveNoItemsInCart = "You have no items in your shopping cart.";


        // Page Methods
        public void SelectItemTypeFromMenus(string gender, string topsBottoms, string typeOfClothing)
        {
            HoverOverMenuItem(gender);
            HoverOverMenuItem(topsBottoms);
            HoverOverMenuItem(typeOfClothing);
            ClickMenuItem(typeOfClothing);
        }

        public void SelectItemTypeFromPage(string gender, string typeOfClothing)
        {
            ClickMenuItem(gender);
            ClickLeftMenuItem(typeOfClothing);
        }


        public void SelectFirstItem()
        {
            webDriver.FindElement(By.CssSelector(".product-image-photo")).Click();
        }

        public void SelectItemChooseItemPage(string itemToPurchase)
        {
            webDriver.FindElement(By.CssSelector(".product-item-link")).Click();
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product-item-link"))).Click();
        }

        public void SelectItemSpecifics(string size, string colour)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("product-options-wrapper")));

            string cssSelectorSize = $"[option-label='{size}']";

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelectorSize)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelectorSize)))
                .Click();


            string cssSelectorColour = $"[option-label='{colour}']";

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelectorColour)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelectorColour)))
                .Click();

            //webDriver.FindElement(By.CssSelector($"[option-label='{colour}']")).Click();
        }

        public void AddToCart()
        {
            webDriver.FindElement(By.Id("product-addtocart-button")).Click();
        }

        public void ClickShoppingCartLink()
        {
                 wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(), 'shopping cart')]")))
                .Click();
    }

        public void ClickProceedToCheckout()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("li > button.action.primary.checkout")))
                .Click();
        }

        public void ClickNextButtonOnOrderPage()
        {
            string cssSelectorNextBtn = ".button.action.continue.primary";
            IWebElement nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelectorNextBtn)));
            ScrollIntoView(nextButton);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelectorNextBtn)));
            Thread.Sleep(500);
            nextButton.Click();
        }

        public void ClickPlaceOrder()
        {
            string cssSelectorPlaceOrdrBtn = "button.action.primary.checkout";
            IWebElement placeOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelectorPlaceOrdrBtn)));
            ScrollIntoView(placeOrderButton);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelectorPlaceOrdrBtn)));
            Thread.Sleep(500);
            placeOrderButton.Click();

        }

        public void SetQuantity(string quantity)
        {
            IWebElement qtyInput = webDriver.FindElement(By.CssSelector("input#qty.input-text.qty"));
            qtyInput.Clear();
            qtyInput.SendKeys(quantity);
        }

        public void ClickOrderNumber()
        {
            webDriver.FindElement(By.CssSelector(".order-number")).Click();
        }

        public void SearchByProductId(string productId)
        {
            IWebElement searchInput = webDriver.FindElement(By.CssSelector("#search.input-text"));
            searchInput.Clear();
            searchInput.SendKeys(productId + Keys.Enter);
        }


        // Helper Methods

        private IWebElement GetMenuItemElement(string menuItemText)
        {
         
            string xPathSelector = $"//a/span[contains(text(),'{menuItemText}')]";
            IWebElement menuItem = wait.Until(ExpectedConditions.ElementExists(By.XPath(xPathSelector)));
            
            // this explicit wait is an extra safeguard because the element exists before it is visible
            wait.Until(ExpectedConditions.TextToBePresentInElement(menuItem, menuItemText));

            return menuItem;
        }

        private IWebElement GetLeftMenuItemElement(string menuItemText)
        {
            string xPathSelector = $"//li/a[contains(text(),'{menuItemText}')]";
            IWebElement menuItem = wait.Until(ExpectedConditions.ElementExists(By.XPath(xPathSelector)));
            
            // this explicit wait is an extra safeguard because the element exists before it is actually visible
            wait.Until(ExpectedConditions.TextToBePresentInElement(menuItem, menuItemText));

            return menuItem;
        }

        private void HoverOverMenuItem(string menuItemText)
        {


            IWebElement menuItem = GetMenuItemElement(menuItemText);

            // Normally we try not to use Thread.sleep, however this is needed here because the wait for text to be present isn't always effective
            Thread.Sleep(400);  
   
            Actions action = new Actions(webDriver);
            action.MoveToElement(menuItem).Perform();
        }



        private void ClickMenuItem(string menuItemText)
        {
            IWebElement menuItem = GetMenuItemElement(menuItemText);
            wait.Until(ExpectedConditions.ElementToBeClickable(menuItem));
            menuItem.Click();
        }

        private void ClickLeftMenuItem(string itemText)
        {
            IWebElement link = GetLeftMenuItemElement(itemText);
            wait.Until(ExpectedConditions.ElementToBeClickable(link));
            link.Click();
        }


        // Verifies shipping page URL
        public bool WaitForShippingPageUrl()
        {
            bool URLFound = wait.Until(ExpectedConditions.UrlContains("checkout/#shipping"));
            return URLFound; 
        }

        public bool VerifyShippingPageMainElement()
        {
            // Wait for the shipping element to be visible
            IWebElement shippingElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#shipping")));
            
            // Return true if the element is visible
            return (shippingElement != null);

        }

            public bool VerifyShoppingPageTitle()

        { // Find the page title element using its CSS class
            IWebElement pageTitleElement = webDriver.FindElement(By.CssSelector(".page-title"));
            return pageTitleElement.Text.Contains("Shopping Cart");
        }

        // Verifies shopping cart page URL
        public bool WaitForShoppingCartPageUrl()
        {
            bool URLFound = wait.Until(ExpectedConditions.UrlContains("checkout/cart"));
            return URLFound;
        }

        // Verifies payment page
        public bool WaitForPaymentPageUrl() 
        {
            bool URLFound = wait.Until(ExpectedConditions.UrlContains("checkout/#payment"));
            return URLFound;
        }

        public bool WaitForPaymentSuccessUrl()
        {
            bool URLFound = wait.Until(ExpectedConditions.UrlContains("checkout/onepage/success"));
            return URLFound;
        }

        // Verifies order complete
        public bool VerifyOrderComplete()
        {
            return webDriver.FindElement(By.CssSelector(".title")).Text.Contains("Order Summary");
        }
             
        // Verifies thank you for your purchase
        public bool VerifyThankYouForYourPurchase()
        {
            IWebElement thankYouElement = webDriver.FindElement(By.CssSelector(".page-title"));
            string thankYouTest = thankYouElement.Text;
            bool thankYouFound = wait.Until(ExpectedConditions.TextToBePresentInElement(thankYouElement,"Thank you"));
            return thankYouFound;

        }

        // Verifies shopping cart is empty
        public bool VerifyShoppingCartIsEmpty()
        {
            return webDriver.FindElement(By.CssSelector(".counter.qty.empty")).Displayed;
        }

        // Verifies correct item is ordered
        public bool VerifyCorrectItemOrdered(string itemToPurchase)
        {
            return webDriver.FindElement(By.CssSelector(".product-item-name")).Text.Contains(itemToPurchase);
        }


        // Construct the XPath to locate the element containing the item price
        string xpathShoppingCartTotalPrice = $"//td[contains(@class, 'amount') and .//*[contains(@class, 'price')]]";


        public bool VerifyOrderTotalIsCorrectShoppingCart(string itemPrice)
        {
            try
            {
                // Construct the XPath to locate the element containing the item price
                string xpathSelector = $"//td[contains(@class, 'amount') and .//*[contains(@class, 'price')]]";

                // Find the element using XPath

                IWebElement totalPriceElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathShoppingCartTotalPrice)));

                // Get the text content of the element
                string totalPriceText = totalPriceElement.Text;

                // Return true if the item price is found in the text content
                if (totalPriceText.Contains(itemPrice))
                    return 
                        true;
                else return 
                        false;
            }
            catch (NoSuchElementException)
            {
                // Return false if the element is not found
                return false;
            }
        }

        public string GetTotalPriceShoppingCart()
        {
               
            IWebElement totalPriceElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathShoppingCartTotalPrice)));

                     // Get the text content of the element
                string totalPriceText = totalPriceElement.Text;

                return totalPriceText;
        }

        public string GetNumberOfItemsInShoppingCart()
        {
            

            IWebElement numberOfTemsInCartElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".counter-number")));
          
            // Get the text content of the element
            string numberOfTemsInCart = numberOfTemsInCartElement.Text;

            return numberOfTemsInCart;
        }

        public string GetNumberOfItemsInShoppingCart(string expectedNumberItems)
        {

            IWebElement numberOfItemsInCartElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".counter-number")));
            bool numItemsAsExpected = wait.Until(ExpectedConditions.TextToBePresentInElement(numberOfItemsInCartElement, expectedNumberItems));

            // Get the text content of the element
            string numberOfTemsInCart = numberOfItemsInCartElement.Text;

            return numberOfTemsInCart;
        }


        // helper methods

        public void ScrollIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

    }

}
