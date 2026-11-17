using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Interfaces;
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
        public IActionResult Details()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            MemberView memberToShow = _memberService.GetById(id)!.ToModelView();
            
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
    }
}
