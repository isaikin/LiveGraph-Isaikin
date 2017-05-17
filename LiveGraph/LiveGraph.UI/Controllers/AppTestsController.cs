using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveGraph.Common;

namespace LiveGraph.UI.Controllers
{
	public class AppTestsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public IActionResult Create(AppTest test)
		{
			return Json(
				new {
					Redirect =  Url.Action("Index", "Home")
				});
		}
	}
}