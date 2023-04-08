using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocalization : MonoBehaviour
{
    public float x, y, z;
    public GameObject teleportedItem;

    public void Teleport()
    {
        teleportedItem.transform.position = new Vector3(x, y, z);
    }

    public void ChangePosition()
    {
        teleportedItem.transform.position += new Vector3(x, y, z);
    }
    
}
