using DolbyIO.Comms;
namespace DolbyIO.Comms.Tests
{
    public class SessionTests
    {
        [Fact]
        public void Test_UserInfo_ShouldMarshall()
        {
            UserInfo src = new UserInfo();
            src.Name = "Test";
            src.ExternalId = "TestExternalId";
            src.AvatarURL = "http://avatar.url";

            UserInfo dest;
            NativeTests.UserInfoTest(src, out dest);

            Assert.NotEqual(src, dest);
            Assert.Equal(src.Name, dest.Name);
            Assert.Equal(src.ExternalId, dest.ExternalId);
            Assert.Equal(src.AvatarURL, dest.AvatarURL);
            Assert.Equal("anonymous", dest.Id);
        }
    }
}