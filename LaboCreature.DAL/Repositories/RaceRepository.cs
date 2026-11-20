using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Repositories
{
    public class RaceRepository : IRaceRepository
    {

        private CreatureDBContext _context;
        public RaceRepository(CreatureDBContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceEntity> GetAll()
        {
            return _context.Races.ToList();
        }

        public RaceEntity? GetById(int id)
        {
            return _context.Races.SingleOrDefault(r => r.Id == id);
        }
    }
}
