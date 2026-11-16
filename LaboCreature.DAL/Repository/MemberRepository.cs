using System;
using System.Collections.Generic;
using System.Text;
using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;



namespace LaboCreature.DAL.Repository 
{
    public class MemberRepository : IMemberRepository
    {
        private readonly CreatureDBContext _context;

        public MemberRepository(CreatureDBContext context)
        {
            _context = context;
        }

        public int CreateMember(MemberEntity entityToAdd)
        {
            _context.Members.Add(entityToAdd);
            _context.SaveChanges();

            return entityToAdd.Id;
        }

        public bool Delete(int id)
        {
            MemberEntity? memberToDelete = _context.Members.FirstOrDefault(m => m.Id == id);
            if (memberToDelete is null)
                return false;

            _context.Members.Remove(memberToDelete);
            _context.SaveChanges();
            return true;
        }

        public MemberEntity? GetByEmail(string email)
        {
            return _context.Members.FirstOrDefault(m => m.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }

        public MemberEntity? GetById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public MemberEntity? GetByUsername(string username)
        {
            return _context.Members.FirstOrDefault(m => m.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool UpdateEmail(int memberId, string newEmail)
        {
            MemberEntity? memberToUpdate = _context.Members.FirstOrDefault(m => m.Id == memberId);
            if (memberToUpdate is null)
                return false;

            memberToUpdate.Email = newEmail;

            _context.SaveChanges();
            return true;
        }

        public bool UpdatePassword(int memberId, string newPasswordHash)
        {
            MemberEntity? memberToUpdate = _context.Members.FirstOrDefault(m => m.Id == memberId);
            if (memberToUpdate is null)
                return false;

            memberToUpdate.PasswordHash = newPasswordHash;

            _context.SaveChanges();
            return true;
        }

        public bool UpdateUsername(int memberId, string newUsername)
        {
            MemberEntity? memberToUpdate = _context.Members.FirstOrDefault(m => m.Id == memberId);
            if (memberToUpdate is null)
                return false;

            memberToUpdate.Username = newUsername;

            _context.SaveChanges();
            return true;
        }
    }
}
