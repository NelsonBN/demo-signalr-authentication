using Demo.Client.dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace Demo.Client.dotnet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new HttpClient();
            var accessToken = client.GetStringAsync("http://localhost:5005/Authentication").GetAwaiter().GetResult();

            this.ViewBag.AccessToken = accessToken;

            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}