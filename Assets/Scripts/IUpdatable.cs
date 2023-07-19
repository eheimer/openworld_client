using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Openworld
{

  /// <summary>
  /// IUpdatable is a subscriber to data update events
  /// </summary>
  public interface IUpdatable
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">the event message </param>
    void UpdateStart(string message);


  }
}
