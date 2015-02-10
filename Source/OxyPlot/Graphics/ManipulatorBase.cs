// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManipulatorBase.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides an abstract base class for controller manipulators.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    /// <summary>
    /// Provides an abstract base class for controller manipulators.
    /// </summary>
    /// <typeparam name="T">The type of the event arguments.</typeparam>
    public abstract class ManipulatorBase<T> where T : OxyInputEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManipulatorBase{T}" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        protected ManipulatorBase(IView view)
            : this(view, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManipulatorBase{T}" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="updateCursor">Whether the cursor will automatically be set during manipulations.</param>
        protected ManipulatorBase(IView view, bool updateCursor)
        {
            this.View = view;
            this.UpdateCursor = updateCursor;
        }

        /// <summary>
        /// Gets the plot view where the event was raised.
        /// </summary>
        /// <value>The plot view.</value>
        public IView View { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the view's cursor will automatically be set during manipulations.
        /// </summary>
        public bool UpdateCursor { get; set; }

        /// <summary>
        /// Occurs when a manipulation is complete.
        /// </summary>
        /// <param name="e">The <see cref="OxyInputEventArgs" /> instance containing the event data.</param>
        public virtual void Completed(T e)
        {
            if (this.UpdateCursor)
            {
                this.View.SetCursorType(CursorType.Default);
            }
        }

        /// <summary>
        /// Occurs when the input device changes position during a manipulation.
        /// </summary>
        /// <param name="e">The <see cref="OxyInputEventArgs" /> instance containing the event data.</param>
        public virtual void Delta(T e)
        {
        }

        /// <summary>
        /// Gets the cursor for the manipulation.
        /// </summary>
        /// <returns>The cursor.</returns>
        public virtual CursorType GetCursorType()
        {
            return CursorType.Default;
        }

        /// <summary>
        /// Occurs when an input device begins a manipulation on the plot.
        /// </summary>
        /// <param name="e">The <see cref="OxyInputEventArgs" /> instance containing the event data.</param>
        public virtual void Started(T e)
        {
            if (this.UpdateCursor)
            {
                this.View.SetCursorType(this.GetCursorType());
            }
        }
    }
}