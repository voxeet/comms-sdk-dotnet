using DolbyIO.Comms;
namespace DolbyIO.Comms.Tests
{   
    public class DolbyIOSDKTests
    {
        private DolbyIOSDK _sdk = new DolbyIOSDK();

        [Fact]
        public async void Test_Session_ThrowsIfNotInitialized()
        {
            UserInfo user = new UserInfo();
            user.Name = "Anonymous";

            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Session.OpenAsync(user));
        }

        [Fact]
        public async void Test_Conference_ThrowsIfNotInitialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Conference.LeaveAsync());
        }

        [Fact]
        public async void Test_MediaDevice_ThrowsIfNotInitialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.MediaDevice.GetAudioDevicesAsync());
        }

        [Fact]
        public async void Test_Audio_ThrowsIfNotInitialized()
        {
            await Assert.ThrowsAsync<DolbyIOException>(async () => await _sdk.Audio.Local.StartAsync());
        }

        [Fact]
        public void Test_Delegates_ThrowsIfNotInitialized()
        {
            Assert.Throws<DolbyIOException>(() => _sdk.InvalidTokenError += delegate { });
            Assert.Throws<DolbyIOException>(() => _sdk.SignalingChannelError += delegate { });
        }
    }
}