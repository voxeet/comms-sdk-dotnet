# Dolby.io Communications .NET SDK

The Dolby.io Communications .NET SDK allows creating high-quality applications for conferencing, streaming, and collaborating. The .NET SDK offers an easily integration of the HD Dolby Voice experience, including spatial audio and shared scenes, into game engines and virtual spaces.

The SDK depends on the [C++ SDK](https://github.com/DolbyIO/comms-sdk-cpp) for core functions and supports the same functionalities for client applications as the C++ SDK.

## Get Started

This guide presents an example of using the SDK to create a basic audio-only conference application. The starter project that you can create by following this procedure provides the foundation upon which you can add additional features as you build out your own solutions for events, collaboration, or live streaming.

You can find the complete code for the application in the [Summary](#summary) section. The created application is available in the [SimpleApp](https://github.com/DolbyIO/comms-sdk-dotnet/tree/master/samples/SimpleApp) folder.

### Prerequisites

Make sure that you have:

- A Dolby.io account. If you do not have an account, you can [sign up](https://dolby.io/signup) for a free account.
- The [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) copied from the [Dolby.io dashboard](https://dashboard.dolby.io/). To create the token, log into the Dolby.io dashboard, create an application, and navigate to the API keys section.

Additionally, if you plan to build the SDK from sources, not the NuGet packet manager, make sure that you have:

- The [Dolby.io Communications C++ SDK 2.0](https://github.com/DolbyIO/comms-sdk-cpp/releases) for your platform
- [.NET SDK 6](https://dotnet.microsoft.com/en-us/download)
- C++ compiler compatible with C++ 17
- CMake 3.23
- A macOS or Windows machine

### How to use

#### 0. Install the SDK

If you want to use NuGet, use the following command:

```shell
dotnet add package DolbyIO.Comms.Sdk
```

#### 1. Initialize the SDK

Initialize the SDK using the secure authentication method that uses a token in the application. For the purpose of this application, use a [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) generated from the Dolby.io dashboard. Use the following code to initialize the SDK using the [DolbyIOSDK.InitAsync](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.DolbyIOSDK.html#DolbyIO_Comms_DolbyIOSDK_InitAsync_System_String_DolbyIO_Comms_RefreshTokenCallBack_) method:

```cs
using DolbyIO.Comms;

DolbyIOSDK _sdk = new DolbyIOSDK();

try
{
    await _sdk.InitAsync("Access Token", () => 
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

**NOTE:** The SDK is fully asynchronous, and all methods can throw the [DolbyIOException](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.DolbyIOException.html).

#### 2. Register event handlers

After initializing the SDK, it is time to register your event handlers. Decide which events you want to add event handlers to and add the handlers as in the following example:

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

#### 3. Open a session

A session is a connection between the client application and the Dolby.io backend. When opening a session, you should provide a name. Commonly, this is the name of the participant who established the session. The session can remain active for the whole lifecycle of your application. 

To open a new session, use the [Session.OpenAsync](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Services.SessionService.html#DolbyIO_Comms_Services_SessionService_OpenAsync_DolbyIO_Comms_UserInfo_) method as in the following example:

```cs
try
{
    UserInfo user = new UserInfo();
    user.Name = "My Name";

    user = await _sdk.Session.OpenAsync(user);
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

#### 4. Create and join a conference

A conference is a multi-person call where participants exchange audio with one another. To distinguish between multiple conferences, you should assign a conference alias or name. When multiple participants join a conference of the same name using a token of the same Dolby.io application, they will be able to meet in a call.

To create and join a conference, use the [Conference.CreateAsync](hhttps://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Services.ConferenceService.html#DolbyIO_Comms_Services_ConferenceService_CreateAsync_DolbyIO_Comms_ConferenceOptions_) and [Conference.JoinAsync](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Services.ConferenceService.html#DolbyIO_Comms_Services_ConferenceService_JoinAsync_DolbyIO_Comms_Conference_DolbyIO_Comms_JoinOptions_) methods as in the following example:

```cs
try
{
    ConferenceOptions options = new ConferenceOptions();
    options.Alias = "Conference alias";

    JoinOptions joinOpts = new JoinOptions();

    Conference conference = await _sdk.Conference.CreateAsync(options);
    Conference joinedConference = await _sdk.Conference.JoinAsync(conference, joinOpts);
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

If the conference already exists, the SDK returns [Conference](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Conference).

#### 5. Leave the conference

To leave the conference, use the [Conference.LeaveAsync](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Services.ConferenceService.html#DolbyIO_Comms_Services_ConferenceService_LeaveAsync) method, as in the following example:

```cs
try
{
    await _sdk.Session.LeaveAsync();
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

#### 6. Close the session and dispose of the SDK

After leaving the conference, close the session and dispose the SDK using the [Session.CloseAsync](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.Services.SessionService.html#DolbyIO_Comms_Services_SessionService_CloseAsync) and [DolbyIOSDK.Dispose](https://dolbyio.github.io/comms-sdk-dotnet/documentation/api/DolbyIO.Comms.DolbyIOSDK.html#DolbyIO_Comms_DolbyIOSDK_Dispose) methods.

```cs
try
{
    await _sdk.Session.CloseAsync();
    _sdk.Dispose();
}
catch (DolbyIOException e)
{
    // Error Handling
}
```

This step releases the underneath unmanaged native resources.

### Summary

All the steps combined create the following code snippet:

```cs
using System;
using DolbyIO.Comms;

public class Call
{
    private DolbyIOSDK _sdk = new DolbyIOSDK();

    public async Task OpenAndJoinAsync()
    {
        try
        {
            await _sdk.InitAsync("Access Token", () =>
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

            user = await _sdk.Session.OpenAsync(user);

            ConferenceOptions options = new ConferenceOptions();
            options.Alias = "Conference alias";

            JoinOptions joinOpts = new JoinOptions();

            Conference conference = await _sdk.Conference.CreateAsync(options);
            Conference joinedConference = await _sdk.Conference.JoinAsync(conference, joinOpts);
        }
        catch (DolbyIOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task LeaveAndCloseAsync()
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

    private void OnConferenceStatus(ConferenceStatus status, string conferenceId)
    {

    }
}

public class Program
{
    public static async Task Main()
    {
        Call call = new Call();
        await call.OpenAndJoinAsync();

        Console.ReadKey();

        await call.LeaveAndCloseAsync();
    }
}
```

## Build the SDK

In order to compile the Dolby.io Communications .NET SDK, you will need to use CMake for the build chain and generating projects. To build the application from sources, use the following commands:

```console
mkdir build && cd build
cmake .. -DDOLBYIO_LIBRARY_PATH=/path/to/c++sdk -DCMAKE_BUILD_TYPE=Debug
cmake --build .
```

Set the environment variable `DOLBYIO_LIBRARY_PATH` as the path to the root folder containing the Dolby.io Communications C++ SDK.

> **Note**: On macOS, it is necessary to unquarantine SDK libraries and sample binaries. Otherwise, quarantine attributes prevent their usage. The simplest way to unquarantine is to strip the quarantine attributes recursively for all the files in the package. The following example presents how to do it via terminal and the macOS attribute stripping command line tool: `xattr -d -r com.apple.quarantine sdk-release/`

`cmake` allows you to generate various project files, for example, if you want to generate a Visual Studio solution on Windows, you can add the following option when generating (`-G "Visual Studio 17 2022"`):

```shell
cmake .. -DDOLBYIO_LIBRARY_PATH=/path/to/c++sdk -DCMAKE_BUILD_TYPE=Debug -G "Visual Studio 17 2022"
```

> **Note**: Currently, the SDK is only compatible with the x86_64 architecture. If you want to compile your application on a Mac with an **Apple Silicon** chip, you need to use the x64 .NET SDK, not the ARM one, and specify the architecture on which CMake should build upon:
>```bash
>cmake .. -DDOLBYIO_LIBRARY_PATH=/path/to/c++sdk -DCMAKE_BUILD_TYPE=Debug -DCMAKE_OSX_ARCHITECTURES=x86_64
>```

Run the unit tests after compiling the SDK using the following command:

```shell
ctest -VV -C RelWithDebInfo
```
