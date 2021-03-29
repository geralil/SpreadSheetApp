// <copyright file="CellXmlTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace NUnit.SpreadsheetTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Class to test xml functionalities of cell class.
    /// </summary>
    [TestFixture]
    public class CellXmlTest
    {
        /// <summary>
        /// Instance of spreadsheet class to test on.
        /// </summary>
        private SpreadsheetClass database = new SpreadsheetClass(20, 20);

        /// <summary>
        /// Testing the write method to check if the function passes.
        /// </summary>
        [Test]
        public void TestWriteMethod()
        {
            XmlWriter xmlWriter = null;
            xmlWriter = XmlWriter.Create("sampledata.xml");

            this.database.GetCell(12, 12).Text = "Bow Down to geralil";
            this.database.GetCell(12, 12).BGcolor = 0xFF8000FF;
            this.database.GetCell(12, 12).WriteXml(xmlWriter);
            xmlWriter.Close();
        }

        /// <summary>
        /// Testing the read method to check if the function passes.
        /// </summary>
        [Test]
        public void TestReadMethod()
        {
            XmlWriter xmlWriter = null;
            xmlWriter = XmlWriter.Create("sampledata.xml");

            this.database.GetCell(12, 12).Text = "Bow Down to geralil";
            this.database.GetCell(12, 12).BGcolor = 0xFF8000FF;
            this.database.GetCell(12, 12).WriteXml(xmlWriter);
            xmlWriter.Close();

            XmlReader xmlReader = null;
            xmlReader = XmlReader.Create("sampledata.xml");

            this.database.GetCell(12, 12).ReadXml(xmlReader);
            xmlReader.Close();
        }
    }
}
