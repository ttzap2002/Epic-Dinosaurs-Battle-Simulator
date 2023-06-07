using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    public class TailAttacKFighter:AreaAttackFighter
    {
        public List<GameObject> ObjectsToHitByTail() 
        {
            // Pozycja dinozaura
            Vector3 position = transform.position;
            // Kierunek dinozaura
            Vector3 forward = transform.forward;
            // Punkt na lewej krawędzi zasięgu ataku
            Vector3 leftPoint = Quaternion.Euler(0, -attackAngle, 0) * (-forward) * attackRange + position;
            // Punkt na prawej krawędzi zasięgu ataku
            Vector3 rightPoint = Quaternion.Euler(0, attackAngle, 0) * (-forward) * attackRange + position;

            // Rysowanie linii do punktów ataku (opcjonalnie)
            Debug.DrawRay(leftPoint, rightPoint - leftPoint, Color.red);
            Debug.DrawRay(position, (leftPoint - position).normalized * attackRange, Color.green);
            Debug.DrawRay(position, (rightPoint - position).normalized * attackRange, Color.green);
            List<GameObject> Target = new List<GameObject>();
            int[] rowColLeftPoint = new int[] { (int)leftPoint.x / 10, (int)leftPoint.z / 10 };
            int[] rowColRightPoint = new int[] { (int)rightPoint.x / 10, (int)rightPoint.z / 10 };
            int[] rowColposition = new int[] { (int)position.x / 10, (int)position.z / 10 };
            if (rowColRightPoint[0] < 0) { rowColRightPoint[0] = 0; }
            if (rowColRightPoint[0] > 9) { rowColRightPoint[0] = 9; }
            if (rowColRightPoint[1] < 0) { rowColRightPoint[1] = 0; }
            if (rowColRightPoint[1] > 9) { rowColRightPoint[1] = 9; }
            if (rowColLeftPoint[0] < 0) { rowColLeftPoint[0] = 0; }
            if (rowColLeftPoint[0] > 9) { rowColLeftPoint[0] = 9; }
            if (rowColLeftPoint[1] < 0) { rowColLeftPoint[1] = 0; }
            if (rowColLeftPoint[1] > 9) { rowColLeftPoint[1] = 9; }
            List<int[]> allSquaresToCheck = GetGridSquaresToCheck(rowColLeftPoint, rowColRightPoint, rowColposition);

            return GetTargets(Target, allSquaresToCheck, leftPoint, rightPoint, position);
    }

    }

