// Autogenerated 5/10/2022 2:34:36 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class CharacterDetail : PublicCharacter
  {
    private int _castSpeed;
    public int castSpeed {
      get => _castSpeed;
      set => Set(ref _castSpeed, value);
    }

    private int _defChance;
    public int defChance {
      get => _defChance;
      set => Set(ref _defChance, value);
    }

    private int _dexterity;
    public int dexterity {
      get => _dexterity;
      set => Set(ref _dexterity, value);
    }

    private int _healSpeed;
    public int healSpeed {
      get => _healSpeed;
      set => Set(ref _healSpeed, value);
    }

    private int _hitChance;
    public int hitChance {
      get => _hitChance;
      set => Set(ref _hitChance, value);
    }

    private int _hp;
    public int hp {
      get => _hp;
      set => Set(ref _hp, value);
    }

    private int _intelligence;
    public int intelligence {
      get => _intelligence;
      set => Set(ref _intelligence, value);
    }

    private Inventory _inventory;
    public Inventory inventory {
      get => _inventory;
      set => Set(ref _inventory, value);
    }

    private int _inventorySize;
    public int inventorySize {
      get => _inventorySize;
      set => Set(ref _inventorySize, value);
    }

    private int _mana;
    public int mana {
      get => _mana;
      set => Set(ref _mana, value);
    }

    private int _movement;
    public int movement {
      get => _movement;
      set => Set(ref _movement, value);
    }

    private int _parry;
    public int parry {
      get => _parry;
      set => Set(ref _parry, value);
    }

    private string _player;
    public string player {
      get => _player;
      set => Set(ref _player, value);
    }

    private int _resistC;
    public int resistC {
      get => _resistC;
      set => Set(ref _resistC, value);
    }

    private int _resistE;
    public int resistE {
      get => _resistE;
      set => Set(ref _resistE, value);
    }

    private int _resistF;
    public int resistF {
      get => _resistF;
      set => Set(ref _resistF, value);
    }

    private int _resistP;
    public int resistP {
      get => _resistP;
      set => Set(ref _resistP, value);
    }

    private int _resistPh;
    public int resistPh {
      get => _resistPh;
      set => Set(ref _resistPh, value);
    }

    private CharacterSkill[] _skills;
    public CharacterSkill[] skills {
      get => _skills;
      set => Set(ref _skills, value);
    }

    private int _stamina;
    public int stamina {
      get => _stamina;
      set => Set(ref _stamina, value);
    }

    private int _strength;
    public int strength {
      get => _strength;
      set => Set(ref _strength, value);
    }

    private int _swingSpeed;
    public int swingSpeed {
      get => _swingSpeed;
      set => Set(ref _swingSpeed, value);
    }


    public override string ToString(){
      return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    } 
  }
}