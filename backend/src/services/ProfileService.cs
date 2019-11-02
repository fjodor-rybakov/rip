using System.Linq;
using backend.core.connectors;
using backend.helper;
using backend.models.dto.user;

namespace backend.services
{
    public class ProfileService
    {
        private readonly ApiErrors _apiErrors;
        private readonly RipDatabase _db;
        
        public ProfileService(RipDatabase db, ApiErrors apiErrors)
        {
            _apiErrors = apiErrors;
            _db = db;
        }

        public UserDto GetProfile(int userId)
        {
            var user = (from userEntity in _db.Users
                where userEntity.Id == userId
                select new UserDto
                {
                    Nickname = userEntity.Nickname,
                    Avatar = userEntity.Avatar,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    RoleId = userEntity.RoleId
                }).FirstOrDefault();

            if (user == null)
            {
                throw _apiErrors.UserNotFount;
            }

            return user;
        }
    }
}