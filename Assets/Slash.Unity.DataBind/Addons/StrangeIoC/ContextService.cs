// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextService.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.StrangeIoC
{
    using System.Collections.Generic;

    /// <summary>
    ///   Base class for a service that holds contexts that can be accessed by a <see cref="int" /> key.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public class ContextService<TContext> : ContextService<TContext, int>
    {
    }

    /// <summary>
    ///   Base class for a service that holds contexts that can be accessed by a key.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    /// <typeparam name="TId">Type of key.</typeparam>
    public class ContextService<TContext, TId>
    {
        private readonly Dictionary<TId, TContext> contexts = new Dictionary<TId, TContext>();

        /// <summary>
        ///   Enumerator for all stored contexts.
        /// </summary>
        public IEnumerable<TContext> Contexts
        {
            get
            {
                return this.contexts.Values;
            }
        }

        /// <summary>
        ///   Returns the context that is stored under the specified key.
        /// </summary>
        /// <param name="id">Id to get context for.</param>
        /// <returns>Context that is stored under the specified id, if there is one; otherwise, null.</returns>
        public TContext GetContext(TId id)
        {
            TContext context;
            this.contexts.TryGetValue(id, out context);
            return context;
        }

        /// <summary>
        ///   Sets the context for the specified id.
        /// </summary>
        /// <param name="id">Id to store context at.</param>
        /// <param name="context">Context to store.</param>
        public void SetContext(TId id, TContext context)
        {
            this.contexts[id] = context;
        }
    }
}