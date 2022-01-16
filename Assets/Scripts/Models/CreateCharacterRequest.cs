using System;

namespace Openworld.Models
{
	[Serializable]
	public class CreateCharacterRequest: BaseModel
	{
        public string name;
        public int maxHp;
        public int baseResist;
        public int inventorySize;
    }
}

