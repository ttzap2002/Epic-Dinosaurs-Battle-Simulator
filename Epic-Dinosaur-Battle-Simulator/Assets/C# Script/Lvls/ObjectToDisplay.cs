using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDisplay 
{
    float xAxis;
    float yAxis;
    float zAxis;
    int prefabId;
    int lvl;

    public ObjectToDisplay(float xAxis, float yAxis, float zAxis, int prefabId, int lvl)
    {
        XAxis = xAxis;
        YAxis = yAxis;
        ZAxis = zAxis;
        PrefabId = prefabId;
        this.lvl = lvl; 
    }

    public float XAxis { get => xAxis; set => xAxis = value; }
    public float YAxis { get => yAxis; set => yAxis = value; }
    public float ZAxis { get => zAxis; set => zAxis = value; }
    public int PrefabId { get => prefabId; set => prefabId = value; }
    public int Lvl { get => lvl; set => lvl = value; }
}
