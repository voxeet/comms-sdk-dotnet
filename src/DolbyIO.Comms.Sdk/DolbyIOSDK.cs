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
        public DolbyIOException(String msg)
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
        private volatile bool _initialised = false;

        private Session _session = new Session();
        private Conference _conference = new Conference();
        private MediaDevice _mediaDevice = new MediaDevice();
        private Audio _audio = new Audio();

        private SignalingChannelErrorEventHandler _signalingChannelError;

        /// <summary>
        /// Raised when an error occurs during a Session Initiation Protocol (SIP) negotiation
        /// of the local participant's peer connection.
        /// </summary>
        /// <returns>The event handler.</returns>
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
        /// Raised when the access token is invalid or has expired.
        /// </summary>
        /// <returns>The event handler.</returns>
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
        /// <returns>The Session class.</returns>
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
        /// <returns>The Conference class.</returns>
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
        /// <returns>The MediaDevice class.</returns>
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
        /// <returns>The Audio class.</returns>
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
        /// <returns>A boolean indicating whether the SDK is initialized.</returns>
        public bool IsInitialized { get => _initialised; }

        /// <summary>
        /// Initializes the SDK using the application key.
        /// </summary>
        /// <param name="appKey">The application secret key.</param>
        /// <param name="cb">The refresh token callback.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
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
        /// Sets the logging level.
        /// </summary>
        /// <param name="level">The required logging level.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
        public async Task SetLogLevel(LogLevel level)
        {
            await Task.Run(() =>  Native.CheckException(Native.SetLogLevel(level))).ConfigureAwait(false);
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
            if (_initialised)
            {
                Native.CheckException(Native.Release());
                _initialised = false;
            }
        }
    }
}
