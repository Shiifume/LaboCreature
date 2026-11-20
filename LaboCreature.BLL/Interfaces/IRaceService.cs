using LaboCreature.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface IRaceService
    {
        public RaceModel? GetById(int id);
        public IEnumerable<RaceModel> GetAll();
    }
}
