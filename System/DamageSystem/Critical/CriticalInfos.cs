﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Base2D.System.DamageSystem.Critical
{
    [Serializable]
    public class CriticalInfos : DamageModifications<CriticalInfo>
    {
        public CriticalInfos()
        {
            _modification = new Dictionary<int, CriticalInfo>();
        }
    }
}
