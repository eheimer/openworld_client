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
  public class GameSelect : MonoBehaviour
  {
    GamesResponse gameData;
    string gameId;
    PublicCharacter character;
    Boolean owner;

    [SerializeField] GameObject addPlayerButton;
    [SerializeField] TMP_Text gameName;
    [SerializeField] TMP_Text characterName;

    GameManager gameManager;

    void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
    }

    public void SetData(GamesResponse data)
    {
      this.gameData = data;
      gameId = data.game.id;
      character = data.character;
      gameName.text = data.game.name;
      characterName.text = data.character.name;
      owner = data.owner;
    }

    public void SelectGame()
    {
      gameManager.currentGame = this.gameId;
      if (this.character != null && !String.IsNullOrEmpty(this.character.id) && !String.IsNullOrEmpty(this.character.name))
      {
        gameManager.LoadScene(SceneName.Battles);
      }
      else
      {
        gameManager.LoadScene(SceneName.Character);
      }
    }

    public void InvitePlayer()
    {
      if (owner)
      {
        gameManager.currentGame = this.gameId;
        gameManager.LoadScene(SceneName.Invite);
      }
    }
  }
}