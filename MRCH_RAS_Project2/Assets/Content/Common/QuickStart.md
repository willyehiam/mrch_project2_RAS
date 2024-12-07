# Quick Start for This Template for MRCH

## Tips Before You Start:

1. The texts included in the project template are written in Markdown format. For an easier reading experience, consider using a Markdown reader or installing a Markdown extension in your programming IDE. With them you can see the structure of the Documentation.md and save your time.

2. It is recommended to use [GitHub Desktop](https://desktop.github.com/) for easier control of changes and updates from Shengyang’s side, or for team collaboration (further setup is needed).

3. Please avoid changing anything, especially in the Assets/Content/Common/Scripts folder.

4. Create your team folder under Assets/Content/[YOUR_UNIQUE_NAME]*. You can include subfolders like Models, Animations, Prefabs, Scenes, Scripts, etc., to keep everything organized. When submitting, place all your added content into your content folder, so it’s best to organize them as you go.

   [^[YOUR_UNIQUE_NAME\]]: It can be anything, your team nickname, the anime you all like... But remember to let Shengyang know it is you

   

## How to Start Building Your Virtual Experience on Cultural Heritage:

1. Find your assigned map under the Assets/Content/Common/Scenes/MapTemplate folder.
2. Copy (don’t move or cut!) it to Assets/Content/[YOUR_UNIQUE_NAME]/Scenes.
3. Rename it to something unique, then double-click to open the scene.
4. Let’s start creating!

## Add Your First Trigger Interaction

------

- Before you start, especially if you are responsible for managing the hierarchy or development, ensure you have a basic understanding of colliders, triggers, and rigidbodies.

- If not, these resources might help:

  - ##### [Colliders and Triggers in Unity — Understanding the Basics](https://christopherhilton88.medium.com/colliders-and-triggers-in-unity-understanding-the-basics-7192714f3440)

  - ##### [Unity Documentation - Collider](https://docs.unity3d.com/ScriptReference/Collider.html)

  - ##### Optional: [Unity Documentation - Rigidbody](https://docs.unity3d.com/ScriptReference/Rigidbody.html)

------

1. Right-click the XR Space and navigate to 3D Objects > Cube.
2. Rename it to avoid confusion later.
3. Disable the Mesh Renderer because we usually don’t need its visualization.
4. **Check ‘Is Trigger’ under the Box Collider.**
5. Click Add Component at the bottom, and navigate to **MRCH-Interact > Interaction Trigger**.
6. Check ‘Use Collider Trigger’.
7. Use the UnityEvent frames to create the actions that will occur when the event is triggered.

## Wait! What is a UnityEvent?

- Understanding UnityEvent is crucial for using this template effectively!
- These materials may help you better understand UnityEvent:
  - **[Unity Documentation - UnityEvents](https://docs.unity3d.com/Manual/UnityEvents.html)**
  - **[UnityEvents Explained in 4 Minutes](https://www.youtube.com/watch?v=djW7g6Bnyrc)**
  - Unity Forum - [UnityEvent, Where Have You Been All My Life?](https://discussions.unity.com/t/unityevent-where-have-you-been-all-my-life/577808) (lol)

OK... I believe you now have a basic idea of what to do... I hope so, hahaha!