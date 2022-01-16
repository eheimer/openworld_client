using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public string authToken;
    [SerializeField] public string playerName;
    [SerializeField] public string playerId;
    public Dictionary<string, string> games { get; set; }
    public string currentGame { get; set; }
    public string character { get; set; }
    public string currentBattle { get; set; }

    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

}
