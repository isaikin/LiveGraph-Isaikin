using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using LiveGraph.InterfaceBLL;
using AutoMapper;
using LiveGraph.UI.Models;

namespace LiveGraph.UI.Controllers
{
	public class AppTestsController : Controller
	{
		private IAppTestBLL _appTestBLL;
		private readonly IMapper _mapper;
		public AppTestsController(IAppTestBLL appTestBLL, IMapper mapper)
		{
			_appTestBLL = appTestBLL;
			_mapper = mapper;
		}
		public IActionResult Index()
		{
			var result = _mapper.Map<IEnumerable<ViewAppTest>>(_appTestBLL.GetAll()).ToList();
			return View(result);
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
		public IActionResult GetAll()
		{
			var result = _mapper.Map<IEnumerable<ViewAppTest>>(_appTestBLL.GetAll()).ToList();
			return View();
		}

		[HttpGet]
		public IActionResult GetById(int id)
		{
			var result = _mapper.Map<IEnumerable<ViewAppTest>>(_appTestBLL.GetAll()).ToList();
			return View();
		}

		[HttpGet]
		public IActionResult Passage(int id)
		{
			var test = _appTestBLL.GetById(id);

			return View(test);
		}

		[HttpGet]
		public IActionResult Marks()
		{
			var testMark = _appTestBLL.GetMarkByName(User.Identity.Name);

			return View(testMark);
		}

		public IActionResult AllMarks()
		{
			var testMark = _appTestBLL.GetMarks();

			return View(testMark);
		}

		[HttpPost]
		public IActionResult Passage(PassageAppTest test)
		{

			_appTestBLL.Passage(test, User.Identity.Name);

			return null;
		}
	}
}