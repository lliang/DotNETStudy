﻿using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DotNETStudy.Auth.JwtWebApi.Dtos;

namespace DotNETStudy.Auth.JwtWebApi.Services
{
    public class TokenService : ITokenService
    {
        private TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);

        public string BuildToken(string key, string issuer, UserDto user)
        {
            var claims = new[]
            {
                new Claim(Claims.ClaimTypes.Subject, user.UserName),
                new Claim(Claims.ClaimTypes.SessionId, Guid.NewGuid().ToString()),
                new Claim(Claims.ClaimTypes.PhoneNumber, user.PhoneNumber)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.UtcNow.Add(ExpiryDuration), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
