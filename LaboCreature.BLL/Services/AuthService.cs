using LaboCreature.BLL.Helpers;
using LaboCreature.BLL.Interfaces;
using LaboCreature.BLL.Models;
using LaboCreature.DAL.Interfaces;
using LaboCreature.BLL.Exceptions;
using LaboCreature.DAL.Entities;
using LaboCreature.BLL.Mappers;

namespace LaboCreature.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IPasswordHasher _hasher;
        private IMemberRepository _repo;
        
        public AuthService(IPasswordHasher hasher, IMemberRepository repo)
        {
            _hasher = hasher;
            _repo = repo;
        }
        
        public int CreateMember(RegisterMemberModel model)
        {
            if(_repo.GetByEmail(model.Email) is not null)
            {
                throw new MemberEmailAlreadyExists("An account already exists with this email.");
            }
            else if(_repo.GetByUsername(model.Username) is not null)
            {
                throw new MemberUsernameAlreadyExists("Username taken");
            }
            string passwordHash = _hasher.HashPassword(model.Password);

            MemberEntity newMember = new()
            {
                Email = model.Email,
                Username = model.Username,
                PasswordHash = passwordHash
            };

            return _repo.CreateMember(newMember);
        }

        public MemberModel? Login(LoginMemberModel loginModel)
        {
            MemberEntity? entity = _repo.GetByEmail(loginModel.Email);

            if (entity is null || !_hasher.Verify(loginModel.Password, entity.PasswordHash))
                return null;
            else
                return entity.ToModel();            
        }
    }
}
