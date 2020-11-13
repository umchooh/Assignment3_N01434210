using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01434210.Models;
//***************************************************************************************
//*Reference for educational purposes-build MVC
//*Title: Blog Project 2
//*Author: Christine Bittle
//* Date: Nov 5, 2020
//* Version :   https://github.com/christinebittle/BlogProject_2/commit/da1b948c83a10c5380621834412b9179a878ace0
//*Availability: https://github.com/christinebittle/BlogProject_2
//*
//***************************************************************************************/
namespace Assignment3_N01434210.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Classes/List
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Class> Classes = controller.ListClasses();
            return View(Classes);
        }

        //GET : /Classes/Show/{Classid}
        public ActionResult Show(int id)
        {
            ClassesDataController controller = new ClassesDataController();
            Class EachClass = controller.FindClass(id);

            return View(EachClass);
        }
    }
}