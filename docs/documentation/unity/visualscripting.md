# Visual Scripting

Visual Scripting in Unity allows creating logic for games or applications without writing code, using visual, node-based graphs. The Dolby.io Communications Plugin for Unity provides nodes that allow using .NET SDK functionalities in Unity Visual Scripting 2021. The plugin can be used with Visual Scripting and C# scripting at the same time. This document describes the available nodes and events.

Before using the plugin for visual scripting, make sure that you added the .NET SDK to Unity and initialized the SDK using the [Initializing](./unity.md#initialization) procedure.

## Visual Scripting Nodes

The Dolby.io nodes are accessible in the "Add Node" contextual menu, under the DolbyIO category:

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/menu.png" width="200px">
</div>

### Initialize

Allows initializing the SDK and opening a session that connects the SDK with the Dolby.io backend.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/init.png" width="300px">
</div>

Parameters:

- **Access Token**: The [access token](xref:DolbyIO.Comms.DolbyIOSDK.InitAsync(System.String,DolbyIO.Comms.RefreshTokenCallBack)) provided by the customer's backend.
- **Participant Name**: The [name](xref:DolbyIO.Comms.UserInfo.Name) of the participant.

### Spatial Conference

Allows entering a conference with enabled Spatial Audio and specifying the settings of the 3D environment.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/spatial.png" width="300px">
</div>

Parameters:

- **Scale, Forward, Up, Right**: [Vectors](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)) that define the 3D environment that you are working with. The default values are based on the Unity Coordinates System. In most cases, you should only modify the scale.
- **Conference Alias**: The conference [alias](xref:DolbyIO.Comms.Conference.Alias).
- **Spatial Audio Style**: The [spatial audio style](xref:DolbyIO.Comms.SpatialAudioStyle) that defines how the spatial location should be communicated between the SDK and the Dolby.io server. By default, the parameter is set to `shared`.

### Demo

Allows experiencing the demo of the Dolby.io platform.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/demo.png" width="200px">
</div>

Parameters:

- **Scale, Forward, Up, Right**: [Vectors](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)) that define the 3D environment that you are working with. The default values are based on the Unity Coordinates System. In most cases, you should only modify the scale.
- **Spatial Audio**: A boolean that indicates whether spatial audio should be enabled.

### Mute Participant

Allows muting a specific participant.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/mute.png" width="250px">
</div>

Parameters:
- **ParticipantId**: The ID of the participant who should be muted. If the ID is not provided, the node mutes the local participant.
- **Muted**: The required mute state.

### Local Player Position

Allows setting the position of the local player.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/local-position.png" width="250px">
</div>

Parameters:
- **Position**: The [position](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3)) of the player.
- **Direction**: The [direction](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialDirectionAsync(System.Numerics.Vector3)) that the player should be facing.

### Remote Player Position

Allows setting the remote player's position.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/remote-position.png" width="250px">
</div>

Parameters:
- **Position**: The [position](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3)) of the player.


### Get Participants

Allows getting objects of all participants who are present at a conference.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/participants.png" width="250px">
</div>

Parameters:
- **Participant Ids**: A list of participant IDs that causes returning only the objects of the listed participants.

### Get Audio Devices

Allows getting available audio devices.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/audio-devices.png" width="250px">
</div>

Parameters:
- **Direction**: The [direction](xref:DolbyIO.Comms.DeviceDirection) of the devices.

### Set Audio Input Device

Allows setting the audio input device.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/audio-input.png" width="250px">
</div>

Parameters:
- **Audio Device**: The [AudioDevice](xref:DolbyIO.Comms.AudioDevice) to set.

### Set Audio Output Device

Allows setting the audio output device.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/audio-output.png" width="250px">
</div>

Parameters:
- **Audio Device**: The [AudioDevice](xref:DolbyIO.Comms.AudioDevice) to set.

## The Visual Scripting Events

### On Conference Status Updated Event

Emitted when a conference status has [changed](xref:DolbyIO.Comms.Services.ConferenceService.StatusUpdated).

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-conference-status.png" width="250px">
</div>

### On Participant Added Event

Emitted when a new participant has been [added](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantAdded) to a conference.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-added.png" width="250px">
</div>

### On Participant Updated Event

Emitted when a conference participant has [changed](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantUpdated) a status.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-updated.png" width="250px">
</div>

### On Active Speaker Change Event

Emitted when an active speaker has [changed](xref:DolbyIO.Comms.Services.ConferenceService.ActiveSpeakerChange).

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-active-speaker.png" width="250px">
</div>

### On Audio Device Added Event

Emitted when an audio device is [added](xref:DolbyIO.Comms.Services.MediaDeviceService.Added).

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-audio-added.png" width="250px">
</div>

### On Audio Device Changed Event

Emitted when the active audio device has [changed](xref:DolbyIO.Comms.Services.MediaDeviceService.Changed).

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-audio-changed.png" width="250px">
</div>

## Examples
### Joining a Conference

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/example-join.png" height="250px">
</div>

### Updating the local player position

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/example-position.png" height="500px">
</div>