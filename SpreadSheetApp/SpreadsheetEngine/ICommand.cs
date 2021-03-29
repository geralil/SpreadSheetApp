// <copyright file="ICommand.cs" company="PlaceholderCompany">
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
    /// Interface commands that will act as parent interface for concrete commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute method for individual concrete commands.
        /// </summary>
        void Execute();

        /// <summary>
        /// Un-execute method for concrete commands.
        /// </summary>
        void UnExecute();

        /// <summary>
        /// Get Type method to return type of command.
        /// </summary>
        /// <returns> string denoting type of command.</returns>
        string GetType();
    }
}
