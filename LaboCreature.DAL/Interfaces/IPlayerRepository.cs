using LaboCreature.DAL.Entities;
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
    }
}
