# Week 1 Lab 1

### Create Unity Project
New Project > Universal 3D (Core)
<img width="1000px" src="https://github.com/user-attachments/assets/bcf20625-ddb1-42c3-9c33-7254deebd9a1">
- In this project, we use only the version "2023.2.20f1"
<br>

### Project Structure
<img width="400px" src="https://github.com/user-attachments/assets/24cc3d55-35e8-4621-9baa-83775785ebe0">

- "Library" folder takes up most of the project's capacity (about 1.4 GB for an initial project)
<br>

### Unity.gitignore Setting
Contain only important folders named "Assets", "ProjectSettings", and "Packages"
Use an official [Unity.gitignore](https://github.com/github/gitignore/blob/main/Unity.gitignore) file to ignore massive folders
<br>

### Manipulating `GameObjects`
<img width="200px" alt="image" src="https://github.com/user-attachments/assets/624ccb00-0701-42be-9b2d-af9fa1f8e9c3">
<br>

### Coordinate System of the Unity
- Left Hand Coordinate
- X (Red), Y (Green), and Z (Blue)
- OpenGL, OpenCL use different coordinate systems, so coordinate conversion may be required during the transfer
<br>

### Rigidbody and Collider (Apply simple physics)
<img width="300px" alt="image" src="https://github.com/user-attachments/assets/ac4c2f28-0564-4435-82a7-ae398846f7f6">
<img width="300px" src="https://github.com/user-attachments/assets/d1021f70-2793-481f-bdbb-0ef4a85b45a5">
<br>

### Object Hierarchies
<img width="127px" src="https://github.com/user-attachments/assets/6b7f0da3-13ca-4cf6-9567-8e8b25ccc428">

- Multiple GameObjects can have a parent, child relationship hierarchically
<br>

### Prefabs
<img width="83px" alt="image" src="https://github.com/user-attachments/assets/6300391c-101f-415e-8a86-2bbb736dcb05">
<img width="200px" alt="image" src="https://github.com/user-attachments/assets/86caa3be-aaf8-42d2-b144-9032cdddeae7">

- Dragging a set of GameObjects into Assets creates a Prefab
- Conversely, dragging a Prefab into Hierarchy makes it easy to create multiple defined GameObjects from the same dictionary
<br>