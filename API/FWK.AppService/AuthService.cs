using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FWK.Domain.Entities;
using FWK.Domain.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace FWK.AppService
{
    public class AuthService : IAuthService
    {
        IHttpContextAccessor _httpContextAccessor;

 

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurretUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }

            var value = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "UserId")?.Value;
            if (!String.IsNullOrEmpty(value))
            {
                return int.Parse(value);
            }

            return null;
        }

        public string GetCurretToken()
        {
            var access_token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault() ?? "";
            access_token = access_token.Replace("Bearer ", "");
            return access_token;
        }

        public string GetSessionID()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "SessionId")?.Value;
        }

        public int GetCurretCountryId()
        {
            var value = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "CountryId")?.Value;
            if (!String.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value);
            }

            //TODO: por ahora el pais es argentina, cuando este el administrar usuario hay que quitar el default
            return 1;
        }

        public string GetCurretUserName()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Username")?.Value;
        } 
    }
}
