using DolbyIO.Comms;
namespace DolbyIO.Comms.Tests
{   
    public class DolbyIOSDKTests
    {
        private DolbyIOSDK _sdk = new DolbyIOSDK();

        [Fact]
        public async void Test_Session_ThrowsIfNotIntialized()
        {
            UserInfo user = new UserInfo();
            user.Name = "Anonymous";

            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Session.Open(user));
        }

        [Fact]
        public async void Test_Conference_ThrowsIfNotIntialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Conference.Leave());
        }

        [Fact]
        public async void Test_MediaDevice_ThrowsIfNotInitialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.MediaDevice.GetAudioDevices());
        }

        [Fact]
        public async void Test_Audio_ThrowsIfNotInitialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Audio.Local.Start());
        }

        [Fact]
        public void Test_Delegates_ThrowsIfNotInitialized()
        {
            Assert.Throws<DolbyIOException>(() => _sdk.InvalidTokenError = delegate { });
            Assert.Throws<DolbyIOException>(() => _sdk.SignalingChannelError = delegate { });
        }
    }
}