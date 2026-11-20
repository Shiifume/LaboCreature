using LaboCreature.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaboCreature.WebApp.ViewComponents
{
    public class PlayerMoneyViewComponent : ViewComponent
    {
        private IPlayerService _playerService;

        public PlayerMoneyViewComponent(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public IViewComponentResult Invoke()
        {
            if (User.Identity?.IsAuthenticated != true)
                return Content("");

            int memberId = int.Parse(UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            int? money = _playerService.GetPlayerMoney(memberId);

            return View(money);
        }
    }
}
