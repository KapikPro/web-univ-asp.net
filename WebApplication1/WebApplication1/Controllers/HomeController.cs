using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using WebApplication1.Models;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(DateTime time)
        {
            Response.Cookies.Append($"{Guid.NewGuid()}", JsonSerializer.Serialize(new
            {
                Message = "...",
                Time = time,
            }));


            return Redirect("/");
        }

        [HttpPost("check")]
        public IActionResult Check()
        {
            var currentTime = DateTime.Now;
            Request.Cookies.ToList().ForEach(cookie =>
            {
                if (Guid.TryParse(cookie.Key, out var _) is false) 
                {
                    return;
                }

                var notification = JsonSerializer.Deserialize<Notification>(cookie.Value);
                if(notification.Time < currentTime) 
                {
                    _logger.LogCritical(notification.Message);
                    Response.Cookies.Delete(cookie.Key);
                }
            });
            return Redirect("/");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly record struct Notification
        {
            [JsonPropertyName("Message")] public required string Message { get; init; }

            [JsonPropertyName("Time")] public required DateTime Time { get; init; }
        }
    }
}
