//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GIOHANG
    {
        public int MaSanPham { get; set; }
        public int MaTaiKhoan { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual SANPHAM SANPHAM { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
