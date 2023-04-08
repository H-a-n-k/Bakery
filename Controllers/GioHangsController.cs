using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;
using Microsoft.Ajax.Utilities;

namespace Bakery.Controllers
{
	[Authorize(Roles = "Customer")]
	public class GioHangsController : Controller
    {
        private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

        private int? GetCustomerID(HttpSessionStateBase session) {
            if (session["CustomerID"] == null) return null;

            return (int)session["CustomerID"];

		}

        // GET: GioHangs
        public ActionResult Index()
        {
            int? makh = GetCustomerID(Session);
			if (makh == null) return RedirectToAction("SignIn", "Auth");

			var gioHangs = db.sp_ds_gioHang(makh);
            return View(gioHangs.ToList());
        }

        [HttpPost]
		public ActionResult Checkout(string addr)
		{
			int? makh = GetCustomerID(Session);
            if (makh == null) return RedirectToAction("SignIn", "Auth");

            db.sp_thanhToan(makh, addr);
            db.sp_delete_gioHang(makh, null);
            //return Json(new { success = true });
            return RedirectToAction("Index");
		}
        //sua calc tong, addcthd
		[HttpPost]
		public ActionResult UpdateQuantity(int masp, int sl)
		{
			int? makh = GetCustomerID(Session);
			if (makh == null) return new EmptyResult();

			db.sp_update_gioHang(makh, masp, sl);
            return new EmptyResult();
		}

		[HttpPost]
		public ActionResult RemoveItem(int masp)
		{
			int? makh = GetCustomerID(Session);
			if (makh == null) return new EmptyResult();

			db.sp_delete_gioHang(makh, masp);
            return new EmptyResult();
		}

		// GET: GioHangs/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: GioHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,MaSP,SoLuong")] GioHang gioHang)
        {
            if (ModelState.IsValid) {
                db.sp_add_gioHang(gioHang.MaKH, gioHang.MaSP);
				//return RedirectToAction("Index", "SanPhams");
				return RedirectToAction("Details", "SanPhams", new { id = gioHang.MaSP });
			}

			return RedirectToAction("Details", "SanPhams", new { id = gioHang.MaSP });
		}

        // GET: GioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", gioHang.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,MaSP,SoLuong")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", gioHang.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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


