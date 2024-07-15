using Microsoft.AspNetCore.Mvc;

namespace TPHDotNetCore.MvcChartApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PieChart()
        {
            return View();
        }
    }
}
