using UnityEngine;
using TMPro;
using Openworld.Models;

namespace Openworld.Forms
{
    public class RegisterForm : BaseForm
    {
        [SerializeField] TMP_InputField emailComponent;
        [SerializeField] TMP_InputField nameComponent;
        [SerializeField] TMP_InputField passwordComponent;
        [SerializeField] TMP_InputField confirmComponent;

        protected override bool ShouldValidate { get { return false; } }

        protected override void DoSubmit()
        {
            if(!passwordComponent.text.Equals(confirmComponent.text)){
                Error("Passwords do not match");
                return;
            }
            communicator.Register(emailComponent.text, nameComponent.text, passwordComponent.text, RegisterSuccess, RequestException);
        }

        protected void RegisterSuccess(PlayerResponse res){
            communicator.Login(emailComponent.text, passwordComponent.text, LoginSuccess, RequestException);
        }

        protected void LoginSuccess(LoginResponse res){
            gameManager.SetToken(res.token);
            communicator.GetPlayer(res.player,gameManager.SetPlayer,RequestException);
        }

        public void Cancel()
        {
            gameManager.LoadScene(SceneName.LogIn);
        }

    }
}
