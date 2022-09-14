using DolbyIO.Comms;
namespace DolbyIO.Comms.Tests
{
    [Collection("Sdk")]
    public class MediaDeviceTests
    {
        private SdkFixture _fixture;

        public MediaDeviceTests(SdkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test_AudioDevice_ShouldMashall()
        {
            AudioDevice dest;
            NativeTests.AudioDeviceTest(out dest);

            var uid = new byte[Constants.DeviceUidSize];
            uid[0] = Convert.ToByte('U');
            uid[1] = Convert.ToByte('I');
            uid[2] = Convert.ToByte('D');

            Assert.Equal(uid, dest.Uid);
            Assert.Equal("dummy device", dest.Name);
            Assert.Equal(DeviceDirection.Output, dest.Direction);
        }

        [Fact]
        public async void Test_MediaDevice_CanCallMethods()
        {
            AudioDevice device = new AudioDevice();

            await _fixture.Sdk.MediaDevice.GetAudioDevices();
            await _fixture.Sdk.MediaDevice.GetCurrentAudioInputDevice();
            await _fixture.Sdk.MediaDevice.GetCurrentAudioOutputDevice();
            await _fixture.Sdk.MediaDevice.SetPreferredAudioInputDevice(device);
            await _fixture.Sdk.MediaDevice.SetPreferredAudioOutputDevice(device);
        }
    }
}