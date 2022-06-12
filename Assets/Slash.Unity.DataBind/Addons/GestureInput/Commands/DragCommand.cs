// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DragCommand.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.GestureInput.Commands
{
    using Slash.Unity.DataBind.UI.Unity.Commands;
    using Slash.Unity.GestureInput.Gestures.Implementations;
    using Slash.Unity.GestureInput.Sources;

    using UnityEngine.Events;

    /// <summary>
    ///   Command to invoke when a drag source is dragged.
    /// </summary>
    public class DragCommand : UnityEventCommand<DragSource, DragEventData>
    {
        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected override UnityEvent<DragEventData> GetEvent(DragSource target)
        {
            return target.Drag;
        }
    }
}