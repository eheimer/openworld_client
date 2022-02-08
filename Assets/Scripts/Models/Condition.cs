// Autogenerated 2/8/2022 1:58:55 PM
using System;


namespace Openworld.Models
{
  [Serializable]
  public class Condition: BaseModel
  {
    public string actionReplace;
    public bool allowMultiple;
    public int cooldown;
    public string damage;
    public DamageType damageType;
    public int delay;
    public int duration;
    public string name;
    public Condition[] overrides;
    public bool removeOnHit;
  }
}