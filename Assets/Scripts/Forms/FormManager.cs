using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public abstract class FormManager : MonoBehaviour {
    [SerializeField] protected Text formError;
    [SerializeField] protected Selectable[] tabStops;
    
    // Start is called before the first frame update
    protected virtual void Start() {
        formError.enabled = false;
        FocusGameObject(false);
        //music = FindObjectOfType<MusicManager>();
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
        Selectable next = tabStops[nextIndex];
        InputField inputField = next.GetComponent<InputField>();
        if (inputField != null) {
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        next.Select();
    }//protected MusicManager music;

    protected abstract void DoSubmit();

    public void Submit() {
        formError.enabled = false;
        DoSubmit();
        AddHandlers();
    }

    protected void AddHandlers() {
    }

    protected void DropHandlers() {
    }

    protected void Error(string message) {
        DropHandlers();
        formError.text = message;
        formError.enabled = true;
    }
    protected void Success() {
        DropHandlers();
    }
}