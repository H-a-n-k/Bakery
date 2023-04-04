using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bakery.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[Authorize]
		public ActionResult About()
		{
			ViewBag.Message = "Đăng nhập mới vào được";

			return View();
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Contact()
		{
			ViewBag.Message = "admin mới vào được";

			return View();
		}
	}
}