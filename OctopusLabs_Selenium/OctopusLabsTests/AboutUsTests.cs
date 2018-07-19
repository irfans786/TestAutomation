using NUnit.Framework;
using OctopusLabs_Selenium;


namespace OctopusLabsTests
{
    [TestFixture]
    public class AboutUsTests : OctopusLabTestBase
    {
        #region "Working Test Scenarios" 
        //----------------------------------------------------------

        //[TestCase ("A-Z")]
        [TestCase("Z-A"), Description("1 - Test Descending sort Is working As per selection")]
        [Category("PositiveTest")]
        public void SortedPersonList(string SortType)
        {

            bool isSorted = AboutPage.SortDisplayedImages(SortType);
            Assert.IsTrue(isSorted, "Person List Not Sorted correctly as : " + SortType);
        }

        [TestCase("Adam Said"), Description("2 - Test PersonName Filter working")]
        [Category("PositiveTest")]
        public void FilterByName(string PersonName)
        {
            AboutPage.SearchByNameOnly(PersonName);
            Assert.IsTrue(AboutPage.IsAtByName(PersonName), "Failed to Find User");
        }


        //Filter By Department and compare count 
        [TestCase("Client Relations team"), Description("3 - Test Department Filter working and refreshing the records count.")]
        [Category("PositiveTest")]
        public void FilterByDepartment(string DepartmentName)
        {
            AboutPage.FilterByDepartment(DepartmentName);
            Assert.AreEqual(PageUtilities.GetRecordCount(), PageUtilities.GetPeopleDisplayedcount(), "Record count mismatch");

        }


        [TestCase("Business Development team", "Adam Said"), Description("4- Test Department Filter with PersonName working and refreshing result with Person Image")]
        [TestCase("Energy team", "Amanda Foon")]
        [Category("PositiveTest")]
        public void FilterByNameAndDepartment(string Department, string Person)
        {
            AboutPage.FilterByDepartment(Department);
            AboutPage.SearchByNameOnly(Person);
            Assert.IsTrue(AboutPage.IsAtByName(Person), "Failed to Find " + Person + " in Department : " + Department);

        }
        //----------------------------------------------------------
        #endregion



        #region "Empty test case blocks"
        [TestCase("Adam Said"), Description("5 - Test personName Image clicking goes to next details page")]
        [Ignore]
        [Category("PositiveTest")]
        public void DisplayPersonDetails(string PersonName)
        {
            //AboutPage.SearchByNameOnly(PersonName);
            //AboutPage.ClickImage
            // Assert to check the next page contains Person details by checking h3 tag contains person name
        }

        [TestCase("Business Development team", "SW183QQ", "Adam Said"), Description("6 - Test Department Filter for BD team with Post Code and Person Name brings result")]
        [Category("PositiveTest")]
        [Ignore]
        public void FilterByPostCode(string Department, string PostCode, string PersonName)
        {
            // AboutPage.FilterByDepartment(Department);
            // AboutPage.FilterByPostCode(PostCode);
            // Assert.IsTrue(AboutPage.IsAtByName(Person), "Failed to Find "+ Person +" in PostCode : " + PostCode);
        }


        [TestCase("Business Development team"), Description("7 - Test to verify PostCode Input available with BD Team Filter.")]
        [Category("PositiveTest")]
        [Ignore]
        public void FilterByPostCodeActive(string Department)
        {
            // AboutPage.FilterByDepartment(Department);
            // Assert to check IsPostCodeInput Active
        }

        [TestCase("Business Development team", "Energy team"), Description("8 - Test to verify PostCode Input available with BD Team and any other Department Filter selected.")]
        [Category("PositiveTest")]
        [Ignore]
        public void FilterByPostCodeActiveMultipleTeams(string Department1, string Department2)
        {
            // AboutPage.FilterByDepartment(Department1);
            // AboutPage.FilterByDepartment(Department2);
            // Assert to check IsPostCodeInput Active
        }

        [TestCase("Energy team"), Description("9 - Test to verify PostCode Input NOT available with other department Filter.")]
        [TestCase("Client Relations team")]
        [Category("Negative")]
        [Ignore]
        public void FilterByPostCodeNotActive(string Department)
        {
            // AboutPage.FilterByDepartment(Department);
            // Assert to check that IsPostCodeInput NotActive
        }

        [TestCase("Business Development team", "SortType", "PostCode"), Description("10 - Test to Filter by Department and .")]
        [Category("Positive")]
        [Ignore]
        public void FilterByDeptPostalAndSort(string Department, string SortType, string PostCode)
        {
            // AboutPage.FilterByDepartment(Department);
            // AboutPage.FilterByPostCode(PostCode);
            // bool IsSorted = AboutPage.SortDisplayedImages(SortType);
            // Assert to check record present by using a valiid post code and records are sorted as per selection.

        }

        #endregion

    }
}
