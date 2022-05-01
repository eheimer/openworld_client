using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Openworld
{
  public class Player : MonoBehaviour
  {
    [SerializeField] public string playerName;
    [SerializeField] public string playerId;
    public Dictionary<string, string> games { get; set; }
    [SerializeField] public string character;
    public string currentGame { get; set; }
    // public string character { get; set; }
    public string currentBattle { get; set; }
  }
}

