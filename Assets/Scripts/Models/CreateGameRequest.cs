using System;

namespace Openworld.Models
{
	[Serializable]
	public class CreateGameRequest: BaseModel
	{
        public string name;
        public int maxPlayers;
    }
}

