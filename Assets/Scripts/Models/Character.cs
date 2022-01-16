using System;

namespace Openworld.Models
{
	[Serializable]
	public class Character: BaseModel
	{
        public string id;
        public string name;
        public int maxHp;
        public int hp;
    }
}
