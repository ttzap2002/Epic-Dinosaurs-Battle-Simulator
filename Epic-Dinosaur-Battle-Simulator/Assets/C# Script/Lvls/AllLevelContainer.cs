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
        SceneLevel Sandbox = new SceneLevel(1000000, 80,0);
        levelList.Add(Sandbox);

        SceneLevel Level1 = new SceneLevel(10000, 30,1);
        levelList.Add(Level1);

        SceneLevel Level2 = new SceneLevel(20000, 20,2);
        levelList.Add(Level2);

        SceneLevel Level3 = new SceneLevel(30000, 25,3);
        levelList.Add(Level3);

    }

}
