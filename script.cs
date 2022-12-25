using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Timers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using System.Threading;
using Matix.Notifications;

namespace hello
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");

            IWebDriver driver = new ChromeDriver(option);

            driver.Url = "https://candidature.1337.ma/users/sign_in";

            IWebElement Email = driver.FindElement(By.Id("user_email"));
            Email.SendKeys("sdfa@gmail.com");
            IWebElement Password = driver.FindElement(By.Id("user_password"));
            Password.SendKeys("afdadfasdf");
            IWebElement Login = driver.FindElement(By.Name("commit"));
            Login.Click();
            if (driver.Url == "https://candidature.1337.ma/piscines")
            {
                Console.WriteLine("Logged in");
            }
            int i = 1;
            Action work = delegate
            {
                while (1 == 1)
                {
                    Console.Write("Attempt " + i + " : ");
                    List<IWebElement> elements = driver.FindElements(By.ClassName("btn-primary")).ToList();
                    foreach (var element in elements)
                    {
                        if (element.Displayed && element.GetAttribute("data-kind") == "piscine")
                        {
                            element.Click();
                            Thread.Sleep(8000);
                            List<IWebElement> windows = driver.FindElements(By.ClassName("btn")).ToList();
                            foreach (var window in windows)
                            {
                                if (window.Text == "OK")
                                {
                                    window.Click();
                                    Console.WriteLine("subscribed in !!!");
                                    return;
                                }
                            }
                        }
                    }
                    Console.WriteLine("No pool yet !");
                    Thread.Sleep(8000);
                    driver.Navigate().Refresh();
                }
            };
            work();
        }
    }
}
