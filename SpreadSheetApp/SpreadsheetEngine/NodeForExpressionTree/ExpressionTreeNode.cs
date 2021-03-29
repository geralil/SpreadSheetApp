// <copyright file="ExpressionTreeNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract class for expression tree node.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// abstract evaluate method for each operator node.
        /// </summary>
        /// <returns> evaluated value of operator node.</returns>
        public abstract double Evaluate();
    }
}
