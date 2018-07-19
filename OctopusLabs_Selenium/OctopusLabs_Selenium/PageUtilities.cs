using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace OctopusLabs_Selenium
{
    public class PageUtilities

    {
        public PageUtilities()
        {
        }

        public static void ScrollScreen(IWebElement element)
        {
            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public static void ScrollScreenToEnd()
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance;
            Driver.Wait(TimeSpan.FromSeconds(2));

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

        }
        public static int GetRecordCount()
        {
            IWebElement element = Driver.Instance.FindElement(By.ClassName("search-filter_result"));
            IWebElement eleCount = element.FindElement(By.TagName("span"));
            return Int16.Parse(eleCount.Text);
        }

        public static int GetPeopleDisplayedcount()
        {
            PageUtilities.ScrollScreenToEnd();

            IList<IWebElement> element = Driver.Instance.FindElements(By.ClassName("search-result_item"));
            return element.Count;

        }

        public static IWebElement GetPersonImageElement(string PersonName)
        {

            IList<IWebElement> ListOfRecords = Driver.Instance.FindElements(By.XPath("//*[@id='grid']/div"));

            if (ListOfRecords.Count > 0)
            {
                PageUtilities.ScrollScreen(ListOfRecords[ListOfRecords.Count - 1]);
                PageUtilities.ScrollScreen(ListOfRecords[0]);

                IWebElement element = ListOfRecords[0].FindElement(By.XPath("//*[@id='grid']/div[1]/a"));

                return element;
            }
            return null;
        }


        public static void ClearTextFilter()
        {
            IWebElement searchByName = Driver.Instance.FindElement(By.Name("search"));
            PageUtilities.ScrollScreen(searchByName);
            Driver.Wait(TimeSpan.FromSeconds(2));
            searchByName.Clear();
        }

        public static void ClearDepartmentFilter()
        {
            IList<IWebElement> departmentList = Driver.Instance.FindElements(By.XPath("//div[4]//label"));

            PageUtilities.ScrollScreen(departmentList[0]);
            PageUtilities.ScrollScreen(departmentList[departmentList.Count - 1]);

            for (int i = 0; i <= (departmentList.Count - 1);)
            {
                IWebElement chkbox = departmentList[i].FindElement(By.TagName("input"));

                if (chkbox.Selected )
                {
                    PageUtilities.ScrollScreen(departmentList[i]);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    departmentList[i].Click();

                }
                i++;
            }


        }

        /// <summary>
        /// Determines if string array is sorted from Z -> A
        /// </summary>
        public static bool IsSortedDescending(string[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                //TODO : To Ignore special character
                if (string.Compare(arr[i], arr[i + 1]) < 0) // If previous is smaller, return false
                {
                    return false;
                }
            }
            return true;
        }
    }
}
