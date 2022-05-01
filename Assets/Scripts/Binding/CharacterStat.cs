// using System.Collections;
// using System.Collections.Generic;
// using Openworld.Models;
// using TMPro;
// using UnityEngine;

// public class CharacterStat : DataBoundComponent<CharacterDetail,string>
// {
//   [SerializeField] GameObject stat;
//   [SerializeField] GameObject subStat;
//   [SerializeField] bool showName = true;
//   [SerializeField] bool showSubStat = true;

//   [SerializeField] TMP_Text target;

//   public void SetCharacter(CharacterDetail detail){
//     SetBindSource(detail, "name");
//   }

//   // Start is called before the first frame update
//   void Start()
//   {

//   }

//   // Update is called once per frame
//   void Update()
//   {
//     target.text = GetValue();
//   }
// }
