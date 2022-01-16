using System;

namespace Models
{
	[Serializable]
	public class BaseResponse
	{
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

