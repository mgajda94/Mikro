using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mikro.Models;
using Mikro.Controllers;
using System.Collections.Generic;

namespace Mikro.Tests
{
    [TestClass]
    public class MikroControllerTests
    {
        [TestMethod]
        public void TestPlusPosts()
        {
            var post = new Post
            {
                Id = 1,
                PlusUsers = new List<ApplicationUser>()
            };

            var controller = new MikroController();
            
        }
    }
}
