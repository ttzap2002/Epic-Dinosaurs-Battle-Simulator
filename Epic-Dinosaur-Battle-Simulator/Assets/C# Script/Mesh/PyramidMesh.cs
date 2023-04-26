using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CustomPyramid : MonoBehaviour
{
    public int a = 0; // zmienna okreœlaj¹ca obrót ostros³upa

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[5];
        int[] triangles = new int[18];

        vertices[0] = new Vector3(0, 1, 0);
        vertices[1] = new Vector3(1, 0, 1);
        vertices[2] = new Vector3(-1, 0, 1);
        vertices[3] = new Vector3(-1, 0, -1);
        vertices[4] = new Vector3(1, 0, -1);

        switch (a)
        {
            case 1:
                vertices[1] = Quaternion.Euler(0, 45, 0) * vertices[1];
                vertices[2] = Quaternion.Euler(0, -45, 0) * vertices[2];
                vertices[3] = Quaternion.Euler(0, -135, 0) * vertices[3];
                vertices[4] = Quaternion.Euler(0, 135, 0) * vertices[4];
                break;
            case 2:
                vertices[1] = Quaternion.Euler(45, 0, 0) * vertices[1];
                vertices[2] = Quaternion.Euler(-45, 0, 0) * vertices[2];
                vertices[3] = Quaternion.Euler(-135, 0, 0) * vertices[3];
                vertices[4] = Quaternion.Euler(135, 0, 0) * vertices[4];
                break;
            case 3:
                vertices[1] = Quaternion.Euler(0, 0, 45) * vertices[1];
                vertices[2] = Quaternion.Euler(0, 0, -45) * vertices[2];
                vertices[3] = Quaternion.Euler(0, 0, -135) * vertices[3];
                vertices[4] = Quaternion.Euler(0, 0, 135) * vertices[4];
                break;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 4;
        triangles[11] = 1;

        triangles[12] = 1;
        triangles[13] = 2;
        triangles[14] = 3;

        triangles[15] = 1;
        triangles[16] = 3;
        triangles[17] = 4;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
