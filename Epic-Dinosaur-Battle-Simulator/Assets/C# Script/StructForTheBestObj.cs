using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.C__Script
{
    public struct StructForTheBestObj
    {
        public StructForTheBestObj(GameObject obj, bool isTrue)
        {
            Obj = obj;
            IsTrue = isTrue;
            Distance= 0;
        }

        public GameObject Obj { get; set; }
        public bool IsTrue { get; set; }

        public float Distance { get; set; }

    }
}
