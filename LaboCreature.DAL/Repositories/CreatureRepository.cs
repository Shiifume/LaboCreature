using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Repositories
{
    public class CreatureRepository : ICreatureRepository
    {
        private CreatureDBContext _context;

        public CreatureRepository(CreatureDBContext context)
        {
            _context = context;
        }
        public int CreateCreature(CreatureEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public CreatureEntity? GetByPlayerId(int playerId)
        {
            return _context.Creatures.Include(c => c.Player)
                .Include(c => c.Race)
                .FirstOrDefault(c => c.PlayerId == playerId);
        }
    }
}
