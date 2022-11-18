using System.Threading.Tasks;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The local video service is used to control local participant's video capture
    /// and sending into conference.
    /// </summary>
    public sealed class LocalVideoService
    {
        public async Task StartAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StartVideo()));
        }

        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopVideo())).ConfigureAwait(false);
        }
    }
}