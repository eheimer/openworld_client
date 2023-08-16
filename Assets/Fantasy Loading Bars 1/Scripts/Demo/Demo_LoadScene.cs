using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo_LoadScene : MonoBehaviour {

	[SerializeField] private string scene;
    [SerializeField] private bool isIndex;

	public void LoadScene()
	{
        if (!string.IsNullOrEmpty(this.scene))
        {
            if (this.isIndex)
            {
                SceneManager.LoadScene(int.Parse(this.scene));
            }
            else
            {
                SceneManager.LoadScene(this.scene);
            }
        }
    }
}
