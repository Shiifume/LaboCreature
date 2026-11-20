using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Entities
{
    public class RaceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string FrontSprite { get; set; } = null!;
    }
}
