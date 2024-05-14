using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.ViewModel
{
    public class AccountViewModel
    {
        GYM_ManagerEntities db = new GYM_ManagerEntities();
        public TAIKHOAN TaiKhoan
        {
            get;
            set;
        }

        public TAIKHOAN find(string username)
        {
            return db.TAIKHOANs.Single(acc => acc.TaiKhoan1.Equals(username));
        }

        public TAIKHOAN login(string username, string password)
        {
            return db.TAIKHOANs.Where(acc => acc.TaiKhoan1.Equals(username) && acc.MatKhau.Equals(password)).FirstOrDefault();
        }
    }
}