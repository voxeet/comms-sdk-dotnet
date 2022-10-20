# Unity Visual Scripting

The Dolby.io Unity Plugin provides nodes for Unity Visual Scripting from 2021.* version. The nodes exposes some of the functionnalities available in the .NET SDK.

>**note:** You can use the plugin with Visual Scripting and C# Scripting at the same time.

## Prerequisites

The prerequisites are the same as for the [Plugin](./unity.md#prerequisites).

For the visual scripting nodes to work you have to add the `DolbyIOManager` as explained in [Adding the .NET SDK to Unity](./unity.md#adding-the-net-sdk-to-unity).

## The Visual Scripting Nodes

Nodes are accessible in the "Add Node" contextual menu, under the DolbyIO Category:

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/menu.png" width="200px">
</div>

### Initializing

This node allows you to initialize the DolbyIO SDK and to open a session to the Dolby.io backend.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/init.png" width="300px">
</div>

Parameters:

- **Access Token**: The access token provided by the customer's backend. See [](xref:DolbyIO.Comms.DolbyIOSDK.InitAsync(System.String,DolbyIO.Comms.RefreshTokenCallBack)).
- **Participant Name**: The name of the participant. See [](xref:DolbyIO.Comms.UserInfo.Name).

### Spatial Conference

This node allows you to enter a spatial conference and specify the 3D Environment settings.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/spatial.png" width="300px">
</div>

Parameters:

- **Scale, Forward, Up, Right**: Those vectors are definition for the 3D Environment you are working with. The defaults are based on the Unity Coordinates System. In most cases only the scale should be modified. See [](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)).
- **Conference Alias**: The conference alias. See [](xref:DolbyIO.Comms.Conference.Alias).
- **Spatial Audio Style**: The spatial audio style, default to Shared. See [](xref:DolbyIO.Comms.SpatialAudioStyle).

### Demo

This node allows you to experience the demo of the Dolby.io platform.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/demo.png" width="200px">
</div>

Parameters:

- **Scale, Forward, Up, Right**: Those vectors are definition for the 3D Environment you are working with. The defaults are based on the Unity Coordinates System. In most cases only the scale should be modified. See [](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialEnvironmentAsync(System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3,System.Numerics.Vector3)).
- **Spatial Audio**: Join the demo with spatial audio or not.

### Mute Participant

This node allows you to mute yourself or a specific participant.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/mute.png" width="250px">
</div>

Parameters:
- **ParticipantId**: The id of the participant to mute. If no Id is provided, the node will mute the local participant.
- **Muted**: The mute state to apply.

### Local Player Position

This node allows you to set the local player position.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/local-position.png" width="250px">
</div>

Parameters:
- **Position**: The position of the player. See [](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3))
- **Direction**: The direction of the player. See [](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialDirectionAsync(System.Numerics.Vector3))

### Remote Player Position

This node allows you to set the remote player position.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/remote-position.png" width="250px">
</div>

Parameters:
- **Position**: The position of the player. See [](xref:DolbyIO.Comms.Services.ConferenceService.SetSpatialPositionAsync(System.String,System.Numerics.Vector3))


### Get Participants

This node allows you to get all the participants of the conference.

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/participants.png" width="250px">
</div>

Parameters:
- **Participant Ids**: A list to filter the resulting participant list.

## The Visual Scripting Events

### On Conference Status Updated Event

This event is emitted when the conference status has changed. See [](xref:DolbyIO.Comms.Services.ConferenceService.StatusUpdated)

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-conference-status.png" width="250px">
</div>

### On Participant Added Event

This event is emitted when a new participant has been added to the conference. See [](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantAdded)

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-added.png" width="250px">
</div>

### On Participant Updated Event

This event is emitted when when a conference participant has changed a status. See [](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantUpdated)

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-updated.png" width="250px">
</div>

### On Active Speaker Change Event

This event is emitted when an active speaker has changed. See [](xref:DolbyIO.Comms.Services.ConferenceService.ActiveSpeakerChange)

<div style="text-align:center">
    <img style="padding:25px 0" src="~/images/nodes/event-active-speaker.png" width="250px">
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