using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string password, string passwordHash);

    }
}
