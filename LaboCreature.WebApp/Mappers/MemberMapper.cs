using LaboCreature.BLL.Models;
using LaboCreature.WebApp.Models;

namespace LaboCreature.WebApp.Mappers
{
    public static class MemberMapper
    {
        public static LoginMemberModel ToLoginModel(this MemberLoginForm formData)
        {
            return new LoginMemberModel()
            {
                Email = formData.Email,
                Password = formData.Password
            };
        }

        public static MemberView ToModelView(this MemberModel member)
        {
            return new MemberView()
            {
                Id = member.Id,
                Email = member.Email,
                Username = member.Username
            };
        }
    }
}
