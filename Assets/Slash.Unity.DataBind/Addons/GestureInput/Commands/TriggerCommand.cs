// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerCommand.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.GestureInput.Commands
{
    using Slash.Unity.DataBind.UI.Unity.Commands;
    using Slash.Unity.GestureInput.Sources;

    using UnityEngine.Events;

    /// <summary>
    ///   Command to invoke when a trigger source is triggered.
    /// </summary>
    public class TriggerCommand : UnityEventCommand<TriggerSource>
    {
        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected override UnityEvent GetEvent(TriggerSource target)
        {
            return target.Trigger;
        }
    }
}