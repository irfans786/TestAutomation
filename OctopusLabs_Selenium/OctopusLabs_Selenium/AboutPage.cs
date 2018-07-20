using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
 
namespace OctopusLabs_Selenium
{
    public class AboutPage
    {
        public AboutPage()
        {
        }

        public static void GoTo()
        {
            
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAdress + "/adviser/about-us/our-people/");
            AcceptCookiesAndConditions();
            Driver.Wait(TimeSpan.FromSeconds(1));

        }

        public static void SearchByNameOnly(string PersonName)
        {
            IWebElement searchByName= Driver.Instance.FindElement(By.Name("search"));
            PageUtilities.ScrollScreen(searchByName);

          //  searchByName.Clear();
            searchByName.SendKeys(PersonName);
            searchByName.Submit();

            Driver.Wait(TimeSpan.FromSeconds(2));

        }

        public static bool IsAtByName(string PersonName)
        {

            IList<IWebElement> ListOfRecords = Driver.Instance.FindElements(By.XPath("//*[@id='grid']/div"));

            if (ListOfRecords.Count > 0)
            {
                PageUtilities.ScrollScreen(ListOfRecords[ListOfRecords.Count - 1]);
                PageUtilities.ScrollScreen(ListOfRecords[0]);

                IWebElement element = ListOfRecords[0].FindElement(By.XPath("//*[@id='grid']/div[1]/a/h2"));

                return element.Text.Contains(PersonName);
            }
                
            return false;
        }


        /// <summary>
        /// Displays list of people in selected department
        /// and compares the total people displayed with count of records.
        /// </summary>
        /// <param name="Department">Department.</param>
        public static void FilterByDepartment (string Department)
        {

            IList<IWebElement> departmentList = Driver.Instance.FindElements(By.XPath("//div[4]//label"));

            PageUtilities.ScrollScreen(departmentList[0]);
            PageUtilities.ScrollScreen(departmentList[departmentList.Count -1]);

            for (int i = 0; i <= (departmentList.Count - 1);)
            {
                IWebElement chkboxText = departmentList[i].FindElement(By.TagName("span"));

                if (chkboxText.Text == Department)
                {
                    Driver.Wait(TimeSpan.FromSeconds(2));

                    PageUtilities.ScrollScreen(departmentList[i]);
                    departmentList[i].Click();
                    break;
                  
                }
                i++;
            }

            PageUtilities.ScrollScreen(departmentList[0]);
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        public static void AcceptCookiesAndConditions()
        {
            try
            {
                IWebElement cookiePrompt = Driver.Instance.FindElement(By.Id("hs-eu-confirmation-button"));
                cookiePrompt.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception)
            {
                //dont do anything
            }


            try
            {
                IWebElement AcceptTerms = Driver.Instance.FindElement(By.XPath("//*[@id='info-modal']//div[2]//div//button"));
                AcceptTerms.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception)
            {
                //dont do anything
            }

            try
            {
                IWebElement cookiePrompt2 = Driver.Instance.FindElement(By.XPath("//*[@id='cookie-modal']//div//div//div//div//div[3]/div//a[1]"));
                cookiePrompt2.Click();
            }
            catch (Exception)
            {
                //dont do anything
            }
        }

        public static bool SortDisplayedImages(string sortType)
        {

            IWebElement selectElement = Driver.Instance.FindElement(By.XPath("//div[3]//div//select"));
            SelectElement select = new SelectElement(selectElement);
            PageUtilities.ScrollScreen(selectElement);
            Driver.Wait(TimeSpan.FromSeconds(2));
            select.SelectByText("Sort " + sortType);

            Driver.Wait(TimeSpan.FromSeconds(1));
            PageUtilities.ScrollScreenToEnd();
            IList<IWebElement> ListOfRecords = Driver.Instance.FindElements(By.ClassName("search-result_item"));

            int recordcount = PageUtilities.GetRecordCount();
            string[] NameArray = new string[recordcount];

           // Console.WriteLine("Before Count: " + ListOfRecords.Count);
            if (ListOfRecords.Count <= 1)
                return true;
            else
            {
                if (ListOfRecords.Count > 0)
                {
                    
                    PageUtilities.ScrollScreen(ListOfRecords[0]);

                    for (int i = 0; i <= recordcount -1; i++)
                    {
                        IWebElement element = ListOfRecords[i].FindElement(By.ClassName("team-item_title"));
                        PageUtilities.ScrollScreen(ListOfRecords[i]);
                       
                        NameArray[i] = element.Text;
                        Console.WriteLine("Person Name : " + element.Text);

                        ListOfRecords = Driver.Instance.FindElements(By.ClassName("search-result_item"));
                    }

                    if (sortType == "Z-A")
                    
                        return PageUtilities.IsSortedDescending(NameArray);
                    else
                        return PageUtilities.IsSortedAscending(NameArray);
                }

                return false;
            }
           
           // elements[1].SendKeys("Sort " + sortType);
        }
    }
}
