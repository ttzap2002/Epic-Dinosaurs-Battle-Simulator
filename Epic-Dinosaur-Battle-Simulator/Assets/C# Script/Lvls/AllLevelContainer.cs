using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllLevelContainer 
{
    List<SceneLevel>[] levelList;

    public AllLevelContainer()
    {
        this.levelList= new List<SceneLevel>[4];
        SetLevelList();
    }

    public List<SceneLevel>[] LevelList { get => levelList; set => levelList = value; }

    private void SetLevelList() 
    {
        for(int i=0;i<levelList.Length;i++) 
        {
            LevelList[i] = new List<SceneLevel>();
        }
    }

    public void AddAllScene() 
    {
        SceneLevel Sandbox = new SceneLevel(1000000, 80,0);
        levelList[0].Add(Sandbox);

        SceneLevel Level1 = new SceneLevel(10000, 30,1);
        levelList[0].Add(Level1);

        SceneLevel Level2 = new SceneLevel(20000, 20,2);
        levelList[0].Add(Level2);

        SceneLevel Level3 = new SceneLevel(30000, 25,3);
        levelList[0].Add(Level3);

        SceneLevel Level4 = new SceneLevel(30000, 25, 4);
        levelList[0].Add(Level4);

    }

}
