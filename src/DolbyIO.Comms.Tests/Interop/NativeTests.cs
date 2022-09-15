using System;
using System.Runtime.InteropServices;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    public class NativeTests
    {

        public const string LibName = "DolbyIO.Comms.Native.Tests";

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void UserInfoTest(UserInfo src, [Out] UserInfo dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ConferenceOptionsTest(ConferenceOptions src, [Out] ConferenceOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ConferenceTest(Conference src, [Out] Conference dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void JoinOptionsTest(JoinOptions src, [Out] JoinOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ListenOptionsTest(ListenOptions src, [Out] ListenOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ParticipantTest([Out] Participant dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void AudioDeviceTest(out AudioDevice dest);
    }
}