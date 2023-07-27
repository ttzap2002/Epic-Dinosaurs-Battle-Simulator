using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public void ChangeMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycasthit)) 
        {
            transform.position = new Vector3(raycasthit.point.x,0, raycasthit.point.z);
        }

    }
}
