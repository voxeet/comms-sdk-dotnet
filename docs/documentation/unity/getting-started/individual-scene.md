## Individual scene
This article demonstrates how to implement individual spatial scene using visual scripting. 

If you followed the previous guides and successfully placed the remote participants in the game, you may have noticed there is no spatial audio separation for the bots; and when the player moves around, the audio of the bots do not change. This is because the demo conference has not been enabled with spatial audio yet. 

In the [individual scene](xref:DolbyIO.Comms.SpatialAudioStyle), each client explicitly configures the spatial audio position of every remote participant for themselves.

[Set remote player position](../visualscripting/nodes.md#set-remote-player-position) is the node that controls individual remote participant's spatial audio position. This node must be invoked when the remote participant joins the conference; and when the remote participant moves around in the scene. 

When the local player moves around in the scene, use the [Set local player position](../visualscripting/nodes.md#set-local-player-position) and the [Set local player direction](../visualscripting/nodes.md#set-local-player-direction) nodes to keep the spatial audio rendering coherent with the local user's visual position.

For more information regarding the individual scene, please refer to this [article](https://docs.dolby.io/communications-apis/docs/guides-integrating-individual-spatial-audio).

## Synchronize spatial audio positions initially
1. Change the demo conference type to `individual` in the [Demo Conference](../visualscripting/nodes.md#demo-conference).
2. Insert the [Set remote player position](../visualscripting/nodes.md#set-remote-player-position) node between the [On participant updated event](../visualscripting/events.md#on-participant-updated) and the `Instantiate` node to create the game object, and link [Set remote participant position](../visualscripting/nodes.md#set-remote-player-position)'s input to the output of the `Select on Flow Branches` node. This effectively synchronizes the bots' visual positions with their spatial audio positions against the local participant's spawn location.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/set-remote-position.png" width="650px">
</div>

Run the app - now you should hear the spatial separation when the bots talk. 

> If you use a Bluetooth headset you may experience mono audio therefore not hearing the spatial separation. This is because the Bluetooth technology prevents stereo rendering if you also use the built-in microphone at the same time. In order to workaround this - simply change the microphone from the system settings menu to another device. 

## Update spatial audio when the player moves around 
Create a new script `positioning` for the **PlayerArmature** game object in the scene menu.

Start with the Unity built-in `On Update` event, emitted whenever the local player avatar changes. Link the output trigger to the `Cooldown` timer node, which provides the ability to perform operations periodically.

- The position update is throttled at 300ms since this is a server side operation.
- The direction update is throttled at 50ms since this is a client side operation. 

Eventually, the [Set Local Player Position](../visualscripting/nodes.md#set-local-player-position) and the [Set Local Player Direction](../visualscripting/nodes.md#set-local-player-direction) nodes are linked to the local players position and direction calculation, this is how the spatial audio positions synchronizes with the local user's visual positions.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/set-local-position-direction.png" width="650px">
</div>

Run the app - now when you move around in the game, you will hear the full spatial audio effect.