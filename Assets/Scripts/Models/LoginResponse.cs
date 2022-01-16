using System;

namespace Openworld.Models
{
	[Serializable]
	public class LoginResponse: BaseModel
	{
		public string player;

		public string token;
	}
}

