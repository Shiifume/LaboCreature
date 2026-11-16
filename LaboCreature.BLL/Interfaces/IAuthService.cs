using LaboCreature.BLL.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface IAuthService
    {        
        int CreateMember(RegisterMemberModel model);

        MemberModel? Login(LoginMemberModel loginModel);
    }
}
