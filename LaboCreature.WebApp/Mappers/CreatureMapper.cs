using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Models.Creatures;
using System.Runtime.CompilerServices;

namespace LaboCreature.WebApp.Mappers
{
    public static class CreatureMapper
    {
        public static CreatureView ToView(this CreatureModel model)
        {
            return new CreatureView()
            {
                Id = model.Id,
                Nickname = model.Name,
                Race = model.Race.ToView()
            };
        }
    }
}
