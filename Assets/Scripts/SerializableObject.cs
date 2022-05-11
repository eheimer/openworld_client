using Newtonsoft.Json;
using UnityEngine;

namespace Openworld
{
  public class SerializableObject
  {
    public override string ToString(){
      return JsonConvert.SerializeObject(this);
    } 
  }
}