using LaboCreature.BLL.Exceptions;
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
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _playerRepo;
        private IMemberRepository _memberRepo;

        public PlayerService(IPlayerRepository playerRepo, IMemberRepository memberRepo)
        {
            _playerRepo = playerRepo;
            _memberRepo = memberRepo;
        }
        
        public void UpdateMoney(int memberId, int amountModification)
        {
            if (amountModification == 0)
                return;

            PlayerEntity? player = _playerRepo.GetByMemberId(memberId);
            if (player is null)
                throw new PlayerNotFoundException("Player not found.");

            if (amountModification < 0 && player.Money < -amountModification)
                throw new PlayerNotEnoughMoneyException("Player does not have enough money");

            long resultMoney = (long)player.Money + amountModification;

            if (resultMoney > int.MaxValue)
                throw new PlayerMoneyLimitException("Player money exceeds limit.");

            _playerRepo.UpdateMoney(memberId, amountModification);
        }

        public int? GetPlayerMoney(int memberId)
        {
            return _playerRepo.GetByMemberId(memberId)?.Money;
        }

        public void CreatePlayer(int memberId)
        {
            //member exists
            MemberEntity? member = _memberRepo.GetById(memberId);

            if (member is null)
                throw new MemberNotFoundException("Member not found");

            //member does not have player yet
            if (member.Player != null)
                throw new PlayerAlreadyInMember("Member already has a player");

            PlayerEntity newPlayer = new()
            {
                MemberId = memberId,
                Money = 0
            };

            _playerRepo.CreatePlayer(newPlayer);
        }

        public PlayerModel? GetByMemberId(int memberId)
        {
            return _playerRepo.GetByMemberId(memberId)?.ToModel();
        }
    }
}
