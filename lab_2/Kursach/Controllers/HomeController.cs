using Kursach.Data;
using Kursach.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kursach
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext; //DependenciEnjection

        public HomeController(ILogger<HomeController> logger,AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
         public IActionResult Reg(Personn person)
        {
            if(ModelState.IsValid)
            {
                _appDbContext.Person.Add(person);
                _appDbContext.SaveChanges();
                return Redirect("/");
            }
                return Redirect("/bbb");
        }

        [HttpPost]
        public IActionResult Vh(Personn person)
        {
            if (ModelState.IsValid)
            {
                    {
                        var users = _appDbContext.Person.ToList();
                        foreach (Personn u in users)
                        {
                         if((u.login==person.login) && (u.password==person.password))
                            return Redirect("/pobeda");
                        }
                    }
            }
            return Redirect("/bbb");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
