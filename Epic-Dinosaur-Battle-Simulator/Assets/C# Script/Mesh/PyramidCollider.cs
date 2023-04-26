using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class PyramidCollider : MonoBehaviour
{
    public float width = 1f;    // szerokoœæ podstawy ostros³upa
    public float height = 1f;   // wysokoœæ ostros³upa
    public float depth = 1f;    // g³êbokoœæ podstawy ostros³upa

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;
    void Start()
    {
        // pobierz referencje do komponentów MeshFilter, MeshRenderer i MeshCollider
        //meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        // utwórz siatkê trójk¹tów
        mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(width, 0, 0),
            new Vector3(0, 0, 0),                                   // wierzcho³ek D
            new Vector3(width / 2f, height, depth / 2f),

            new Vector3(width / 2f, 0, depth / 2f)                 // wierzcho³ek C
        };
        mesh.triangles = new int[] {
            0, 1, 2,            // trójk¹t A-D-B
            0, 2, 3,            // trójk¹t A-B-C
            2, 1, 3,            // trójk¹t B-D-C
            0, 3, 1             // trójk¹t A-C-D
        };
        mesh.RecalculateNormals();

        // ustaw siatkê trójk¹tów dla komponentu MeshFilter
        //meshFilter.mesh = mesh;
        
        // ustaw siatkê trójk¹tów dla komponentu MeshCollider
        meshCollider.sharedMesh = mesh;
    }
}

