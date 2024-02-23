
using System.Security.Cryptography;

namespace Vegefoods.Application.Common.Helpers
{
	public class HashHelper
	{
		public static string HashPassword(string password)
		{
			using (var md5Hash = MD5.Create())
			{
				var data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				var sBuilder = new System.Text.StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
		}
	}
}
