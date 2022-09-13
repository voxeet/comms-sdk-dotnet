using System;
using System.CommandLine;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Serilog;

using DolbyIO.Comms;
using Command = System.CommandLine.Command;

public class CommandLine
{
    private static volatile bool _keepRunning = true;
    private static DolbyIOSDK _sdk = new DolbyIOSDK();

    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        var logLevelOption = new Option<int>("--loglevel", description: "Log Level", getDefaultValue: () => 0);
        logLevelOption.AddAlias("-l");

        var aliasOption = new Option<string>("--alias", "Conference alias") { IsRequired = true };
        aliasOption.AddAlias("-a");

        var tokenOption = new Option<string>("--token", description: "App Token") { IsRequired = true };
        tokenOption.AddAlias("-t");

        var nameOption = new Option<string>("--user", description: "User Name", getDefaultValue: () => "Anonymous");
        nameOption.AddAlias("-u");

        var rootCommand = new RootCommand("DolbyIO SDK Command Line");
        rootCommand.AddGlobalOption(logLevelOption);

        var joinCommand = new Command("join", "Joins a conference");
        joinCommand.AddOption(aliasOption);
        joinCommand.AddOption(tokenOption);
        joinCommand.AddOption(nameOption);

        joinCommand.SetHandler(async (alias, appKey, name, logLevel) => 
        {
            await Init(appKey, name, logLevel);
            await JoinConference(alias);
        }, aliasOption, tokenOption, nameOption, logLevelOption);

        var demoCommand = new Command("demo", "Experience Demo conference");
        demoCommand.AddOption(tokenOption);
        demoCommand.AddOption(nameOption);

        demoCommand.SetHandler(async (appKey, name, logLevel) => 
        {
            await Init(appKey, name, logLevel);
            await DemoConference();
        }, tokenOption, nameOption, logLevelOption);

        var listenCommand = new Command("listen", "Listen to a conference");
        listenCommand.AddOption(aliasOption);
        listenCommand.AddOption(tokenOption);
        listenCommand.AddOption(nameOption);

        listenCommand.SetHandler(async (alias, appKey, name, logLevel) =>
        {
            await Init(appKey, name, logLevel);
            await ListenConference(alias);
        }, aliasOption, tokenOption, nameOption, logLevelOption);

        var devicesCommand = new Command("devices", "List available devices");
        devicesCommand.AddOption(tokenOption);
        devicesCommand.AddOption(nameOption);

        devicesCommand.SetHandler(async (appKey, name, logLevel) => 
        {
            await Init(appKey, name, logLevel);
            await ListDevices();
        }, tokenOption, nameOption, logLevelOption);

        rootCommand.Add(joinCommand);
        rootCommand.Add(demoCommand);
        rootCommand.Add(listenCommand);
        rootCommand.Add(devicesCommand);

        Console.TreatControlCAsInput = false;
        Console.CancelKeyPress += OnCancelKeyPressed;
        
        var res = await rootCommand.InvokeAsync(args);

        _sdk.Dispose();
        
        Log.CloseAndFlush();

