using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BeautifulNumbers.Models;

namespace BeautifulNumbers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        public ViewResult Info()
        {
            return View(Repository.Response);
        }
    }
}
