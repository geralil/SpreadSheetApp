// <copyright file="VariableNode.cs" company="PlaceholderCompany">
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
    /// Class implementing variable node, inheriting from expression tree node class.
    /// </summary>
    public class VariableNode : ExpressionTreeNode
    {
        /// <summary>
        /// Storing string value of variable name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Storing the dictionary of variables.
        /// </summary>
        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name"> name of variable.</param>
        /// <param name="variables"> dictionary containing all variable values.</param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.name = name;
            this.variables = variables;
        }

        /// <summary>
        /// Function to extract value of variable from variables dictionary.
        /// </summary>
        /// <returns> value of the given variable name.</returns>
        public override double Evaluate()
        {
            double value = 0.0;

            if (this.variables.ContainsKey(this.name))
            {
                value = this.variables[this.name];
                return value;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
