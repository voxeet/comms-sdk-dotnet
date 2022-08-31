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
            await _fixture.Sdk.Audio.Local.Start();
            await _fixture.Sdk.Audio.Local.Stop();
            await _fixture.Sdk.Audio.Local.Mute(true);
        }

        [Fact]
        public async void Test_Audio_CanCallRemoteMethod()
        {
            await _fixture.Sdk.Audio.Remote.Start("participantId");
            await _fixture.Sdk.Audio.Remote.Stop("participantId");
            await _fixture.Sdk.Audio.Remote.Mute(true, "participantId");
        }
    }
}