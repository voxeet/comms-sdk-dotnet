# Plugin

The Dolby.io Virtual World plugin for Unity requires the Unity package manager to be added to your project. This document describes how to add the .NET SDK to Unity and initialize the SDK.

## Prerequisites

Make sure that you have:

- Apple macOS x64 or Microsoft Windows x64
- A Dolby.io account. If you do not have an account, you can [sign up](https://dolby.io/signup) for a free account.
- The [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) copied from the Dolby.io dashboard. To create the token, log in to the [Dolby.io dashboard](https://dashboard.dolby.io/), create an application, and navigate to the API keys section.

## Adding the .NET SDK to Unity  

The Dolby.io Virtual World plugin for Unity provides access to an instance of the SDK through a custom `DolbyIO Manager` manager. To add the SDK to Unity, follow these steps:

1. Create an empty game object in your scene and provide a name for the object. In this example, we call the object `ApplicationManager`:
<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/unity_1.png" width="400px">
</div>

2. Select the created game object and select `Component` > `DolbyIO` > `DolbyIO Manager` from the Unity menu.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/unity_2.png">
</div>

This step adds the `DolbyIO Manager` to the game object.

3. Make sure that the .NET SDK is now available in all scripts:

```cs
using Unity;
using DolbyIO.Comms;
using DolbyIO.Comms.Unity;

public class MyScript : MonoBehaviour
{
    private DolbyIOSDK _sdk = DolbyIOManager.Sdk;
}
```

## Initialization

To initialize the SDK, follow these steps:

1. Copy the access token form the Dolby.io dashboard. 

2. Call the [InitAsync](xref:DolbyIO.Comms.DolbyIOSDK#DolbyIO_Comms_DolbyIOSDK_InitAsync_System_String_DolbyIO_Comms_RefreshTokenCallBack_) method using the token to initialize the SDK.

The Init method should be called only once. We recommend calling the Init method in the Awake method of a new script that you can add as a component of the created game object.

```cs
using Unity;
using DolbyIO.Comms;
using DolbyIO.Comms.Unity;

public class MyScript : MonoBehaviour
{
    private DolbyIOSDK _sdk = DolbyIOManager.Sdk;

    async void Awake()
    {
        try
        {
            await _sdk.InitAsync("Access Token", () => 
            {
                return "New Access Token";
            });
        }
        catch (DolbyIOException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
```

>The `DolbyIOManager` calls the Dispose method of the SDK automatically in OnApplicationQuit. There is also a default option to automatically leave the current conference and close the opened session.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/unity_3.png" width="400px">
</div>

3. Make sure that the SDK is initialized and that you can call SDK methods. For more information, see the the [Getting Started](../sdk/started.md) guide and [reference](/documentation/api/DolbyIO.Comms.Services.html) documentation.

## MacOS application entitlements

To allow MacOS applications to capture audio, modify the `Info.plist` file by adding the following keys at the end of the file:

```xml
<key>com.apple.security.device.audio-input</key>
<true/>
<key>com.apple.security.device.microphone</key>
<true/>
```
