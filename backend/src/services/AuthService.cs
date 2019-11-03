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
using backend.models.assets;
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
            var user = _db.Users.Where(userEntity =>
                userEntity.Email == createUserDto.Email || userEntity.Nickname == createUserDto.Nickname).ToList();
            if (user.Count != 0)
            {
                throw _apiErrors.UserAlreadyExist;
            }

            var newUserEntity = new UserEntity()
            {
                Email = createUserDto.Email, Nickname = createUserDto.Nickname, Password = createUserDto.Password, RoleId = 1
            };
            _db.Users.Add(newUserEntity);
            _db.SaveChanges();
            return "Пользователь был зарегистрирован";
        }

        public TokenInfo GetTokenInfo(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token) && handler.ReadToken(token) is JwtSecurityToken tokenS)
                return new TokenInfo
                {
                    Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value,
                    RoleName = tokenS.Claims.FirstOrDefault(claim => claim.Type == "RoleName")?.Value,
                    UserId = int.Parse(tokenS.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value ?? throw new Exception("UserId must me a number"))
                };
            throw _apiErrors.InvalidToken;
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
                    role.RoleName,
                    user.Id
                }).FirstOrDefault();

            if (userData == null)
            {
                throw _apiErrors.UserNotFount;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userData.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userData.RoleName),
                new Claim("UserId", userData.Id.ToString())
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