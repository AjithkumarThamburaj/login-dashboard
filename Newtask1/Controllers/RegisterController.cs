using Newtask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtask1.Controllers
{
    public class RegisterController : Controller
    {
        newtasksEntities1 db = new newtasksEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(Userdetail model)
        {
            db.Userdetails.Add(model);
            db.SaveChanges();
            return Json("Registration Succesfull", JsonRequestBehavior.AllowGet);   
        }

        public JsonResult CheckValidUser(Userdetail model)
        {
            string result = "Fail";
            var DataItems = db.Userdetails.Where(x => x.Username == model.Username && x.Password == model.Password).SingleOrDefault();
            if(DataItems!= null){
                Session["UserID"] = DataItems.Id.ToString();
                Session["USerName"] = DataItems.Username.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AfterLogin()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}