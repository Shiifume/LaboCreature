using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Entities
{
    public class CreatureEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RaceId { get; set; }
        public RaceEntity Race { get; set; } = null!;
        public int PlayerId { get; set; }
        public PlayerEntity Player { get; set; } = null!;
    }
}
