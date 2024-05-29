using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.ViewModel
{
    public class ThongTinGiaoHang
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        [Display(Name = "Gift Wrap")]
        public bool GiftWrap { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
    }

    public enum PaymentMethod
    {
        [Display(Name = "Collect On Delivery")]
        NhanTienKhiGiaoHang,
        [Display(Name = "Credit Transfer")]
        ChuyenKhoan
    }
}