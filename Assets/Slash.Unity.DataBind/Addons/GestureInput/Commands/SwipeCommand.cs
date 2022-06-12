// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwipeCommand.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.GestureInput.Commands
{
    using Slash.Unity.DataBind.Core.Utils;
    using Slash.Unity.DataBind.UI.Unity.Commands;
    using Slash.Unity.GestureInput.Gestures.Implementations;
    using Slash.Unity.GestureInput.Sources;

    using UnityEngine.Events;

    /// <summary>
    ///   Command to invoke when a swipe source is swiped.
    /// </summary>
    public class SwipeCommand : UnityEventCommand<SwipeSource, SwipeEventData>
    {
        /// <summary>
        ///   Swipe directions to invoke command for.
        /// </summary>
        public SwipeDirection DirectionMask;

        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected override UnityEvent<SwipeEventData> GetEvent(SwipeSource target)
        {
            return target.Swipe;
        }

        /// <summary>
        ///   Called when an the event on the target occurred that this command is listening to.
        ///   By default this will invoke the command with the received event data, but derived commands may modify the event data
        ///   first.
        /// </summary>
        /// <param name="eventData">Data send with the event.</param>
        protected override void OnEvent(SwipeEventData eventData)
        {
            // Check if correct direction.
            if (!this.DirectionMask.IsOptionSet(eventData.Direction))
            {
                return;
            }

            base.OnEvent(eventData);
        }
    }
}