        return res;
    }

    private static async void OnCancelKeyPressed(object? sender, ConsoleCancelEventArgs eventArgs)
    {
        Log.Debug("Cleaning");
        eventArgs.Cancel = true;

        try
        {
            await _sdk.Conference.Leave();
            await _sdk.Session.Close();
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to leave conference.");
        }

        _keepRunning = false;
        Log.Debug("Cleaning End");
        eventArgs.Cancel = false; // Terminates process
    }

    private static async Task InputLoop()
    {
        while (_keepRunning)
        {
            await Task.CompletedTask;
        }
    }

    private static async Task Init(string appKey, string name, int logLevel)
    {
        try
        {
            await _sdk.SetLogLevel((LogLevel)logLevel);
            
            await _sdk.Init(appKey, () => 
            {
                Log.Debug("RefreshTokenCallBack called.");
                return "dummy";
            });

            _sdk.SignalingChannelError = OnSignalingChannelError;
            _sdk.InvalidTokenError = OnInvalidTokenError;

            _sdk.Conference.ParticipantAdded = OnParticipantAdded;
            _sdk.Conference.ParticipantUpdated = OnParticipantUpdated;
            _sdk.Conference.StatusUpdated = OnConferenceStatusUpdated;
            _sdk.Conference.ActiveSpeakerChange = OnActiveSpeakerChange;

            _sdk.Conference.DvcError = OnDvcError;
            _sdk.Conference.PeerConnectionError = OnPeerConnectionError;

            _sdk.MediaDevice.Added = new DeviceAddedEventHandler((AudioDevice device) => 
            {
                Log.Debug($"OnDeviceAdded: {device.Name}");
            });

            _sdk.MediaDevice.Removed = new DeviceRemovedEventHandler((byte[] uid) =>
            {
                Log.Debug($"OnDeviceRemoved: {uid}");
            });

            _sdk.MediaDevice.Changed = new DeviceChangedEventHandler((AudioDevice device, bool noDevice) =>
            {
                Log.Debug($"OnDeviceChanged: {device.Name}");
            });

            UserInfo user = new UserInfo();
            user.Name = name;

            user = await _sdk.Session.Open(user);

            Log.Debug($"Session opened: {user.Id}");
        }
        catch (DolbyIOException e)
        {   
            Log.Error(e, "Failed to open session.");
        }
    }

    private static async Task JoinConference(string alias)
    {
        try
        {
            ConferenceOptions options = new ConferenceOptions();
            options.Params.DolbyVoice = true;
            options.Params.SpatialAudioStyle = SpatialAudioStyle.Individual;
            options.Alias = alias;
            
            JoinOptions joinOpts = new JoinOptions();
            joinOpts.Connection.SpatialAudio = true;
            
            ConferenceInfos infos = await _sdk.Conference.Create(options);
            
            ConferenceInfos result = await _sdk.Conference.Join(infos, joinOpts);
            var permissions = result.Permissions;

            await _sdk.Audio.Local.Start();
            await _sdk.Conference.SetSpatialEnvironment
            (
                new Vector3(1.0f, 1.0f, 1.0f),  // Scale
                new Vector3(0.0f, 0.0f, -1.0f), // Forward
                new Vector3(0.0f, 1.0f, 0.0f),  // Up
                new Vector3(1.0f, 0.0f, 0.0f)   // Right
            );

            await InputLoop();
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to join conference.");
        }
    }

    private static async Task ListenConference(string alias)
    {
        try {
            ConferenceOptions options = new ConferenceOptions();
            options.Params.DolbyVoice = true;
            options.Params.SpatialAudioStyle = SpatialAudioStyle.Individual;
            options.Alias = alias;
            
            ConferenceInfos infos = await _sdk.Conference.Create(options);

            ListenOptions listenOptions = new ListenOptions();
            listenOptions.Connection.SpatialAudio = true;

            var result = _sdk.Conference.Listen(infos, listenOptions);

            await InputLoop();
        }
        catch (DolbyIOException e) {
            Log.Error(e, "Failed to listen to conference.");
        }
    }

    private static async Task DemoConference()
    {
        try
        {
            ConferenceInfos infos = await _sdk.Conference.Demo(true);
            await _sdk.Audio.Local.Start();

            await _sdk.Conference.SetSpatialPosition(_sdk.Session.User.Id, new Vector3(0.0f, 0.0f, 0.0f));

            await InputLoop();
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to join demo conference.");
        }
    }

    private static async Task ListDevices()
    {
        try
        {
            List<AudioDevice> devices = await _sdk.MediaDevice.GetAudioDevices();
            devices.ForEach(d => Console.WriteLine(d.Uid + " : " + d.Name));
            
            var device = await _sdk.MediaDevice.GetCurrentAudioInputDevice();
            await _sdk.MediaDevice.SetPreferredAudioInputDevice(device);

            await InputLoop();
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to list devices.");
        }
    }

    private static void OnInvalidTokenError(string reason, string description)
    {
        Log.Debug($"OnInvalidTokenException: {reason} {description}");
    }

    private static void OnSignalingChannelError(string message)
    {
        Log.Debug($"OnSignalingChannelException: {message}");
    }

    private static void OnDvcError(string reason)
    {
        Log.Debug($"OnDvcErrorException: {reason}");
    }

        private static void OnPeerConnectionError(string reason)
    {
        Log.Debug($"OnPeerConnectionFailedException: {reason}");
    }

    private static void OnConferenceStatusUpdated(ConferenceStatus status, string conferenceId) 
    {
        Log.Debug($"OnConferenceStatusUpdated: {conferenceId} status: {status}");
    }

    private static async void OnParticipantAdded(Participant participant)
    {
        Log.Debug($"OnParticipantAdded: {participant.Id} {participant.Info.Name} {participant.Status}");
        try 
        {
            var infos = await _sdk.Conference.Current();
            if (SpatialAudioStyle.None != infos.SpatialAudioStyle)
            {
                await _sdk.Conference.SetSpatialPosition(participant.Id, new Vector3(0.0f, 0.0f, 0.0f));
            }
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to set spatial position");
        }
    }

    private static async void OnParticipantUpdated(Participant participant)
    {
        Log.Debug($"OnParticipantUpdated: {participant.Id} {participant.Info.Name} {participant.Status}");
        try 
        {
            var infos = await _sdk.Conference.Current();
            if (SpatialAudioStyle.None != infos.SpatialAudioStyle)
            {
                await _sdk.Conference.SetSpatialPosition(participant.Id, new Vector3(0.0f, 0.0f, 0.0f));
            }
        }
        catch (DolbyIOException e)
        {
            Log.Error(e, "Failed to set spatial position");
        }    
    }

    private static void OnActiveSpeakerChange(string conferenceId, int count, string[]? activeSpeakers) {
        Log.Debug($"OnActiveSpeakerChange: {conferenceId} {count}");
        if (activeSpeakers != null) {
            foreach (string s in activeSpeakers) {
                Log.Debug($"-- ActiveSpeaker : {s}");
            }
        }
    }
     
}
