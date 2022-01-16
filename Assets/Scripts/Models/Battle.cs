using System;

namespace Openworld.Models
{
	[Serializable]
	public class Battle: BaseModel
	{
        public string id;
        public string createdAt;
        public int round;

        public string getName(){
            //convert createdAt to DateTime, then format to string
            DateTime createdAt = DateTime.Parse(this.createdAt);
            return createdAt.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}

