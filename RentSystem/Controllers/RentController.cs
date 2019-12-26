﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentSystem.Models;

namespace RentSystem.Controllers
{
    public class RentController : Controller
    {

        private RentContext context = ContextHolder.Context;

        // GET: Rent
        public ActionResult Index()
        {
            var apps = context.Appartments;
            List<Appartment> appartments = apps.ToList();
            ViewBag.Appartments = appartments;

            return View();
        }

        [HttpGet]
        public ActionResult Issue(int buildingId)
        { 
            Appartment appartment = context.Appartments.ToList().FindLast(
            delegate (Appartment appartments)
            {
                return appartments.Building_Id == buildingId;
            }
            );
            ViewBag.Appartment = appartment;
            ViewBag.Landlord = context.Users.ToList().FindLast(
            delegate (User user)
            {
                return user.User_Id == appartment.Landlord_Id;
            }
            );
            ViewBag.UserId = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Rented(Rent rent, Object NumOfMonths)
        {
            if (rent.Start_Date != null && NumOfMonths != null)
            {
                var sessionVar = Session["userId"];
                if (sessionVar != null)
                {
                    rent.User_Id = Int32.Parse(sessionVar.ToString());
                    rent.Finish_Date = rent.Start_Date.AddMonths(Int32.Parse(NumOfMonths.ToString()));
                    var appartmentId = rent.Building_Id;
                    context.Appartments.Find(appartmentId).Status = "rented";
                    context.Rent.Add(rent);
                    context.SaveChanges();
                    return View();
                }
                else
                    return View("~/Views/NotAuthorized/Index.cshtml");
            }
            else
                return View("Issue", rent.Building_Id);
        }
    }
}
