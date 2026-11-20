using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Interfaces;
using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Services
{
    public class CreatureService : ICreatureService
    {
        private ICreatureRepository _creatureRepo;
        private IRaceRepository _raceRepo;
        private IPlayerRepository _playerRepo;

        public CreatureService(ICreatureRepository creatureRepository, IRaceRepository raceRepo, IPlayerRepository playerRepo   )
        {
            _creatureRepo = creatureRepository;
            _raceRepo = raceRepo;
            _playerRepo = playerRepo;
        }

        public void CreateCreature(int playerId, int raceId, string nickname)
        {
            PlayerEntity? player = _playerRepo.GetById(playerId);
            if(player is null)
                throw new PlayerNotFoundException();

            if (player.Creatures.Count >= 3)
                throw new PlayerHasReachedMaxCreatures();

            RaceEntity? race = _raceRepo.GetById(raceId);
            if (race is null)
                throw new RaceNotFoundException();

            CreatureEntity creature = new CreatureEntity()
            {
                Name = nickname,
                RaceId = raceId,
                PlayerId = playerId
            };

            _creatureRepo.CreateCreature(creature);
        }
    }
}
