using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Openworld;
using Openworld.Models;
using Proyecto26;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{
    public class BattleSelect : MonoBehaviour
    {
        public string battleId;
    [SerializeField] TMP_Text battleNumberDisplay;

    public void SelectBattle(){
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.currentBattle = this.battleId;
            gameManager.LoadScene(SceneName.BattleBoard);
        }
    }
}