using Openworld.Models;
using Openworld.Scenes;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Openworld.Forms
{
    public class RegisterForm : BaseForm
    {
        [SerializeField] InputField emailComponent;
        [SerializeField] InputField nameComponent;
        [SerializeField] InputField passwordComponent;
        [SerializeField] InputField confirmComponent;

        protected override bool ShouldValidate { get { return false; } }

        protected override void DoSubmit()
        {
            if(!passwordComponent.text.Equals(confirmComponent.text)){
                Error("Passwords do not match");
                return;
            }
            communicator.Register(emailComponent.text, nameComponent.text, passwordComponent.text, RegisterSuccess, RequestException);
        }

        protected void RegisterSuccess(ResponseHelper res){
            communicator.Login(emailComponent.text, passwordComponent.text, LoginSuccess, RequestException);
        }

        protected void LoginSuccess(LoginResponse res){
            gameManager.SetToken(res.token);
            communicator.GetPlayerDetail(res.player,gameManager.SetPlayer,RequestException);
        }

        public void Cancel()
        {
            gameManager.LoadScene(SceneName.LogIn);
        }

    }
}
