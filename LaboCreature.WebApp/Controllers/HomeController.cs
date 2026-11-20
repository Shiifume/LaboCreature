using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Services;
using LaboCreature.WebApp.Mappers;
using LaboCreature.WebApp.Models;
using LaboCreature.WebApp.Models.Creatures;
using LaboCreature.WebApp.Models.Members;
using LaboCreature.WebApp.Models.Players;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LaboCreature.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private IMemberService _memberService;
        public HomeController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction(nameof(Dashboard));
            else
                return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            MemberView? member = _memberService.GetById(id)?.ToModelView();
            if(member is null)
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            if (member.Player is null)
                return RedirectToAction("Welcome", "Player");
            
            return View(member);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
