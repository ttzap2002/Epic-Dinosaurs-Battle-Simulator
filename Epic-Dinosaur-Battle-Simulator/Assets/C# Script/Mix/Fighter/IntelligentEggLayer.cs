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
    
        if (CheckForFirstCase()) 
        {

        }
        return new Vector3(0,0,0);
    }

    private bool CheckForFirstCase() 
    {
        return (fighter.row == 0 && fighter.col == 0) || (fighter.row == 0 && fighter.col == 1) || (fighter.row == 1 && fighter.col == 0) || (fighter.row == 9 && fighter.col == 9) || (fighter.row == 9 && fighter.col == 8) || (fighter.row == 8 && fighter.col == 9);
    }




}

