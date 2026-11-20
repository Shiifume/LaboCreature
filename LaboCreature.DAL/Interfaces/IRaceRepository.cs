using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Interfaces
{
    public interface IRaceRepository
    {
        public RaceEntity? GetById(int id);
        public IEnumerable<RaceEntity> GetAll();
    }
}
