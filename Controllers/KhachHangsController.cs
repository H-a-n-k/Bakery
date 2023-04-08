using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Eventing.Reader;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bakery.Models;
using Bakery.Models.ViewModels;

/* Go to Model browser
1st place- Under Complex Types-> as MyStoreProc_result
2nd Place- Under Function Imports -> as MyStoreProc
3rd Place - Under Stored Procdures/ Functions -> as MyStoreProc*/

namespace Bakery.Controllers
{
    [Authorize]
    public class KhachHangsController : Controller
    {
        private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

        // GET: KhachHangs/Details/5
        public ActionResult Details()
        {
            int? id = Session["CustomerID"] == null ? null : (int?)Session["CustomerID"];
            if (id == null) return RedirectToAction("SignIn", "Auth");

			var khachHang = db.sp_profilekhachhang(id).SingleOrDefault();
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            var ctkh = new CTKHVM();
            ctkh.kh = khachHang;

            var hds = new List<CTHDVM>();
            var dshd = db.sp_dshoadon(id, null).ToList();
            foreach (var x in dshd) {
                var cthds = db.sp_ds_cthd(x.MaHD).ToList();
                hds.Add(new CTHDVM() { hd = x, cthds = cthds });
            }
            ctkh.hds = hds;

            return View(ctkh);
        }

		// GET: KhachHangs/Create
        [AllowAnonymous]
		public ActionResult Create()
		{
			return View();
		}

		// POST: KhachHangs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public ActionResult Create(SignUpVM kh)
		{
			if (ModelState.IsValid)
			{
				db.sp_dkytaikhoan(kh.TenKH, kh.GioiTinh, kh.SoDienThoai, kh.NgaySinh, kh.TaiKhoan, kh.MatKhau);
				return RedirectToAction("Index", "Home");
			}

			return View(kh);
		}

        public ActionResult AddReview(List<sp_ds_cthd_Result> reviews) {
            foreach (var x in reviews) {
                db.sp_danhGiaSP(x.MaHD, x.MaSP, x.SoSaoDanhGia, x.NoiDungDanhGia);
			}
            return RedirectToAction("Details");
        }

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
