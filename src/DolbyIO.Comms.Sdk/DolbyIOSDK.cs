using System;
using System.Threading.Tasks;
using DolbyIO.Comms.Services;

namespace DolbyIO.Comms 
{
    public class DolbyIOException : Exception
    {
        public DolbyIOException(string msg)
            : base(msg)
        {
        }
    }

    /// <summary>
    /// The DolbyIOSDK Class is a starting point that allows initializing the
    /// C# SDK and accessing the underlying services.
    /// </summary>
    public class DolbyIOSDK : IDisposable
    {   
        private volatile bool _initialized = false;

        private Session _session = new Session();
        private Conference _conference = new Conference();
        private MediaDevice _mediaDevice = new MediaDevice();
        private Audio _audio = new Audio();

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

        ~DolbyIOSDK()
        {
            Dispose(false);
        }

        /// <summary>
        /// The Session service accessor.
        /// </summary>
        public Session Session 
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
        public Conference Conference 
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
        public MediaDevice MediaDevice
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
        public Audio Audio
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
        /// Indicates that the SDK is initialized. 
        /// </summary>
        public bool IsInitialized { get => _initialized; }

        /// <summary>
        /// Initializes the SDK with an access token that is provided by the customer's backend.
        /// </summary>
        /// <param name="accessToken">The access token provided by the customer's backend.</param>
        /// <param name="cb">The refresh token callback.</param>
        public async Task Init(string accessToken, RefreshTokenCallBack cb)
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
        /// <param name="logLevel">The required logging level.</param>
        public async Task SetLogLevel(LogLevel logLevel)
        {
            await Task.Run(() =>  Native.CheckException(Native.SetLogLevel(logLevel))).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_initialized)
            {
                Native.CheckException(Native.Release());
                _initialized = false;
            }
        }
    }
}
