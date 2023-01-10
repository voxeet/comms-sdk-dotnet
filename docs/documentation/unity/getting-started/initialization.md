## Initialization

This guide demonstrates how to initialize, authenticate, and connect to a demo conference. The demo conference has several bots injecting audio, making it easier to test the connection and project setup during the prototyping phase. 

To start fresh, you can create a new project from the Unity Hub. This guide use the `Third Person` template in Unity version 2021.3.6f1 LTS. The following video shows the workflow:

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/43ef2d4c32824acd952741b281e8c5c4" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe></div>

## Set up up the DolbyIO Manager
The Unity plugin provides access to an instance of the SDK through a custom `DolbyIO Manager` manager.
To add the SDK to Unity, follow these steps:

1. Create an empty game object in your scene and provide a name for the object. In this example, we call the object `AppManager`:
    <div style="text-align:left">
        <img style="padding:25px 0" src="~/images/unity_1.png" width="350px">
    </div>

2. Select the created game object and select `Component` > `DolbyIO` > `DolbyIO Manager` from the Unity menu. This step adds the `DolbyIO Manager` to the game object.
    <div style="text-align:left">
        <img style="padding:25px 0" src="~/images/unity_2.png" width="700px">
    </div>

## Set up the initial visual scripting
1. Select the `AppManager` object and add a `Script Machine` by choosing `Component` > `Visual Scripting` > `Script Machine` from the Unity menu.

2. Add a new script to the `Script Machine` component. In the component click the `New` button and name and save your new script. Start editing the script with the `Edit Graph` button.

    The newly created script graph contains `On Start`, which is the initial triggering event. This event must be explicitly set as `Coroutine` in order for it to work with the `Initialize` node. Select the node and turn on the `Coroutine` check box.

3. Right-clicking on the script background and add these new nodes to the script:

    * `Dolby IO` > `Initialise`
    * `Dolby IO` > `Demo Conference`
    * `String Literal`, which can be found from a search in the new node window
        * For a more secure method, add the [Get Token](../visualscripting/nodes.md#get-token) node. See below for more details.

    Add these connections:

    * Connect the `On Start` event to the input trigger of the `Initialize` node
    * Connect the output of the `String` node to the `Access Token` input of the `Initialize` node
    * Connect the `Initialize` event to the input trigger of the `Demo Conference` node

> To keep things simple for now, set the `Spatial Audio` option to `none` in the `Demo Conference` node. This is because unless you have provided your spatial position, the default platform behavior for the spatial audio conference is **no rendering**, which means you will not hear any audio. Setting the `Spatial Audio` flag to `none` informs the platform to always render the audio for the local participant. In the next sections we will explain how to setup the spatial audio conference.

## Step 4. Set up the Client Access Token
You should have signed up in Dolby.io by now. In the app that is automatically created for you in the Dolby.io dashboard, acquire a temporary Client Access Token and paste in the `String` literal node. For security reasons, the token you acquired will expire in 12 hours. You will have to provide a new token after the expiration.

The following visual scripting example illustrates how to connect the nodes together once you acquired a Client Access Token from the dashboard.
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
