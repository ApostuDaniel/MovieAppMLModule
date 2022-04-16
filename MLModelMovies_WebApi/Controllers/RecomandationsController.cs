using Microsoft.AspNetCore.Mvc;

namespace MLModelMovies_WebApi.Controllers
{
    public class RecomandationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
