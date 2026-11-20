using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerModel ToModel(this PlayerEntity entity)
        {
            return new PlayerModel
            {
                Id = entity.Id,
                Money = entity.Money,
                MemberId = entity.MemberId,
                Creatures = entity.Creatures.Select(e => e.ToModel()).ToList()
            };
        }

        public static PlayerEntity ToEntity(this PlayerModel model)
        {
            return new PlayerEntity
            {
                Id = model.Id,
                Money = model.Money,
                MemberId = model.MemberId
            };
        }
    }
}
