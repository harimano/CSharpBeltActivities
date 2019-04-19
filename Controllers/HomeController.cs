using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {

        public BeltContext dbContext;

        public HomeController(BeltContext context)
        {
            dbContext = context;
        }

        [Route("/success")]
        [HttpGet]
        public IActionResult Success(LoginUser userSubmit)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login","Login");
            }
        else{
            List<Models.Activity> allActivity = dbContext.Activity
                .Include(a =>a.RSVPs)
                .Include(w =>w.User)
                .OrderBy(c =>c.Date)
                .ToList();
            
            var temp = dbContext.Users
                    .FirstOrDefault(n =>n.UserId == HttpContext.Session.GetInt32("UserId") );
            ViewBag.fname = temp.FirstName;
            ViewBag.lname = temp.LastName;
            
            
            
            return View("Dashboard",allActivity);
            }
            
        }

        [Route("/addactivity")]
        [HttpGet]
        public IActionResult AddActivity()
        {
            
            return View("AddActivity");
        }

        [HttpPost("addtheactivity")]
        public IActionResult AddTheActivity(Models.Activity activity)
        {
            activity.UserId = (int) HttpContext.Session.GetInt32("UserId");
            if(ModelState.IsValid)
            {
                dbContext.Add(activity);
                dbContext.SaveChanges();
            }
            else
            {
                return View("AddActivity");
            }
            return RedirectToAction("Success");
        }

        [Route("/viewactivity/{id}")]
        [HttpGet]
        public IActionResult ViewActivity(int id)
        {
            Models.Activity queryact = dbContext.Activity
                    .Include(q =>q.RSVPs)
                    .ThenInclude(u =>u.User)
                    .FirstOrDefault(w =>w.ActivityId == id);
            
            var temp = dbContext.Users
                    .FirstOrDefault(n =>n.UserId == queryact.UserId);
            ViewBag.fname = temp.FirstName;
            ViewBag.lname = temp.LastName;
            return View("ViewActivity",queryact);
        }

        [Route("/delete/{id}")]
        [HttpGet]
        public IActionResult DeleteActivity(int id)
        {
            Models.Activity queryact = dbContext.Activity
                    .FirstOrDefault(w =>w.ActivityId == id);
                if(HttpContext.Session.GetInt32("UserId")==queryact.UserId)
                {
                    dbContext.Remove(queryact);
                    dbContext.SaveChanges();
                }
            return RedirectToAction("Success");
        }


        [Route("/rsvp/{id}")]
        [HttpGet]
        public IActionResult RSVPActivity(int id)
        {   
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            RSVP temprsvp = new RSVP();
            temprsvp.UserId = userid;
            temprsvp.ActivityId = id;
            dbContext.RSVP.Add(temprsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Success");
        }

        [Route("/unrsvp/{id}")]
        [HttpGet]
        public IActionResult UNRSVPActivity(int id)
        {   
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            RSVP temprsvp = dbContext.RSVP.FirstOrDefault(r => r.UserId == userid);
            
            dbContext.RSVP.Remove(temprsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Success");
        }
    }
}
