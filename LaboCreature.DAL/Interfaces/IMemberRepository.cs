using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LaboCreature.DAL.Interfaces
{
    public interface IMemberRepository
    {
        MemberEntity? GetById(int id);
        MemberEntity? GetByUsername(string username);
        MemberEntity? GetByEmail(string email);

        int CreateMember(MemberEntity entityToAdd);
        bool Delete(int id);
        bool UpdateUsername(int memberId, string newUsername);
        bool UpdateEmail(int memberId, string newEmail);
        bool UpdatePassword(int memberId, string newPasswordHash);

    }
}