using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Mappers;
using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Services
{
    public class RaceService : IRaceService
    {
        private IRaceRepository _raceRepository;

        public RaceService(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public IEnumerable<RaceModel> GetAll()
        {
            List<RaceEntity> entities = _raceRepository.GetAll().ToList();

            return entities.Select(e => e.ToModel());

        }

        public RaceModel? GetById(int id)
        {
            RaceEntity? race = _raceRepository.GetById(id);
            if (race is null)
                return null;

            return race.ToModel();
        }
    }
}
