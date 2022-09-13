using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    public class MediaDeviceTests
    {
        [Fact]
        public void Test_AudioDevice_ShouldMarshall()
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
    }
}