using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_SP_No_File.Models;

namespace CRUD_SP_No_File.Controllers
{
    public class HomeController : Controller
    {
        EmployeeMaster em=new EmployeeMaster();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            //BindCountry();
            return View();
        }
        //[NonAction]
        //void BindCountry()
        //{
        //    string cmd = "Select * from StateMaster";
        //    DataTable dt = em.ExecuteQuery(cmd);
        //    List<SelectListItem> lst = new List<SelectListItem>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        SelectListItem item = new SelectListItem();
        //        item.Text = dr["StateName"].ToString();
        //        item.Value = dr["StateId"].ToString();
        //        lst.Add(item);
        //    }
        //    ViewBag.StateName = lst;
        //}
        public JsonResult Index(EmployeeMaster em)
        {
            ViewBag.msg = em.AddEmployee();
            var a=ViewBag.msg;
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowAll(EmployeeMaster em)
        {
            List<EmployeeMaster> lst = em.GetAllEmployee();
            return View(lst);
        }
        [HttpGet]
        public ActionResult Delete(int RId)
        {
            EmployeeMaster em = new EmployeeMaster();
            TempData["msg"] = em.DeleteEmployee(RId);
            return RedirectToAction("ShowAll");
        }
        [HttpGet]
        public ActionResult Edit(int RId)
        {
            EmployeeMaster em = new EmployeeMaster();
            em=em.GetSingleEmployee(RId);
            return View(em);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeMaster em)
        {
            TempData["msg"]=em.UpdateEmployee(em.RId);
            return RedirectToAction("ShowAll");
        }
    }
}