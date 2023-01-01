## Initialize and connect to a demo conference
The Demo conference node allows connecting to a conference that has several bots injecting audio, making it easier to test the connection and project setup during the prototyping phase. This example demonstrates how to initialize, authenticate and connect to a demo conference.

To start fresh, you can create a new project from Unity Hub using one of the provided templates, such as `Core 3D`. The following video shows the workflow.

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/43ef2d4c32824acd952741b281e8c5c4" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe></div>

### Step 0. Install the Unity plugin
This guide assumes you have already installed the plugin. If you have not done so, please refer to [this article](../unity.md#installation) for more information then come back to this guide. 

### Step 1. Add the DolbyIO Manager and Script Machine components
Once you've added the `AppManager` GameObject to the scene, you can add the following two components to the `AppManager`.

- `DolbyIO Manager` provides access to the SDK functionalities.
- `Script Machine` is a component that contains the visual representation of a script that is a script graph.

### Step 2. Edit the script graph
The newly created script graph contains `On Start`, which is the initial triggering event. This event must be explicitly set as `coroutine` in order for it to work with the `Initialize` node. 

Add the following nodes to the script graph:
- Unity **String** literal, alternatively, add [Get Token](../visualscripting/nodes.md#get-token) node.
- [Initialize](../visualscripting/nodes.md#initialize).
- [Demo Conference](../visualscripting/nodes.md#demo-conference).

Connect the `On Start` event to the input trigger of `Initialize` node, of which the output trigger connects to the `Demo Conference` input trigger. 

To keep things simple for now, uncheck the `Spatial Audio` option in `Demo Conference` node. This is because unless you've provided your spatial position, the default platform behavior for spatial audio conference is `no rendering`, which means you won't hear any audio. Unchecking the `Spatial Audio` flag informs the platform to always render the audio for the local participant. 

### Step 3. Set up the Client Access Token
You should have signed up in Dolby.io by now. In the app that is automatically created for you in the dashboard, acquire a temporary Client Access Token and paste in the **String** literal then connect to the `Access Token` input of the `Initialize` node. For security reasons the token you acquired will expire in 12 hours, you will have to provide a new token after the expiration. 

The following visual scripting example illustrates how to connect the nodes together once you acquired a Client Access Token from the dashboard.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/string-token-initialize.png" width="450px">
</div>

Alternatively, you can add the [Get Token](../visualscripting/nodes.md#get-token) node to the graph, and pass the `App Key` and `App Secret` to the node. Connecting the output of the node to the `Access Token` input of the `Initialize` node will generate a Client Access Token from the Unity app and initialize the plugin.

> Please note using `Get Token` effectively includes the permanent app credential in your Unity app, which is not safe for production deployment. Please follow our [security best practices](https://docs.dolby.io/communications-apis/docs/guides-client-authentication) and setup a backend server to retrieve temporary access token on behalf of the Unity app. 

The following visual scripting example illustrates how to connect the nodes together if you decided to use the [Get Token](../visualscripting/nodes.md#get-token) node.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/get-token-initialize.png" width="450px">
</div>

Once this is all done, close the visual script editing window and run the app, if everything works you should be able to hear the audio coming from the Dolby.io service, this confirms you have properly configured the development environment with the Unity plugin installed ready to go.

### Apple Silicon
If you are using Unity from a Apple Silicon Mac (e.g., M1), please be aware that currently the SDK is only distributed as x64 binary for Mac. You need to configure the Unity project under `File` > `Build Settings` and select `Intel 64bits` to make it work.  