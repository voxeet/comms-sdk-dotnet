using System;
using System.Threading.Tasks;
using DolbyIO.Comms.Services;

namespace DolbyIO.Comms 
{
    /// <summary>
    /// Main entry point that allows the application to interact with the Dolby.io services.
    /// </summary>
    public sealed class DolbyIOSDK : IDisposable
    {
        private SignalingChannelErrorEventHandler _signalingChannelError;

        /// <summary>
        /// Raised when an error occurs during a Session Initiation Protocol (SIP) negotiation
        /// of the local participant's peer connection.
        /// </summary>
        /// <value>The <see cref="SignalingChannelErrorEventHandler"/> event handler raised when an error occurs.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public SignalingChannelErrorEventHandler SignalingChannelError
        {
            set 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                Native.SetOnSignalingChannelExceptionHandler(value);
                _signalingChannelError = value;
            }
        }

        private InvalidTokenErrorEventHandler _invalidTokenError;

        /// <summary>
        /// Raised when the access token is invalid or has expired.
        /// </summary>
        /// <value>The <see cref="InvalidTokenErrorEventHandler"/> event handler raised when the access token is invalid or has expired.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public InvalidTokenErrorEventHandler InvalidTokenError
        {
            set
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }
                
                Native.SetOnInvalidTokenExceptionHandler(value);
                _invalidTokenError = value;
            }
        }

        private SessionService _session = new SessionService();

        /// <summary>
        /// Gets the session service.
        /// </summary>
        /// <value>The service that allows manipulating sessions.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public SessionService Session 
        {
            get 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                return _session; 
            } 
        }
        
        private ConferenceService _conference = new ConferenceService();

        /// <summary>
        /// Gets the conference service.
        /// </summary>
        /// <value>The service that allows manipulating conferences.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public ConferenceService Conference 
        {
            get 
            { 
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                return _conference; 
            } 
        }

        private MediaDeviceService _mediaDevice = new MediaDeviceService();

        /// <summary>
        /// Gets the media device service.
        /// </summary>
        /// <value>The service that allows manipulating media devices.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public MediaDeviceService MediaDevice
        {
            get
            {
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                return _mediaDevice;
            }
        }

        private AudioService _audio = new AudioService();

        /// <summary>
        /// Gets the audio service.
        /// </summary>
        /// <value>The service that allows accessing to the remote and local audio services.</value>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public AudioService Audio
        {
            get 
            {
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                return _audio;
            }
        }

        private VideoService _video = new VideoService();

        public VideoService Video
        {
            get 
            {
                if (!_initialized)
                {
                    throw new DolbyIOException($"{nameof(DolbyIOSDK)} is not initialized!");
                }

                return _video;
            }
        }

        private volatile bool _initialized = false;

        /// <summary>
        /// Gets if the SDK is initialized. 
        /// </summary>
        /// <value><c>true</c> if the SDK is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized { get => _initialized; }

        /// <summary>
        /// Initializes the SDK with an access token that is provided by the customer's backend.
        /// </summary>
        /// <param name="accessToken">The access token provided by the customer's backend.</param>
        /// <param name="cb">The refresh token callback.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="DolbyIOException">Is thrown when <see cref="InitAsync(string, RefreshTokenCallBack)"/> has not yet been called.</exception>
        public async Task InitAsync(string accessToken, RefreshTokenCallBack cb)
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
        /// Sets the logging level for the SDK.
        /// </summary>
        /// <param name="logLevel">The new logging level value.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetLogLevelAsync(LogLevel logLevel)
        {
            await Task.Run(() => Native.CheckException(Native.SetLogLevel(logLevel))).ConfigureAwait(false);
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
        void Dispose(bool disposing)
        {
            if (_initialized)
            {
                Native.CheckException(Native.Release());
                _initialized = false;
            }
        }
    }
}
