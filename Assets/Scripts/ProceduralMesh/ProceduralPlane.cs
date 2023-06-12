using System;
using UnityEngine;

namespace ProceduralMeshGenerate
{
    public class ProceduralPlane
    {
        public Mesh GeneratePlane(Vector3 startPos, Vector3 endPos, float width)
        {
            Mesh mesh = new();

            int numVertices = 4;
            int numTriangles = 2;

            Vector3[] vertices = new Vector3[numVertices];
            Vector2[] uv = new Vector2[numVertices];
            int[] triangles = new int[numTriangles * 3];

            Vector3 offset = Vector3.Cross((endPos - startPos).normalized, Vector3.up).normalized * (width * 0.5f);

            vertices[0] = startPos - offset;
            vertices[1] = startPos + offset;
            vertices[2] = endPos + offset;
            vertices[3] = endPos - offset;

            uv[0] = new(0f, 0f);
            uv[1] = new(0f, 1f);
            uv[2] = new(1f, 1f);
            uv[3] = new(1f, 0f);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        public static implicit operator ProceduralPlane(Type v)
        {
            throw new NotImplementedException();
        }
    }
}
