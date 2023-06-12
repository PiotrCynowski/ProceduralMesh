using UnityEngine;
using ProceduralMeshGenerate;
using ProceduralTexture;

namespace GenerateMesh
{
    public class GenerateMeshTest : MonoBehaviour
    {
        [Header("Generate Plane")]
        [SerializeField] private Transform planeStart;
        [SerializeField] private Transform planeEnd;
        [SerializeField] private float planeWidth;

        [Header("Generate Stack of Cubes")]
        [SerializeField] private Transform cubesStackPosition;
        [SerializeField] private Color[] stackCubesColorsArray;
        [SerializeField] private int stackCubesColorMatrix;
        [SerializeField] private float stackSingleCubeSize = 1f;
        [SerializeField] private float spaceBetweenCubes = 0.1f;
        [SerializeField] private int howManyCubesForStack = 4;

        [Header("Generate multiple Cubes randomly around position")]

        [SerializeField] private Transform cubesCenterPosition;
        [SerializeField] private float spread;
        [SerializeField] private Color[] singleCubesColorsArray;
        [SerializeField] private int singleCubesColorMatrix;
        [SerializeField] private float cubeSize = 1f;
        [SerializeField] private int howManyCubes = 5;

        private ProceduralCubes cubesStackGen;
        private ProceduralCubes singleCubesGen;
        private ProceduralPlane planeGen;
        private GenerateTexture2DColorMatrix textureGen;

        private void Awake()
        {
            cubesStackGen = new ProceduralCubes(cubeSize, spaceBetweenCubes, stackCubesColorMatrix);
            singleCubesGen = new ProceduralCubes(cubeSize, 0, singleCubesColorMatrix);
            planeGen = new ProceduralPlane();
            textureGen = new GenerateTexture2DColorMatrix();
        }

        private void Start()
        {
            GeneratePlane();
            GenerateCubesStack();
            GenerateSeparateCubes();
        }

        #region generate mesh
        private void GeneratePlane()
        {
            Mesh mesh = planeGen.GeneratePlane(planeStart.position, planeEnd.position, planeWidth);
            GameObject planeObject = new GameObject("Plane");
            planeObject.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
            planeObject.AddComponent<MeshFilter>().sharedMesh = mesh;
        }

        private void GenerateCubesStack()
        {
            Mesh mesh = cubesStackGen.GenerateCubesStack(howManyCubesForStack);
            Texture2D tex = textureGen.GenerateTexture2d(stackCubesColorsArray, stackCubesColorMatrix);
            GameObject stackObject = new GameObject("StackOfCubes");
            Material mat = new Material(Shader.Find("Standard"));
            mat.mainTexture = tex;
            stackObject.AddComponent<MeshRenderer>().material = mat;
            stackObject.AddComponent<MeshFilter>().sharedMesh = mesh;
            stackObject.transform.position = cubesStackPosition.position;
        }

        private void GenerateSeparateCubes()
        {
            Mesh[] meshArray = singleCubesGen.GenerateArrayCubes(howManyCubes);
            Texture2D tex = textureGen.GenerateTexture2d(singleCubesColorsArray, singleCubesColorMatrix);
            Material mat = new Material(Shader.Find("Standard"));
            mat.mainTexture = tex;
            Vector3 pos = cubesCenterPosition.position;
            Vector3 randomPos = new();
            float x, z = new();

            for (int i = 0; i < meshArray.Length; i++)
            {
                GameObject cubeObject = new GameObject("Cube");
                cubeObject.AddComponent<MeshRenderer>().material = mat;
                cubeObject.AddComponent<MeshFilter>().sharedMesh = meshArray[i];
                x = Random.Range(pos.x - spread, pos.x + spread);
                z = Random.Range(pos.z - spread, pos.z + spread);
                randomPos = new Vector3(x, 0.5f * cubeSize, z);
                cubeObject.transform.position = randomPos;
            }
        }
        #endregion
    }
}