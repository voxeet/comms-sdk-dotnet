## Remote participants
Rendering remote participants in the scene is an essential task for building multi-party applications. This guide demonstrates how to render bots from the demo conference using visual scripting. At the end of this task, you will see the following scene with the remote bots placed in their designated positions:

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/remote-participants.png" width="450px">
</div>

### Participant events
Once the plugin is successfully connected to the conference, it emits various events that the application can listen to. In this example, we will use the [On Participant Updated Event](../visualscripting/events.md#on-participant-updated), which is emitted whenever the remote participant state changes. This event provides a custom type [Participant](xref:DolbyIO.Comms.Participant) for the application to retrieve the participant information. In your application, you can choose other triggering event based on your needs.

> In order to access Dolby.io Communications SDK custom types from visual scripting, you need to explicitly add them to the visual scripting library. In the Unity editor, click on **Edit** > **Project Settings** > **Visual Scripting**, expand the **Node Library**, click on **+** then add `DolbyIO.Comms.Sdk`. After that, expand **Type Operations**, click on **+**, and then add the `Participant` and `ParticipantInfo` objects. Finally, click on **Regenerate Nodes** to make sure that the node library is updated with the changes. 

### Participant identification
There are several ways to identify a participant in the Dolby.io conference.
- [participant ID](xref:DolbyIO.Comms.Participant.Id): Dolby.io generated unique identifier in a UUID format.
- [external ID](xref:DolbyIO.Comms.ParticipantInfo.ExternalId): A free-form string defined by you as a developer to uniquely identify a participant in your application. 
- [name](xref:DolbyIO.Comms.ParticipantInfo.Name): A free-form string provided during [initialization](../visualscripting/nodes.md#initialize). Dolby.io does not guarantee the name to be unique, which means more than one participant can use the same name to join the conference. For demonstration purposes, we will use the `name` to identify the remote participants in the demo conference.

There are three bots in the demo conference, their names are:

- `Dan`
- `Ruth`
- `JC`

The name is accessible through the [ParticipantInfo](xref:DolbyIO.Comms.ParticipantInfo) object. See the following visual scripting for details:

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/participant-info.png" width="650px">
</div>

### Mapping the positions
By mapping the name to a Vector3 object and passing it to the Unity **Instantiate** node, we can create an avatar at the desired position. 

In this example, we set `Dan` to `x:-2, y:0, z: 2.5`, `Ruth` to `x: 0, y:0, z: 5`, and `JC` to `x:2, y:0, z:2.5`. For all three bots, we set the direction to `y:180` so they all face the local user when they are spawned in the game. 

Finally, select the built-in `Armature` 3D model for the **Original** field of the **Instantiate** node.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/participant-positions.png" width="650px">
</div>

Run the app. Now you should see the three bots laid out in the scene and hear them talking.