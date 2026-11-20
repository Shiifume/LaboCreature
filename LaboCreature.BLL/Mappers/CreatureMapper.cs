using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Mappers
{
    public static class CreatureMapper
    {
        public static CreatureModel ToModel(this CreatureEntity entity)
        {
            return new CreatureModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                PlayerId = entity.PlayerId,
                Race = entity.Race.ToModel()
            };
        }

        public static CreatureEntity ToEntity(this CreatureModel model)
        {
            return new CreatureEntity()
            {
                Id = model.Id,
                Name = model.Name,
                PlayerId = model.PlayerId,
                RaceId = model.Race.Id
            };
        }
    }
}
