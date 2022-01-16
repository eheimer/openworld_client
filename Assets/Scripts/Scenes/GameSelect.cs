using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;
using System.Text;
using Openworld.Models;
using Openworld;

namespace Openworld.Scenes
{
    public class GameSelect : MonoBehaviour
    {
        public string gameId;
        public Character character;

        [SerializeField] GameObject addPlayerButton;

        GameManager gameManager;

        void Start(){
            gameManager = FindObjectOfType<GameManager>();
        }

        public void SetOwner(bool owner){
            addPlayerButton.GetComponent<Button>().enabled = owner;
            addPlayerButton.GetComponent<Image>().enabled = owner;
        }

        public void SelectGame(){
            gameManager.currentGame = this.gameId;
            if(this.character != null && !String.IsNullOrEmpty(this.character.id) && !String.IsNullOrEmpty(this.character.name)){
                                gameManager.LoadScene(SceneName.Battles);
            } else
            {
                gameManager.LoadScene(SceneName.Character);
            }
        }

        public void InvitePlayer(){
            gameManager.currentGame = this.gameId;
            gameManager.LoadScene(SceneName.Invite);
        }
    }
}