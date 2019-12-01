﻿using RentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentSystem.Controllers
{
    public class UserPageController : Controller
    {
        private RentContext context = ContextHolder.Context;

        // GET: UserPage
        public ActionResult Show()
        {

            var sessionVar = Session["userId"];
            if (sessionVar != null) { 
            var id = Int32.Parse(sessionVar.ToString());
            var users = context.Users;
            User user = users
                .ToList()
                .FindLast(
                delegate (User currentUser) {
                    return currentUser.User_Id == id;
                });
            ViewBag.User = user;
            List<Appartment> appartments = context.Appartments.ToList().FindAll(
                delegate (Appartment appartment)
                {
                    return appartment.Landlord_Id == user.User_Id;
                });
            ViewBag.Appartments = appartments;
            return View();
        }
            else
                return View("~/Views/NotAuthorized/Index.cshtml");
        }
    }
}