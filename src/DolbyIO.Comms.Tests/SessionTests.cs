using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    [Collection("Sdk")]
    public class SessionTests
    {
        private SdkFixture _fixture;

        public SessionTests(SdkFixture fixture)
        {
            _fixture = fixture;
        }

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

        [Fact]
        public async void Test_Session_CanCallNativeMethods()
        {
            await _fixture.Sdk.Init("Dumy");

            UserInfo info = new UserInfo();
            await _fixture.Sdk.Session.Open(info);
            await _fixture.Sdk.Session.Close();

            _fixture.Sdk.Dispose();
        }
    }
}