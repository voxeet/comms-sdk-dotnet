# Dolby.io Communications .NET SDK

The Dolby.io Communications .NET SDK allows you to create high-quality video conferencing applications. The .NET SDK communicates with the Dolby.io backend and provides conferencing functionalities, such as opening and closing sessions, joining and leaving conferences, sending and receiving messages, and injecting and receiving WebRTC media streams. An additional advantage of the SDK is the [spatial audio](https://docs.dolby.io/communications-apis/docs/guides-spatial-audio) support. This functionality is especially useful for game engines and virtual spaces. The .NET SDK lets you place conference participants spatially in a 3D-rendered audio scene and hear the participants' audio rendered at the given locations. Additionally, the SDK offers spatial audio styles that allow you to either locally set remote participants' positions or create a spatial scene shared by all participants, where the relative positions among participants are calculated by the Dolby.io server.

# Get Started

This guide explains how to create a basic audio-only conference application using the Dolby.io Communications .NET SDK. The starter project that you can create by following this procedure provides the foundation upon which you can add additional features as you build out your own solutions for events, collaboration, or live streaming.

You can find the complete code for the application in the [Summary](#summary) section. The application lets you create, join, and leave the conference. 

## Prerequisites

Make sure that you have:

- A Dolby.io account. If you do not have an account, you can [sign up](https://dolby.io/signup) for a free account.
- The [client access token](doc:overview-developer-tools#test-client-access-token) copied from the [Dolby.io dashboard](https://dashboard.dolby.io/). To create the token, log in to the Dolby.io dashboard, create an application, and navigate to the API keys section.

Additionally, if you plan to build the SDK from sources, not the NuGet packet manager, make sure that you have:
- The [Dolby.io Communications C++ SDK 2.0](https://github.com/DolbyIO/comms-sdk-cpp/releases) for your platform
- [Dotnet](https://dotnet.microsoft.com/en-us/download) 6.x
- C++ compiler compatible with C++ 17
- CMake 3.23
- A macOS or Windows machine

## Building the application

### 0. Install the SDK

To install the SDK, you can either use the NuGet packet manager or build the SDK from sources.

#### NuGet

[TODO]


#### Sources

The .NET SDK uses CMake for the build chain and generating projects. To build the application from sources, use the following commands:

```console
mkdir build && cd build
cmake .. -DDOLBYIO_LIBRARY_PATH=/path/to/c++sdk -DCMAKE_BUILD_TYPE=Debug
cmake --build .
```

You can define `DOLBYIO_LIBRARY_PATH` as an environment variable.

After generating your project, you can find it in the `build/dotnet` folder.

### 1. Initialize the SDK

Initialize the SDK using the secure authentication method that uses a token in the application. For more information, see the [Initializing the SDK document](doc:initializing-javascript). This sample application requires the access token to be provided as a command line parameter when launching the executable. For the purpose of this application, use a [client access token](doc:overview-developer-tools#client-access-token) generated from the Dolby.io dashboard.

Create a file where you want to store code for the sample application. Open the file in your favorite text editor and add there the following code to initialize the SDK using the [DolbyIOSDK.Init](xref:DolbyIO.Comms.Init) method:

```cs
using DolbyIO.Comms;

DolbyIOSDK _sdk = new DolbyIOSDK();

try
{
    await _sdk.Init("Token", () => 
    {
        // Refresh Callback
    });
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

**NOTE:** The SDK is fully asynchronous, so all methods can throw the [DolbyIOException](xref:DolbyIO.Comms.DolbyIOException).

### 2. Register event handlers

After initializing the SDK it is time to register your event handlers. Decide to which events you want to add event handlers and add the handlers as in the following example:

```cs
// Registering event handlers
_sdk.Conference.StatusUpdated = OnConferenceStatus;

// or inline
_sdk.Conference.ParticipantAdded = new ParticipantAddedEventHandler
(
    (Participant participant) => 
    {

    }
);
```

### 3. Open a session

A session is a connection between the client application and the Dolby.io backend. When opening a session, you should provide a name. Commonly, this is the name of the participant who established the session. The session can remain active for the whole lifecycle of your application. 

To open a new session, use the [Session.Open](xref:DolbyIO.Comms.Services.Session#DolbyIO_Comms_Services_Session_Open) method as in the following example:

```cs
try
{
    UserInfo user = new UserInfo();
    user.Name = "My Name";

    user = await _sdk.Session.Open(user);
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

###  4. Create and join a conference

A conference is a multi-person call where participants exchange audio with one another. To distinguish between multiple conferences, you should assign a conference alias or name. When multiple participants join a conference of the same name using a token of the same Dolby.io application, they will be able to meet in a call.

To create and join a conference, use the [Conference.Create](xref:DolbyIO.Comms.Services.Conference#DolbyIO_Comms_Services_Conference_Create_DolbyIO_Comms_ConferenceOptions_) and [Conference.Join](xref:DolbyIO.Comms.Services.Conference#DolbyIO_Comms_Services_Conference_Join_DolbyIO_Comms_ConferenceInfos_DolbyIO_Comms_JoinOptions_) methods as in the following example:

```cs
try
{
    ConferenceOptions options = new ConferenceOptions();
    options.Alias = "Conference alias";

    JoinOptions joinOpts = new JoinOptions();

    ConferenceInfos createInfos = await _sdk.Conference.Create(options);
    ConferenceInfos joinInfos = await _sdk.Conference.Join(createInfos, joinOpts);
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

If the conference already exists, the SDK returns [ConferenceInfos](xref:DolbyIO.Comms.Native.Struct#DolbyIO_Comms_Native_Struct_ConferenceInfos).

### 5. Leave the conference

To leave the conference, use the [Conference.Leave](xref:DolbyIO.Comms.Services.Conference#DolbyIO_Comms_Services_Conference_Leave) method, as in the following example:

```cs
try
{
    await _sdk.Session.Leave();
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

### 6. Close the session and dispose of the SDK

After leaving the conference, close the session and dispose of the SDK using the [Session.Close](xref:DolbyIO.Comms.Services.Session#DolbyIO_Comms_Services_Session_Close) and [DolbyIOSDK.Dispose](xref:DolbyIO.Comms.Dispose) methods.

```cs
try
{
    await _sdk.Session.Close();
    _sdk.Dispose();
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

This step causes releasing the underneath unmanaged native resources.

## Summary

All the steps combined create the following code snippet:

```cs
using System;
using DolbyIO.Comms;

public class Call
{
    private DolbyIOSDK _sdk = new DolbyIOSDK();

    public async Task OpenAndJoin()
    {
        try
        {
            await _sdk.Init("Access Token");

            // Registering event handlers
            _sdk.Conference.StatusUpdated = OnConferenceStatus;

            // or inline
            _sdk.Conference.ParticipantAdded = new ParticipantAddedEventHandler
            (
                (Participant participant) =>
                {

                }
            );

            UserInfo user = new UserInfo();
            user.Name = "My Name";

            user = await _sdk.Session.Open(user);

            ConferenceOptions options = new ConferenceOptions();
            options.Alias = "Conference alias";

            JoinOptions joinOpts = new JoinOptions();

            ConferenceInfos createInfos = await _sdk.Conference.Create(options);
            ConferenceInfos joinInfos = await _sdk.Conference.Join(createInfos, joinOpts);
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
            await _sdk.Conference.Leave();
            await _sdk.Session.Close();

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
```