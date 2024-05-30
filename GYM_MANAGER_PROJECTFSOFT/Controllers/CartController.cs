using GYM_MANAGER_PROJECTFSOFT.Models;
using GYM_MANAGER_PROJECTFSOFT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class CartController : Controller
    {
        GYM_ManagerEntities db = new GYM_ManagerEntities();

        [HttpPost]
        public ViewResult Checkout(ThongTinGiaoHang thongtingiaohang)
        {
            TAIKHOAN taikhoan = (TAIKHOAN)Session["TaiKhoan"];
            var giohang = db.GIOHANGs.Where(x => x.MaTaiKhoan == taikhoan.MaTaiKhoan);
            if (giohang.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                DONHANG donhang = new DONHANG();
                donhang.PhuongThucThanhToan = (int)thongtingiaohang.PaymentMethod;
                donhang.MaTaiKhoan = taikhoan.MaTaiKhoan;
                donhang.DaThanhToan = thongtingiaohang.PaymentMethod == PaymentMethod.NhanTienKhiGiaoHang ? false : true;
                db.DONHANGs.Add(donhang);
                db.SaveChanges();

                foreach(var item in giohang)
                {
                    CHITIETDONHANG chitietdonhang = new CHITIETDONHANG();
                    chitietdonhang.MaDonHang = donhang.MaDonHang;
                    chitietdonhang.MaSanPham = item.MaSanPham;
                    chitietdonhang.SoLuong = item.SoLuong == null ? 0 : (int)item.SoLuong;
                    db.CHITIETDONHANGs.Add(chitietdonhang);

                    db.GIOHANGs.Remove(item);
                }
                db.SaveChanges();
                return View("Completed");
            }
            else
            {
                return View(new ThongTinGiaoHang());
            }
        }

        public ViewResult Checkout()
        {
            return View(new ThongTinGiaoHang());
        }

        public ViewResult Index(string returnUrl)
        {
            TAIKHOAN taikhoan = (TAIKHOAN)Session["TaiKhoan"];
            var giohang = db.GIOHANGs.Where(x => x.MaTaiKhoan == taikhoan.MaTaiKhoan).OrderBy(x => x.MaSanPham).ToList();
            ViewBag.returnUrl = returnUrl;
            return View(giohang);
        }

        public RedirectToRouteResult AddToCart(int MaSanPham, string returnUrl)
        {
            TAIKHOAN taikhoan = (TAIKHOAN)Session["TaiKhoan"];
            var sanpham = db.SANPHAMs
            .FirstOrDefault(p => p.MaSanPham == MaSanPham);
            if (sanpham != null)
            {
                GIOHANG giohang = db.GIOHANGs.FirstOrDefault(x => x.MaTaiKhoan == taikhoan.MaTaiKhoan && x.MaSanPham == MaSanPham);
                if (giohang != null)
                    giohang.SoLuong++;
                else
                {
                    giohang = new GIOHANG();
                    giohang.MaTaiKhoan = taikhoan.MaTaiKhoan;
                    giohang.MaSanPham = MaSanPham;
                    giohang.SoLuong = 1;
                    db.GIOHANGs.Add(giohang);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int MaSanPham, string returnUrl)
        {
            TAIKHOAN taikhoan = (TAIKHOAN)Session["TaiKhoan"];
            var giohang = db.GIOHANGs
            .FirstOrDefault(p => p.MaSanPham == MaSanPham && p.MaTaiKhoan == taikhoan.MaTaiKhoan);
            if (giohang != null)
            {
                db.GIOHANGs.Remove(giohang);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}