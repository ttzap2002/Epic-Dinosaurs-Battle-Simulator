using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using System.Threading.Tasks;
using Unity.VisualScripting;

public class IntelligentEggLayer: LayEggsFighter
{
    private void Start()
    {
        base.Start();
    }

    protected override Vector3 GetPositionToMove()
    {
        return new Vector3();
    }






}

