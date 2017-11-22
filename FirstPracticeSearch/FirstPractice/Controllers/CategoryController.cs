using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstPractice.Models;
using PagedList;
using PagedList.Mvc;

namespace FirstPractice.Controllers
{
    public class CategoryController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        private int pageSize = 5;
        // GET: Category
        public ActionResult Index(int page=1)
        {
          
            //跟Model要資料
            List<Category> categories = CategoryDataContext.LoadCategories();
            int currentPage = page < 1 ? 1 : page;
            var category = db.Categories.OrderBy(x => x.CategoryID);//資料排序並挑出所有欄位資料
            var result = categories.ToPagedList(currentPage, pageSize);
            //將資料傳給View
            return View(result);
        }
        [HttpPost]
        public ActionResult Index(string id,int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            var category = db.Categories.OrderBy(x => x.CategoryID);//資料排序並挑出所有欄位資料
            var category1 = category.Where(xx => xx.CategoryName.Contains(id));//搜尋排序過的資料
            var result = category1.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        public ActionResult Insert()
        {
            if(Request.Form.Count>0)
            {
                //接收表單傳過來的資料
                Category _category = new Category();
                _category.CategoryName = Request.Form["CategoryName"];
                _category.Description = Request.Form["Description"];
                //將資料傳給Model
                CategoryDataContext.InsertCategory(_category);
                //轉到另外一個Action
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id=0)
        {
            CategoryDataContext.DeleteCategory(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            Category _category = CategoryDataContext.LoadCategoryByID(id);
            return View(_category);
        }
        [HttpPost]
        public ActionResult Edit()
        {
            Category _category = new Category();
            _category.CategoryID = Convert.ToInt32(Request.Form["CategoryID"]);
            _category.CategoryName = Request.Form["CategoryName"];
            _category.Description = Request.Form["Description"];
            CategoryDataContext.EditCategory(_category);
            return RedirectToAction("Index");
        }
    }
}