using System.Security.Claims;
using Vegefoods.Application.Dtos;

namespace Vegefoods.Application.Interfaces
{
	public interface IJwtAuthenticationManagerService
	{
		Task<Tokens> GenerateJwtToken(string email);
		//ClaimsPrincipal GetPrincipalFromToken(string token);
	}
}
