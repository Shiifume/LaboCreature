using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Models;

namespace LaboCreature.WebApp.Mappers
{
    public static class MemberFormMapper
    {
        public static LoginMemberModel ToLoginModel(this MemberLoginForm formData)
        {
            return new LoginMemberModel()
            {
                Email = formData.Email,
                Password = formData.Password
            };
        }
    }
}
