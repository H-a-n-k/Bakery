﻿using Bakery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bakery.Controllers
{
    public class AuthController : Controller
    {

		private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

		// GET: Auth
		public ActionResult Index()
        {
			return RedirectToAction("SignIn");
        }

		public ActionResult SignIn()
		{

			return View();
		}

		[HttpPost]
		public ActionResult SignIn(string user, string pass)
		{
			var kh = db.sp_khachdangnhap(user, pass).SingleOrDefault();
			if (kh != null)
			{
				FormsAuthentication.SetAuthCookie(user, false);
				if (kh.QuyenQuanTri.Value)
				{
					Session["Role"] = "Admin";
					return RedirectToAction("Index", "SanPhams", new { area = "Admin" });
				}
				else {
					Session["Role"] = "Customer";
					Session["CustomerID"] = kh.MaKH;
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}

		public ActionResult SignOut()
		{
			FormsAuthentication.SignOut();
			Session["Role"] = null;
			Session["CustomerID"] = null;
			return RedirectToAction("SignIn");
		}
	}
}