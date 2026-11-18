using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Helpers;
using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Mappers;
using LaboCreature.BLL.Models;
using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Services
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _repo;
        private IPasswordHasher _hasher;

        public MemberService(IMemberRepository repo, IPasswordHasher hasher)
        {
            _repo = repo;
            _hasher = hasher;
        }
        
        public MemberModel? GetById(int id)
        {
            MemberEntity? member = _repo.GetById(id);
            if (member is null)
                return null;

            return member.ToModel();
        }

        public MemberModel? GetByUsername(string username)
        {
            MemberEntity? member = _repo.GetByUsername(username);
            if (member is null)
                return null;

            return member.ToModel();
        }
        public void ChangeUsername(int id, string newUsername, string password)
        {
            MemberEntity? member = _repo.GetById(id);
            if (member is null)
                throw new MemberNotFoundException("Member do not exist");

            MemberEntity? existingMember = _repo.GetByUsername(newUsername.ToLower().Trim());

            if (existingMember is not null && existingMember.Username != member.Username)
                throw new MemberUsernameAlreadyExistsException("Username taken");

            if (!_hasher.Verify(password, member.PasswordHash))
                throw new MemberPasswordInvalidException("Invalid password");

            _repo.UpdateUsername(id, newUsername);
        }

        public void ChangeEmail(int id, string newEmail, string password)
        {
            MemberEntity? member = _repo.GetById(id);
            if (member is null)
                throw new MemberNotFoundException("Member do not exist"); 

            MemberEntity? existingMember = _repo.GetByEmail(newEmail.ToLower().Trim());

            if (existingMember is not null && existingMember.Email != member.Email)
                throw new MemberEmailAlreadyExistsException("Email already in use");

            if (!_hasher.Verify(password, member.PasswordHash))
                throw new MemberPasswordInvalidException("Invalid password");

            _repo.UpdateEmail(id, newEmail);
        }

        public void ChangePassword(int id, string newPassword, string password)
        {
            MemberEntity? member = _repo.GetById(id);
            if (member is null)
                throw new MemberNotFoundException("Member do not exist");

            if(!_hasher.Verify(password, member.PasswordHash))
                throw new MemberPasswordInvalidException("Invalid password");

            string passwordHash = _hasher.HashPassword(newPassword);

            _repo.UpdatePassword(id, passwordHash);
        }

    }
}
