// <copyright file="ExpressionTreeTestClass_New.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace NUnit.SpreadsheetTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Class for testing additional functionalities added to
    /// expression tree class.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTestClass_New
    {
        /// <summary>
        /// private member holding expression tree.
        /// </summary>
        private ExpressionTree expressionTree = new ExpressionTree(" ");

        /// <summary>
        /// Function to test the implementation of
        /// shunting yard algorithm and check postfix expression
        /// for correctness.
        /// </summary>
        [Test]
        public void ShuntingYardAlgorithmTest()
        {
            string testExpression = "1*2+3";
            List<string> postFixExpression = new List<string>();

            postFixExpression = this.expressionTree.ShuntingYardAlgorithm(testExpression);
            string postFixExpressionString = string.Empty;

            for (int i = 0; i < postFixExpression.Count; i++)
            {
                postFixExpressionString += postFixExpression[i].ToString();
            }

            Assert.AreEqual("12*3+", postFixExpressionString.ToString());
        }

        /// <summary>
        /// Function to test if shunting yard algorithm takes care
        /// of brackets.
        /// </summary>
        [Test]
        public void ShuntingYardAlgorithmWithBrackets()
        {
            string testExpression = "(1*2)+3";
            List<string> postFixExpression = new List<string>();

            postFixExpression = this.expressionTree.ShuntingYardAlgorithm(testExpression);
            string postFixExpressionString = string.Empty;

            for (int i = 0; i < postFixExpression.Count; i++)
            {
                postFixExpressionString += postFixExpression[i].ToString();
            }

            Assert.AreEqual("12*3+", postFixExpressionString.ToString());
        }

        /// <summary>
        /// Function to test tree generation method
        /// using postfix expression.
        /// </summary>
        [Test]
        public void BuildingExpressionTreeUsingPostfixExpression()
        {
            string testExpression = "(1*2)+3";

            this.expressionTree = new ExpressionTree(testExpression);
            double expressionValue = 5;

            Assert.AreEqual(expressionValue, this.expressionTree.Evaluate());
        }

        /// <summary>
        /// Function to test if the expression tree handles multiple
        /// parenthesis and combination of operators well.
        /// </summary>
        [Test]
        public void TestingExpressionWithParenthesisAndCombinationOperators()
        {
            string testExpression = "(100*10/5-1)+(3+6-9*8)";

            this.expressionTree = new ExpressionTree(testExpression);
            double expressionValue = 136;

            Assert.AreEqual(expressionValue, this.expressionTree.Evaluate());
        }
    }
}
