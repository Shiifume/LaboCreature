using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Models
{
    public class RegisterMemberModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
