using LaboCreature.BLL.Interfaces;
using LaboCreature.WebApp.Mappers;
using LaboCreature.WebApp.Models;
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
    }
}
