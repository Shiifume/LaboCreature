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
        private IRaceService _raceService;
        private ICreatureService _creatureService;

        private const int _startingCoins = 100;

        public PlayerController(IPlayerService playerService, IRaceService raceService, ICreatureService creatureService)
        {
            _playerService = playerService;
            _raceService = raceService;
            _creatureService = creatureService;
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
        public IActionResult CreateCreature(int raceId, string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter a name for your creature."
                });
            }

            int memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            PlayerModel? player = _playerService.GetByMemberId(memberId);

            if (player is null)
            {
                return Json(new
                {
                    success = false,
                    redirectUrl = Url.Action("Welcome", "Player")
                });
            }

            try
            {
                _creatureService.CreateCreature(player.Id, raceId, nickname);

                return Json(new
                {
                    success = true,
                    message = "Your creature has been created!"
                });
            }
            catch (PlayerHasReachedMaxCreatures)
            {
                return Json(new
                {
                    success = false,
                    message = "You can only have 3 creatures."
                });
            }
            catch (RaceNotFoundException)
            {
                return Json(new
                {
                    success = false,
                    message = "The selected race doesn't exist."
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

        [HttpGet]
        public IActionResult GetRandomRaces()
        {
            var races = _raceService.GetAll()
                .OrderBy(_ => Random.Shared.Next())
                .Take(3)
                .ToList();

            return Json(races);
        }
    }
}