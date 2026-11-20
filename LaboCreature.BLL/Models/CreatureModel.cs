using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Models
{
    public class CreatureModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public RaceModel Race { get; set; } = null!;
        public PlayerModel Player { get; set; } = null!;
    }
}
