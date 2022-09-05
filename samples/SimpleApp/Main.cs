using System;
using DolbyIO.Comms;

public class Call
{
    private DolbyIOSDK _sdk = new DolbyIOSDK();

    public async Task Join()
    {
        try
        {
            await _sdk.Init("My Access Token", () => {
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
            user.Name = "Dummy";

            user = await _sdk.Session.Open(user);

            ConferenceOptions options = new ConferenceOptions();
            options.Alias = "Conference alias";
            
            JoinOptions joinOpts = new JoinOptions();
            
            ConferenceInfos createInfos = await _sdk.Conference.Create(options);
            ConferenceInfos joinInfos = await _sdk.Conference.Join(createInfos, joinOpts);

            await _sdk.Conference.Leave();
            await _sdk.Session.Close();            
        }
        catch (DolbyIOException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _sdk.Dispose();
        }

        void OnConferenceStatus(ConferenceStatus status, string conferenceId)
        {

        }
    }
}

public class Program
{
    public static async Task Main()
    {
        Call call = new Call();
        await call.Join();
    }
}