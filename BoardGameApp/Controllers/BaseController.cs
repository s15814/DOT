using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameApp.Models;

namespace BoardGameApp.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext _context;

        public BaseController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        protected void Info(string info)
        {
            @TempData[Notifications.MESSAGE_INFO] = info;

        }

        protected void Alert(string alert)
        {
            @TempData[Notifications.MESSAGE_ALERT] = alert;
        }

        protected void Success(string success)
        {
            @TempData[Notifications.MESSAGE_SUCCESS] = success;
        }
    }
}