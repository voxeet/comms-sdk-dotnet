## Installation

Make sure that you have:

- Apple MacOS x64 or Microsoft Windows 10+ x64.
- A Dolby.io account. If you do not have an account, you can [sign up](https://dolby.io/signup) for a free account.
- The [client access token](https://docs.dolby.io/communications-apis/docs/overview-developer-tools#client-access-token) copied from the Dolby.io dashboard. To create the token, log in to the [Dolby.io dashboard](https://dashboard.dolby.io/), create an application, and navigate to the API keys section.

You can install the Unity plugin from the Unity Package Manager. First, download `dolbyio-comms-unity-plugin` archive in the [Release](https://github.com/DolbyIO/comms-sdk-dotnet/releases) page and extract it to a suitable folder.

Open the Package Manager from the Unity Editor and click the <img src="~/images/plus_sign.png" height="20px"/> sign in the upper left corner. Select "Add package from disk", and look for the `package.json` file located where you extracted the aforementioned plugin archive.

>On MacOS, it is necessary to unquarantine SDK libraries. Otherwise, quarantine attributes prevent their usage. The simplest way to unquarantine is to strip the quarantine attributes recursively for all the files in the package. Follow these steps:
>1. Open **Terminal**.
>2. Assuming you have unzipped the SDK under `~/Downloads/dolbyio-comms-unity-plugin`, run this command `xattr -d -r com.apple.quarantine ~/Downloads/dolbyio-comms-unity-plugin`.

## Apple Silicon
If you are using Unity from an Apple Silicon Mac (e.g., M1), please be aware that currently the SDK is only distributed as x64 binary for Mac. You need to configure the Unity project under `File` > `Build Settings` and select `Intel 64bits` to make it work. 

## MacOS application entitlements

To allow MacOS applications to capture audio, modify the `Info.plist` file by adding the following keys at the end of the file:

```xml
<key>com.apple.security.device.audio-input</key>
<true/>
<key>com.apple.security.device.microphone</key>
<true/>
```

