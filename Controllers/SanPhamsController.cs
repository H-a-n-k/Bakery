using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class SanPhamsController : Controller
    {
        private BakeryStoreDBEntities db = new BakeryStoreDBEntities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var danhsach = db.sp_DSSP(true);
            return View(danhsach.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sanPham= db.sp_ChiTietSP(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM");
            ViewBag.maLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaSP,MotaSP,img,Sao,SoLuotDanhGia,SoluongSP,maLoai,MaKM,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
            ViewBag.maLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.maLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
            ViewBag.maLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.maLoai);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaSP,MotaSP,img,Sao,SoLuotDanhGia,SoluongSP,maLoai,MaKM,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
            ViewBag.maLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.maLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
