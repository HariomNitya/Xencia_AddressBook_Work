using hariDemo.Models;
using hariDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hariDemo.Controllers
{
    public class AddressBookController : Controller
    {
        // GET: AddressBook
        public ActionResult GetAllDetails()
        {

            DBrepository EmpRepo = new DBrepository();
            ModelState.Clear();
            return View(EmpRepo.GetDetails());
        }

        public ActionResult AddDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDetails(AddressBookModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBrepository EmpRepo = new DBrepository();
                    if (EmpRepo.AddDetails(obj))
                    {
                        ViewBag.Message = "Details added successfully";
                    }
                    return RedirectToAction("GetAllDetails");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditDetails(int id)
        {
            DBrepository EmpRepo = new DBrepository();

            return View(EmpRepo.GetDetails().Find(Emp => Emp.Id == id));
        }
        [HttpPost]
        public ActionResult EditDetails(int id, AddressBookModel obj)
        {
            try
            {
                DBrepository EmpRepo = new DBrepository();
                EmpRepo.UpdateDetails(obj);
                return RedirectToAction("GetAllDetails");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteDetails(int id)
        {
            try
            {
                DBrepository EmpRepo = new DBrepository();
                if (EmpRepo.DeleteDetails(id))
                {
                    ViewBag.AlertMsg = "Details deleted successfully";

                }
                return RedirectToAction("GetAllDetails");

            }
            catch
            {
                return View();
            }
        }        
    }
}