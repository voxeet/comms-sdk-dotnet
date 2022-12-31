## Initialize and connect to a demo conference
This example demonstrates how to initialize the Unity plugin and connect to a demo conference using visual scripting. The Demo conference node allows connecting to a conference that has several bots injecting audio. This makes it easier for testing the connection during the prototyping phase.

To start fresh, you can create a new project from Unity Hub using one of the provided templates, such as `Core 3D`. The following video shows the workflow.

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/2b41901f211a409c96dcac2a178fabcf" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 80%; height: 100%;"></iframe></div>

### Step 0. Install the Unity plugin
This guide assumes you have already installed the plugin. If you have not done so, please refer to [this article](../unity.md#installation) for more information then come back to this guide. 

### Step 1. Add the DolbyIO Manager and Script Machine components
Once you've added the `AppManager` GameObject to the scene, you can add the following two components to the `AppManager`.

- `DolbyIO Manager` provides access to the SDK functionalities
- `Script Machine` is a component that contains the visual representation of a script that is a script graph.

### Step 2. Edit the script graph
The newly created script graph contains two nodes. `On Start` is the initial triggering event, which must be explicitly enabled as `coroutine` in order for it to work with the Unity plugin. 

Add two DolbyIO nodes to the script graph:
- [Initialize](../visualscripting/nodes.md#initialize)
- [Demo Conference](../visualscripting/nodes.md#demo)

Connect the `On Start` event to the input trigger of `Initialize` node, of which the output trigger connects to the `Demo Conference` input trigger as shown below. 

To keep things simple for now, uncheck the `Spatial Audio` option in `Demo Conference` node. This is because unless you've provided your spatial position, the default platform behavior for spatial audio conference is `no rendering`, which means you won't hear any audio. Unchecking the `Spatial Audio` flag informs the platform to always render the audio for the local participant.  

### Step 3. Get a Dolby.io token from the dashboard
You should have signed up in Dolby.io by now. In the app that is automatically created for you in the dashboard, acquire a temporary Client Access Token and paste in the `Initialize` node. For security reasons the token you acquired will expire in 12 hours, you will have to provide a new token after the expiration. 

Once this is all done, close the visual script editing window and run the app, if everything works you should be able to hear the audio coming from the Dolby.io service, this confirms you have proper setup of development environment as well as Unity plugin installed ready to go.

### Apple Silicon
If you are using Unity from a Apple Silicon Mac (e.g., M1), please be aware that currently the SDK is only distributed as x64 binary for Mac. You need to configure the Unity project under `File` > `Build Settings` and select `Intel 64bits` to make it work.  