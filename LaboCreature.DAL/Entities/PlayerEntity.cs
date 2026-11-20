using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace LaboCreature.DAL.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public int MemberId { get; set; }
        public MemberEntity Member { get; set; }
        public List<CreatureEntity> Creatures { get; set; } = new();
    }
}
