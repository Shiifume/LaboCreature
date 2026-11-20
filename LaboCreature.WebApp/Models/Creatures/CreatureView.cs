using LaboCreature.WebApp.Models.Players;

namespace LaboCreature.WebApp.Models.Creatures
{
    public class CreatureView
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public RaceView Race { get; set; }
    }
}
