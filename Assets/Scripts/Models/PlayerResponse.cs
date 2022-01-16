using System;

namespace Openworld.Models
{
	[Serializable]
	public class PlayerResponse: BaseModel
	{
        public string id;
        public string email;
        public string name;
        public DateTime lastSeenAt;
    }
}

