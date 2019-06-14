using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameApp.Models;

namespace BoardGameApp.Controllers
{
    [RoutePrefix("BoardGame")]
    public class BoardGameController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("List")]
        public ActionResult List()
        {
            ViewBoardGamesModel viewBoardGamesModel = new ViewBoardGamesModel();
            viewBoardGamesModel.BoardGames = _context.BoardGames.ToList();
            return View(viewBoardGamesModel);
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            BoardGame boardGame = _context.BoardGames.SingleOrDefault(m => m.Id == id);

            if (boardGame == null)
            {
                return HttpNotFound();
            }

            return View(boardGame);
        }

        [Route("Remove/{id}")]
        public ActionResult Remove(int id)
        {
            BoardGame boardGame = _context.BoardGames.SingleOrDefault(m => m.Id == id);

            if (boardGame == null)
            {
                return HttpNotFound();
            }

            _context.BoardGames.Remove(boardGame);
            _context.SaveChanges();

            return RedirectToAction("List", "BoardGame");
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            BoardGame boardGame = _context.BoardGames.SingleOrDefault(m => m.Id == id);

            if (boardGame == null)
            {
                return HttpNotFound();
            }

            return View(boardGame);
        }

        [Route("Update")]
        [HttpPost]
        public ActionResult Update(BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", boardGame);
            }
            else
            {
                BoardGame b = _context.BoardGames.SingleOrDefault(m => m.Id == boardGame.Id);
                b.Title = boardGame.Title;
                b.Publisher = boardGame.Publisher;
                b.Players = boardGame.Players;
                b.Playtime = boardGame.Playtime;
                b.Description = boardGame.Description;
                _context.SaveChanges();
                return RedirectToAction("List", "BoardGame");
            }
        }

        [Route("Save")]
        [HttpPost]
        public ActionResult Save(BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", boardGame);
            }
            else
            {
                _context.BoardGames.Add(boardGame);
                _context.SaveChanges();
                return RedirectToAction("Index", "BoardGame");
            }
        }

        [Route("Add")]
        public ActionResult Add(BoardGame boardGame)
        {
            return View(boardGame);
        }
    }
}