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

        // GET: GioHangs
        public ActionResult Index(int maKh)
        {
            if (Session["CustomerID"] == null) return RedirectToAction("signIn", "khachhangs");

            if ((int)Session["CustomerID"] != maKh) return Content("This is not your cart");

            var gioHangs = db.sp_ds_gioHang(maKh);
            return View(gioHangs.ToList());
        }

        [HttpPost]
		public ActionResult Checkout(List<sp_ds_gioHang_Result> Items)
		{

            return Json(new { success = true });
			//return RedirectToAction("Index", new { maKh = Items.First().MaKH });
		}

		[HttpPost]
		public ActionResult UpdateQuantity(List<sp_ds_gioHang_Result> Items)
		{
            foreach (var x in Items) {
                db.sp_update_gioHang(x.MaKH, x.MaSP, x.SoLuong);
            }

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


