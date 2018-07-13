using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeekOne.Common;
using WeekOne.Models;

namespace WeekOne.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        客戶資料Repository 客戶資料Repo;
        客戶銀行資訊Repository 客戶銀行資訊Repo;
        客戶聯絡人Repository 客戶聯絡人Repo;

        public 客戶資料Controller() {
            客戶資料Repo = RepositoryHelper.Get客戶資料Repository(unitOfWork);
            客戶銀行資訊Repo = RepositoryHelper.Get客戶銀行資訊Repository(unitOfWork);
            客戶聯絡人Repo = RepositoryHelper.Get客戶聯絡人Repository(unitOfWork);
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            var list = 客戶資料Repo.All().AsQueryable();
            ViewBag.CList = Selector.GetCList();
            ViewBag.CustomClass = Selector.GetCustomerClass();

            return View(list);
        }


        //public ActionResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //    var students = from s in db.Students
        //                   select s;
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            students = students.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            students = students.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "date_desc":
        //            students = students.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.LastName);
        //            break;
        //    }
        //    return View(students.ToList());
        //}


        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            if(客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            //ViewBag.CustomerClass = Selector.GetCustomerClass();
            ViewBag.客戶分類 = new SelectList(Selector.GetCustomerClass(), "Value", "Text");

            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料Repo.Add(客戶資料);
                客戶資料Repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = 客戶資料Repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            客戶銀行資訊Repo.Delete(客戶資料.Id);
            客戶聯絡人Repo.Delete(客戶資料.Id);

            客戶資料Repo.Delete(客戶資料);
            客戶資料Repo.UnitOfWork.Commit();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶資料Repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string sortBy, string keyword)
        {
            var data = 客戶資料Repo.Search(sortBy, keyword);
            ViewBag.CList =new SelectList(Selector.GetCList(), "Value", "Text", sortBy);

            return View("Index", data);
        }
    }
}
