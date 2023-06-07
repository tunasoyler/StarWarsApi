using Microsoft.AspNetCore.Mvc;

namespace StarWars.UI.Controllers
{
    public class FilmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
