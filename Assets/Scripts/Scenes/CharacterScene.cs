using Openworld.Models;
using UnityEngine;

namespace Openworld.Scenes

{
  public class CharacterScene : SwipableScene
  {
    public CharacterDetail character;
    protected override void Start()
    {
      base.Start();
      if (menu != null)
      {
        menu.CloseMenu();
      }
    }

    protected override void GetData()
    {
      var gameManager = GetGameManager();
      var communicator = gameManager.GetCommunicator();
      var player = gameManager.GetPlayer();
      //get the character detail from the server, if character is not set, or 404 from server, show the menu
      if (!(player.character is null) && !player.character.Equals(""))
      {
        GetGameManager().GetCommunicator().GetCharacterDetail(player.character, (CharacterDetailResponse resp) =>
        {
          Debug.Log(resp);
          this.character = resp;
        }, RequestException);
      }
    }
  }
}