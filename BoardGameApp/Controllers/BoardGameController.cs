using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameApp.Models;
using Microsoft.AspNet.Identity;

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
            if (viewBoardGamesModel.BoardGames.Count == 0)
            {
                Info("Aktualnie nie mamy żadnych gier.");
            }
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
        [Authorize(Roles = Notifications.ROLE_EMPLOYEE)]
        public ActionResult Remove(int id)
        {
            BoardGame boardGame = _context.BoardGames.SingleOrDefault(m => m.Id == id);

            if (boardGame == null)
            {
                return HttpNotFound();
            }

            _context.BoardGames.Remove(boardGame);
            _context.SaveChanges();

            Info("Gra usunięta.");

            return RedirectToAction("List", "BoardGame");
        }

        [Route("Edit/{id}")]
        [Authorize(Roles = Notifications.ROLE_EMPLOYEE)]
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Notifications.ROLE_EMPLOYEE)]
        public ActionResult Update(BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                Alert("Nie udało się edytować gry.");
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
                Info("Dane gry zaktualizowane.");
                return RedirectToAction("List", "BoardGame");
            }
        }

        [Route("Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = Notifications.ROLE_EMPLOYEE)]
        public ActionResult Save(BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                Alert("Nie udało się dodać nowej gry.");
                return View("Add", boardGame);
            }
            else
            {
                _context.BoardGames.Add(boardGame);
                for (int i = 0; i < boardGame.Amount; i++)
                {
                    BoardGameCopy c = new BoardGameCopy
                    {
                        BoardGameRefId = boardGame.Id
                    };
                    _context.BoardGameCopies.Add(c);
                }
                _context.SaveChanges();
                Info("Nowa gra dodana.");
                return RedirectToAction("Index", "BoardGame");
            }
        }

        [Route("Add")]
        [Authorize]
        [Authorize(Roles = Notifications.ROLE_EMPLOYEE)]
        public ActionResult Add()
        {
            return View();
        }

        [Route("Order/{id}")]
        [Authorize]
        public ActionResult Order(int id)
        {
            BoardGame b = _context.BoardGames.Single(m => m.Id == id);
            string clientId = User.Identity.GetUserId(); //readerId is same as userId
            Client r = _context.Clients.First(m => m.Id == clientId);
            if (b == null)
            {
                Alert("Użyto niewłaściwego id gry.");
            }
            else
            {
                if (b.Amount <= 0)
                {
                    Alert("Gra jest niedostępna.");
                }
                else
                {
                    List<BoardGameCopy> boardGameCopies =
                        _context.BoardGameCopies.Where(m => m.ClientRefId == clientId).ToList();

                    BoardGameCopy temp = boardGameCopies.Find(m => m.BoardGameRefId == id);

                    if (temp == null)
                    {
                        b.Amount -= 1;
                        BoardGameCopy copy = _context.BoardGameCopies.First(
                            m => (m.BoardGameRefId == id && m.ClientRefId == null)
                        );
                        copy.ClientRefId = clientId;

                        _context.SaveChanges();
                        Success("Zarezerwowano grę.");
                    }
                    else
                    {
                        Alert("Już wypożyczyłeś tą grę, można wypożyczyć tylko jeden egzemplarz danej gry.");
                        return RedirectToAction("Details/" + id, "BoardGame");
                    }
                }
            }
            return RedirectToAction("Index", "BoardGame");
        }
    }
}