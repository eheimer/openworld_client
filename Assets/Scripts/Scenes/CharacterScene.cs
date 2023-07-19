using System;
using System.Collections.ObjectModel;
using Openworld.Models;
using UnityEngine;

namespace Openworld.Scenes

{
  public class CharacterScene : SwipableScene//, IUpdatable
  {
    protected RacesResponse[] races;

    protected override void Start()
    {
      base.Start();
      if (menu != null)
      {
        menu.CloseMenu();
      }
      if (string.IsNullOrEmpty(GetGameManager().GetPlayer().character))
      {
        //showing the menu will prompt the user to create a character
        menu.ShowMenu();
      }
    }

    protected override void GetData()
    {
      var gameManager = GetGameManager();
      //add the spinner to the canvas
      var spinner = Instantiate(gameManager.GetSpinner(), FindObjectOfType<Canvas>().transform) as GameObject;

      var communicator = gameManager.GetCommunicator();
      var player = gameManager.GetPlayer();

      //get the character detail from the server and put it on the gameManager
      if (!String.IsNullOrEmpty(player.character))
      {
        GetGameManager().GetCommunicator().GetCharacterDetail(player.character, (CharacterDetailResponse resp) =>
        {
          Destroy(spinner);
          gameManager.character = resp;
        }, RequestException);
      }
    }
  }
}