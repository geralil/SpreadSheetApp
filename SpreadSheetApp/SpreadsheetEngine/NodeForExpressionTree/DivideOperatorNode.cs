// <copyright file="DivideOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class implementing division operator node.
    /// inheriting from operator node class.
    /// </summary>
    public class DivideOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the operator sign for division.
        /// </summary>
        public static char Operator => '/';

        /// <summary>
        /// Gets the precedence value for division.
        /// </summary>
        public static ushort Precendence => 6;

        /// <summary>
        /// Gets the associativity value for division.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Evaluates the division operator node.
        /// Divides left and right nodes from expression tree.
        /// </summary>
        /// <returns> evaluated value of division operator.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
