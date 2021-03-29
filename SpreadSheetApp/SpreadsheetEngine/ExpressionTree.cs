// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for implementing the expression tree.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// root of expression tree.
        /// </summary>
        private ExpressionTreeNode root;

        /// <summary>
        /// Operator node factory instance.
        /// </summary>
        private OperatorNodeFactory operatorNodeFactory = new OperatorNodeFactory();

        /// <summary>
        /// Dictionary of variables.
        /// </summary>
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Cell which expression tree belongs to.
        /// </summary>
        private Cell cellForTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Denotes the expression to be converted.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.Compile(expression);
            this.Expression = expression;
        }

        /// <summary>
        /// Gets or sets the string denoting the expression.
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// function to assign the cell which tree belongs to.
        /// </summary>
        /// <param name="cell"> Cell which tree belongs to.</param>
        public void SetCellForTree(CellClass cell)
        {
            Cell scell = cell as Cell;
            this.cellForTree = scell;
        }

        /// <summary>
        /// Method to set variables to the dictionary member.
        /// </summary>
        /// <param name="variableName"> name of variable.</param>
        /// <param name="variableValue"> variable value.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables.Add(variableName, variableValue);
        }

        /// <summary>
        /// Returns back a list of variables used in the expression.
        /// </summary>
        /// <returns> List of string values.</returns>
        public List<string> GetVariableNames()
        {
            List<string> expressionVariables = new List<string>();  // list of strings storing variables.//
            var postFixExpression = this.ShuntingYardAlgorithm(this.Expression);

            foreach (var item in postFixExpression)
            {
                // check until variable is found.//
                if (item.Length == 1 && this.IsOperatorOrParenthesis(item[0]))
                {
                    continue;
                }
                else
                {
                    double number = 0.0;
                    if (double.TryParse(item, out number))
                    {
                        continue;
                    }
                    else
                    {
                        // Add variable to list.//
                        expressionVariables.Add(item);
                    }
                }
            }

            return expressionVariables;
        }

        /// <summary>
        /// Evaluates the expression to a double value.
        /// </summary>
        /// <returns> value of expression.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Function that performs the shunting yard algorithm.
        /// Code snippets taken from examples in class.
        /// </summary>
        /// <param name="expression"> string denoting expression.</param>
        /// <returns> List of postfix expression characters.</returns>
        public List<string> ShuntingYardAlgorithm(string expression)
        {
            List<string> postFixExpression = new List<string>();    // stores postfix expression characters.//
            Stack<char> operators = new Stack<char>();          // stack storing operators.//
            int operandStart = -1;          // operand start variable to handle multi-character variables.//

            // iterating through expression.//
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                // check if character is operator or parenthesis.//
                if (this.IsOperatorOrParenthesis(c))
                {
                    // if the expression encountered a variable before the operator, output operand to list.//
                    if (operandStart != -1)
                    {
                        string operand = expression.Substring(operandStart, i - operandStart);
                        postFixExpression.Add(operand);
                        operandStart = -1;
                    }

                    // if left parenthesis encountered, push it onto stack.//
                    if (this.IsLeftParenthesis(c))
                    {
                        operators.Push(c);
                    }

                    // if right parenthesis encountered, pop all operators till left parenthesis encountered.//
                    if (this.IsRightParenthesis(c))
                    {
                        char op = operators.Pop();
                        while (!this.IsLeftParenthesis(op))
                        {
                            postFixExpression.Add(op.ToString());
                            op = operators.Pop();
                        }
                    }
                    else if (this.operatorNodeFactory.IsValidOperator(c))
                    {
                        // if operator stack is empty or left parenthesis is on top of stack, push the operator onto the stack.//
                        if (operators.Count == 0 || this.IsLeftParenthesis(operators.Peek()))
                        {
                            operators.Push(c);
                        }
                        else if (this.IsHigherPrecendence(c, operators.Peek())
                            || (this.IsSamePrecedence(c, operators.Peek()) && this.IsRightAssociative(c)))
                        {
                            operators.Push(c);
                        }
                        else if (this.IsLowerPrecendence(c, operators.Peek())
                            || (this.IsSamePrecedence(c, operators.Peek()) && this.IsLeftAssociative(c)))
                        {
                            do
                            {
                                char op = operators.Pop();
                                postFixExpression.Add(op.ToString());
                            }
                            while (operators.Count > 0 && !this.IsLeftParenthesis(operators.Peek())
                            && (this.IsLowerPrecendence(c, operators.Peek())
                            || (this.IsSamePrecedence(c, operators.Peek()) && this.IsLeftAssociative(c))));
                            operators.Push(c);
                        }
                    }
                }
                else if (operandStart == -1)
                {
                    operandStart = i;
                }
            }

            // if operands are encountered, output to postfix expression.//
            if (operandStart != -1)
            {
                postFixExpression.Add(expression.Substring(operandStart, expression.Length - operandStart));
                operandStart = -1;
            }

            // if expression is empty and operator stack isn't, pop and output operators to postfix expression.//
            while (operators.Count > 0)
            {
                postFixExpression.Add(operators.Pop().ToString());
            }

            return postFixExpression;
        }

        /// <summary>
        /// function to update the variable dictionary of tree with the
        /// referenced cells.
        /// </summary>
        /// <param name="cell"> Cell that is referenced in expression.</param>
        public void UpdateVariableDict(CellClass cell)
        {
            if (this.variables.ContainsKey(cell.GetCellIndex()))
            {
                // Dictionary contains the referenced variable value.//
                double value = 0.0;
                if (double.TryParse(cell.Value, out value))
                {
                    this.variables[cell.GetCellIndex()] = value;
                }
                else
                {
                    this.variables[cell.GetCellIndex()] = 0.0;
                }
            }
            else
            {
                // Dictionary does not contain the referenced variable value.//
                double value = 0.0;
                if (double.TryParse(cell.Value, out value))
                {
                    this.SetVariable(cell.GetCellIndex(), value);
                }
                else
                {
                    this.SetVariable(cell.GetCellIndex(), 0);
                }
            }
        }

        /// <summary>
        /// Method to deal with changes to referenced cells in expression.
        /// Function also reevaluates the expression tree and sets it to the parent cell.
        /// </summary>
        /// <param name="sender"> Cell that got changed.</param>
        /// <param name="e"> event args.</param>
        public void CellChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;

            if (this.cellForTree.Text != string.Empty)
            {
                if (cell.Value != "!(circular reference)")
                {
                    this.UpdateVariableDict(cell);

                    this.cellForTree.Value = this.Evaluate().ToString();    // updating value of the parent cell, to update in UI.//
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Function to convert string expression into expression tree.
        /// </summary>
        /// <param name="expression"> string denoting expression.</param>
        /// <returns> returns the expression Tree Node.</returns>
        private ExpressionTreeNode Compile(string expression)
        {
            Stack<ExpressionTreeNode> nodes = new Stack<ExpressionTreeNode>();

            // return null if string is empty.//
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            var postFixExpression = this.ShuntingYardAlgorithm(expression);

            foreach (var item in postFixExpression)
            {
                if (item.Length == 1 && this.IsOperatorOrParenthesis(item[0]))
                {
                    OperatorNode node = this.operatorNodeFactory.CreateOperatorNode(item[0]);
                    node.Right = nodes.Pop();
                    node.Left = nodes.Pop();
                    nodes.Push(node);
                }
                else
                {
                    double number = 0.0;
                    if (double.TryParse(item, out number))
                    {
                        nodes.Push(new ConstantNode(number));
                    }
                    else
                    {
                        nodes.Push(new VariableNode(item, ref this.variables));
                    }
                }
            }

            return nodes.Pop();
        }

        /// <summary>
        /// Function to evaluate the value of the expression tree.
        /// </summary>
        /// <param name="root"> Expression tree root.</param>
        /// <returns> evaluated value of tree.</returns>
        private double Evaluate(ExpressionTreeNode root)
        {
            ConstantNode constantNode = root as ConstantNode;   // will return null if node isn't constantNode.//
            if (constantNode != null)
            {
                return constantNode.Value;
            }

            VariableNode variableNode = root as VariableNode;
            if (variableNode != null)
            {
                return variableNode.Evaluate();
            }

            OperatorNode operatorNode = root as OperatorNode;
            if (operatorNode != null)
            {
                return operatorNode.Evaluate();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// function to check if character is operator or parenthesis.
        /// </summary>
        /// <param name="c"> expression character.</param>
        /// <returns> boolean value.</returns>
        private bool IsOperatorOrParenthesis(char c)
        {
            if (c == '(' || c == ')')
            {
                return true;
            }
            else if (this.operatorNodeFactory.IsValidOperator(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// function to check if character is right parenthesis.
        /// </summary>
        /// <param name="c"> expression character.</param>
        /// <returns> boolean value.</returns>
        private bool IsRightParenthesis(char c)
        {
            if (c == ')')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// function to check if character is left parenthesis.
        /// </summary>
        /// <param name="c"> expression character.</param>
        /// <returns> boolean value.</returns>
        private bool IsLeftParenthesis(char c)
        {
            if (c == '(')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// function to compare precedence of two operators.
        /// </summary>
        /// <param name="left"> left operator.</param>
        /// <param name="right"> right operator.</param>
        /// <returns> -1, 0, 1, denoting left>right, equal and right>left respectively.</returns>
        private int ComparePrecedence(char left, char right)
        {
            uint leftPrecedence = this.operatorNodeFactory.GetPrecedence(left);
            uint rightPrecedence = this.operatorNodeFactory.GetPrecedence(right);

            return (leftPrecedence > rightPrecedence) ? -1 : (leftPrecedence < rightPrecedence) ? 1 : 0;
        }

        /// <summary>
        /// function to check if two operators have same precedence.
        /// </summary>
        /// <param name="left"> left operator.</param>
        /// <param name="right"> right operator.</param>
        /// <returns> boolean value.</returns>
        private bool IsSamePrecedence(char left, char right)
        {
            return this.ComparePrecedence(left, right) == 0;
        }

        /// <summary>
        /// function to check if left operator has higher precedence.
        /// </summary>
        /// <param name="left"> left operator.</param>
        /// <param name="right"> right operator.</param>
        /// <returns> boolean value.</returns>
        private bool IsHigherPrecendence(char left, char right)
        {
            return this.ComparePrecedence(left, right) > 0;
        }

        /// <summary>
        /// function to check if left operator has lower precedence.
        /// </summary>
        /// <param name="left"> left operator.</param>
        /// <param name="right"> right operator.</param>
        /// <returns> boolean value.</returns>
        private bool IsLowerPrecendence(char left, char right)
        {
            return this.ComparePrecedence(left, right) < 0;
        }

        /// <summary>
        /// function to check if operator is right associative.
        /// </summary>
        /// <param name="c"> character denoting sign.</param>
        /// <returns> boolean value.</returns>
        private bool IsRightAssociative(char c)
        {
            return this.operatorNodeFactory.GetAssociativity(c) == OperatorNode.Associative.Right;
        }

        /// <summary>
        /// function to check if operator is left associative.
        /// </summary>
        /// <param name="c"> character denoting sign.</param>
        /// <returns> boolean value.</returns>
        private bool IsLeftAssociative(char c)
        {
            return this.operatorNodeFactory.GetAssociativity(c) == OperatorNode.Associative.Left;
        }
    }
}
