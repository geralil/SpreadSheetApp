// <copyright file="PlusOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class implementing plus operator node, inheriting from operator node.
    /// </summary>
    public class PlusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the operator character for node.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets the precedence value for plus operator.
        /// </summary>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets the associative value for plus operator.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method to evaluate the value of plus operator.
        /// Adds left and right node of plus operator node.
        /// </summary>
        /// <returns> double value after adding.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
