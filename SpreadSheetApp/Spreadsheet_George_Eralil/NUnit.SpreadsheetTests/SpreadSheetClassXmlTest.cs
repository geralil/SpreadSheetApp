// <copyright file="SpreadSheetClassXmlTest.cs" company="PlaceholderCompany">
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
    using System.Xml;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Class to test the spreadsheet xml functions
    /// to load and save.
    /// </summary>
    [TestFixture]
    public class SpreadSheetClassXmlTest
    {
        /// <summary>
        /// instance of spreadsheet class to perform tests.
        /// </summary>
        private SpreadsheetClass database = new SpreadsheetClass(20, 20);

        /// <summary>
        /// Function to test if the save method executes
        /// successfully.
        /// </summary>
        [Test]
        public void TestSaveMethod()
        {
            XmlWriter xmlWriter = null;
            xmlWriter = XmlWriter.Create("sampledata.xml");

            this.database.GetCell(12, 12).Text = "Bow Down to geralil";
            this.database.GetCell(12, 12).BGcolor = 0xFF8000FF;
            this.database.SaveToXml(xmlWriter);
            xmlWriter.Close();
        }

        /// <summary>
        /// Function to test if the Load method executes
        /// successfully.
        /// </summary>
        [Test]
        public void TestReadMethod()
        {
            XmlWriter xmlWriter = null;
            xmlWriter = XmlWriter.Create("sampledata.xml");

            this.database.GetCell(12, 12).Text = "Bow Down to geralil";
            this.database.GetCell(12, 12).BGcolor = 0xFF8000FF;
            this.database.SaveToXml(xmlWriter);
            xmlWriter.Close();

            XmlReader reader = null;
            reader = XmlReader.Create("sampledata.xml");

            this.database.LoadFromXml(reader);
            Assert.AreEqual("Bow Down to geralil", this.database.GetCell(12, 12).Text);
            reader.Close();
        }
    }
}
