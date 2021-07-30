using KeyVaultDemo.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace KeyVaultDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration mConfiguration;

        private readonly ILogger<HomeController> mLogger;

        private const string KeyVaultUrl = "https://az204-kv.vault.azure.net/";


        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            mConfiguration = configuration;
            mLogger = logger;
        }


        public IActionResult Index()
        {
            try
            {
                var client = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());
                var secret = client.GetSecretAsync("colour").Result.Value.Value;
                ViewBag.Colour = secret;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
