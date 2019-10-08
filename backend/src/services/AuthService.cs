using backend.core.connectors;
using backend.models.dto.user;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using backend.core.configs;
using backend.helper;
using backend.models.entities;

namespace backend.services
{
    public class AuthService
    {
        private readonly RipDatabase _db;
        private readonly DataAuthConfig _authConfig;
        private readonly ApiErrors _apiErrors;

        public AuthService(RipDatabase ripDatabase, DataAuthConfig authConfig, ApiErrors apiErrors)
        {
            _db = ripDatabase;
            _authConfig = authConfig;
            _apiErrors = apiErrors;
        }

        public string Login(LoginUserDto loginUserDto)
        {
            var identity = GetIdentity(loginUserDto);
            return GetToken(identity);
        }

        public string Registration(CreateUserDto createUserDto)
        {
            var user = _db.Users.Select(userEntity =>
                userEntity.Email == createUserDto.Email || userEntity.Nickname == createUserDto.Nickname).ToList();
            if (user.Count != 0)
            {
                throw _apiErrors.UserAlreadyExist;
            }

            var newUserEntity = new UserEntity()
            {
                Email = createUserDto.Email, Nickname = createUserDto.Nickname, Password = createUserDto.Password
            };
            _db.Users.Add(newUserEntity);
            return "Пользователь был зарегистрирован";
        }

        private string GetToken(ClaimsIdentity identity)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var lifeTime = Convert.ToDouble(_authConfig.Lifetime);

            var jwt = new JwtSecurityToken(
                _authConfig.Issuer,
                _authConfig.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(lifeTime)),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(LoginUserDto loginUserParam)
        {
            var userData = (from user in _db.Users
                join role in _db.Roles on user.RoleId equals role.Id
                where user.Email == loginUserParam.Email && user.Password == loginUserParam.Password
                select new
                {
                    user.Email,
                    role.RoleName
                }).FirstOrDefault();

            if (userData == null)
            {
                throw _apiErrors.UserNotFount;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userData.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userData.RoleName)
            };

            return new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
        }
    }
}