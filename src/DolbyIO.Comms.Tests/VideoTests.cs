using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    [Collection("Sdk")]
    public class VideoTests
    {
        private SdkFixture _fixture;

        public VideoTests(SdkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Test_Video_CanCallScreenShareMethods()
        {
            ScreenShareSource source = new ScreenShareSource();

            await _fixture.Sdk.Video.Local.StartScreenShareAsync(source, null);
            await _fixture.Sdk.Video.Local.StopScreenShareAsync();
        }
    }
}