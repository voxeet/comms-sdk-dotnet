## Initialization

This guide demonstrates how to initialize, authenticate, and connect to a demo conference. The demo conference has several bots injecting audio, making it easier to test the connection and project setup during the prototyping phase. 

To start fresh, you can create a new project from Unity Hub using the `Third Person` template. The following video shows the workflow:

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/43ef2d4c32824acd952741b281e8c5c4" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe></div>

## 1. Set up the AppManager
To add the SDK to Unity, create an empty GameObject in your scene and provide a name for the object. In this example, we call the object `AppManager`.

Then, you can add the following two components to the `AppManager`:

- `Dolby.io Manager`: Provides access to the SDK functionalities.
- `Script Machine`: Contains the visual representation of a script that is a script graph.

## 3. Set up the initial visual scripting
The newly created script graph contains `On Start`, which is the initial triggering event. This event must be explicitly set as `coroutine` in order for it to work with the `Initialize` node. 

Add the following nodes to the script graph:
- Unity **String** literal, alternatively, add the [Get Token](../visualscripting/nodes.md#get-token) node.
- [Initialize](../visualscripting/nodes.md#initialize).
- [Demo Conference](../visualscripting/nodes.md#demo-conference).

Connect the `On Start` event to the input trigger of the `Initialize` node, of which the output trigger connects to the `Demo Conference` input trigger. 

> To keep things simple for now, set the `Spatial Audio` option to `none` in the `Demo Conference` node. Unless you have provided your spatial position, the default platform behavior for the spatial audio conference is **no rendering**, which means you will not hear any audio. Setting the `Spatial Audio` flag to `none` informs the platform to always render the audio for the local participant. In the next sections we will explain how to set up the spatial audio conference.

## 4. Set up the client access token
You should have signed up for a Dolby.io by now. In the app that is automatically created for you in the dashboard, acquire a temporary client access token and paste in the **String** literal. Then, connect to the `Access Token` input of the `Initialize` node. For security reasons, the token you acquired will expire in 12 hours. You will have to provide a new token after the expiration. 

The following visual scripting example illustrates how to connect the nodes together once you acquired a client access token from the dashboard:
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/string-token-initialize.png" width="450px">
</div>

Alternatively, you can add the [Get Token](../visualscripting/nodes.md#get-token) node to the graph, and pass the `App Key` and `App Secret` to the node. Connecting the output of the node to the `Access Token` input of the `Initialize` node will generate a client access token from the Unity app and initialize the plugin.

> ⚠️ Please note that using `Get Token` effectively includes the permanent app credential in your Unity app, which is not safe for production deployment. Please follow our [security best practices](https://docs.dolby.io/communications-apis/docs/guides-client-authentication) and set up a backend server to retrieve a temporary access token on behalf of the Unity app. 

The following visual scripting example illustrates how to connect the nodes together if you decided to use the [Get Token](../visualscripting/nodes.md#get-token) node:
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/get-token-initialize.png" width="450px">
</div>

Once this is all done, run the app. If everything works, you should be able to hear the audio coming from the Dolby.io service, which confirms you have properly configured the development environment with the Unity plugin installed and are ready to go. 
