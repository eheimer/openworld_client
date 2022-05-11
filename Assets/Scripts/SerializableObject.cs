using Newtonsoft.Json;
using UnityEngine;

namespace Openworld
{
  public class SerializableObject : MonoBehaviour
  {
    public override string ToString(){
      return JsonConvert.SerializeObject(this);
    } 
  }
}