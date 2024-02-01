using NUnit.Framework;
using SocialApp.BLL.Models;
using SocialApp.BLL.Services;

namespace SocialApp.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void UserServiceTest()
        {
            var userServiceTest = new UserService();
            var newAuthentication = new UserAuthenticationData() { Email = "email", Password = "pass"};

            var test = userServiceTest.Authenticate(newAuthentication);

            Assert.That(test, newAuthentication);
        }
    }
}
