# Dolby.io Communications .NET SDK

The Dolby.io Communications .NET SDK allows creating high-quality video conferencing applications with spatial audio. The SDK is especially useful for building game engines and virtual spaces for collaboration. It allows placing participants spatially in a 3D-rendered audio scene and hear the participants' audio rendered at their locations.

The .NET SDK depends on the C++ SDK for core functionalities and communicates with the Dolby.io backend to provide conferencing functionalities, such as opening and closing sessions, joining and leaving conferences, sending and receiving messages, and injecting and receiving WebRTC media streams. An additional advantage of the SDK is the [spatial audio](https://docs.dolby.io/communications-apis/docs/guides-spatial-audio) and spatial audio styles support. The spatial audio styles allow you to either locally set remote participants' positions or create a spatial scene shared by all participants, where the relative positions among participants are calculated by the Dolby.io server.

The .NET SDK can connect to only one conference at a time. Joining multiple conferences at a time requires running multiple instances of the application.

To get started quickly, follow the [Readme](../../Readme.md) guide.

