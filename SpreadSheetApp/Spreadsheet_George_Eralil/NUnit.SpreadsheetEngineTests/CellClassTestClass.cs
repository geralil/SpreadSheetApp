// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Cpts321;

namespace NUnit.SpreadsheetEngineTests
{
    [TestFixture]
    public class CellClassTestClass
    {
        private int rowIndex = 0, columnIndex = 0;
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }
}
