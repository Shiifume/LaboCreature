using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Mappers
{
    public static class MemberMapper
    {
        public static MemberModel ToModel(this MemberEntity entity)
        {
            return new MemberModel()
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                Player = entity.Player?.ToModel()
            };
        }
    }
}
