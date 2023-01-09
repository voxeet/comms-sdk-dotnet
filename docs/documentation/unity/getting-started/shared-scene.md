## Shared scene
Shared scene is built purposely for the virtual world use cases to simplify the programming model. With shared scene, there is no need to explicitly set each remote participant's spatial audio position. Instead, each participant needs to report their own position and direction. 

For more information regarding the individual scene, please refer to this [article](https://docs.dolby.io/communications-apis/docs/guides-integrating-shared-spatial-audio).

## Set up the conference
- Change the demo conference type to `shared` in the [Demo Conference](../visualscripting/nodes.md#demo-conference).
- There is no need to explicitly synchronize spatial audio positions in the shared scene. However, it is critical that you are aware of where the remote participants are at all times so you can render their visual representation at the right location. 

## Update spatial audio when the player moves around 
The periodical update logic is identical to the individual scene script. Refer to this [article](individual-scene.md#update-spatial-audio-when-the-player-moves-around) for more details. 
