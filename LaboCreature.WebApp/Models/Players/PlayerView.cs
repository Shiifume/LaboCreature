using LaboCreature.WebApp.Models.Creatures;
using LaboCreature.WebApp.Models.Members;

namespace LaboCreature.WebApp.Models.Players
{
    public class PlayerView
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public IEnumerable<CreatureView> Creatures { get; set; } = new List<CreatureView>();
    }
}
