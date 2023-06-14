## Components
### Dolby.io Manager

The `Dolby.io Manager` is a component required to let you access the functionnalities of the Unity plugin. It will also provide some usefull parameters to configure the plugin.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/dolbyiomanager.png" width="300px">
</div>

| Name | Type | Description  |
|---|:---|:---|
| **Player Name** | String | The user name for the Dolby.io session. |
| **Auto Leave Conference** | Boolean | Allows the plugin to automatically leave the conference when the application is quitting. |
| **Auto Close Session** | Boolean | Allows the plugin to automatically close the session when the application is quitting. |
| **Position Duration** | Float (seconds) | The throttling time for setting the local player position. |
| **Direction Duration** | Float (seconds) | The throttling time for setting the local player direction. |
| **Audio Listener** | GameObject | A GameObject containing the AudioListener component to get the local player position from. If empty, the default AudioListener will be chosen. | 

### Credentials

Those components allows you to provide the required credentials to the Dolby.io Manager. You can use only one of them at a time.

#### Access Token

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/token.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Acess Token** | String | An access token from the [Dolby.io dashboard](https://dashboard.dolby.io/).|

#### App Key

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/appkey.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **App Key** | String | An app key from the [Dolby.io dashboard](https://dashboard.dolby.io/).|
| **App Secret** | String | An app secret from the [Dolby.io dashboard](https://dashboard.dolby.io/).|

### Conference Controller

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/conferencecontroller.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Conference Alias** | String | The conference alias to join. |
| **Audio Style** | Enum | A <xref href="DolbyIO.Comms.SpatialAudioStyle"/> for the conference. |
| **Scale** | Vector3 | The scale of the 3D environment. |
| **Auto Join** | Bool | Joins automatically the conference on application start. |
| **Video Device** | GameObject | The GameObject Dropdown of the video device source. |
| **Screen Share Source** | GameObject | The GameObject Dropdown of the screen share source. |

### Devices
#### Audio Device Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this component allows you to display and select the Input/Output audio device.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/audiodevices.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Direction** | Enum | A <xref href="DolbyIO.Comms.DeviceDirection"/> to display the corresponding devices. |

#### Video Device Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this component allows you to display and select a video capture device.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/videodevices.png" width="300px">
</div>

#### Screen Share Source Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this components allows you to display and select a screenshare source to capture.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/screensharesources.png" width="300px">
</div>