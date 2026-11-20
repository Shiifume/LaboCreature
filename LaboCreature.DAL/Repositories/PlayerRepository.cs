using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly CreatureDBContext _context;

        public PlayerRepository(CreatureDBContext context)
        {
            _context = context;
        }

        public int CreatePlayer(PlayerEntity playerToAdd)
        {
            _context.Players.Add(playerToAdd);
            _context.SaveChanges();

            return playerToAdd.Id;
        }

        public PlayerEntity? GetByMemberId(int memberId)
        {
            return _context.Players.SingleOrDefault(p => p.MemberId == memberId);
        }

        public PlayerEntity? GetById(int id)
        {
            return _context.Players.SingleOrDefault(p => p.Id == id);
        }

        public bool UpdateMoney(int memberId, int amountModification)
        {
            PlayerEntity? playerToUdate = _context.Players.SingleOrDefault(p => p.MemberId == memberId);
            if (playerToUdate == null)
                return false;
            
            playerToUdate.Money += amountModification;
            _context.SaveChanges();
            return true;
        }
    }
}
