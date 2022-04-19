using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Openworld.Menus

{
  public class MenuButton : MonoBehaviour
  {
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UIDocument>().rootVisualElement.Q<Button>().clickable.clicked += ShowMenu;
    }

    public void ShowMenu()
    {
      FindObjectOfType<UIManagerBase>(true)?.ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }
  }

}