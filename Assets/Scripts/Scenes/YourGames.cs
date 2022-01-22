using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Openworld.Models;
using Openworld.Scenes;
using Proyecto26;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{
  public class YourGames : BaseScene
  {
    [SerializeField] GameObject gameObjectPrefab;
    [SerializeField] GameObject container;

    protected override void GetData()
    {
      communicator.GetGames(player.playerId, SetGames, RequestException);
    }

    public void SetGames(GamesResponse[] games)
    {
      if (games == null || games.Length == 0)
      {
        gameManager.LoadScene(SceneName.NewGame);
        return;
      }

      foreach (GamesResponse game in games)
      {
        Debug.Log("Game: " + game.game.name + "(" + game.character.name + ")");
        string text = game.game.name + "(" + game.character.name + ")";
        GameObject obj = Instantiate(gameObjectPrefab, container.transform);
        obj.GetComponentInChildren<TMPro.TMP_Text>().text = text;
        GameSelect item = obj.GetComponent<GameSelect>();
        item.gameId = game.game.id;
        item.character = game.character;
        item.SetOwner(game.owner);
      }
    }

    public void CreateGame()
    {
      gameManager.LoadScene(SceneName.NewGame);
    }

    public void Cancel()
    {
      gameManager.LoadScene(SceneName.LogIn);
    }
  }
}