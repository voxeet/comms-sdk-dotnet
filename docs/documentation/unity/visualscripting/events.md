## Events
Events are the most basic part of scripts in Visual Scripting to trigger asynchronous operations. 
Once the plugin is successfully installed, the Dolby.io events are accessible in the `Add Node` contextual menu, under the Events/DolbyIO category:
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/events.png" width="650px">
</div>

---
### On Conference Status Updated

Emitted when a conference status has [changed](xref:DolbyIO.Comms.Services.ConferenceService.StatusUpdated).

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-conference-status.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Conference Status** | Output | [Conference Status](xref:DolbyIO.Comms.ConferenceStatus)| The new status of the conference. |

---
### On Participant Added

Emitted when a new participant has been [added](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantAdded) to a conference.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-added.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Participant** | Output | [Participant](xref:DolbyIO.Comms.Participant)| The participant object. |

---
### On Participant Updated

Emitted when a conference participant has [changed](xref:DolbyIO.Comms.Services.ConferenceService.ParticipantUpdated) a status. This also applies to when the remote participant has left the conference.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-participant-updated.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Participant** | Output | [Participant](xref:DolbyIO.Comms.Participant)| The participant object. Check the [ParticipantStatus](xref:DolbyIO.Comms.ParticipantStatus) for the current status of the participant.|

---
### On Active Speaker Change

Emitted when active speakers has [changed](xref:DolbyIO.Comms.Services.ConferenceService.ActiveSpeakerChange). 

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-active-speaker.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Participant Ids** | Output | List of Strings| The list of participant IDs that are currently speaking.|

---
### On Audio Device Added

Emitted when an audio device is [added](xref:DolbyIO.Comms.Services.MediaDeviceService.Added).

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-audio-added.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Audio Device** | Output | [Audio Device](xref:DolbyIO.Comms.AudioDevice)| The audio device that has been added.|

---
### On Audio Device Changed

Emitted when the active audio device has [changed](xref:DolbyIO.Comms.Services.MediaDeviceService.Changed).

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-audio-changed.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Audio Device** | Output | [Audio Device](xref:DolbyIO.Comms.AudioDevice)| The audio device that has changed.|

---
### On Signaling Channel Error

Emitted when an error occured when the SDK tries to connect to a conference.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-signaling-error.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Message** | Output | String| Additional debug information indicating why the error happened.|

---
### On Invalid Token Error

Emitted when the access token is invalid or has expired. You need to acquire a new token from the service to continue the operation.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/nodes/event-token-error.png" width="250px">
</div>

| Name  | Direction | Type | Description  |
|---|:---|:---|:---|
| **Message** | Output | String| Additional debug information indicating why the error happened.|
