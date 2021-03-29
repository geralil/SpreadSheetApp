// <copyright file="Program.cs" company="PlaceholderCompany">
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
    /// Program class containing entry point of application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main program which is the entry point for application.
        /// function calls execute code in runCode class.
        /// </summary>
        /// <param name="args"> string args.</param>
        public static void Main(string[] args)
        {
            RunCode run = new RunCode();
            run.ExecuteCode();
        }
    }
}
