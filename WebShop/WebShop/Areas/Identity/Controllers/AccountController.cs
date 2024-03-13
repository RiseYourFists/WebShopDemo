
namespace WebShop.App.Areas.Identity.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using WebShop.Core.Models.Identity;
    using WebShop.Services.Models.Account;
    using static Services.ErrorMessages.AccountErrorMsgs.LoginErrors;
    using System.Web;

    [Authorize]
    [Area(nameof(Identity))]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            var model = new LoginModel();
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                model.ReturnUrl = returnUrl;
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !user.IsActive)
            {
                ModelState.AddModelError(string.Empty, UserNotFound);
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, UserNotFound);
                return View(model);
            }

            var fullname = $"{user.FirstName} {user.LastName}";

            HttpContext.Session.SetString("UserFullName", fullname);

            if (model.ReturnUrl != null)
            {
                string decodedReturnUrl = HttpUtility
                    .UrlDecode(model.ReturnUrl)
                    .Replace("amp;", "");

                if (decodedReturnUrl.StartsWith("/"))
                {
                    return Redirect(decodedReturnUrl);
                }
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            var message = "You don't have permission to access this page.";
            return RedirectToAction("DetailedError", "Error", new { Message = message });
        }
    }
}
