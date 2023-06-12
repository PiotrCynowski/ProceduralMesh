# ProceduralMesh
This repository contains a simple project focused on procedural mesh generation and texture creation.

### - Mesh Generator
The mesh generator scripts provide functionality to generate meshes. The scripts return a mesh of a stack of cubes, an array of individual cube meshes with different UV coordinates, and a mesh for a plane.

The project's main scene can be found at Assets/Scenes/Main.

### Using the Main Scene
The main scene includes a prefab with the GenerateMeshScript. This prefab allows you to test mesh generation by configuring the following settings:

#### Plane Generation

Settings:
- Transforms reference for the plane's start and end points.
- Plane width

#### Single Cubes Generation
![alt text](https://github.com/PiotrCynowski/ProceduralMesh/blob/master/pic/cubes.png?raw=true)

Settings:
- Transforms reference for the random position of each cube around that transform.
- Array of colors for the cubes. All cubes will use the same texture, with only the UV coordinates varying.
- Size of a single cube.
- Number of cubes to be spawned.

#### Cubes Stack Generation
![alt text](https://github.com/PiotrCynowski/ProceduralMesh/blob/master/pic/stackCubes.png?raw=true)

Settings:
- Transforms reference for the position of the cube stack.
- Array of colors for the cube stack. The colors will be assigned from bottom to top.
- Matrix of colors for the stack to use.
- Size of a single cube.
- Space between cubes.
- Number of cube stacks to be created.
