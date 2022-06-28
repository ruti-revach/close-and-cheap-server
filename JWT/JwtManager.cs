using close_and_cheap.Data.DTO;
using close_and_cheap.JWT.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace close_and_cheap.JWT
{
    public class JwtManager : IJwtManager
    {

        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;
        public JwtManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(_jwtTokenConfig.Secret);
        }

        public JwtResultDTO GenerateToken(Claim[] claims)
        {
            var jwtToken = new JwtSecurityToken(_jwtTokenConfig.Issuer, _jwtTokenConfig.Audience,
                claims, expires: DateTime.Now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha512Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtResultDTO
            {
                AccessToken = accessToken,//this address token with all the proprties behaver
                Type = "Bearer",//const value
                Expired = _jwtTokenConfig.AccessTokenExpiration//only time to show
            };
        }

    }
}
