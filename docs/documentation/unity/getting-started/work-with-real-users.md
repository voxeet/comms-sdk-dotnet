## Work with real users
Now that you have learned how to render remote participants and manipulate the spatial audio scene, it's time to modify the project to work with real remote users. 

<div style="position: relative; padding-bottom: 56.25%; height: 0;"><iframe src="https://www.loom.com/embed/e01bdefa0dee4713be1138ccd29749b0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe></div>

Replace the **Demo Conference** node with the **Conference** node, and connect the **Initialize** node to it as shown below. Then enter `spatial-demo` as the **Conference Alias**. Make sure **Spatial Audio Style** is set to `Shared`.

Go to Dolby.io dashboard and click on **Run a Demo**. Under the **Launch a pre-built demo application with no code**, find **Spatial Audio for Web SDK**, and click on **Launch demo**. Once you've loaded the web test app, enter one of the bots' name, for example, `JC`, then click on **Join conference**.

Now run the Unity project, you should see one user `JC` in the game and you should be able to hear the audio in real-time.

> ⓘ In order for the audio and visual positions to completely align, you will need to communicate the participants position between various clients. This is not covered in this tutorial. 
