using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using LiveGraph.InterfaceBLL;

namespace LiveGraph.UI.Controllers
{
	public class AppTestsController : Controller
	{
		private IAppTestBLL _appTestBLL;
		public AppTestsController(IAppTestBLL appTestBLL)
		{
			_appTestBLL = appTestBLL;
		}
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
			_appTestBLL.Add(test);
			return Json(
				new
				{
					Redirect = Url.Action("Index", "Home")
				});


		}
		[HttpGet]
		public List<AppTest> GetAll()
		{
			return _appTestBLL.GetAll().ToList();
		}
	}
}