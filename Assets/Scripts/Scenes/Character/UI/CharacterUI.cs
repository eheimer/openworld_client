using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class CharacterUI : UIBase
  {

    [SerializeField]
    FormBase createCharacterForm;

    public event Action CreateCharacterSuccess;
    public event Action<Exception> CreateCharacterFail;
    public event Action CreateCharacterCancel;

    public void NewCharacter()
    {
      createCharacterForm.Show();
      createCharacterForm.FormSuccess += RaiseCreateCharacterSuccess;
      createCharacterForm.FormFail += RaiseCreateCharacterFail;
    }

    private void RaiseCreateCharacterSuccess()
    {
      createCharacterForm.FormSuccess -= RaiseCreateCharacterSuccess;
      createCharacterForm.FormFail -= RaiseCreateCharacterFail;
      RaiseEvent(CreateCharacterSuccess);
    }

    private void RaiseCreateCharacterFail(Exception ex)
    {
      createCharacterForm.FormSuccess -= RaiseCreateCharacterSuccess;
      createCharacterForm.FormFail -= RaiseCreateCharacterFail;
      if (ex is FormCancelException)
      {
        RaiseEvent(CreateCharacterCancel);
      }
      else
      {
        RaiseEvent(CreateCharacterFail, ex);
      }
    }
  }
}