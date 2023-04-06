using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
        public ActionResult Index(bool? tinhtrang, string keyword, int? maloai , int? page, int? pagelength, int? orderOpt)
        {
			ObjectParameter count = new ObjectParameter("totalPage", typeof(Int32));
            var danhsach = db.sp_DSSP(count, tinhtrang, keyword, maloai, orderOpt, page, pagelength).ToList();
            ViewBag.PageCount = Convert.ToInt32(count.Value);
			return View(danhsach);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sanPham= db.sp_ChiTietSP(id).SingleOrDefault();
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.maLoai = new SelectList(db.sp_ds_loaisp(), "MaLoai", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaSP,MotaSP,img,SoluongSP,maLoai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.sp_ThemSP(sanPham.TenSP, sanPham.GiaSP, sanPham.MotaSP, sanPham.img, sanPham.maLoai, sanPham.SoluongSP);
                return RedirectToAction("Index");
            }

			ViewBag.maLoai = new SelectList(db.sp_ds_loaisp(), "MaLoai", "TenLoai");
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
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaSP,MotaSP,img,Sao,SoLuotDanhGia,SoluongSP,maLoai,MaKM")] SanPham sp)
        {
            if (ModelState.IsValid)
            {
                db.sp_SuaSP(sp.MaSP, sp.TenSP, sp.GiaSP, sp.MotaSP, sp.img, sp.Sao, sp.SoLuotDanhGia, sp.maLoai, sp.SoluongSP, sp.MaKM);
                return RedirectToAction("Index");
            }
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sp.MaKM);
            ViewBag.maLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sp.maLoai);
            return View(sp);
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
            db.sp_AnHienSP(id);
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
