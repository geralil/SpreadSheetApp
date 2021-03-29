// <copyright file="RunCode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace DemoExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cpts321;

    /// <summary>
    /// Class RunCode to implement the console application menu
    /// and subsequent functions.
    /// </summary>
    public class RunCode
    {
        /// <summary>
        /// Instance of expression tree class.
        /// </summary>
        private ExpressionTree expression = new ExpressionTree(string.Empty);

        /// <summary>
        /// Function to execute displaying of menu and
        /// perform subsequent functionalities.
        /// </summary>
        public void ExecuteCode()
        {
            int option = 0;

            while (option != 4)
            {
                option = this.DisplayMenu();
                this.PerformFunction(option);
            }
        }

        /// <summary>
        /// Function to perform the functionality based on the menu option selected.
        /// </summary>
        /// <param name="option"> menu option selected.</param>
        private void PerformFunction(int option)
        {
            switch (option)
            {
                // Enter new expression.//
                case 1:
                    Console.WriteLine("\nEnter new expression: ");
                    string newExpression = Console.ReadLine();
                    this.expression = new ExpressionTree(newExpression);
                    break;

                // Enter variable names and values.//
                case 2:
                    Console.WriteLine("\nEnter Variable Name: ");
                    string variableName = Console.ReadLine();
                    Console.WriteLine("\nEnter Variable Value: ");
                    int variableValue = Convert.ToInt32(Console.ReadLine());
                    this.expression.SetVariable(variableName, variableValue);
                    break;

                // Evaluate expression tree generated.//
                case 3:
                    double expressionValue = this.expression.Evaluate();
                    Console.WriteLine("\nExpression Evaluates to: {0}", expressionValue);
                    break;

                // option to quit.//
                case 4:
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("\nInvalid option!");
                    break;
            }
        }

        /// <summary>
        /// Function to display menu items.
        /// </summary>
        /// <returns> menu option selected.</returns>
        private int DisplayMenu()
        {
            int option = 0;

            Console.WriteLine("Menu (current expression = {0})\n", this.expression.Expression);
            Console.WriteLine("\t 1. Enter new expression: \n");
            Console.WriteLine("\t 2. Set a variable value: \n");
            Console.WriteLine("\t 3. Evaluate Tree: \n");
            Console.WriteLine("\t 4. Quit \n");
            option = Convert.ToInt32(Console.ReadLine());

            return option;
        }
    }
}
