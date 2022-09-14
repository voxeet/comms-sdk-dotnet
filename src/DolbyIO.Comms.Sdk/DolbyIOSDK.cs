using System;
using System.Threading.Tasks;
using DolbyIO.Comms.Services;

namespace DolbyIO.Comms 
{
    /// <summary>
    /// The DolbyIOException class is responsible for wrapping the underlying C++ SDK exceptions.
    /// </summary>
    public class DolbyIOException : Exception
    {
        /// <summary>
        /// The DolbyIOException that wraps the underlying C++ SDK exception.
        /// </summary>
        /// <param name="msg">A message describing the error.</param>
        public DolbyIOException(string msg)
            : base(msg)
        {
        }
    }

    /// <summary>
    /// The DolbyIOSDK class is the main object that allows initializing the
    /// .NET SDK and accessing the underlying services.
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
        /// Raised when an error occurs during a Session Initiation Protocol (SIP) negotiation
        /// of the local participant's peer connection.
        /// </summary>
        /// <value>The event handler.</value>
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
        /// Raised when the access token is invalid or has expired.
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
        /// <returns>The Session class.</returns>
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
        /// <returns>The Conference class.</returns>
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
        /// <returns>The MediaDevice class.</returns>
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
        /// <returns>The Audio class.</returns>
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
                throw new DolbyIOException("Already initialized, call Dispose first.");
            }
            
            await Task.Run(() =>
            {
                Native.CheckException(Native.Init(accessToken, cb));
                _initialized = true;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the logging level.
        /// </summary>
        /// <param name="logLevel">The required logging level.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
        public async Task SetLogLevel(LogLevel logLevel)
        {
            await Task.Run(() =>  Native.CheckException(Native.SetLogLevel(logLevel))).ConfigureAwait(false);
        }

        ~DolbyIOSDK()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases the unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources.
        /// </summary>
        /// <param name="disposing">A boolean that indicates whether the method call comes from the Dispose method (true) or from a finalizer (false).</param>
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
