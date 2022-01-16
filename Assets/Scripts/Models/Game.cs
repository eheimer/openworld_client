using System;

namespace Openworld.Models
{
	[Serializable]
	public class Game: BaseModel
	{
        public string id;
        public string name;
        public int maxPlayers;
    }
}
