using LaboCreature.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Interfaces
{
    public interface ICreatureRepository
    {
        int CreateCreature(CreatureEntity entity);
    }
}
