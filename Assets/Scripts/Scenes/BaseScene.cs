using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Proyecto26;
using Openworld.Models;

namespace Openworld.Scenes
{
    public abstract class BaseScene : MonoBehaviour
    {
        protected GameManager gameManager;
        protected Communicator communicator;
        protected Player player;
        protected virtual bool ShouldValidate { get { return true; } }

        [SerializeField] protected Selectable[] tabStops;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            communicator = gameManager.GetCommunicator();
            player = gameManager.GetPlayer();

            if(ShouldValidate && !Validate()){
                ValidateFail();
            }

            GetData();
            
            FocusGameObject(false);
        }

        protected virtual bool Validate(){
            return (player != null && !string.IsNullOrEmpty(player.playerId));
        }

        protected virtual void ValidateFail(){
            gameManager.LoadScene(SceneName.LogIn);
        }

        void Update() {
            
            if (Input.GetKeyDown(KeyCode.Tab)) {
        FocusGameObject(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            }
        }

        void FocusGameObject(bool isBackward) {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            bool none = !selectedObject;
            int currentIndex = 0;
            int nextIndex;
            if (!none) {
                //find the current tab index
                for (var i = 0; i < tabStops.Length; i++) {
                    if (tabStops[i].gameObject == selectedObject.gameObject) {
                        currentIndex = i;
                        break;
                    }
                    // none = true;
                }
            }
            if (none) {
                nextIndex = isBackward ? tabStops.Length - 1 : 0;
            } else {
                nextIndex = (isBackward ? currentIndex - 1 : currentIndex + 1);
                if (nextIndex < 0) nextIndex = tabStops.Length - 1;
                if (nextIndex > tabStops.Length - 1) nextIndex = 0;
            }
            if (tabStops.Length > nextIndex)
            {
                Selectable next = tabStops[nextIndex];
                InputField inputField = next.GetComponent<InputField>();
                if (inputField != null)
                {
                    inputField.OnPointerClick(new PointerEventData(EventSystem.current));
                }
                next.Select();
            }
        }
        public virtual void RequestException(RequestException err)
        {
            Error(UnityEngine.JsonUtility.FromJson<ErrorResponse>(err.Response).error.message);
        }

        protected virtual void Error(string message) {
            gameManager.LogMessage("Scene load error:", message);
        }

        protected abstract void GetData();
    }
}