// using System.Collections;
// using System.Collections.Generic;
// using Openworld;
// using UnityEngine;

// public class DataBoundComponent<MODEL, FIELDTYPE> : MonoBehaviour where MODEL : ObservableObject
// {
//   MODEL bindSource;
//   string fieldName;

//   public void SetBindSource(MODEL source, string fieldName)
//   {
//     this.bindSource = source;
//     this.fieldName = fieldName;
//   }

//   protected FIELDTYPE GetValue()
//   {
//     return (FIELDTYPE)this.bindSource.GetType().GetField(this.fieldName).GetValue(this.bindSource);
//   }
// }
