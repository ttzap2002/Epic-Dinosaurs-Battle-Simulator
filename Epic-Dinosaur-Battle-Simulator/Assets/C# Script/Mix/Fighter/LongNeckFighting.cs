using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LongNeckFighting : MonoBehaviour
{
    public float attackRange = 20f; // zasiêg ataku
    public float attackAngle = 135f; // k¹t ataku
    public LayerMask attackLayer; // warstwa obiektów, na których mo¿na zaatakowaæ
   
    private List<GameObject> Targets;
    private bool isReadyToHit = false;

    [SerializeField]private int ile = 0;
    public UnityEvent onNoTargets;
    private void Start()
    {
        Targets = new List<GameObject>();
    }
    private void Update()
    {
        // Pozycja dinozaura
        Vector3 position = transform.position;

        // Kierunek dinozaura
        Vector3 forward = transform.forward;

        // Punkt na przodzie zasiêgu ataku
        Vector3 frontPoint = position + forward * attackRange;

        // Punkt na lewej krawêdzi zasiêgu ataku
        Vector3 leftPoint = Quaternion.Euler(0, -attackAngle, 0) * (-forward) * attackRange + position;

        // Punkt na prawej krawêdzi zasiêgu ataku
        Vector3 rightPoint = Quaternion.Euler(0, attackAngle, 0) * (-forward) * attackRange + position;

        // Rysowanie linii do punktów ataku (opcjonalnie)
        //Debug.DrawLine(leftPoint, rightPoint, Color.red);
        //Debug.DrawLine(position, leftPoint, Color.red);
        //Debug.DrawLine(position, rightPoint, Color.red);
        Debug.DrawRay(leftPoint, rightPoint - leftPoint, Color.red);
        Debug.DrawRay(position, (leftPoint - position).normalized * attackRange, Color.green);
        Debug.DrawRay(position, (rightPoint - position).normalized * attackRange, Color.green);
       

        // Wykonanie raycastów do obiektów z warstwy attackLayer
        RaycastHit hit;

        if (Physics.Raycast(leftPoint, (rightPoint - leftPoint), out hit, attackRange, attackLayer))
        {
            // Wykryto trafienie w obiekt
            if(hit.collider.gameObject.tag != this.gameObject.tag && !Targets.Contains(hit.collider.gameObject)) 
            {
                Targets.Add(hit.collider.gameObject);
            }
            Debug.Log("Pies");
         
        }

        if (Physics.Raycast(position, (leftPoint - position).normalized, out hit, attackRange, attackLayer))
        {
            // Wykryto trafienie w obiekt
            if (hit.collider.gameObject.tag != this.gameObject.tag && !Targets.Contains(hit.collider.gameObject))
            {
                Targets.Add(hit.collider.gameObject);

            }
        }

        if (Physics.Raycast(position, (rightPoint - position).normalized, out hit, attackRange, attackLayer))
        {
            // Wykryto trafienie w obiekt
            if (hit.collider.gameObject.tag != this.gameObject.tag && !Targets.Contains(hit.collider.gameObject) )
            {
                Targets.Add(hit.collider.gameObject);
            }
        }

        if (Targets.Count == 0)
        {
            onNoTargets.Invoke(); // Wywo³aj zdarzenie, gdy nie ma celów
        }


        ile = Targets.Count;
        
    }

    public bool HitAllEnemies(float damage)
    {
        if (Targets.Count > 0)
        {
            for (int i = Targets.Count - 1; i >= 0; i--)
            {
                GameObject g = Targets[i];
                if (g == null)
                {
                    Targets.RemoveAt(i);
                    continue;
                }
                CreatureStats c = g.GetComponent<CreatureStats>();
                c.hp -= damage;
                if (c.hp <= 0)
                {
                    GameManager.Instance.battleManager.RemoveFromList(c.gameObject.GetComponent<FighterPlacement>());
                    Targets.RemoveAt(i);
                    Destroy(c.gameObject);
                }
            }
        }
        if (Targets.Count > 0) 
        {
            return true;
        }
        return false;
    }


    
       
    // Funkcja, która sprawdza, czy punkt znajduje siê wewn¹trz trójk¹ta
    private bool IsPointInsideTriangle(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 testPoint)
    {
        // Sprawdzenie, czy punkt jest po tej samej stronie co trzeci punkt wzglêdem ka¿dego boku
        bool isOnSameSideAsPointC = IsOnSameSideAsPoint(pointA, pointB, testPoint, pointC);
        bool isOnSameSideAsPointB = IsOnSameSideAsPoint(pointA, pointC, testPoint, pointB);
        bool isOnSameSideAsPointA = IsOnSameSideAsPoint(pointB, pointC, testPoint, pointA);

        // Jeœli punkt znajduje siê po tej samej stronie co trzeci punkt wzglêdem ka¿dego boku,
        // to oznacza to, ¿e punkt znajduje siê wewn¹trz trójk¹ta
        return isOnSameSideAsPointC && isOnSameSideAsPointB && isOnSameSideAsPointA;
    }

    // Funkcja, która sprawdza, czy punkt znajduje siê po tej samej stronie co trzeci punkt wzglêdem danego boku
    private bool IsOnSameSideAsPoint(Vector3 pointA, Vector3 pointB, Vector3 testPoint, Vector3 thirdPoint)
    {
        Vector3 normal = Vector3.Cross(pointB - pointA, thirdPoint - pointA);
        float dot1 = Vector3.Dot(normal, testPoint - pointA);
        float dot2 = Vector3.Dot(normal, thirdPoint - pointA);
        return Mathf.Sign(dot1) == Mathf.Sign(dot2);
    }

}






