using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface IPlayerService
    {
        public void UpdateMoney(int memberId,  int amount);
        int? GetPlayerMoney(int memberId);
        public void CreatePlayer(int memberId);
        public PlayerModel? GetByMemberId(int memberId);
    }
}
