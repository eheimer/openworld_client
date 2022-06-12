// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataContextMediator.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.StrangeIoC
{
    using strange.extensions.mediation.impl;

    using Slash.Unity.DataBind.Core.Data;

    /// <summary>
    ///   Base class for a mediator of a DataContextView.
    /// </summary>
    /// <typeparam name="TView">Type of view.</typeparam>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public abstract class DataContextMediator<TView, TContext> : Mediator
        where TView : DataContextView<TContext> where TContext : Context
    {
        /// <summary>
        ///   Current context of the view/mediator.
        /// </summary>
        public TContext Context
        {
            get
            {
                return this.View.Context;
            }
            set
            {
                this.View.Context = value;
            }
        }

        /// <summary>
        ///   View which belongs to the mediator.
        /// </summary>
        [Inject]
        public TView View { get; set; }

        /// <summary>
        ///   This method fires immediately after injection.
        ///   Override to perform the actions you might normally perform in a constructor.
        /// </summary>
        public override void OnRegister()
        {
            this.View.ContextChanged += this.OnContextChanged;
            this.OnContextChanged(this.View.Context, null);
        }

        /// <summary>
        ///   This method fires just before a GameObject will be destroyed.
        ///   Override to clean up any listeners, or anything else that might keep the View/Mediator pair from being garbage
        ///   collected.
        /// </summary>
        public override void OnRemove()
        {
            this.View.Context = null;
            this.View.ContextChanged -= this.OnContextChanged;
        }

        /// <summary>
        ///   Called when the mediator should register itself to the specified context.
        ///   E.g. add listeners to events.
        /// </summary>
        /// <param name="context">Context to register to.</param>
        protected virtual void RegisterContext(TContext context)
        {
        }

        /// <summary>
        ///   Called when the mediator should unregister itself from the specified context.
        ///   E.g. remove listeners to events.
        /// </summary>
        /// <param name="context">Context to unregister from.</param>
        protected virtual void UnregisterContext(TContext context)
        {
        }

        private void OnContextChanged(TContext newContext, TContext oldContext)
        {
            if (oldContext != null)
            {
                this.UnregisterContext(oldContext);
            }
            if (newContext != null)
            {
                this.RegisterContext(newContext);
            }
        }
    }
}