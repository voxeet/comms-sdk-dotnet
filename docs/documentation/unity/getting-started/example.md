### Built-in Sample

The plugin is shipped with a simple example demonstrating the basic concept of using Spatial Audio APIs.

This example connects the game to the Dolby.io platform, automatically instantiate 3 bots representing remote participants, and inject their audio into the conference. You can move around in the game to get a feel of how the spatial audio sounds like.

- To try out the sample, create a new project using any of the Unity templates
- Install the plugin from the package manager, then expand the `Samples` section and click on `Import`. This will copy the sample scene into your project. 
- Go to the `SharedScene` assets folder and drag the `SharedScene` object into the scene list. 
- Click on `App` then enter the client access token you acquired from Dolby.io dashboard.
- Under `Audio Listener`, set `Main Camera`, this automatically links the main camera movement to the spatial audio placement controls without writing any code. 
- Play the game, you should hear the sound now. Use WSAD keys to move around to hear the spatial effect.

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/4ce93126fd6f4e54b292f72a6251d2e1" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe></div>