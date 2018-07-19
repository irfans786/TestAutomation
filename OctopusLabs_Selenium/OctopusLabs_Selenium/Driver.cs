using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.Configuration;

namespace OctopusLabs_Selenium
{
    public class Driver
    {
        private static string _currentdriver;
        private static string _driverlocation;
        private static string _baseaddress;


        public static IWebDriver Instance { get; set; }

        public static string BaseAdress 
        { 
            get { return _baseaddress; }
            //internal set; 
        }

        public static void Initialize ()
        {
            try
            {
                //Open the configuration file using the dll location
                Configuration myDllConfig =
                    ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
                // Get the appSettings section
                AppSettingsSection myDllConfigAppSettings =
                       (AppSettingsSection)myDllConfig.GetSection("appSettings");
                
                _currentdriver = myDllConfigAppSettings.Settings["CurrentDriver"].Value;


                _driverlocation = myDllConfigAppSettings.Settings[_currentdriver + "DriverLocation"].Value;
                _baseaddress =myDllConfigAppSettings.Settings["BaseUrl"].Value;
            }
            catch (ConfigurationException)
            {
                Console.WriteLine("Error reading app settings");
            }

            if (_currentdriver == "Chrome")
            {
                Instance = new ChromeDriver(_driverlocation);
            }
            else
            {
                Instance = new FirefoxDriver();
            }
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void Close()
        {
           Instance.Close();

        }

        /// <summary>
        /// Wait the specified timeSpan. Thread will sleep for specific time.
        /// </summary>
        /// <param name="timeSpan">Time span.</param>
        public static void Wait(TimeSpan timeSpan)
        {
            //TODO : Refactor wait time code to use Explicit wait/Fluent Wait to better handle Ajax calls
            Thread.Sleep((int)timeSpan.TotalSeconds * 1000 );
        }
    }

}
