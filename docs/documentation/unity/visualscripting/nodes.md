# Visual Scripting

Visual Scripting in Unity allows creating logic for virtual world applications using visual, node-based graphs without writing code. The Dolby.io Virtual World plugin is compatible with Unity Visual Scripting 2021. The plugin can be used with Visual Scripting and C# scripting at the same time. 

## Nodes

Nodes are the most basic part of scripts in Visual Scripting. A node can listen for events, get the value of a variable, modify a component on a GameObject, and more.
Once the plugin is successfully installed, the Dolby.io nodes are accessible in the `Add Node` contextual menu, under the DolbyIO category:
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/menu.png" width="500px">
</div>

---
### Initialize

Initializes the SDK and connects to the Dolby.io platform. During the onboarding and prototyping phase, you can obtain a client access token from the Dolby.io dashboard, or use the [GetToken](#get-token) node to retrieve a token directly.

>In your production application deployment, please follow our security best practice [here](https://docs.dolby.io/communications-apis/docs/guides-client-authentication) to set up a server through which you can acquire a temporary client access token that you can pass to this node.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/init.png" width="300px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Access Token** | Input | String or Function| The [client access token](xref:DolbyIO.Comms.DolbyIOSDK.InitAsync(System.String,DolbyIO.Comms.RefreshTokenCallBack)) provided by your backend server, linked to the output of a String node. Alternatively, the output from the [GetToken](#get-token) node.|
| **Participant Name** | Input | String | The [name](xref:DolbyIO.Comms.UserInfo.Name) of the local participant. |

---
### Get Token

A helper node that retrieves a client access token directly from within the Unity application. 

>Using this node effectively distributes the permanent app credential with your Unity application which is not safe for production deployment. Follow our security best practice [here](https://docs.dolby.io/communications-apis/docs/guides-client-authentication) to set up a server through which you can acquire a temporary client access token. 
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/get-token.png" width="300px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **App Key** | Input | String| The app key from the Dolby.io dashboard. |
| **App Secret** | Input | String | The app secret from the Dolby.io dashboard. |
| **Token Action** | Output | System.Func<string> | A function that returns the access token. It can be linked to the [Initialize](#initialize) node. |

---
### Conference

Connects to a conference with preferred spatial audio style and the settings of the 3D environment. If the conference does not exist, this operation automatically creates the conference. 
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/spatial.png" width="300px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Scale, Forward, Up, Right** | Input | Vector3| [Vectors](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)) that define the 3D environment that you are working with. The default values are based on the Unity Coordinates System. In most cases, you should only modify the scale.|
| **Conference Alias** | Input | String | The conference [alias](xref:DolbyIO.Comms.Conference.Alias), a nickname that is used to identify the conference. |
| **Spatial Audio Style** | Input | Spatial Audio Style | The [spatial audio style](xref:DolbyIO.Comms.SpatialAudioStyle) that defines how the spatial location should be communicated between the SDK and the Dolby.io platform. By default, the parameter is set to `shared` indicating the client application just reports its own position within the virtual world.|

---
### Demo Conference

Allows connecting to a conference that has several bots injecting audio. This makes it easier for testing the connection during the prototyping phase without having to instantiate multiple remote participants.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/demo.png" width="200px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Scale, Forward, Up, Right** | Input | Vector3| [Vectors](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)) that define the 3D environment that you are working with. The default values are based on the Unity Coordinates System. In most cases, you should only modify the scale.|
| **Spatial Audio** | Input | Spatial Audio Style | The [spatial audio style](xref:DolbyIO.Comms.SpatialAudioStyle) that defines how the spatial location should be communicated between the SDK and the Dolby.io platform. By default, the parameter is set to `shared` indicating the client application just reports its own position within the virtual world. | 

---
### Mute Participant

Allows muting a participant. This node works for both local participant (e.g., mute myself) and remote participant (e.g., mute others for me). 
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/mute.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **ParticipantId** | Input | String| The ID of the participant who should be muted. If the ID is not provided, the node mutes the local participant. |
| **Muted** | Input | Boolean | The required mute state.| 

---
### Get Participants

Retrieves a list of participants present in the conference. 
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/participants.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Participant Ids** | Input | String Array| A list of participant IDs that can be used to filter the complete list of participants in a conference. An empty list indicates no filter applied and all participants present in the conference will be returned.|
| **Participants** | Output | List of Participants| A list of participant [objects](xref:DolbyIO.Comms.Participant).|

---
### Get Audio Devices

Retrieves a list of available audio devices.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/audio-devices.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Direction** | Input | Device Direction | The direction, either `input`, `output`, or `both` indicating `microphone`, `speaker/headphone`, or `both` types.|
| **Audio Devices** | Output | List of Audio Devices | A list of [audio devices](xref:DolbyIO.Comms.AudioDevice) filtered by the `Direction` input.|


---
### Set Audio Input Device

Configures active audio input as the specified device.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/audio-input.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Audio Device** | Input | Audio Device | The user preferred [AudioDevice](xref:DolbyIO.Comms.AudioDevice) for audio capture.|

---
### Set Audio Output Device

Configures active audio output as the specified device.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/audio-output.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Audio Device** | Input | Audio Device | The user preferred [AudioDevice](xref:DolbyIO.Comms.AudioDevice) for audio rendering.|

---
### Set Local Player Direction

Allows setting the direction of the local player.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/local-direction.png" width="220px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Direction** | Input | Vector3 | The [direction](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialDirectionAsync(System.Numerics.Vector3)) that the player should be facing.| 

---
### Set Local Player Position

Allows setting the position of the local player. 
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/local-position.png" width="220px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Position** | Input | Vector3| The [position](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3)) of the player.|

---
### Set Remote Player Position

Sets the remote participant's spatial audio position for the local participant. This is only applicable when the spatial audio style is set as `individual` in the Spatial Conference node. The individual style requires each client to inform the server how they would like the platform to render each individual remote participant's audio for them.  
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/remote-position.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Position** | Input | Vector3| The [position](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3)) of the player.|
