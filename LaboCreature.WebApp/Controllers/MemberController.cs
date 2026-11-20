using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Models;
using LaboCreature.BLL.Services;
using LaboCreature.WebApp.Mappers;
using LaboCreature.WebApp.Models.Members;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaboCreature.WebApp.Controllers
{
    public class MemberController : Controller
    {
        private IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            MemberModel? member = _memberService.GetById(id);

            if(member is null)
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                return RedirectToAction("Index", "Home");
            }

            MemberView memberToShow = member.ToModelView();
            
            return View(memberToShow);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeUsername()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeUsername(MemberUpdateUsernameForm data)
        {
            if(!ModelState.IsValid)
                return View(data);
            
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            try
            {
                _memberService.ChangeUsername(id, data.NewUsername, data.Password);
            }
            catch(MemberNotFoundException)
            {
                TempData["message"] = "Error. This member does not exist.";

                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
                return RedirectToAction("Index", "Home");
            }
            catch(MemberUsernameAlreadyExistsException ex)
            {
                ModelState.AddModelError(nameof(data.NewUsername), ex.Message);
                return View(data);
            }
            catch(MemberPasswordInvalidException ex)
            {
                ModelState.AddModelError(nameof(data.Password), ex.Message);
                return View(data);
            }

            TempData["message"] = "Username succesfully changed";
            return RedirectToAction(nameof(Details));
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(MemberUpdateEmailForm data)
        {
            if(!ModelState.IsValid)
                return View(data);

            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            try
            {
                _memberService.ChangeEmail(id, data.NewEmail, data.Password);
            }
            catch (MemberNotFoundException)
            {
                TempData["message"] = "Error. This member does not exist.";

                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
                return RedirectToAction("Index", "Home");
            }
            catch (MemberEmailAlreadyExistsException ex)
            {
                ModelState.AddModelError(nameof(data.NewEmail), ex.Message);
                return View(data);
            }
            catch (MemberPasswordInvalidException ex)
            {
                ModelState.AddModelError(nameof(data.Password), ex.Message);
                return View(data);
            }

            TempData["message"] = "Email succesfully changed";
            return RedirectToAction(nameof(Details));

        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(MemberUpdatePasswordForm data)
        {
            if (!ModelState.IsValid)
                return View(data);

            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            try
            {
                _memberService.ChangePassword(id, data.NewPassword, data.CurrentPassword);
            }
            catch (MemberNotFoundException)
            {
                TempData["message"] = "Error. This member does not exist.";

                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
                return RedirectToAction("Index", "Home");
            }
            catch (MemberPasswordInvalidException ex)
            {
                ModelState.AddModelError(nameof(data.CurrentPassword), ex.Message);
                return View(data);
            }

            TempData["message"] = "Password succesfully changed";
            return RedirectToAction(nameof(Details));

        }

        [HttpGet]
        public IActionResult CheckUsername(string username)
        {
            if (username is null || username.Trim() == string.Empty)
            {
                return Json(new
                {
                    available = false,
                    message = "Please enter a username."
                });
            }

            if (username.Trim().Length < 5)
            {
                return Json(new
                {
                    available = false,
                    message = "Username must me at least 5 characters long"
                });
            }

            bool usernameExists = _memberService.GetByUsername(username.ToLower()) != null;

            return Json(new
            {
                available = !usernameExists,
                message = usernameExists
                    ? "Username already taken."
                    : "Username available!"
            });
        }
    }
}
