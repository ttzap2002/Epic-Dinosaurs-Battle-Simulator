using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLevelContainer 
{
    List<SceneLevel> levelList;

    public AllLevelContainer()
    {
        this.levelList= new List<SceneLevel>();
    }

    public List<SceneLevel> LevelList { get => levelList; set => levelList = value; }

    public void AddAllScene() 
    {
        SceneLevel Sandbox = new SceneLevel(1000000, 80);
        levelList.Add(Sandbox);

        SceneLevel Level1 = new SceneLevel(10000, 30);
        levelList.Add(Level1);

    }

}
