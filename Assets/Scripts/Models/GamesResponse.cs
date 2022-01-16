using System;

namespace Openworld.Models
{
	[Serializable]
	public class GamesResponse: BaseModel
	{
        public Game game;
        public Character character;

        public bool owner;
    }
}

