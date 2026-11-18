using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Mappers;
using LaboCreature.WebApp.Models.Members;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;

namespace LaboCreature.WebApp.Controllers
{
    public class AuthController : Controller
    {

        private IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginForm data)
        {
            if (!ModelState.IsValid)
                return View();

            MemberModel? memberLogged = _authService.Login(data.ToLoginModel());
            if (memberLogged is null)
            {
                ModelState.AddModelError("", "Email and password combination is invalid");
                return View(data);
            }

            await CreateMemberSessionAsync(memberLogged.Id, memberLogged.Username, memberLogged.Email);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterForm data)
        {
            if(!ModelState.IsValid)
                return View(data);

            RegisterMemberModel memberToRegister = new()
            {
                Username = data.Username.Trim().ToLower(),
                Email = data.Email.Trim().ToLower(),
                Password = data.Password
            };

            try
            {
                int memberId = _authService.CreateMember(memberToRegister);
                await CreateMemberSessionAsync(memberId, memberToRegister.Username, memberToRegister.Email);
                return RedirectToAction("Index", "Home");
            }
            catch(MemberEmailAlreadyExistsException e)
            {
                ModelState.AddModelError(nameof(data.Email), e.Message);
                return View(data);
            }
            catch(MemberUsernameAlreadyExistsException e)
            {
                ModelState.AddModelError(nameof(data.Username), e.Message);
                return View(data);
            }
        }

        private async Task CreateMemberSessionAsync(int memberId, string username, string email)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, memberId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {             
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Index", "Home");
        }


    }
}
