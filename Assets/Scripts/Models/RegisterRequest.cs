using System;

namespace Openworld.Models
{
	[Serializable]
	public class RegisterRequest : BaseModel
	{
		public string email;

		public string name;

		public string password;
	}
}

