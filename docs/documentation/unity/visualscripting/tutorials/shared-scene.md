## Shared scene
Shared scene is built purposely for virtual world use cases to simplify the spatial audio programming model. In this scenario, all participants contribute to the shared scene by setting their positions only. All participants are heard from the positions they set.

In shared scene, the plugin automatically maps the local player's position and direction by detecting the Audio Listener in the scene. You can also configure it explicitly in [Dolby.io Manager](../../components/index.md).

For more information regarding the shared scene, please refer to this [article](https://docs.dolby.io/communications-apis/docs/guides-integrating-shared-spatial-audio).

> ⓘ If you use a Bluetooth headset, you may experience mono audio and, therefore, not hear the spatial separation. This is because the Bluetooth technology prevents stereo rendering if you also use the built-in microphone at the same time. In order to work around this, simply change the microphone from the system settings menu to another device. 

Run the app. Now, when you move around in the game, you will hear the full spatial audio effect.