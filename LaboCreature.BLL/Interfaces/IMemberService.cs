using LaboCreature.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface IMemberService
    {
        public MemberModel? GetById(int id);
        public MemberModel? GetByUsername(string username);
        public void ChangeUsername(int id, string newUsername, string password);
        public void ChangeEmail(int id, string newEmail, string password);
        public void ChangePassword(int id, string newPassword, string password);
    }
}
