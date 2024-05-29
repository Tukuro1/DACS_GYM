using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ProductController : Controller
    {
        GYM_ManagerEntities db = new GYM_ManagerEntities();

        public ViewResult List(int? page = 1)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            var products = db.SANPHAMs.OrderBy(x =>x.MaSanPham).ToPagedList(pageNum, pageSize);
            return View(products);
        }
    }
}