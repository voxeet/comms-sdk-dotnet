using DolbyIO.Comms;
namespace DolbyIO.Comms.Tests
{
    public class MediaDeviceTests
    {
        [Fact]
        public void Test_AudioDevice_ShouldMashall()
        {
            AudioDevice dest;
            NativeTests.AudioDeviceTest(out dest);

            var uid = new byte[] { Convert.ToByte('U'), Convert.ToByte('I'), Convert.ToByte('D') };
            Assert.Equal(uid, dest.Uid);
            Assert.Equal("dummy device", dest.Name);
            Assert.Equal(DeviceDirection.Output, dest.Direction);
        }
    }
}