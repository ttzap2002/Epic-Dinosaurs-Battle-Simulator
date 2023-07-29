using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEditor;
using UnityEngine;

public class YaverlandiaFighter: LayEggsFighter
{

    StunningFigter f;
    bool ifSetforstunningfighterdisactive;

    private void Start()
    {
        f=gameObject.GetComponent<StunningFigter>();
        base.Start();
        
    }


    private void Update()
    {
        base.Update();

    }


    protected override void SetEgg()
    {
        base.SetEgg();
        f.IsActiveForBattle = true;
        f.TimeWaitngDuringStunning = 5.0f;
        Destroy(this);
    }

  
}

