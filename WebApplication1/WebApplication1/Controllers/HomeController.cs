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
        public IActionResult Add(int year)
        {
            double Ashours;
            int Achours;
            int currentYear = DateTime.Now.Year;
            if (currentYear < year || currentYear-year>4)
            {
                Ashours = 0;
                Achours = 0;
            }
            else
            {
                // Рассчитываем количество лет учебы
                int studyYears = currentYear - year + 1;

                // Количество дней в учебном году (учитывая високосные годы)
                DateTime startDate = new DateTime(year, 9, 1);
                DateTime endDate = new DateTime(currentYear, 5, 31);
                TimeSpan studyPeriod = endDate - startDate;
                int studyDays = studyPeriod.Days + 1; // +1 чтобы включить последний день

                // Учитываем выходные дни (суббота и воскресенье)
                int weekends = 0;
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        weekends++;
                }
                studyDays -= weekends;

                Ashours = studyDays *3*1.5;
                Achours = studyDays * 3*2;
            }
            Hours notification = new Hours(Achours,Ashours);
            return View("Index",notification);
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
