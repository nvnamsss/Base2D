﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crom.System.BuffSystem.ScriptableObject
{
    public abstract class ScriptableBuff
    {

        //Duration of the buff
        public float Duration;

        public abstract TimedBuff InitializeBuff(GameObject obj);

    }
}
