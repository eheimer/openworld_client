// Autogenerated 2/8/2022 1:58:55 PM
using System;


namespace Openworld.Models
{
  [Serializable]
  public class WeaponInstance: BaseModel
  {
    public WeaponInstanceAttribute[] attributes;
    public bool damaged;
    public bool equipped;
    public Inventory inventory;
    public Material material;
    public Weapon weapon;
  }
}