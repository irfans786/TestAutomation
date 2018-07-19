using System;
using System.Configuration;
using NUnit.Framework;
using OctopusLabs_Selenium;

namespace OctopusLabsTests
{
    public class OctopusLabTestBase
    {
        public OctopusLabTestBase()
        {
        }

        [TestFixtureSetUp]
        public void BeforeTestFixture()
        {
            Driver.Initialize();
            AboutPage.GoTo();
        }

        //This will be used to clear filters and return to original state.
        [TearDown]
        public void AfterEveryTest()
        {

            // clear down search text box
            PageUtilities.ClearTextFilter();
            // clear down department wise filter and make it un-check
            PageUtilities.ClearDepartmentFilter();

        }

        [TestFixtureTearDown]
        public void AfterTestFixture()
        {
            Driver.Close();
        }
    }
}
