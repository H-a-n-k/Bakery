using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bakery.Models;

/* Go to Model browser
1st place- Under Complex Types-> as MyStoreProc_result
2nd Place- Under Function Imports -> as MyStoreProc
3rd Place - Under Stored Procdures/ Functions -> as MyStoreProc*/

namespace Bakery.Controllers
{
    public class KhachHangsController : Controller
    {
        private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

        // GET: KhachHangs
        public ActionResult Index(bool? isActive, string keyword, int? page, int? pageLength)
        {
            ObjectParameter count = new ObjectParameter("pageCount", typeof(Int32));
            var list = db.sp_dskhachhang(isActive, keyword, page, pageLength, count).ToList();
			ViewBag.TotalPage = Convert.ToInt32(count.Value);
            return View(list);
        }

        // GET: KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var khachHang = db.sp_profilekhachhang(id).SingleOrDefault();
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenKH,GioiTinh,SoDienThoai,NgaySinh,TaiKhoan,MatKhau")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.sp_dkytaikhoan(kh.TenKH, kh.GioiTinh, kh.SoDienThoai, kh.NgaySinh, kh.TaiKhoan, kh.MatKhau);
                return RedirectToAction("Index");
            }

            return View(kh);
        }

        // GET: KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var khachHang = db.sp_profilekhachhang(id).SingleOrDefault();
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,GioiTinh,SoDienThoai,NgaySinh")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.sp_updatekhachhang(kh.MaKH, kh.TenKH, kh.GioiTinh, kh.SoDienThoai, kh.NgaySinh);
                return RedirectToAction("Index");
            }
            var khachHang = db.sp_profilekhachhang(kh.MaKH).SingleOrDefault();
			return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var khachHang = db.sp_profilekhachhang(id).SingleOrDefault();
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.sp_deletekhachhang(id);
            return RedirectToAction("Index");
        }

		public ActionResult SignIn()
		{

            return View();
		}

        [HttpPost]
        public ActionResult SignIn(string user, string pass)
		{
            bool? role = db.sp_khachdangnhap(user, pass).SingleOrDefault();
            if (role.HasValue) {
				FormsAuthentication.SetAuthCookie(user, false);
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		public ActionResult SignOut()
		{
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
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
