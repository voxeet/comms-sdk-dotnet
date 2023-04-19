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
        public void Test_AudioDevice_ShouldMarshall()
        {
            AudioDevice dest;
            NativeTests.AudioDeviceTest(out dest);

            //Assert.Equal("dummy device", dest.Name);
            //Assert.Equal(DeviceDirection.Output, dest.Direction);
        }

        [Fact]
        public void Test_VideoDevice_ShouldMarshall()
        {
            VideoDevice dest;
            NativeTests.VideoDeviceTest(out dest);

            Assert.Equal("UID", dest.Uid);
            Assert.Equal("dummy device", dest.Name);
        }

        [Fact]
        public async void Test_MediaDevice_CanCallAudioMethods()
        {
            AudioDevice device = new AudioDevice();

            await _fixture.Sdk.MediaDevice.GetAudioDevicesAsync();
            await _fixture.Sdk.MediaDevice.GetCurrentAudioInputDeviceAsync();
            await _fixture.Sdk.MediaDevice.GetCurrentAudioOutputDeviceAsync();
            await _fixture.Sdk.MediaDevice.SetPreferredAudioInputDeviceAsync(device);
            await _fixture.Sdk.MediaDevice.SetPreferredAudioOutputDeviceAsync(device);
        }

        [Fact]
        public async void Test_MediaDevice_CanCallVideoMethods()
        {
            await _fixture.Sdk.MediaDevice.GetVideoDevicesAsync();
            await _fixture.Sdk.MediaDevice.GetCurrentVideoDeviceAsync();
        }

        [Fact]
        public async void Test_MediaDevice_CanCallScreenShareMethods()
        {
            await _fixture.Sdk.MediaDevice.GetScreenShareSourcesAsync();
        }
    }
}