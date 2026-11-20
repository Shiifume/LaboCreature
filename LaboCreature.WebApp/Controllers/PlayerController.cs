using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Models;
using LaboCreature.BLL.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaboCreature.WebApp.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {

        private IPlayerService _playerService;

        private const int _startingCoins = 100;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        
        [HttpGet]
        public IActionResult Welcome()
        {
            int memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            PlayerModel? model = _playerService.GetByMemberId(memberId);
            if (model != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlayer()
        {
            int memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            
            try
            {
                _playerService.CreatePlayer(memberId);
                return Json(new { success = true,
                                  message = "Player succesfully created."});
            }
            catch(MemberNotFoundException)
            {
                await HttpContext.SignOutAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme
                                );

                return Json(new
                {
                    success = false,
                    redirectUrl = Url.Action("Login", "Member")
                });
            }
            catch(PlayerAlreadyInMember)
            {
                TempData["message"] = "You already have a player!";
                return Json(new
                {
                    success = false,
                    redirectUrl = Url.Action("Index", "Home")
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFirstCoins()
        {
            int memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            try
            {
                _playerService.UpdateMoney(memberId, _startingCoins);
                return Json(new
                {
                    success = true
                });

            }
            catch(PlayerNotFoundException)
            {
                return Json(new
                {
                    success = false,
                    message = "Player not found.",
                    redirectUrl = Url.Action("Welcome", "Player")
                });
            }
        }
    }
}