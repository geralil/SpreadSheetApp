// <copyright file="TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace NUnit.SpreadsheetTests
{
    // NUnit 3 tests
    // See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
    using System.Collections;
    using System.Collections.Generic;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Test Class to implement testing of spreadsheet class.
    /// Testing implementations of get cell and get row and get columns methods.
    /// </summary>
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        /// Private member holding spreadsheet class.
        /// </summary>
        private SpreadsheetClass dataBase = new SpreadsheetClass(100, 100);

        /// <summary>
        /// Test method to check for the implementation of
        /// get cell method. Test method checks for successful passing of function
        /// and checks if row index and column index values of returned cell match
        /// expected values.
        /// </summary>
        [Test]
        public void TestGetCellMethod()
        {
            // normal case.//
            CellClass cell = this.dataBase.GetCell(50, 50);
            Assert.AreEqual(49, cell.GetRowIndexValue());
            Assert.AreEqual(49, cell.GetColumnIndexValue());
        }

        /// <summary>
        /// Test method to check if get cell from cell index value
        /// function implements correctly.
        /// </summary>
        [Test]
        public void TestGetCellFromIndexMethod()
        {
            CellClass cell = this.dataBase.GetCellFromIndex("B1");
            Assert.AreEqual(0, cell.GetRowIndexValue());
            Assert.AreEqual(1, cell.GetColumnIndexValue());
        }

        /// <summary>
        /// Function to test for edge cases of get cell method:
        /// [i,j] > size of sheet; [i,j] less than size of sheet; [i,j] equals max row and max column.
        /// </summary>
        [Test]
        public void TestGetCellEdgeCaseMethod()
        {
            CellClass cell = this.dataBase.GetCell(102, 121);
            Assert.IsNull(cell);

            cell = this.dataBase.GetCell(0, 0);
            Assert.IsNull(cell);

            cell = this.dataBase.GetCell(100, 100);
            Assert.AreEqual(99, cell.GetRowIndexValue());
            Assert.AreEqual(99, cell.GetColumnIndexValue());
        }

        /// <summary>
        /// Test method to check for successful implementation
        /// of row count and column count methods.
        /// </summary>
        [Test]
        public void TestRowColumnCountMethods()
        {
            Assert.AreEqual(100, this.dataBase.ColumnCount());
            Assert.AreEqual(100, this.dataBase.RowCount());

            this.dataBase = new SpreadsheetClass(10, 100);
            Assert.AreEqual(100, this.dataBase.ColumnCount());
            Assert.AreEqual(10, this.dataBase.RowCount());
        }
    }
}
