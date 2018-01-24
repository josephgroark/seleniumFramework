using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace personalLoans
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //reference and navigate
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.co-operativebank.co.uk/loans/personal");

            //Find elements using Css and className
            var borrowAmount = driver.FindElement(By.CssSelector("input[type=number][name='plc-amount-to-borrow']"));
            var repayAmount = driver.FindElement(By.ClassName("js-total-repayment-display"));

            borrowAmount.Clear();
            borrowAmount.SendKeys("9550");

            //press calculate button
            var calcButton = driver.FindElement(By.CssSelector("a[href='#loans-calc-1-results']"));
           
            string borrow = borrowAmount.GetAttribute("value");
            string repay = repayAmount.Text;

            //conditional if borrowing is 7500
            if (borrow == "7500")
            {
                Assert.Equal(repay, "£8,449.80");
            }
            else
            {
                borrowAmount.Clear();
                borrowAmount.SendKeys("7500");
                calcButton.Click();
                string newRepay = repayAmount.Text;
                Assert.Equal(newRepay, "£8,449.80");
            }
        }
    }
}
