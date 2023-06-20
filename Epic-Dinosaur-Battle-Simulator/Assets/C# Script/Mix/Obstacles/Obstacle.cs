using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Obstacle
{
    float xAxis;
    float yAxis;
    float zAxis;
    int prefabId;

    public Obstacle(float xAxis, float yAxis, float zAxis, int prefabId)
    {
        XAxis = xAxis;
        YAxis = yAxis;
        ZAxis = zAxis;
        PrefabId = prefabId;
    }

    public float XAxis { get => xAxis; set => xAxis = value; }
    public float YAxis { get => yAxis; set => yAxis = value; }
    public float ZAxis { get => zAxis; set => zAxis = value; }
    public int PrefabId { get => prefabId; set => prefabId = value; }
}

