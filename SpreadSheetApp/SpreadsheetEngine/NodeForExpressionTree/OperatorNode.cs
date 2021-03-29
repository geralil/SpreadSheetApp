// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
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
    /// Abstract class to implement node for operators.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Enumerated values for associative property.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// right associative property
            /// </summary>
            Right,

            /// <summary>
            /// left associative property
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets or sets left node of current node.
        /// </summary>
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets right node of current node.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }
    }
}
