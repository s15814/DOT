using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameApp.Models;

namespace BoardGameApp.Controllers
{
    [RoutePrefix("boardGame")]
    public class BoardGameController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("add")]
        public ActionResult Add(BoardGame boardGame)
        {
            return View();
        }
    }
}