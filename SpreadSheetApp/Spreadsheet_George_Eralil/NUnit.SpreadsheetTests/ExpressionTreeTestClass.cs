// <copyright file="ExpressionTreeTestClass.cs" company="PlaceholderCompany">
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
    /// Test class testing expression tree class.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTestClass
    {
        /// <summary>
        /// Test to check for successful generation of expression tree.
        /// </summary>
        [Test]
        public void ExpressionTreeImplementationTestMethod()
        {
            string testExpression = "1+2+3+4";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.IsInstanceOf(typeof(ExpressionTree), expression);
        }

        /// <summary>
        /// test to check if implementation handles single constants.
        /// </summary>
        [Test]
        public void ExpressionTreeSingleConstantTestMethod()
        {
            string testExpression = "21";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.AreEqual(21, expression.Evaluate());
        }

        /// <summary>
        /// test to check if implementation handles variables.
        /// </summary>
        [Test]
        public void ExpressionTreeVariableTestMethod()
        {
            string testExpression = "AB+CD+DE+EF";
            ExpressionTree expression = new ExpressionTree(testExpression);
            expression.SetVariable("AB", 10);
            expression.SetVariable("CD", 11);
            expression.SetVariable("DE", 13);
            expression.SetVariable("EF", 14);
            Assert.AreEqual(48, expression.Evaluate());
        }

        /// <summary>
        /// test to check if implementation handles single variables.
        /// </summary>
        [Test]
        public void ExpressionTreeSingleVariableTestMethod()
        {
            string testExpression = "AB";
            ExpressionTree expression = new ExpressionTree(testExpression);
            expression.SetVariable("AB", 10);
            Assert.AreEqual(10, expression.Evaluate());
        }

        /// <summary>
        /// test to check implementation of plus operator.
        /// </summary>
        [Test]
        public void ExpressionTreePlusEvaluateTestMethod()
        {
            string testExpression = "1+2+3+4";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.AreEqual(10, expression.Evaluate());
        }

        /// <summary>
        /// test to check implementation of minus operator.
        /// </summary>
        [Test]
        public void ExpressionTreeMinusEvaluateTestMethod()
        {
            string testExpression = "1-2-3-4";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.AreEqual(-8, expression.Evaluate());
        }

        /// <summary>
        /// test to check implementation of divide operator.
        /// </summary>
        [Test]
        public void ExpressionTreeDivideEvaluateTestMethod()
        {
            string testExpression = "10/5";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.AreEqual(2, expression.Evaluate());
        }

        /// <summary>
        /// test to check implementation of multiply operator.
        /// </summary>
        [Test]
        public void ExpressionTreeMultiplyEvaluateTestMethod()
        {
            string testExpression = "1*2*3*4";
            ExpressionTree expression = new ExpressionTree(testExpression);
            Assert.AreEqual(24, expression.Evaluate());
        }
    }
}
