// <copyright file="MultiplyOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class implementing operator node for multiplication, inheriting from operator node.
    /// </summary>
    public class MultiplyOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets character for operator node.
        /// </summary>
        public static char Operator => '*';

        /// <summary>
        /// Gets precedence value for multiplication.
        /// </summary>
        public static ushort Precendence => 6;

        /// <summary>
        /// Gets Associativity value for operator node.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Evaluate method for multiplication.
        /// Multiplies left and right node of expression tree.
        /// </summary>
        /// <returns> double value after multiplication.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
