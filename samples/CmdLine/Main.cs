using System.CommandLine;
using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Serilog;

using DolbyIO.Comms;

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

        var aliasOption = new Option<string>("--alias", "Conference alias") { IsRequired = true };
        aliasOption.AddAlias("-a");

        var tokenOption = new Option<string>("--token", description: "App Token") { IsRequired = true };
        tokenOption.AddAlias("-t");

        var nameOption = new Option<string>("--user", description: "User Name", getDefaultValue: () => "Anonymous");
        nameOption.AddAlias("-u");

        var rootCommand = new RootCommand("DolbyIO SDK Command Line");

        var joinCommand = new Command("join", "Joins a conference");
        joinCommand.AddOption(aliasOption);
        joinCommand.AddOption(tokenOption);
        joinCommand.AddOption(nameOption);

        joinCommand.SetHandler(async (alias, appKey, name) => 
        {
            await Init(appKey, name);
            await JoinConference(alias);
        }, aliasOption, tokenOption, nameOption);

        var demoCommand = new Command("demo", "Experience Demo conference");
        demoCommand.AddOption(tokenOption);
        demoCommand.AddOption(nameOption);

        demoCommand.SetHandler(async (appKey, name) => 
        {
            await Init(appKey, name);
            await DemoConference();
        }, tokenOption, nameOption);

        var listenCommand = new Command("listen", "Listen to a conference");
        listenCommand.AddOption(aliasOption);
        listenCommand.AddOption(tokenOption);
        listenCommand.AddOption(nameOption);

        listenCommand.SetHandler(async (alias, appKey, name) =>
        {
            await Init(appKey, name);
            await ListenConference(alias);
        }, aliasOption, tokenOption, nameOption);

        var devicesCommand = new Command("devices", "List available devices");
        devicesCommand.AddOption(tokenOption);
        devicesCommand.AddOption(nameOption);

        devicesCommand.SetHandler(async (appKey, name) => 
        {
            await Init(appKey, name);
            await ListDevices();
        }, tokenOption, nameOption);

        rootCommand.Add(joinCommand);
        rootCommand.Add(demoCommand);
        rootCommand.Add(listenCommand);
        rootCommand.Add(devicesCommand);

        Console.TreatControlCAsInput = false;
        Console.CancelKeyPress += OnCancelKeyPressed;
        
        //Console.ReadKey();

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

    private static void InputLoop()
    {
        while (_keepRunning)
        {
            //var keyInfo = Console.ReadKey();
        }
    }

    private static async Task Init(string appKey, string name)
    {
        try
        {
            await _sdk.Init(appKey);
            await _sdk.SetLogLevel(LogLevel.Off);

            _sdk.SignalingChannelError = OnSignalingChannelError;
            _sdk.InvalidTokenError = OnInvalidTokenError;

            _sdk.Conference.ParticipantAdded = OnParticipantAdded;
            _sdk.Conference.ParticipantUpdated = OnParticipantUpdated;
            _sdk.Conference.StatusUpdated = OnConferenceStatusUpdated;
            _sdk.Conference.ActiveSpeakerChange = OnActiveSpeakerChange;

            _sdk.Conference.DvcError = OnDvcError;
            _sdk.Conference.PeerConnectionError = OnPeerConnectionError;

            _sdk.MediaDevice.Added = new DeviceAddedEventHandler((AudioDevice device) => {
                Log.Debug($"OnDeviceAdded: {device.Name}");
            });

            UserInfo user = new UserInfo();
            user.Name = name;

            user = await _sdk.Session.Open(user);

            Log.Debug($"Session opened: {user.Id}");
        }
        catch(DolbyIOException e)
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
            
            await _sdk.Conference.StartAudio();
            await _sdk.Conference.SetSpatialEnvironment
            (
                new Vector3(1.0f, 1.0f, 1.0f),  // Scale
                new Vector3(0.0f, 0.0f, -1.0f), // Forward
                new Vector3(0.0f, 1.0f, 0.0f),  // Up
                new Vector3(1.0f, 0.0f, 0.0f)   // Right
            );

            InputLoop();
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

            InputLoop();
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
            await _sdk.Conference.StartAudio();

            await _sdk.Conference.SetSpatialPosition(_sdk.Session.User.Id, new Vector3(0.0f, 0.0f, 0.0f));

            InputLoop();
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

            InputLoop();
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

    private static void OnConferenceStatusUpdated(ConferenceStatus status, String conferenceId) 
    {
        Log.Debug($"OnConferenceStatusUpdated: {conferenceId} status: {status}");
    }

    private static async void OnParticipantAdded(Participant participant)
    {
        Log.Debug($"OnParticipantAdded: {participant.Id} {participant.Info.Name}");
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
        Log.Debug($"OnParticipantUpdated: {participant.Id} {participant.Info.Name}");
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

    private static void OnActiveSpeakerChange(string conferenceId, int count, string[] activeSpeakers) {
        Log.Debug($"OnActiveSpeakerChange: {conferenceId}");
        foreach(string s in activeSpeakers) {
            Log.Debug($"-- ActiveSpeaker : {s}");
        }
    }
     
}
