# MRCH Template Documentation + Manual

## Tips Before You Start

- Please read the `QuickStart.md` file in the same folder before proceeding.
- Some technical details are mentioned in `Scripts/Readme.md`.

------

## Where to Find These Components

**Inspector => Add Component => MRCH-Interact**
OR
**Inspector => Add Component => \*Search\***

## Interaction Triggers

### Collider Trigger

- **Usage**: This trigger activates three events: `Trigger First Enter`, `Trigger Enter (each time)`, and `Trigger Exit`. These events are related to the Trigger component (a Collider with `isTrigger` enabled) on the same GameObject.

- **Requirement**: The GameObject using the Interaction Trigger with `Use Collider Trigger` enabled must have a Collider with `isTrigger` set to true.

- Variables:

  - `Use Collider Trigger` (boolean): Enables the trigger functionality.

- Functions:

  - `On Trigger First Enter`: Triggered the **first time** the player enters the collider. The “first time” flag resets when the scene or app reloads.
  - `On Trigger Enter`: Triggered each time the player enters the collider.
  - `On Trigger Exit`: Triggered each time the player exits the collider.

### Distance Trigger

- **Usage**: Activates three events: `Distance First Enter`, `Distance Enter (each time)`, and `Distance Exit`.

- **Requirement**: None.

- Variables:

  - `Use Distance Trigger` (boolean): Enables the trigger functionality.
  - `Distance` (float): The distance within which the events are triggered. Be aware that the scale ratio may differ when scanning, so testing the ratio between the real world and the scanned point cloud is recommended. (XD)
  
- Functions:

  - `On Distance First Enter`: Triggered the **first time** the player enters the distance range. The “first time” flag resets when the scene or app reloads.
  - `On Distance Enter`: Triggered each time the player enters the distance range.
  - `On Distance Exit`: Triggered each time the player exits the distance range.

### LookAt Trigger

- **Usage**: Activates three events: `Look At First Enter`, `Look At Enter (each time)`, and `Look At Distance Exit`.

- **Requirement**: None.

- Variables:

  - `Use LookAt Trigger` (boolean): Enables the trigger functionality.
  - `Look At Angle` (float): The angle between the player’s line of sight and the object. Imagine two rays: one from the player's viewpoint and the other aligned with the object’s center. The angle between these two rays is calculated. This angle threshold is one of the two conditions that trigger the events.
  - `Look At Distance` (float): The distance within which the events are triggered. As with distance triggers, ensure you test the real-world-to-scanned ratio if needed. (XD)
  
- Functions:

  - `On Look At First Enter`: Triggered when the player enters the `Look At Distance` range and the angle is below the `Look At Angle` for the **first time**. The “first time” flag resets when the scene or app reloads.
  - `On Look At Enter`: Triggered each time the player enters the `Look At Distance` range and the angle is below the `Look At Angle`.
  - `On Look At Distance Exit`: Triggered when the player **exits the Look At Distance range** (the angle is not considered).

------

### Event Triggers

#### Start Trigger

- **Usage**: Triggers the `On Start` event.

- **Requirement**: None.

- Variables:

  - `Use Start Trigger` (boolean): Enables the trigger functionality.

- Functions:

  - `On Start`: Triggered when the Unity MonoBehaviour `Start()` method is called (i.e., during the frame when a script is first enabled, right before any Update methods are called). [(ref)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html)

#### On Enable Trigger

- **Usage**: Triggers the `On Enable` event.

- **Requirement**: None.

- Variables:

  - `Use On Enable Trigger` (boolean): Enables the trigger functionality.

- Functions:

  - `On Enable`: Triggered when the Unity MonoBehaviour `OnEnable()` method is called (i.e., when the object becomes enabled and active). [(ref)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html)

#### Update Trigger

- **Usage**: Triggers the `On Update` event.

- **Requirement**: None.

- Variables:

  - `Use Update Trigger` (boolean): Enables the trigger functionality.

- Functions:

  - `On Update`: Triggered when the Unity MonoBehaviour `Update()` method is called (i.e., every frame, if the MonoBehaviour is enabled). [(ref)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html)

#### On Disable Trigger

- **Usage**: Triggers the `On Disable` event.

- **Requirement**: None.

- Variables:

  - `Use On Disable Trigger` (boolean): Enables the trigger functionality.

- Functions:

  - `On Disable`: Triggered when the Unity MonoBehaviour `OnDisable()` method is called (i.e., when the behaviour becomes disabled or destroyed). [(ref)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html)

------

### Debugging

#### debugMode

- **Type**: boolean
  If enabled, all events will log messages via `Debug.Log()` for easier debugging.

------

