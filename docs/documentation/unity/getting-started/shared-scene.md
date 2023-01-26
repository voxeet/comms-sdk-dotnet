## Shared scene
If you followed the previous guides and successfully placed the remote participants in the game, you may have noticed that there is no spatial audio separation for the bots; when the player moves around, the audio of the bots does not change. This is because the demo conference has not been enabled with spatial audio yet. 

Shared scene is built purposely for virtual world use cases to simplify the spatial audio programming model. In this scenario, all participants contribute to the shared scene by setting their positions only. All participants are heard from the positions they set.

For more information regarding the shared scene, please refer to this [article](https://docs.dolby.io/communications-apis/docs/guides-integrating-shared-spatial-audio).

## Set up the conference
Change the demo conference **Spatial Audio Style** to `shared` in the [Demo Conference](../visualscripting/nodes.md#demo-conference) node.

> ⓘ If you use a Bluetooth headset, you may experience mono audio and, therefore, not hear the spatial separation. This is because the Bluetooth technology prevents stereo rendering if you also use the built-in microphone at the same time. In order to work around this, simply change the microphone from the system settings menu to another device. 

Run the app. Now, when you move around in the game, you will hear the full spatial audio effect.