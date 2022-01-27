using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Openworld.Models;
using Openworld.Scenes;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{
  public class Battles : BaseScene
  {
    [SerializeField] GameObject battleObjectPrefab;
    [SerializeField] GameObject container;

    protected override void GetData()
    {
      communicator.GetBattles(gameManager.currentGame, SetBattles, RequestException);
    }

    public void SetBattles(Battle[] battles)
    {
      Debug.Log("Battles: " + battles.Length);
      foreach (Battle battle in battles)
      {
        string text = battle.getName();
        GameObject obj = Instantiate(battleObjectPrefab, container.transform);
        obj.GetComponentInChildren<TMPro.TMP_Text>().text = text;
        BattleSelect item = obj.GetComponentInChildren<BattleSelect>();
        item.battleId = battle.id;
      }
    }

    public void CreateBattle()
    {
      communicator.CreateBattle(gameManager.currentGame, CreateBattleSuccess, RequestException);
    }

    public void CreateBattleSuccess(ResponseHelper res)
    {
      gameManager.LoadScene(SceneName.Battles);
    }

    public void Cancel()
    {
      gameManager.LoadScene(SceneName.YourGames);
    }
  }
}