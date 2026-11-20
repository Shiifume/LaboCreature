using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Models.Players;

namespace LaboCreature.WebApp.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerView ToView(this PlayerModel model)
        {
            return new PlayerView()
            {
                Id = model.Id,
                Money = model.Money,
                Creatures = model.Creatures.Select(c => c.ToView()),
            };
        }
    }
}
