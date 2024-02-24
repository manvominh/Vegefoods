using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;

namespace Vegefoods.Persistence.Repositories
{
	public class JwtAuthenticationManagerService : IJwtAuthenticationManagerService
	{
		public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
		public const int JWT_TOKEN_VALIDITY_MINS = 20;

		public Task<Tokens> GenerateJwtToken(string email)
		{
			///* Validating the User Credentials */
			if (email == null)
				return Task.FromResult(new Tokens { Token = null, IsSuccess = false });
			/* Generating JWT Token */
			var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
			var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
			var expiryTimeStamp = new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINS).ToString());

			var claimEmailAddress = new Claim(ClaimTypes.Email, email);

			var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, expiryTimeStamp }, "jwtAuth");
			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(tokenKey),
				SecurityAlgorithms.HmacSha256Signature);
			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Expires = tokenExpiryTimeStamp,
				SigningCredentials = signingCredentials,
			};

			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
			var token = jwtSecurityTokenHandler.WriteToken(securityToken);
			return Task.FromResult(new Tokens { Token = token, IsSuccess = true });
		}
		
		public ClaimsPrincipal GetPrincipalFromToken(string token)
		{
			try
			{
				var key = Encoding.UTF8.GetBytes(JWT_SECURITY_KEY);
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,  // don't care about the token's expiration date  if false
				};
				var tokenHandler = new JwtSecurityTokenHandler();
				var jwt = tokenHandler.ReadJwtToken(token);
				var expiredTime = jwt.Claims.First(c => c.Type == ClaimTypes.Expiration).Value;
				if (DateTime.Parse(expiredTime) < DateTime.UtcNow)
				{
					return null;
				}
				SecurityToken securityToken;
				//validating the token
				var principle = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
				JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
				if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				{
					throw new SecurityTokenException("Invalid token");
				}

				return principle;
			}
			catch (Exception ex)
			{
				//logging the error and returning null
				Console.WriteLine("Exception : " + ex.Message);
				return null;
			}

		}
	}
}
