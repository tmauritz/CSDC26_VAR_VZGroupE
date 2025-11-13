# Readme
This is repository for our VR Project.

For my fellow devs:
## Prefab Usage
### Button
- drag Button into Scene, position as needed
- Button Events are exposed via Unity Event Wrapper
### Grabbables and Sockets
Welcome to the fuckery of physics and layers.
#### Step 1: Build your Grabbable
- For a Grabbable you want to attach to other Grabbables, see the AttachableCube Prefab
	- Make sure the Layer is set to *Attachable*
	- Uncheck "Kinematic while selected" in the Grabbable-Script
- For a Grabbable you want to put Sockets on, see the BaseCube
	- do NOT put Sockets on Attachable Objects
#### Step 2: Build your Socket
See the Socket Prefab.
- SocketVisualiser is purely visual, can be deleted and replaced with other visuals, but make sure to re-link them in the Socket script.
