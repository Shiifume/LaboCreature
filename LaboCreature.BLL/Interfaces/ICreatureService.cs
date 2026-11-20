using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface ICreatureService
    {
        public void CreateCreature(int playerId, int raceId, string nickname); 
    }
}
