using System;

namespace Openworld.Models
{
	[Serializable]
	public class LoginRequest : BaseModel
	{
		public string email;

		public string password;
	}
}

