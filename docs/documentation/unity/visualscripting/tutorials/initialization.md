## Initialization

This guide demonstrates how to initialize, authenticate, and connect to a demo conference using Visual Scripting. The demo conference has several bots injecting audio, making it easier to test the connection and project setup during the prototyping phase. 

To start fresh, you can create a new project from the Unity Hub. This guide uses the `Third Person` template in Unity version 2021.3.6f1 LTS. 

## Set up up the DolbyIO Manager
The Unity plugin provides access to an instance of the SDK through a custom `DolbyIO Manager` manager.
To add the SDK to Unity, follow these steps:

1. Create an empty game object in your scene and provide a name for the object. In this example, we call the object `AppManager`:

2. Select the created game object and select `Component` > `DolbyIO` > `DolbyIO Manager` from the Unity menu. This step adds the `DolbyIO Manager` to the game object.

## Set up the initial visual scripting
1. Select the `AppManager` object and add a `Script Machine` by choosing `Component` > `Visual Scripting` > `Script Machine` from the Unity menu.

2. Add a new script to the `Script Machine` component. In the component, click the `New` button and name and save your new script. Start editing the script with the `Edit Graph` button.
    The newly created script graph contains `On Start`, which is the initial triggering event. This event must be explicitly set as `Coroutine` in order for it to work with the `Initialize` node. Select the node and turn on the `Coroutine` check box.

3. Right-clicking on the script background and add these new nodes to the script:
    * `Dolby IO` > `Initialize`
    * `Dolby IO` > `Demo Conference`
    * `String Literal`, which can be found from a search in the new node window
        * For a more convenient method, add the [Get Token](../nodes.md#get-token) node. See below for more details.

4. Add these connections:
    * Connect the `On Start` event to the input trigger of the `Initialize` node
    * Connect the output of the `String` node to the `Access Token` input of the `Initialize` node
    * Connect the `Initialize` event to the input trigger of the `Demo Conference` node
    * Set `Spatial Audio Style` to `Shared` to use shared scene. 

## Set up the Client Access Token
You should have signed up in Dolby.io by now. In the app that is automatically created for you in the Dolby.io dashboard, acquire a temporary Client Access Token and paste it into the `String` literal node. For security reasons, the token you acquired expires after 12 hours. You will have to provide a new token after the expiration.

The following visual scripting example illustrates how to connect the nodes together once you acquired a Client Access Token from the dashboard.
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/string-token-initialize.png" width="450px">
</div>

Alternatively, you can add the [Get Token](../nodes.md#get-token) node to the graph, and pass the `App Key` and `App Secret` to the node. Connecting the output of the node to the `Access Token` input of the `Initialize` node will generate a client access token from the Unity app and initialize the plugin.

> ⚠️ Please note that using `Get Token` effectively includes the permanent app credential in your Unity app, which is not safe for production deployment. Please follow our [security best practices](https://docs.dolby.io/communications-apis/docs/guides-client-authentication) and set up a backend server to retrieve a temporary access token on behalf of the Unity app. 

The following visual scripting example illustrates how to connect the nodes together if you decided to use the [Get Token](../nodes.md#get-token) node:
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/samples/demo/get-token-initialize.png" width="450px">
</div>

Once this is all done, run the app. If everything works, you should be able to hear the audio coming from the Dolby.io service, which confirms you have properly configured the development environment with the Unity plugin installed and are ready to go. 
