// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Derived class cell from abstract base class CellClass.
    /// Cell aims to allow only the spreadsheet class to change the value associated with a cell.
    /// </summary>
    internal class Cell : CellClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndexValue"> Cell row index value.</param>
        /// <param name="columnIndexValue"> Cell column index value.</param>
        public Cell(int rowIndexValue, int columnIndexValue)
            : base(rowIndexValue, columnIndexValue)
        {
            this.Text = string.Empty;
        }

        /// <summary>
        /// Gets or sets evaluated Value.
        /// overriding Value property from abstract base class.
        /// </summary>
        public override string Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                this.evalValue = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
