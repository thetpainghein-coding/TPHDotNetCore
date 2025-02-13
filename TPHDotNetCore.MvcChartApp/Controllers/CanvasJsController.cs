﻿using Microsoft.AspNetCore.Mvc;

namespace TPHDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        private readonly ILogger<CanvasJsController> _logger;

		public CanvasJsController(ILogger<CanvasJsController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult LineChart()
        {
            _logger.LogInformation("Canvas Chart: Line Chart...");
            return View();
        }
    }
}
