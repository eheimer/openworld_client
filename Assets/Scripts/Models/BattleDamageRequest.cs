// Autogenerated 2/8/2022 1:58:55 PM
using System;


namespace Openworld.Models
{
  [Serializable]
  public class BattleDamageRequest: BaseModel
  {
    public int[] characters;
    public int[] creatures;
    public int damageAmt;
    public int damageType;
  }
}