using System;
using System.Threading.Tasks;
using DolbyIO.Comms.Services;

namespace DolbyIO.Comms 
{
    /// <summary>
    /// The DolbyIOSDK class is a starting point that allows initializing the
    /// .NET SDK and accessing the underlying services.
    /// </summary>
    public sealed class DolbyIOSDK : IDisposable
    {   
        private volatile bool _initialized = false;

        private SessionService _session = new SessionService();
        private ConferenceService _conference = new ConferenceService();
        private MediaDeviceService _mediaDevice = new MediaDeviceService();
        private AudioService _audio = new AudioService();

        private SignalingChannelErrorEventHandler _signalingChannelError;

        /// <summary>
        /// The signaling channel error event. Raised when an error occurs during a SIP negotiation
        /// on the local peer connection.
        /// </summary>
        public SignalingChannelErrorEventHandler SignalingChannelError
        {
            set 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }

                Native.SetOnSignalingChannelExceptionHandler(value);
                _signalingChannelError = value;
            }
        }

        private InvalidTokenErrorEventHandler _invalidTokenError;

        /// <summary>
        /// The invalid token error event. Raised when the access token is invalid or expired.
        /// </summary>
        public InvalidTokenErrorEventHandler InvalidTokenError
        {
            set
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                Native.SetOnInvalidTokenExceptionHandler(value);
                _invalidTokenError = value;
            }
        }

        /// <summary>
        /// The Session service accessor.
        /// </summary>
        public SessionService Session 
        {
            get 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                return _session; 
            } 
        }
        
        /// <summary>
        /// The Conference service accessor.
        /// </summary>
        public ConferenceService Conference 
        {
            get 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                return _conference; 
            } 
        }

        /// <summary>
        /// The MediaDevice service accessor.
        /// </summary>
        public MediaDeviceService MediaDevice
        {
            get
            {
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                return _mediaDevice;
            }
        }

        /// <summary>
        /// The Audio service accessor.
        /// </summary>
        public AudioService Audio
        {
            get 
            {
                if (!_initialized)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                return _audio;
            }
        }

        /// <summary>
        /// Initializes the SDK with an access token that is provided by the customer's backend.
        /// </summary>
        /// <param name="accessToken">The access token provided by the customer's backend.</param>
        /// <param name="cb">The refresh token callback.</param>
        public async Task InitAsync(string accessToken, RefreshTokenCallBack cb)
        {
            if (_initialized)
            {
                throw new DolbyIOException("DolbyIOSDK is already initialized, call Dispose first.");
            }
            
            await Task.Run(() =>
            {
                Native.CheckException(Native.Init(accessToken, cb));
                _initialized = true;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows to set the logging level of the library.
        /// </summary>
        /// <param name="level">The required logging level.</param>
        public async Task SetLogLevelAsync(LogLevel level)
        {
            await Task.Run(() =>  Native.CheckException(Native.SetLogLevel(level))).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (_initialized)
            {
                Native.CheckException(Native.Release());
                _initialized = false;
            }
        }

        ~DolbyIOSDK()
        {
            Dispose(false);
        }
    }
}
