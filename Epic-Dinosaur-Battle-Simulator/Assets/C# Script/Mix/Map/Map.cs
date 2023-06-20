using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Map
{
    public int Id { get; set; }
    List<Obstacle> obstacles;

    public Map(int id, List<Obstacle> obstacles)
    {
        Id = id;
        this.obstacles = obstacles;
    }


    public void SetObjectToScene()
    {

        foreach (var obj in obstacles)
        {
            GameObject gameObj = GameObject.Instantiate(GameManager.Instance.obstaclesObjects[obj.PrefabId]);
            gameObj.gameObject.transform.position = new Vector3(obj.XAxis, obj.YAxis, obj.ZAxis);
            gameObj.SetActive(true);
        }
    }
}

