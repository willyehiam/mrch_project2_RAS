# Release notes for MRCH Template

## v1.1

Date: 10.19.2024, by Shengyang

Github Commit Hash: [ToBeFilledNextTime]

Changes:

* Update the structure of the abstract scripts and wrappers. They are now under different namespaces.
* Changed the input from the mix of input manager and input system to sole Input System. Fixed the problem of Screen Touching. You now can test touch interactions in the editor runtime under both Game and Simulator modes.
* Improved the MapModel script. They should not be loaded into the built version to reduce the size of the package and improve the efficiency.
* Added ‘Start New Line When Overflow’ option to Simple TMP Typewriter to improve the typing process of especially English text. (It is not simple anymore hahaha.)
* **Added ‘XR Origin Editor Controller’, now you can use WASD to move in the editor, IJKL or right click mouse to rotate the camera to have a quick test of your interactions.**
* Made more variables are now open to inherit.
* Fixed the problem

Known Issues:

* Some initializations should not be done when starts but after localization finished. It would mostly affect some functions in MoveAndRotate when “back to initial position”.
* Some Text on the screen doesn’t appear correctly.
* You may fail to find the map model GameObject in the scene. You can RE-extract textures manually to fix the problem.

## v1.0

Date: 10.17.2024, by Shengyang

Github Commit Hash: 4b93f45441cbe655227272d7aca8c54fde75807b

Changes: 

* Finished the rough documentation and asked AI to improve it. So, you may see the style is not unified. I guess it’s fine... Anyway I will probably keep improve it later.