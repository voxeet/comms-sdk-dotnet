using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The Conference class contains information about a conference. This structure provides
    /// conference details that are required to join a specific conference. The SDK
    /// returns Conference to describe the created or joined conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Conference
    {
        /// <summary>
        /// The unique conference identifier.
        /// </summary>
        /// <returns>The conference identifier.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Id;

        /// <summary>
        /// The conference alias. The alias must be a logical and unique string that consists
        /// of up to 250 characters, such as letters, digits, and symbols other than #.
        /// The alias is case insensitive, which means that using "foobar" and "FOObar"
        /// aliases refers to the same conference. The alias is optional in the case
        /// of using the conference ID.
        /// </summary>
        /// <returns>The conference alias.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;

        /// <summary>
        /// A boolean that indicates whether the conference represented by the object has been just created.
        /// </summary>
        /// <returns>If true, the conference is new.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsNew;

        /// <summary>
        /// The current status of the conference.
        /// </summary>
        /// <returns>The conference status.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ConferenceStatus Status;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxPermissions)]
        private ConferenceAccessPermissions[] _permissions = new ConferenceAccessPermissions[Constants.MaxPermissions];

        private int _permissionsCount = 0;

        /// <summary>
        /// The spatial audio style used in the joined conference.
        /// </summary>
        /// <returns>The spatial audio style.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly SpatialAudioStyle SpatialAudioStyle = 0;

        /// <summary>
        /// Permissions that allow a conference participant to perform limited
        /// actions during a protected conference.
        /// </summary>
        /// <returns>The conference permissions.</returns>
        public List<ConferenceAccessPermissions> Permissions
        {
            get
            {
                return _permissions.Take(_permissionsCount).ToList();
            }

            set
            {
                if (value.Count > Constants.MaxPermissions) {
                    throw new DolbyIOException("Too many permissions");
                }
                Array.Copy(value.ToArray(), _permissions, value.Count);
                _permissionsCount = value.Count;
            }
        }
    }
}