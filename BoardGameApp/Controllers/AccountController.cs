using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BoardGameApp.App_Start;
using BoardGameApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BoardGameApp.Controllers
{
    [Authorize]
    [RoutePrefix("Account")]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Alert("Niepoprawne dane logowania!");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                    Alert("Logowanie nieudane.");
                    return View(model);
                default:
                    Alert("W trakcie logowania nastąpił błąd.");
                    return View(model);
            }
        }

        [Route("Register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, Notifications.ROLE_USER);
                    //UserManager.AddToRole(user.Id, Notifications.ROLE_EMPLOYEE);
                    //UserManager.AddToRole(user.Id, Notifications.ROLE_ADMIN);
                    Client client = new Client
                    {
                        Id = user.Id,
                        BoardGameCopies = new List<BoardGameCopy>(),
                        Name = model.Name,
                        Surname = model.Surname
                    };
                    _context.Clients.Add(client);
                    _context.SaveChanges();

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                Alert(result.Errors.First());
            }
            else
            {
                Alert("Niepoprawne dane!");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Route("LogOff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Route("Details")]
        public ActionResult Details()
        {
            return View();
        }

        [Route("MyData")]
        public ActionResult UserData()
        {
            UserDataViewModel viewModel = new UserDataViewModel();
            string clientId = User.Identity.GetUserId();
            Client r = _context.Clients.First(m => m.Id == clientId);
            viewModel.Client = r;
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        [Authorize]
        [Route("UserBoardGames")]
        public ActionResult UserBoardGames()
        {
            ViewBoardGamesModel viewBoardGamesModel = new ViewBoardGamesModel
            {
                BoardGames = new List<BoardGame>()
            };
            string clientId = User.Identity.GetUserId();
            List<BoardGameCopy> boardGameCopies =
                _context.BoardGameCopies.Where(m => m.ClientRefId == clientId).ToList();

            if (boardGameCopies.Count == 0)
            {
                Info("Nie zarezerwowałeś żadnych gier!");
            }

            foreach (var temp in boardGameCopies)
            {
                BoardGame b = _context.BoardGames.Where(m => m.Id == temp.BoardGameRefId).First();

                if (b != null)
                {
                    viewBoardGamesModel.BoardGames.Add(b);
                }
            }
            return View(viewBoardGamesModel);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}