using Openworld.Models;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class NewGame : FormBase
  {
    protected override void RegisterButtonHandlers()
    {
      HandleClick("newgame-submit", CreateGame);
      HandleClick("newgame-cancel", CancelClick);
    }
    protected override void ClearForm()
    {
      var me = GetVisualElement();
      me.Q<TextField>("name").value = "";
    }

    void CreateGame()
    {
      var me = GetVisualElement();
      GetGameManager().GetCommunicator().CreateGame(
          me.Q<TextField>("name").value,
          CreateSuccess, (ex) => RaiseFail(ex));
    }

    void CreateSuccess(Game resp)
    {
      GetGameManager().currentGame = resp.id;
      RaiseSuccess();
    }

    void CancelClick()
    {
      RaiseFail(null);
    }
  }
}
