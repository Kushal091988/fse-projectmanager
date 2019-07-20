using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Api;
using System.Web.Http;

namespace Web.Api.Tests
{
    [TestFixture]
    public class UsersControllerTest
    {
        private UsersController _usersController;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _usersController = new UsersController();
        }

        [Test]
        public void GetUsers_Test()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
