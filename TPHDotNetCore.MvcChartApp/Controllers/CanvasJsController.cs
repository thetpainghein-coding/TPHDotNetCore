using Microsoft.AspNetCore.Mvc;

namespace TPHDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LineChart()
        {
            return View();
        }
    }
}
