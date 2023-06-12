using UnityEngine;

namespace ProceduralMeshGenerate
{
    public class ProceduralCubes
    {
        private float cubeSize = 1f;
        private float spaceBetweenCubes = 0.1f;
        private int cubeTexColorMatrix = 4;

        public ProceduralCubes(float _cubeSize, float _spaceBetweenCubes, int _cubeTextColorMatrix)
        {
            cubeSize = _cubeSize;
            spaceBetweenCubes = _spaceBetweenCubes;
            cubeTexColorMatrix = _cubeTextColorMatrix;
        }

        public Mesh GenerateCubesStack(int cubeCount)
        {
            Mesh meshCubes = new();

            Vector3[] vertices = new Vector3[cubeCount * 8];
            Vector2[] uv = new Vector2[cubeCount * 8];
            int[] triangles = new int[cubeCount * 36];

            for (int i = 0; i < cubeCount; i++)
            {
                float yPosition = i * (cubeSize + spaceBetweenCubes);
                GenerateCubeVertices(vertices, i, yPosition);
                GenerateCubeUV(uv, i, i);
                GenerateCubeTriangles(triangles, i);
            }

            meshCubes.Clear();
            meshCubes.vertices = vertices;
            meshCubes.uv = uv;
            meshCubes.triangles = triangles;
            meshCubes.RecalculateNormals();

            return meshCubes;
        }

        public Mesh[] GenerateArrayCubes(int amount)
        {
            Mesh[] cubesArray = new Mesh[amount];

            Vector3[] vertices = new Vector3[8];
            Vector2[] uv = new Vector2[8];
            int[] triangles = new int[36];

            for (int i = 0; i < amount; i++)
            {
                cubesArray[i] = new();

                GenerateCubeVertices(vertices, 0, 0 - (0.5f * cubeSize));
                GenerateCubeUV(uv, 0, i);
                GenerateCubeTriangles(triangles, 0);

                cubesArray[i].vertices = vertices;
                cubesArray[i].uv = uv;
                cubesArray[i].triangles = triangles;
                cubesArray[i].RecalculateNormals();
            }
            return cubesArray;
        }

        private void GenerateCubeVertices(Vector3[] vertices, int index, float yPosition)
        {
            float halfCubeSize = cubeSize / 2f;
            float cubeHeight = cubeSize;

            int startIndex = index * 8;

            vertices[startIndex + 0] = new Vector3(-halfCubeSize, yPosition, -halfCubeSize);
            vertices[startIndex + 1] = new Vector3(-halfCubeSize, yPosition, halfCubeSize);
            vertices[startIndex + 2] = new Vector3(halfCubeSize, yPosition, halfCubeSize);
            vertices[startIndex + 3] = new Vector3(halfCubeSize, yPosition, -halfCubeSize);

            vertices[startIndex + 4] = new Vector3(-halfCubeSize, yPosition + cubeHeight, -halfCubeSize);
            vertices[startIndex + 5] = new Vector3(-halfCubeSize, yPosition + cubeHeight, halfCubeSize);
            vertices[startIndex + 6] = new Vector3(halfCubeSize, yPosition + cubeHeight, halfCubeSize);
            vertices[startIndex + 7] = new Vector3(halfCubeSize, yPosition + cubeHeight, -halfCubeSize);
        }

        private void GenerateCubeUV(Vector2[] uv, int index, int colorIndex)
        {
            int startIndex = index * 8;
            int colorIndexX = colorIndex % cubeTexColorMatrix;
            int colorIndexY = colorIndex / cubeTexColorMatrix;

            float uvWidth = 1f / cubeTexColorMatrix;
            float uvHeight = 1f / cubeTexColorMatrix;

            // Bottom side
            uv[startIndex + 0] = new Vector2(colorIndexX * uvWidth, colorIndexY * uvHeight);
            uv[startIndex + 1] = new Vector2((colorIndexX + 1) * uvWidth, colorIndexY * uvHeight);
            uv[startIndex + 2] = new Vector2((colorIndexX + 1) * uvWidth, (colorIndexY + 1) * uvHeight);
            uv[startIndex + 3] = new Vector2(colorIndexX * uvWidth, (colorIndexY + 1) * uvHeight);

            // Front side
            uv[startIndex + 4] = new Vector2(colorIndexX * uvWidth, (colorIndexY + 1) * uvHeight);
            uv[startIndex + 5] = new Vector2((colorIndexX + 1) * uvWidth, (colorIndexY + 1) * uvHeight);
            uv[startIndex + 6] = new Vector2(colorIndexX * uvWidth, colorIndexY * uvHeight);
            uv[startIndex + 7] = new Vector2((colorIndexX + 1) * uvWidth, colorIndexY * uvHeight);
        }

        private void GenerateCubeTriangles(int[] triangles, int index)
        {
            int startIndex = index * 36;
            int vertexStartIndex = index * 8;

            // Bottom
            triangles[startIndex + 0] = vertexStartIndex + 3;
            triangles[startIndex + 2] = vertexStartIndex + 7;
            triangles[startIndex + 3] = vertexStartIndex + 0;
            triangles[startIndex + 4] = vertexStartIndex + 4;
            triangles[startIndex + 5] = vertexStartIndex + 7;
            triangles[startIndex + 1] = vertexStartIndex + 0;

            // Back
            triangles[startIndex + 6] = vertexStartIndex + 7;
            triangles[startIndex + 7] = vertexStartIndex + 5;
            triangles[startIndex + 8] = vertexStartIndex + 6;
            triangles[startIndex + 9] = vertexStartIndex + 5;
            triangles[startIndex + 10] = vertexStartIndex + 7;
            triangles[startIndex + 11] = vertexStartIndex + 4;

            // Right
            triangles[startIndex + 18] = vertexStartIndex + 3;
            triangles[startIndex + 19] = vertexStartIndex + 6;
            triangles[startIndex + 20] = vertexStartIndex + 2;
            triangles[startIndex + 21] = vertexStartIndex + 6;
            triangles[startIndex + 22] = vertexStartIndex + 3;
            triangles[startIndex + 23] = vertexStartIndex + 7;
        }
    }
}