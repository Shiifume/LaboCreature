using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Models
{
    public class CreatureModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PlayerId { get; set; }
        public RaceModel Race { get; set; } = null!;
    }
}
