using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public PlayerModel? Player { get; set; }
    }
}