## Unity Event Library

### NamedUnityEvent

- **Class**: *NamedUnityEvent*

- Variables:

  - `eventName` (string): The name of the event.
  - `waitBeforeInvoke` (float): Time to delay the event trigger.
  - `unityEvent` (UnityEvent): The event to be triggered.

### NamedUnityEventSequence

- **Class**: *NamedUnityEventSequence*

- Variables:

  - `sequenceName` (string): The name of the sequence.
- `namedUnityEventSequence` (NamedUnityEvent[]): A sequence of NamedUnityEvents. Each event can have a delay (`waitBeforeInvoke`) before triggering.

### Public Methods

- **TriggerEventByName**(string eventName): Triggers a NamedUnityEvent by its name.
- **TriggerSequenceByName**(string sequenceName): Triggers a NamedUnityEventSequence by its name.

------

## Move and Rotate

### Inspector Settings

#### Move Options

- **Move Target** (Transform): The target to move to. **Required** if movement is needed.
- **Move Speed** (float): Reference move speed. If a move type other than `Linear` is used, the speed is only for reference.
- **Move For Once On Enable** (boolean): Determines if the object moves to the Move Target upon enabling. Use `MoveBackForOnce()` or `JumpBackToInitialPosition()` for multiple movements.
- **Move Forth And Back On Enable** (boolean): Determines if the object moves back and forth between its original position and the Move Target upon enabling.
- **Move Type** (enum Dotween.MoveType): Default is `In Out Sine`. Experiment with other types to see different effects.

#### Rotate Options

- **Keep Rotating On Enable** (boolean): Determines if the object continues rotating along an axis, with each rotation cycle lasting the specified `Rotate Duration`.
- **Rotation Axis** (Vector3): The axis along which the object will rotate.
- **Rotate Duration** (float): Time in seconds for a full rotation.
- **Rotate Type** (enum Dotween.MoveType): Default is `Linear`.

### Public Methods

- **MoveForOnce()**: Moves the object to the Move Target once.
- **MoveBackForOnce()**: Moves the object back to its original position once.
- **JumpBackToInitialPosition()**: Instantly sets the object back to its initial position.
- **MoveForthAndBack()**: Moves the object between its initial and target positions back and forth.
- **RotateObject()**: Starts rotating the object.
- **StopMovement()**: Stops the object’s movement.
- **StopRotation()**: Stops the object’s rotation.

------

## Object Toolset

### Public Methods

- **ToggleComponentEnabled(Component)**: Toggles the enabled status of a component (if toggleable).
- **ToggleObjectEnabled(GameObject)**: Toggles the active status of a GameObject. Inactive GameObjects will also deactivate their children, although their children can retain an active self-state (`GameObject.activeSelf`).

---

### Image Fade

This component allows fading effects on a `Raw Image`, `Image`, or `SpriteRenderer`.

#### Inspector Variables

##### secondsToFade

- **Type**: `float/seconds`
- **Description**: The duration of the fade in or fade out effect.

##### fadeInOnAwake

- **Type**: `boolean`
- **Description**: If enabled, the image will start fully transparent and fade in over the `secondsToFade` duration when the GameObject is enabled.

#### Public Methods

##### FadeIn()

- **Description**: Sets the image to fully transparent and then fades it in to full opacity over the `secondsToFade` duration.

##### FadeOut()

- **Description**: Sets the image to fully opaque and then fades it out to fully transparent over the `secondsToFade` duration.

##### SetTimeToFade(float)

- **Parameter**: `float` - New fade duration in seconds.
- **Description**: Updates the `secondsToFade` value.

------

### Keep Facing To Cam

This component makes an object always face the main camera (i.e., the player). Be cautious when the player can "step" on the object, as it may result in unexpected rotation behavior.

#### Inspector Variables

##### lockYAxis

- **Type**: `boolean`
- **Description**: If enabled, the object will only rotate on the X and Z axes, with the Y-axis locked.

##### faceToCamOnEnable

- **Type**: `boolean`
- **Description**: If enabled, the object will begin facing the camera as soon as it is activated.

#### Public Methods

##### SetFaceToCam(bool)

- **Parameter**: `bool`
- **Description**: Sets whether the object should face the camera.

------

### Shining Text

This component animates text with a shining effect.

#### Inspector Variables

##### cycleTime

- **Type**: `float`
- **Description**: Duration of one full shine cycle.

##### playOnAwake

- **Type**: `boolean`
- **Description**: If enabled, the text will start shining immediately when the object is activated.

#### Public Methods

##### SetShiningText(bool)

- **Parameter**: `bool`
- **Description**: Starts or stops the shining effect based on the provided value.

