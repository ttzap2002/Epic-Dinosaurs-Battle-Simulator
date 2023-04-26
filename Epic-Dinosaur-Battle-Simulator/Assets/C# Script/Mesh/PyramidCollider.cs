using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class PyramidCollider : MonoBehaviour
{
    public float width = 1f;    // szeroko�� podstawy ostros�upa
    public float height = 1f;   // wysoko�� ostros�upa
    public float depth = 1f;    // g��boko�� podstawy ostros�upa

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;
    void Start()
    {
        // pobierz referencje do komponent�w MeshFilter, MeshRenderer i MeshCollider
        //meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        // utw�rz siatk� tr�jk�t�w
        mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(width, 0, 0),
            new Vector3(0, 0, 0),                                   // wierzcho�ek D
            new Vector3(width / 2f, height, depth / 2f),

            new Vector3(width / 2f, 0, depth / 2f)                 // wierzcho�ek C
        };
        mesh.triangles = new int[] {
            0, 1, 2,            // tr�jk�t A-D-B
            0, 2, 3,            // tr�jk�t A-B-C
            2, 1, 3,            // tr�jk�t B-D-C
            0, 3, 1             // tr�jk�t A-C-D
        };
        mesh.RecalculateNormals();

        // ustaw siatk� tr�jk�t�w dla komponentu MeshFilter
        //meshFilter.mesh = mesh;
        
        // ustaw siatk� tr�jk�t�w dla komponentu MeshCollider
        meshCollider.sharedMesh = mesh;
    }
}

