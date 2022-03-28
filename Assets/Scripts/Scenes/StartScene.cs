namespace Openworld.Scenes
{
  public class StartScene : BaseScene
  {

    // show the menu if we aren't logged in
    // the menu validation will take care of which menu (login) to display

    protected override bool Validate()
    {
      return gameManager.GetAuthToken() != null && !gameManager.GetAuthToken().Equals("");
    }

    protected override void ValidateFail()
    {
      uiManager.ShowMenu();
    }

  }
}