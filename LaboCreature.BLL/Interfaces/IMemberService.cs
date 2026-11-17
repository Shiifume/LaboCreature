using LaboCreature.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Interfaces
{
    public interface IMemberService
    {
       public MemberModel? GetById(int id);
    }
}
