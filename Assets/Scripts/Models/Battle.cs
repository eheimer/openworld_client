// Autogenerated 2/8/2022 1:58:55 PM
using System;


namespace Openworld.Models
{
  [Serializable]
  public class Battle: BaseModel
  {
    public CreatureInstance[] enemies;
    public CreatureInstance[] friendlies;
    public Game game;
    public Character initiator;
    public Character[] participants;
    public int round;
  }
}