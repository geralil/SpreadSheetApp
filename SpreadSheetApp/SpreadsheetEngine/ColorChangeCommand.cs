// <copyright file="ColorChangeCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Color changed command inheriting from Command Interface.
    /// Defines the methods that need to be defined for Color change command.
    /// </summary>
    internal class ColorChangeCommand : ICommand
    {
        /// <summary>
        /// New back color for the cell.
        /// </summary>
        private uint newBackColor;

        /// <summary>
        /// Old back color for the cell.
        /// </summary>
        private uint oldBackColor;

        /// <summary>
        /// Cell in the spreadsheet engine that the command references.
        /// </summary>
        private CellClass cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangeCommand"/> class.
        /// </summary>
        /// <param name="cell"> Cell class cell.</param>
        /// <param name="newBGcolor"> the new background color for cell.</param>
        public ColorChangeCommand(CellClass cell, uint newBGcolor)
        {
            this.cell = cell;
            this.oldBackColor = this.cell.BGcolor;
            this.newBackColor = newBGcolor;
        }

        /// <summary>
        /// Execute command that sets the new color as the cell's background
        /// color.
        /// </summary>
        public void Execute()
        {
            this.cell.BGcolor = this.newBackColor;
        }

        /// <summary>
        /// Un-execute command that sets the last color as the cell's background
        /// color.
        /// </summary>
        public void UnExecute()
        {
            this.cell.BGcolor = this.oldBackColor;
        }

        /// <summary>
        /// Get Type method that returns the type of command
        /// the given instance is.
        /// </summary>
        /// <returns> string denoting type.</returns>
        string ICommand.GetType()
        {
            return "Color Change";
        }
    }
}
