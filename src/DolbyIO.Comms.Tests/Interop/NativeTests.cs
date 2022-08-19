using System;
using System.Runtime.InteropServices;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    public class NativeTests
    {

        public const string LibName = "DolbyIO.Comms.Native";

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void UserInfoTest(UserInfo src, out UserInfo dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ConferenceOptionsTest(ConferenceOptions src, out ConferenceOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ConferenceInfosTest(ConferenceInfos src, out ConferenceInfos dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void JoinOptionsTest(JoinOptions src, out JoinOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ListenOptionsTest(ListenOptions src, out ListenOptions dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void ParticipantTest(out Participant dest);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern void AudioDeviceTest(out AudioDevice dest);
    }
}