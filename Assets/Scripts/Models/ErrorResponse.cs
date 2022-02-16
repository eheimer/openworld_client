using System;

namespace Openworld.Models
{
  [Serializable]
  public class ErrorResponse
  {
    [Serializable]
    public class Error
    {
      public string type;
      public string message;

      public override string ToString()
      {
        return UnityEngine.JsonUtility.ToJson(this, true);
      }
    }

    public Error error;
    public override string ToString()
    {
      return UnityEngine.JsonUtility.ToJson(this, true);
    }
  }
}

