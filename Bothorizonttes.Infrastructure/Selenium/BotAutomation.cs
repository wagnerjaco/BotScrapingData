using System;
using System.Collections.Generic;
using System.Text;
using Bothorizonttes.Application.Interfaces;
using Bothorizonttes.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Bothorizonttes.Infrastructure.Selenium
{
    public class BotAutomation : IBotAutomation
    {
        private readonly Settings _settings;

        public BotAutomation(Settings settings)
        {
            _settings = settings;
        }

        public async Task ExecutarAsync()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            using var driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

           
            driver.Navigate().GoToUrl(_settings.Url);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")))
                .SendKeys(_settings.Username);

            driver.FindElement(By.Id("password"))
                  .SendKeys(_settings.Password);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            await Task.Delay(5000);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnToggle")))
                .Click();

            await Task.Delay(2000);

            driver.FindElement(By.XPath("//*[@id='root']/aside/div[2]/ul/li[13]/p"))
                  .Click();

            await Task.Delay(2000);

            driver.FindElement(By.XPath("//*[@id='mui-18-5']/div/div[2]/div/a/p"))
                  .Click();

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("iFramePage"));

            driver.FindElement(By.XPath("//*[@id='linhaRow_24']//*[@id='copiar.gif']"))
                  .Click();

            await Task.Delay(500);

            driver.FindElement(By.XPath("//*[@id='linhaRow_25']//*[@id='copiar.gif']"))
                  .Click();

            await Task.Delay(TimeSpan.FromMinutes(2));

            driver.SwitchTo().DefaultContent();

            try
            {
                driver.FindElement(By.Id("beamerAnnouncementSnippet")).Click();
            }
            catch { }

            driver.FindElement(By.XPath("//*[@id='root']/header/div[2]/div[7]/button"))
                  .Click();

            await Task.Delay(1000);

            driver.FindElement(By.CssSelector("a[href='/sienge/logoff.jsp']"))
                  .Click();
        }

    }
}
