using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;

public class AreaAttackFighter : MonoBehaviour
{
    [SerializeField] protected float attackRange = 20f; // zasięg ataku
    [SerializeField] protected float attackAngle = 135f; // kąt ataku
    [SerializeField] private LayerMask attackLayer; // warstwa obiektów, na których można zaatakować




    protected List<int[]> GetGridSquaresToCheck(int[] pointA, int[] pointB, int[] pointC)
    {
        List<int[]> gridSquaresToCheck = new List<int[]>() { pointA, pointB, pointC };

        int xDiff = pointB[0] - pointA[0];
        int zDiff = pointB[1] - pointA[1];

        int xDirection = xDiff != 0 ? xDiff / Math.Abs(xDiff) : 0;
        int zDirection = zDiff != 0 ? zDiff / Math.Abs(zDiff) : 0;

        // Dodajemy pośrednie kwadraty wzdłuż osi x
        for (int x = 1; x < Math.Abs(xDiff); x++)
        {
            gridSquaresToCheck.Add(new int[] { pointA[0] + x * xDirection, pointA[1] });
        }

        // Dodajemy pośrednie kwadraty wzdłuż osi z
        for (int z = 1; z < Math.Abs(zDiff); z++)
        {
            gridSquaresToCheck.Add(new int[] { pointA[0], pointA[1] + z * zDirection });
        }

        return gridSquaresToCheck;
    }

    
    protected List<int[]> GetAllListToCheck(float[] dinoPoint, List<float[]> vertex,  int increment) 
    {
        List<int[]> dinoToVertex1 = GetSquares(dinoPoint, vertex[0], increment);
        List<int[]> dinoToVertex2 = GetSquares(dinoPoint, vertex[1], increment);
        List<int[]> vertex1Tovertex2 = GetSquares(vertex[0], vertex[1], increment);
        return dinoToVertex1.Concat(vertex1Tovertex2).Concat(dinoToVertex2).Distinct(new IntArrayEqualityComparer()).ToList();
    }

    private List<int[]> GetSquares(float[] dinoPoint, float[] vertex, int increment)
    {
        float[] A = { 0f, 0f };
        float[] B = { vertex[0] - dinoPoint[0], vertex[1] - dinoPoint[1] };

        float paramA = (B[1] - A[1]) / (B[0] - A[0]);

        int[] finalPointInSquare = new int[2];
        finalPointInSquare[0] = (int)(vertex[0] / 10);
        finalPointInSquare[1] = (int)(vertex[1] / 10);
        float[] temporaryVector = { dinoPoint[0], dinoPoint[1] };
        int[] temporarySquares = new int[2];
        temporarySquares[0] = (int)(temporaryVector[0] / 10);
        temporarySquares[1] = (int)(temporaryVector[1] / 10);
        int multiplier = 1;

        multiplier = SetMultiplier(B, multiplier);

        List<int[]> getSquare = new List<int[]>
            {
                new int[2] { (int)(dinoPoint[0] / 10), (int)(dinoPoint[1] / 10) }
            };
        int i = 0;
        while (temporarySquares[0] != finalPointInSquare[0] && temporarySquares[1] != finalPointInSquare[1] && i < 1000)
        {
            temporaryVector[0] += (increment * multiplier);
            temporaryVector[1] += (paramA * increment * multiplier);
            temporarySquares[0] = (int)(temporaryVector[0] / 10);
            temporarySquares[1] = (int)(temporaryVector[1] / 10);

            if (!itContainSquare(getSquare, temporarySquares))
            {
                int[] newSquares = new int[2];
                newSquares[0] = temporarySquares[0];
                newSquares[1] = temporarySquares[1];
                getSquare.Add(newSquares);
            }
            i++;
        }
        return getSquare;
    }

    private int SetMultiplier(float[] B, int multiplier)
    {
        if (B[0] < 0)
        {
            multiplier = -1;
        }

        return multiplier;
    }

    bool itContainSquare(List<int[]> getSquare, int[] intList) => getSquare.Any(item => item.SequenceEqual(intList));




    protected bool IsPointInsideTriangle(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 testPoint)
    {
        // Sprawdzenie, czy punkt jest po tej samej stronie co trzeci punkt względem każdego boku
        bool isOnSameSideAsPointC = IsOnSameSideAsPoint(pointA, pointB, testPoint, pointC);
        bool isOnSameSideAsPointB = IsOnSameSideAsPoint(pointA, pointC, testPoint, pointB);
        bool isOnSameSideAsPointA = IsOnSameSideAsPoint(pointB, pointC, testPoint, pointA);

        // Jeśli punkt znajduje się po tej samej stronie co trzeci punkt względem każdego boku,
        // to oznacza to, że punkt znajduje się wewnątrz trójkąta
        return isOnSameSideAsPointC && isOnSameSideAsPointB && isOnSameSideAsPointA;
    }


    private bool IsOnSameSideAsPoint(Vector3 pointA, Vector3 pointB, Vector3 testPoint, Vector3 thirdPoint)
    {
        Vector3 normal = Vector3.Cross(pointB - pointA, thirdPoint - pointA);
        float dot1 = Vector3.Dot(normal, testPoint - pointA);
        float dot2 = Vector3.Dot(normal, thirdPoint - pointA);
        return Mathf.Sign(dot1) == Mathf.Sign(dot2);
    }


    protected bool IsPointInTriangle(Vector3 p, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float dX = p.x - p2.x;
        float dY = p.y - p2.y;
        float dX21 = p2.x - p1.x;
        float dY12 = p1.y - p2.y;
        float D = dY12 * (p0.x - p2.x) + dX21 * (p0.y - p2.y);
        float s = dY12 * dX + dX21 * dY;
        float t = (p2.y - p0.y) * dX + (p0.x - p2.x) * dY;
        if (D < 0) return s <= 0 && t <= 0 && s + t >= D;
        return s >= 0 && t >= 0 && s + t <= D;
    }

    protected List<GameObject> GetTargets(List<GameObject> Target, List<int[]> allSquaresToCheck
       , Vector3 left, Vector3 right, Vector3 me
       )
    {
        // Sprawdzenie i dodanie celów dla punktu centralnego
        if (tag == "Blue")
        {
            foreach (int[] k in allSquaresToCheck)
                foreach (FighterPlacement fighter in GameManager.Instance.battleManager.EnemyFighters[k[0], k[1]])
                {
                    if (IsPointInTriangle(fighter.transform.position, me, left, right))
                    {
                        Target.Add(fighter.gameObject);
                    }
                }
        }
        else
        {
            foreach (int[] k in allSquaresToCheck)
                foreach (FighterPlacement fighter in GameManager.Instance.battleManager.BlueFighters[k[0], k[1]])
                {
                    if (IsPointInTriangle(fighter.transform.position, me, left, right))
                    {
                        Target.Add(fighter.gameObject);
                    }
                }

        }
        return Target;
    }

}

public class IntArrayEqualityComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        return Enumerable.SequenceEqual(x, y);
    }

    public int GetHashCode(int[] obj)
    {
        if (obj == null) return 0;

        int hash = 19;
        foreach (var val in obj)
            hash = hash * 31 + val.GetHashCode();
        return hash;
    }
}
