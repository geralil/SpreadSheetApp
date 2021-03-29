// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
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
    /// Class to implement constantNode operator.
    /// Inherits from expression tree node.
    /// </summary>
    public class ConstantNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value"> value of constant node.</param>
        public ConstantNode(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the value of constant node.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Method to return the value of constant node.
        /// </summary>
        /// <returns> value of constant node.</returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
