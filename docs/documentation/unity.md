# Unity

## Installation

The Dolby.io .NET SDK for unity is available on the unity store, and can be added to your project with the unity package manager.

## Supported Platforms

For now, supported platforms includes MacOS x64 and Windows x64.

## Usage

The unity plugin provides access to an instance of the SDK through a custom manager call `DolbIOManager`. To use it, create an empty `GameObject` in your scene. Call it for example `ApplicationManager`. (This name is just an example and can be modified to whatever suits your needs).

![Unity1](~/images/unity_1.png)

Select the newly created `GameObject` and go to the menu Components > DolbyIO > DolbyIO Manager. This will add the `DolbyIOManager` to this `GameObject`.

![Unity2](~/images/unity_2.png)

The DolbyIO .NET SDK is now available in every scripts :

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

The SDK needs to be initialized before any call to its functionalities using the method [Init](xref:DolbyIO.Comms.DolbyIOSDK#DolbyIO_Comms_DolbyIOSDK_Init_System_String_DolbyIO_Comms_RefreshTokenCallBack_).

For information on how to retrieve an Access Token, please see: [Prerequisite](./started.md#prerequisites).

The init method should only be called once. The best place to call it is in the Awake method of a new script you will add as a component to the previously created `ApplicationManager` GameObject.

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
            await _sdk.Init("Access Token", () => 
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

>The `DolbyIOManager` calls the Dispose method of the SDK automatically in OnApplicationQuit. You just need to leave the conference and close the session at the end of your application.

After this, you are up to call the various SDK methods described in the [Getting Started](./started.md) and in the reference documentation [Reference](/documentation/api/DolbyIO.Comms.Services.html).