using UnityEngine;
using ProceduralMeshGenerate;
using ProceduralTexture;

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
        cubesStackGen = new(cubeSize, spaceBetweenCubes, stackCubesColorMatrix);
        singleCubesGen = new(cubeSize, 0, singleCubesColorMatrix);

        planeGen = new();

        textureGen = new();
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

        var obj = new GameObject();

        obj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        obj.AddComponent<MeshFilter>().sharedMesh = mesh;

        obj.name = "Plane";
    }

    private void GenerateCubesStack()
    {
        Mesh mesh = cubesStackGen.GenerateCubesStack(howManyCubesForStack);
        Texture2D tex = textureGen.GenerateTexture2d(stackCubesColorsArray, stackCubesColorMatrix);

        var obj = new GameObject();

        Material mat = new Material(Shader.Find("Standard"));
        mat.mainTexture = tex;

        obj.AddComponent<MeshRenderer>().material = mat;
        obj.AddComponent<MeshFilter>().sharedMesh = mesh;

        obj.name = "StackOfCubes";

        obj.transform.position = cubesStackPosition.position;
    }

    private void GenerateSeparateCubes()
    {
        Mesh[] meshArray = singleCubesGen.GenerateArrayCubes(howManyCubes);
        Texture2D tex = textureGen.GenerateTexture2d(singleCubesColorsArray, singleCubesColorMatrix);

        Material mat = new Material(Shader.Find("Standard"));
        mat.mainTexture = tex;

        Vector3 pos = cubesCenterPosition.position;

        for (int i = 0; i < meshArray.Length; i++)
        {
            var obj = new GameObject();
            obj.AddComponent<MeshRenderer>().material = mat;
            obj.AddComponent<MeshFilter>().sharedMesh = meshArray[i];

            obj.name = "Cube";

            float x = Random.Range(pos.x - spread, pos.x + spread);
            float z = Random.Range(pos.z - spread, pos.z + spread);

            Vector3 randomPos = new Vector3(x, 0.5f*cubeSize, z);

            obj.transform.position = randomPos;
        }
    }
    #endregion
}