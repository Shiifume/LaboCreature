using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LaboCreature.BLL.Mappers
{
    public static class RaceMapper
    {
        public static RaceModel ToModel(this RaceEntity entity)
        {
            return new RaceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                FrontSprite = entity.FrontSprite
            };
        }

        public static RaceEntity ToEntity(this RaceModel model)
        {
            return new RaceEntity()
            {
                Id = model.Id,
                Name = model.Name,
                FrontSprite = model.FrontSprite
            };
        }
    }
}
