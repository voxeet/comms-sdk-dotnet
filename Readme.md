# Dolby.io Communications .NET SDK

The Dolby.io Communications .NET SDK allows creating high-quality video conferencing applications with spatial audio. The SDK is especially useful for building game engines and virtual spaces for collaboration. It allows placing participants spatially in a 3D-rendered audio scene and hear the participants' audio rendered at their locations.

## Get Started

This guide presents an example of using the SDK to create a basic audio-only conference application. The starter project that you can create by following this procedure, provides the foundation upon which you can add additional features as you build out your own solutions for events, collaboration, or live streaming.

You can find the complete code for the application in the [Summary](#summary) section.

## Prerequisites

Make sure that you have:

- A Dolby.io account. If you do not have an account, you can [sign up](https://dolby.io/signup) for a free account.
- The [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) copied from the [Dolby.io dashboard](https://dashboard.dolby.io/). To create the token, log in to the Dolby.io dashboard, create an application, and navigate to the API keys section.

Additionally, if you plan to build the SDK from sources, not the NuGet packet manager, make sure that you have:
- The [Dolby.io Communications C++ SDK 2.0](https://github.com/DolbyIO/comms-sdk-cpp/releases) for your platform
- [.NET SDK 6](https://dotnet.microsoft.com/en-us/download)
- C++ compiler compatible with C++ 17
- CMake 3.23
- A macOS or Windows machine

## Installation

To install the SDK, you can either use the NuGet packet manager or build the SDK from sources.

### NuGet

Add the NuGet package `DolbyIO.Comms.Sdk` using dotnet CLI:

```shell
dotnet add package DolbyIO.Comms.Sdk
```

### Sources

In order to compile the Dolby.io Communications .NET SDK, you will need to use CMake for the build chain and generating projects. To build the application from sources, use the following commands:

```console
mkdir build && cd build
cmake .. -DDOLBYIO_LIBRARY_PATH=/path/to/c++sdk -DCMAKE_BUILD_TYPE=Debug
cmake --build .
```

Define the environment variable `DOLBYIO_LIBRARY_PATH` as the path to the root folder containing the Dolby.io Communications C++ SDK.

After generating your project, you can find your project in the `build/dotnet` folder.

## Sample usage

### 1. Initialize the SDK

Initialize the SDK using the secure authentication method that uses a token in the application. This sample application requires the access token to be provided as a command line parameter when launching the executable. For the purpose of this application, use a [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) generated from the Dolby.io dashboard.

Create a file where you want to store code for the sample application. Open the file in your favorite text editor and add there the following code to initialize the SDK using the [DolbyIOSDK.Init](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.DolbyIOSDK.html#DolbyIO_Comms_DolbyIOSDK_Init_System_String_DolbyIO_Comms_RefreshTokenCallBack_) method:

```cs
using DolbyIO.Comms;

DolbyIOSDK _sdk = new DolbyIOSDK();

try
{
    await _sdk.Init("Token", () => 
    {
        // Refresh Callback
        return "Refreshed Access Token";
    });
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

**NOTE:** The SDK is fully asynchronous and all methods can throw the [DolbyIOException](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/DolbyIOSDK/DolbyIOException).

### 2. Register event handlers

After initializing the SDK, it is time to register your event handlers. Decide which events you want to add event handlers and add the handlers as in the following example:

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

To open a new session, use the [Session.Open](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Session/Open) method as in the following example:

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

To create and join a conference, use the [Conference.Create](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Conference/Create) and [Conference.Join](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Conference/Join) methods as in the following example:

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

If the conference already exists, the SDK returns [ConferenceInfos](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Struct/ConferenceInfos).

### 5. Leave the conference

To leave the conference, use the [Conference.Leave](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Conference/Leave) method, as in the following example:

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

After leaving the conference, close the session and dispose of the SDK using the [Session.Close](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/Session/Close) and [DolbyIOSDK.Dispose](https://dolbyio.github.io/comms-sdk-dotnet/dolbyio_comms_sdk_dotnet/DolbyIOSDK/Dispose) methods.

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
    private DolbyIOSDK sdk = new DolbyIOSDK();

    public async Task OpenAndJoin()
    {
        try
        {
            await _sdk.Init("Access Token", () =>
            {
                return "Refreshed Access Token";
            });

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

    private void OnConferenceStatus(ConferenceStatus status, string conferenceId)
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
