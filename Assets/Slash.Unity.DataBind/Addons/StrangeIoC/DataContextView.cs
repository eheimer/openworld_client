// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataContextView.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.StrangeIoC
{
    using strange.extensions.context.impl;
    using strange.extensions.mediation.api;
    using strange.extensions.mediation.impl;
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;
    using Context = Slash.Unity.DataBind.Core.Data.Context;

    /// <summary>
    ///     Base class for a view that has a data context.
    /// </summary>
    /// <typeparam name="TContext">Type of data context.</typeparam>
    [RequireComponent(typeof(ContextHolder))]
    public class DataContextView<TContext> : View
        where TContext : Context
    {
        /// <summary>
        ///     Delegate for the ContextChanged event.
        /// </summary>
        /// <param name="newContext">New context of the view.</param>
        /// <param name="oldContext">Old context of the view.</param>
        public delegate void ContextChangedDelegate(TContext newContext, TContext oldContext);

        /// <summary>
        ///     If set, the view won't be attached to the first context, but instead waits for
        ///     being attached later on.
        /// </summary>
        [Tooltip(
            "If set, the view won't be attached to the first context, but instead waits for being attached later on")]
        public bool AllowLateContextAttaching;

        private TContext context;

        private ContextHolder contextHolder;

        /// <summary>
        ///     Current context of the view.
        /// </summary>
        public TContext Context
        {
            get { return this.context; }
            set
            {
                if (value == this.context)
                {
                    return;
                }

                var oldContext = this.context;
                this.context = value;

                if (this.ContextHolder != null && this.ContextHolder.Context != this.context)
                {
                    this.ContextHolder.Context = this.context;
                }

                this.OnContextChanged(value, oldContext);
            }
        }

        private ContextHolder ContextHolder
        {
            get
            {
                if (this.contextHolder == null)
                {
                    this.contextHolder = this.GetComponent<ContextHolder>();
                }

                return this.contextHolder;
            }
        }

        /// <summary>
        ///     Called when the context of the view changed.
        /// </summary>
        public event ContextChangedDelegate ContextChanged;

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();

            this.requiresContext = !this.AllowLateContextAttaching;
        }

        /// <summary>
        ///     Recurses through Transform.parent to find the GameObject to which ContextView is attached
        ///     Has a loop limit of 100 levels.
        ///     By default, raises an Exception if no Context is found.
        /// </summary>
        /// <param name="view">View to add/remove to/from context.</param>
        /// <param name="toAdd">Indicates if the view should be added or removed.</param>
        /// <param name="finalTry">Indicates if this is the final try and the first context should be used if no other is found.</param>
        protected override void bubbleToContext(MonoBehaviour view, bool toAdd, bool finalTry)
        {
            const int loopMax = 100;

            var trans = view.transform;
            var loopLimiter = 0;
            while (trans != null && loopLimiter < loopMax)
            {
                loopLimiter++;
                var contextView = trans.GetComponent<ContextView>();
                if (contextView != null)
                {
                    if (contextView.context != null)
                    {
                        var contextViewContext = contextView.context;
                        if (toAdd)
                        {
                            contextViewContext.AddView(view);
                            this.registeredWithContext = true;
                        }
                        else
                        {
                            contextViewContext.RemoveView(view);
                        }

                        return;
                    }
                }

                trans = trans.parent;
            }

            if (this.requiresContext && finalTry)
            {
                //last ditch. If there's a Context anywhere, we'll use it!
                if (strange.extensions.context.impl.Context.firstContext != null)
                {
                    strange.extensions.context.impl.Context.firstContext.AddView(view);
                    this.registeredWithContext = true;
                    return;
                }

                var msg = loopLimiter == loopMax
                    ? "A view couldn't find a context. Loop limit reached."
                    : "A view was added with no context. Views must be added into the hierarchy of their ContextView lest all hell break loose.";
                msg += "\nView: " + view;
                throw new MediationException(msg, MediationExceptionType.NO_CONTEXT);
            }
        }

        /// <summary>
        ///     Unity callback.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (this.ContextHolder != null)
            {
                this.ContextHolder.ContextChanged -= this.OnContextHolderContextChanged;
                this.Context = null;
            }
        }

        /// <summary>
        ///     Called when the view should register itself to the specified context.
        ///     E.g. add listeners to events.
        /// </summary>
        /// <param name="context">Context to register to.</param>
        protected virtual void RegisterContext(TContext context)
        {
        }

        /// <summary>
        ///     Unity callback.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            if (this.ContextHolder != null)
            {
                if (this.context != null)
                {
                    this.ContextHolder.Context = this.context;
                }
                else
                {
                    this.Context = this.ContextHolder.Context as TContext;
                }

                this.ContextHolder.ContextChanged += this.OnContextHolderContextChanged;
            }
        }

        /// <summary>
        ///     Called when the view should unregister itself from the specified context.
        ///     E.g. remove listeners to events.
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

            var handler = this.ContextChanged;
            if (handler != null)
            {
                handler(newContext, oldContext);
            }
        }

        private void OnContextHolderContextChanged(object newContext)
        {
            this.Context = newContext as TContext;
        }
    }
}