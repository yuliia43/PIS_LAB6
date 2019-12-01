﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentSystem.Models;

namespace RentSystem.Controllers
{
    public class AddAppartmentController : Controller
    {
        //// GET: AddAppartment
        ////[HttpGet]
        //public ActionResult Added()
        //{
        //    return View();
        //}
        private RentContext context = ContextHolder.Context;

        [HttpGet]
        public ActionResult Adding()
        {
            var sessionVar = Session["userId"];
            if (sessionVar != null)
            {
                ViewBag.Id = sessionVar.ToString();
                return View();
            }
            else
                return View("~/Views/NotAuthorized/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Added(Appartment appartments)
        {
            var id = Int32.Parse(Session["userId"].ToString());
            appartments.Status = "not rented";
            appartments.Landlord_Id = id;
            context.Appartments.Add(appartments);
            context.SaveChanges();
            return View();
        }
    }
}