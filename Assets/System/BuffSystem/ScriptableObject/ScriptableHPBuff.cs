﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crom.System.BuffSystem.ScriptableObject
{
    class ScriptableHPBuff : ScriptableBuff
    {
        public float HPIncrease;

        public override TimedBuff InitializeBuff(GameObject obj)
        {
            return new TimedHPBuff(Duration, this, obj);
        }
    }
}