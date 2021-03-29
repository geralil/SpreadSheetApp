// <copyright file="MinusOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class implementing minus operator node, inheriting from operator node.
    /// </summary>
    public class MinusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the operator character.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Gets precedence value for minus operator.
        /// </summary>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets associativity value for minus operator.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method to find the value of the minus operator.
        /// Subtracts left and right node of expression tree.
        /// </summary>
        /// <returns> evaluated value of minus operator.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
