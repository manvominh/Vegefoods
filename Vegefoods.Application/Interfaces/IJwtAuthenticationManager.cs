using System.Security.Claims;
using Vegefoods.Application.Dtos;

namespace Vegefoods.Application.Interfaces
{
	public interface IJwtAuthenticationManagerService
	{
		Task<Tokens> GenerateJwtToken(UserDto user);
		ClaimsPrincipal GetPrincipalFromToken(string token);
	}
}
