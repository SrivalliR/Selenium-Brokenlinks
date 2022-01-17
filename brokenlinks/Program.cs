using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace brokenlinks
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpWebRequest req = null;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(options);

            //finding brokenlinks
            //IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            
            var links=driver.FindElements(By.TagName("a"));
            Console.WriteLine("Total no. of links in the Url = "+ links.Count);

            //method 1
            foreach (var link in links)
            {
                string url = link.GetAttribute("href");
                IsLInkWorking(url);
            }

            bool IsLInkWorking(string url)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

                request.AllowAutoRedirect = true;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //Console.WriteLine("Response code is OK and description is "+response.StatusDescription);
                        response.Close();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(url + "description is " + response.StatusDescription);
                        return false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(url + " notworking");
                    return false;
                }
            }


            //method 2
            //foreach (var link in links) {
            //    if (!(link.Text.Contains("Email") || link.Text == ""))
            //    {
            //        req = (HttpWebRequest)WebRequest.Create(link.GetAttribute("href"));
            //        try
            //        {
            //            var response = (HttpWebResponse)req.GetResponse();
            //            if (response.StatusCode==HttpStatusCode.OK)
            //            {
            //                Console.WriteLine($"URL:{link.GetAttribute("href")} status is:{response.StatusCode}");
            //                //Console.WriteLine($"URL:{link.GetAttribute("href")} status is:{response.StatusDescription}");
            //                response.Close();
            //            }
            //            else
            //            {
            //                Console.WriteLine($"URL:{link.GetAttribute("href")} status is:{response.StatusCode}");
            //            }
            //        }
            //        catch (WebException e)
            //        {
            //            var errorResponse = (HttpWebResponse)e.Response;
            //            Console.WriteLine($"URL:{link.GetAttribute("href")} status is :{errorResponse.StatusCode}");
            //        }                    
            //    }               
        //}
        }        
    }
}
