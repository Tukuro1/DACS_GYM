using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ProductManagerController : Controller
    {
        GYM_ManagerEntities db = new GYM_ManagerEntities();
        // GET: DeviceManager
        public ActionResult ProductTables(string SearchString, int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            var lstSanPham = db.SANPHAMs.Where(x => true);
            if (!String.IsNullOrEmpty(SearchString))
            {
                lstSanPham = lstSanPham.Where(s => s.TenSanPham.Contains(SearchString));
            }
            return View(lstSanPham.OrderBy(x => x.MaSanPham).ToPagedList(pageNum, pageSize));
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(SANPHAM sanpham, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sanpham.ImageMimeType = image.ContentType;
                    sanpham.HinhAnh= new byte[image.ContentLength];
                    image.InputStream.Read(sanpham.HinhAnh, 0, image.ContentLength);
                }

                if (sanpham.MaSanPham == 0)
                {
                    db.SANPHAMs.Add(sanpham);
                    db.SaveChanges();
                }
                else
                {
                    var sanphamDb = db.SANPHAMs.Find(sanpham.MaSanPham);
                    sanphamDb.TenSanPham = sanpham.TenSanPham;
                    sanphamDb.MoTa = sanpham.MoTa;
                    sanphamDb.Gia = sanpham.Gia;
                    sanphamDb.DanhMuc = sanpham.DanhMuc;
                    if (sanpham.HinhAnh != null)
                    {
                        sanphamDb.HinhAnh = sanpham.HinhAnh;
                        sanphamDb.ImageMimeType = sanpham.ImageMimeType;
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("ProductTables");
            }
            return View(sanpham);
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("ProductTables");
        }

        public FileContentResult GetImage(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham != null && sanpham.HinhAnh != null)
            {
                return File(sanpham.HinhAnh, sanpham.ImageMimeType);
            }
            return null;
        }

        public ActionResult CreateProduct()
        {
            return View("EditProduct", new SANPHAM());
        }
    }
}