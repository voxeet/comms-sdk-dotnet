using System;
using DolbyIO.Comms;

public class Call
{
    private DolbyIOSDK _sdk = new DolbyIOSDK();

    public async Task OpenAndJoin()
    {
        try
        {
            await _sdk.InitAsync("My Access Token", () => {
                return "";
            });

            // Registering event handlers
            _sdk.Conference.StatusUpdated = OnConferenceStatus;

            // Or inline
            _sdk.Conference.ParticipantAdded = new ParticipantAddedEventHandler
            (
                (Participant participant) =>
                {

                }
            );

            UserInfo user = new UserInfo();
            user.Name = "My Name";

            user = await _sdk.Session.OpenAsync(user);

            ConferenceOptions options = new ConferenceOptions();
            options.Alias = "Conference alias";

            JoinOptions joinOpts = new JoinOptions();

            Conference conference = await _sdk.Conference.CreateAsync(options);
            conference = await _sdk.Conference.JoinAsync(conference, joinOpts);
        }
        catch (DolbyIOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task LeaveAndClose()
    {
        try
        {
            await _sdk.Conference.LeaveAsync();
            await _sdk.Session.CloseAsync();

            _sdk.Dispose();
        }
        catch (DolbyIOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    void OnConferenceStatus(ConferenceStatus status, string conferenceId)
    {

    }
}

public class Program
{
    public static async Task Main()
    {
        Call call = new Call();
        await call.OpenAndJoin();

        Console.ReadKey();

        await call.LeaveAndClose();
    }
}