------

### SimpleTmpTypewriter

This component simulates a typewriter effect for both `TextMeshPro - Text` and `TextMeshPro UGUI`.

#### Inspector Variables

##### Content to Type

- **Type**: `string`
- **Description**: The content that will be typed in the `TextMeshPro` component.
- **Note**: It’s recommended not to place content directly into the TMP text input field, as it will be cleared when typing begins. However, you may find a way to take use of it.

##### Type Speed

- **Type**: `float`
- **Description**: The delay (in seconds) between typing each character.

#### Settings

##### Start New Line When Overflow

- **Type**: `boolean`
- **Description**: 
- It will start a new line in advance if the text overflows when typing the next word. It is recommended to enable, especially if it is in English-like language. However, if the width is too short or it is Chinese, it might be fine to disable it.

##### Type On Enable

- **Type**: `boolean`
- **Description**: If enabled, typing starts when the object is activated.

##### Only Type For The First Time

- **Type**: `boolean`
- **Description**: If enabled, the type-on-enable action will only occur once. If `Save Cross Scene` is disabled, it resets when the scene is reloaded.

##### Save Cross Scene

- **Type**: `boolean`
- **Description**: If enabled, the first-time typing status will persist across scene reloads.

##### Type Sound

- **Type**: `AudioClip`, optional
- **Description**: The sound effect to be played with each typed character. Requires an `AudioSource` on the same object.

#### Public Methods

##### StartTyping()

- **Description**: Begins the typing effect.

##### FinishTyping()

- **Description**: Instantly completes the typing, displaying the full content in TMP.

------

### TouchableManager

Manages interaction with touchable objects in a scene.

#### Inspector Variables

##### Touchable Layer

- **Type**: `enum`
- **Description**: Specifies the layer for touchable objects.

##### Universal Touch Event

- **Type**: `UnityEvent`
- **Description**: Event triggered whenever any object on the touchable layer is touched.

##### Universal Return Event

- **Type**: `UnityEvent`
- **Description**: Event triggered whenever any object marked as "returnable" is touched.

#### Settings

##### Touch Range

- **Type**: `float`, range: 0-300
- **Description**: The maximum range in which touch input is considered valid.

##### Disable Touch Of Other Objects

- **Type**: `boolean`
- **Description**: If enabled, players must touch a return object before interacting with other objects.

#### Public Methods

##### OnReturn()

- **Description**: Triggers the `UniversalReturnEvent` and resets the touchable status if `Disable Touch Of Other Objects` is enabled.

------

### TouchableObject

A component for objects that respond to touch events.

#### Inspector Variables

##### isReturn

- **Type**: `boolean`
- **Description**: If enabled, this object functions as a "return" object and will trigger the `onReturnEvent`.

##### On TouchEvent

- **Type**: `UnityEvent`
- **Description**: Triggered when a successful touch on this object occurs, provided `isReturn` is disabled.

##### On Return Event

- **Type**: `UnityEvent`
- **Description**: Triggered when a successful touch on this object occurs if `isReturn` is enabled.

# Environmental Occlusion Culling

### Description

To bring better AR experience, or want your content to have a better connection to reality (Wow that’s Mixed Reality!). The environmental occlusion is essential element. In the template, Prof. Zhang wrote a out-of-box shader to use for the environmental buildings to mask the virtual content. You can find it at `Assets/Plugins/Occlusion/Occlusion_Mat.mat`, or in your MapTemplate, the invisible cubes under `XR Space>Occlusion Culling` all have been assigned with this material. 

### How to enable the environmental occlusion culling

1. Make sure the shapes that mimics the surrounding buildings has been assigned with the material mentioned above. 
2. The visible and culling-able objects are not using the default materials, because they are not adjustable. 
3. **Go to the materials (component), then navigate to ‘Advanced Options’, adjust the ‘Sorting Priority’ higher**. 
4. All done and you can test now!

# Optional: Modify or Inherit Common Scripts

Most of the scripts in the common folders are ‘abstract’, or say inheritable, and most of the functions are ‘virtual’ or say overridable. 

Here are some materials you can learn from to know about how inheriting and overriding work in Unity C#:

* [C# Overriding in Unity! - Intermediate Scripting Tutorial](https://youtu.be/h0J4gs4DW5A?si=jgqt5dqfbGeZA4xB)
* [override (C# reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override)
* [Protected, Virtual, Abstract methods - What are they and when to use them?](https://www.reddit.com/r/Unity3D/comments/5rmj0v/protected_virtual_abstract_methods_what_are_they/) 

If you get lost and you are sure that you need to modify the common scripts, you can of course ask Shengyang for help!
