using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoaDonsController : Controller
    {
        private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.sp_dshoadon(null, null).ToList();
            return View(hoaDons);
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,NgayHD,TinhTrangGiao,DiaChiGiao,MaKH,TongTien")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hoaDon = db.sp_detailhoadon(id).FirstOrDefault();
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            var tinhtrangs = new SelectList(new[] {
                new Tuple<string, bool>("Đã giao", true),
                new Tuple<string, bool>("Chưa giao", false)
            }, "Item2", "Item1", hoaDon.TinhTrangGiao);

            var cthds = db.sp_ds_cthd(id).ToList();

            ViewBag.cthds = cthds;
            ViewBag.TinhTrangGiao = tinhtrangs;
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sp_detailhoadon_Result hoaDon)
        {
            try
            {
                db.sp_update_hoadon(hoaDon.MaHD, hoaDon.TinhTrangGiao, hoaDon.DiaChiGiao);
                return RedirectToAction("Index");
            }
            catch {
                var tinhtrangs = new SelectList(new[] {
                    new Tuple<string, bool>("Đã giao", true),
                    new Tuple<string, bool>("Chưa giao", false)
                }, "Item2", "Item1", hoaDon.TinhTrangGiao);

                ViewBag.TinhTrangGiao = tinhtrangs;
                ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
                return View(hoaDon);
            }
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
