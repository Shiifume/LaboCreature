using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Entities
{
    public class MemberEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public PlayerEntity? Player { get; set; }
    }
}
