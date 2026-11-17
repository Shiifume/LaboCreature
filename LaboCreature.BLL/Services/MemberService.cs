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

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }
        
        public MemberModel? GetById(int id)
        {
            MemberEntity? member = _repo.GetById(id);
            if (member is null)
                return null;

            return member.ToModel();
        }
    }
}
