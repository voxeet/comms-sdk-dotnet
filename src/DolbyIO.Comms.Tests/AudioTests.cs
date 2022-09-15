using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    [Collection("Sdk")]
    public class AudioTests
    {
        private SdkFixture _fixture;

        public AudioTests(SdkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Test_Audio_CanCallLocalMethod()
        {
            await _fixture.Sdk.Audio.Local.StartAsync();
            await _fixture.Sdk.Audio.Local.StopAsync();
            await _fixture.Sdk.Audio.Local.MuteAsync(true);
        }

        [Fact]
        public async void Test_Audio_CanCallRemoteMethod()
        {
            await _fixture.Sdk.Audio.Remote.StartAsync("participantId");
            await _fixture.Sdk.Audio.Remote.StopAsync("participantId");
            await _fixture.Sdk.Audio.Remote.MuteAsync(true, "participantId");
        }
    }
}