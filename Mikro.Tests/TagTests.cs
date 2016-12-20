using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mikro.Models;
using System.Linq;

namespace Mikro.Tests
{
    /// <summary>
    /// Summary description for TagTests
    /// </summary>
    [TestClass]
    public class TagTests
    {
        public TagTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            List<Tag> Tags = new List<Tag>();
            var tag1 = new Tag
            {
                Id = 1,
                Name = "test"
            };

            Tags.Add(tag1);
            var result = Tags.FirstOrDefault(x => x.Name == "test");
            Console.Write(result.Name);
            Assert.AreEqual(result, tag1);
        }
    }
}
