using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        public int CreatePlayer(PlayerEntity entityToAdd);
        public PlayerEntity? GetByMemberId(int memberId);
        public bool UpdateMoney(int memberId, int newAmount);
        public PlayerEntity? GetById(int id);
    }
}
