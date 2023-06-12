# ProceduralMesh
simple project with procedural mesh and texture generation

### - Mesh Generator
mesh generator scripts returns Mesh with stack of cubes, and array of single cube Mesh with different UVs, also mesh for plane
texture generator script returns Texture2d

the main scene is in Assets/Scenes/Main
### - In main scene there is prefab with GenerateMeshScript
With this prefab we can test mesh generation by putting right settings

#### - Plane Generate
settings:
-transforms reference for plane start and plane end
-plane width

#### - Cubes Stack Generate
![alt text](https://github.com/PiotrCynowski/ProceduralMesh/blob/master/pic/cubes.png?raw=true)
settings:
-transforms reference for cube stack position
-array of colors for cube stack, colors will be assigned from bottom to top
-matrix of colors stack should use
-size of single cube
-space between cubes
-number of cubes stack will be created with

#### - Single Cubes Generate
![alt text](https://github.com/PiotrCynowski/ProceduralMesh/blob/master/pic/stackCubes.png?raw=true)
settings:
-transforms reference for cube random position around that transform
-array of colors for cubes, all cubes will use same texture, only UV will be different
-size of single cube
-how many cubes should be spawned
