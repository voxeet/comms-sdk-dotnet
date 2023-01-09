## Individual scene
This article demonstrates how to implement an individual spatial scene using visual scripting. 

In the [individual scene](xref:DolbyIO.Comms.SpatialAudioStyle), each client explicitly configures the spatial audio position of every remote participant for themselves.

[Set remote player position](../visualscripting/nodes.md#set-remote-player-position) is the node that controls the individual remote participant's spatial audio position. This node must be invoked when the remote participant joins the conference, and when the remote participant moves around in the scene. 

When the local player moves around in the scene, use the [Set local player position](../visualscripting/nodes.md#set-local-player-position) and the [Set local player direction](../visualscripting/nodes.md#set-local-player-direction) nodes to keep the spatial audio rendering coherent with the local user's visual position.

For more information regarding the individual scene, please refer to this [article](https://docs.dolby.io/communications-apis/docs/guides-integrating-individual-spatial-audio).

## Synchronize spatial audio positions
1. Change the demo conference **Spatial Audio Style** to `individual` in the [Demo Conference](../visualscripting/nodes.md#demo-conference) node.
2. Insert the [Set remote player position](../visualscripting/nodes.md#set-remote-player-position) node between the [On participant updated event](../visualscripting/events.md#on-participant-updated) and the `Instantiate` node to create the game object, and link the [Set remote participant position](../visualscripting/nodes.md#set-remote-player-position)'s input to the output of the `Select on Flow Branches` node. This effectively synchronizes the bots' visual positions with their spatial audio positions against the local participant's spawn location.

    <div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/set-remote-position.png" width="650px">
    </div>

3. Run the app. Now, when you move around in the game, you will hear the full spatial audio effect.
