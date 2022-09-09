using System;
using System.Threading.Tasks;
using DolbyIO.Comms.Services;

namespace DolbyIO.Comms 
{
    public class DolbyIOException : Exception
    {
        public DolbyIOException(String msg)
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
        private volatile bool _initialised = false;

        private Session _session = new Session();
        private Conference _conference = new Conference();
        private MediaDevice _mediaDevice = new MediaDevice();
        private Audio _audio = new Audio();

        private SignalingChannelErrorEventHandler _signalingChannelError;

        /// <summary>
        /// The signaling channel error event. Raised when an error occurs during a SIP negociation
        /// on the local peer connection.
        /// </summary>
        public SignalingChannelErrorEventHandler SignalingChannelError
        {
            set 
            { 
                if (!_initialised)
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
                if (!_initialised)
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
                if (!_initialised)
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
                if (!_initialised)
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
                if (!_initialised)
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
                if (!_initialised)
                {
                    throw new DolbyIOException("DolbyIOSDK is not initialized!");
                }
                return _audio;
            }
        }

        /// <summary>
        /// Indicates that the SDK is initialized. 
        /// </summary>
        public bool IsInitialized { get => _initialised; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey">Application secret key.</param>
        /// <param name="cb">The refresh token callback.</param>
        public async Task Init(String appKey, RefreshTokenCallBack cb)
        {
            if (_initialised)
            {
                throw new DolbyIOException("Already initialized, call Dispose first.");
            }
            
            await Task.Run(() =>
            {
                Native.CheckException(Native.Init(appKey, cb));
                _initialised = true;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows to set the logging level of the library.
        /// </summary>
        /// <param name="level">The required logging level.</param>
        /// <returns></returns>
        public async Task SetLogLevel(LogLevel level)
        {
            await Task.Run(() =>  Native.CheckException(Native.SetLogLevel(level))).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_initialised)
            {
                Native.CheckException(Native.Release());
                _initialised = false;
            }
        }
    }
}
