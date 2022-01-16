using System;

namespace Openworld.Models
{
	[Serializable]
	public class BaseModel
	{
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

