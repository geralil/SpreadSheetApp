// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Operator Node factory that creates operator node
    /// and checks if given operator is valid.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Dictionary of operators.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Function to create operator node based on what the operator sign is.
        /// </summary>
        /// <param name="operatorSign"> character denoting operator.</param>
        /// <returns> OperatorNode object.</returns>
        public OperatorNode CreateOperatorNode(char operatorSign)
        {
            if (this.operators.ContainsKey(operatorSign))
            {
                OperatorNode operatorNode = (OperatorNode)Activator.CreateInstance(this.operators[operatorSign]);
                return operatorNode;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// function to check if the given operator is supported by
        /// our implementation. If yes, then true. If no, then false.
        /// </summary>
        /// <param name="operatorSign"> character denoting operator.</param>
        /// <returns> boolean value.</returns>
        public bool IsValidOperator(char operatorSign)
        {
            if (this.operators.ContainsKey(operatorSign))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// function to get the precedence value of an operator sign.
        /// </summary>
        /// <param name="sign"> character denoting sign.</param>
        /// <returns> integer value of precedence.</returns>
        internal uint GetPrecedence(char sign)
        {
            switch (sign)
            {
                case '+':
                    return PlusOperatorNode.Precedence;
                case '-':
                    return MinusOperatorNode.Precedence;
                case '*':
                    return MultiplyOperatorNode.Precendence;
                case '/':
                    return DivideOperatorNode.Precendence;
                default: // if it is not any of the operators that we support, throw an exception:
                    throw new NotSupportedException("this operator isn't supported");
            }
        }

        /// <summary>
        /// function to get the associativity enum value of an
        /// operator node.
        /// </summary>
        /// <param name="sign"> character denoting sign.</param>
        /// <returns> operator node associativity enum value.</returns>
        internal OperatorNode.Associative GetAssociativity(char sign)
        {
            switch (sign)
            {
                case '+':
                    return PlusOperatorNode.Associativity;
                case '-':
                    return MinusOperatorNode.Associativity;
                case '*':
                    return MultiplyOperatorNode.Associativity;
                case '/':
                    return DivideOperatorNode.Associativity;
                default: // if it is not any of the operators that we support, throw an exception:
                    throw new NotSupportedException("this operator isn't supported");
            }
        }

        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);

                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
