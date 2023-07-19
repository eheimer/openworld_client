// Autogenerated 5/17/2022 6:05:16 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class CharacterDetail : PublicCharacter
  {
    private int _castSpeed;
    public int castSpeed
    {
      get => _castSpeed;
      set => Set(ref _castSpeed, value);
    }

    private int _defChance;
    public int defChance
    {
      get => _defChance;
      set => Set(ref _defChance, value);
    }

    private int _dexterity;
    public int dexterity
    {
      get => _dexterity;
      set => Set(ref _dexterity, value);
    }

    private int _healSpeed;
    public int healSpeed
    {
      get => _healSpeed;
      set => Set(ref _healSpeed, value);
    }

    private int _hitChance;
    public int hitChance
    {
      get => _hitChance;
      set => Set(ref _hitChance, value);
    }

    private float _hp;
    public float hp
    {
      get => _hp;
      set => Set(ref _hp, value);
    }

    private int _hpReplenish;
    public int hpReplenish
    {
      get => _hpReplenish;
      set => Set(ref _hpReplenish, value);
    }

    private float _hunger;
    public float hunger
    {
      get => _hunger;
      set => Set(ref _hunger, value);
    }

    private int _intelligence;
    public int intelligence
    {
      get => _intelligence;
      set => Set(ref _intelligence, value);
    }

    private Inventory _inventory;
    public Inventory inventory
    {
      get => _inventory;
      set => Set(ref _inventory, value);
    }

    private int _inventorySize;
    public int inventorySize
    {
      get => _inventorySize;
      set => Set(ref _inventorySize, value);
    }

    private float _mana;
    public float mana
    {
      get => _mana;
      set => Set(ref _mana, value);
    }

    private int _manaReplenish;
    public int manaReplenish
    {
      get => _manaReplenish;
      set => Set(ref _manaReplenish, value);
    }

    private int _maxHp;
    public int maxHp
    {
      get => _maxHp;
      set => Set(ref _maxHp, value);
    }

    private int _maxMana;
    public int maxMana
    {
      get => _maxMana;
      set => Set(ref _maxMana, value);
    }

    private int _maxStamina;
    public int maxStamina
    {
      get => _maxStamina;
      set => Set(ref _maxStamina, value);
    }

    private string _movement;
    public string movement
    {
      get => _movement;
      set => Set(ref _movement, value);
    }

    private int _parry;
    public int parry
    {
      get => _parry;
      set => Set(ref _parry, value);
    }

    private PublicPlayer _player;
    public PublicPlayer player
    {
      get => _player;
      set => Set(ref _player, value);
    }

    private int _resistC;
    public int resistC
    {
      get => _resistC;
      set => Set(ref _resistC, value);
    }

    private int _resistE;
    public int resistE
    {
      get => _resistE;
      set => Set(ref _resistE, value);
    }

    private int _resistF;
    public int resistF
    {
      get => _resistF;
      set => Set(ref _resistF, value);
    }

    private int _resistP;
    public int resistP
    {
      get => _resistP;
      set => Set(ref _resistP, value);
    }

    private int _resistPh;
    public int resistPh
    {
      get => _resistPh;
      set => Set(ref _resistPh, value);
    }

    private CharacterSkill[] _skills;
    public CharacterSkill[] skills
    {
      get => _skills;
      set => Set(ref _skills, value);
    }

    private float _sleep;
    public float sleep
    {
      get => _sleep;
      set => Set(ref _sleep, value);
    }

    private float _stamina;
    public float stamina
    {
      get => _stamina;
      set => Set(ref _stamina, value);
    }

    private int _staminaReplenish;
    public int staminaReplenish
    {
      get => _staminaReplenish;
      set => Set(ref _staminaReplenish, value);
    }

    private int _strength;
    public int strength
    {
      get => _strength;
      set => Set(ref _strength, value);
    }

    private int _swingSpeed;
    public int swingSpeed
    {
      get => _swingSpeed;
      set => Set(ref _swingSpeed, value);
    }

    private Battle _battle;
    public Battle battle
    {
      get => _battle;
      set => Set(ref _battle, value);
    }
  }
}