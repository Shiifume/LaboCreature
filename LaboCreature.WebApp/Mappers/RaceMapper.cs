using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Models;

namespace LaboCreature.WebApp.Mappers
{
    public static class RaceMapper
    {
        public static RaceView ToView(this RaceModel model)
        {
            return new RaceView()
            {
                Id = model.Id,
                Name = model.Name,
                FrontSprite = model.FrontSprite
            };
        }
    }
}
