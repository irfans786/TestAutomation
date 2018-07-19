using System;
namespace OctopusLabs_Selenium
{
    public class EmptyClass
    {
        public EmptyClass()
        {
        }
    }
}
//IWebElement departmentName = Driver.Instance.FindElement(By.XPath("//div[4]//*//span[text()='"+ Department + "']//preceding::input"));
IList<IWebElement> departmentList = Driver.Instance.FindElements(By.XPath("//div[4]//label"));

ScrollScreen(departmentList[0]);
ScrollScreen(departmentList[departmentList.Count - 1]);

//Console.WriteLine("Department Name: " + departmentName.Text);
Console.WriteLine("Department Checkbox: " + departmentList.Count);
            Console.WriteLine(departmentList[1].TagName);
            Console.WriteLine(departmentList[2].TagName);

            for (int i = 2; i <= (departmentList.Count - 1);)
            {

                departmentList[i].Click();
System.Threading.Thread.Sleep(2000);
                i++;
                break;
            }