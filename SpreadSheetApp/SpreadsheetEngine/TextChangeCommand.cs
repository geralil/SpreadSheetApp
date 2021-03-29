// <copyright file="TextChangeCommand.cs" company="PlaceholderCompany">
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
    /// Text change command that inherits from Command Interface
    /// Defines methods required for by text changed command.
    /// </summary>
    internal class TextChangeCommand : ICommand
    {
        /// <summary>
        /// Old text of the cell.
        /// </summary>
        private string oldTextInCell;

        /// <summary>
        /// New Text of the cell.
        /// </summary>
        private string newTextInCell;

        /// <summary>
        /// Cell class cell denoting the cell referenced by
        /// command.
        /// </summary>
        private CellClass cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangeCommand"/> class.
        /// </summary>
        /// <param name="cell"> Cell class cell that the command references.</param>
        /// <param name="newText"> the new text that will be assigned to cell.</param>
        public TextChangeCommand(CellClass cell, string newText)
        {
            this.cell = cell;
            this.oldTextInCell = this.cell.Text;
            this.newTextInCell = newText;
        }

        /// <summary>
        /// Execute method to set the new text as the cell's text.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newTextInCell;
        }

        /// <summary>
        /// Un-execute method to set the old text as the cell's text.
        /// </summary>
        public void UnExecute()
        {
            this.cell.Text = this.oldTextInCell;
        }

        /// <summary>
        /// Method to return the type of command.
        /// </summary>
        /// <returns> String denoting type of command.</returns>
        string ICommand.GetType()
        {
            return "Text Change";
        }
    }
}
