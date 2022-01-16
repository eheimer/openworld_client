using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;
using System.Text;
using Openworld.Models;
using Openworld;

namespace Openworld.Scenes
{
    public class BattleSelect : MonoBehaviour
    {
        public string battleId;

        public void SelectBattle(){
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.currentBattle = this.battleId;
            gameManager.LoadScene(SceneName.BattleBoard);
        }
    }
}