﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crom.System.ActionSystem
{
    [Flags]
    public enum ActionType
    {
        Moving = 1,
        Attack = 2,
        CastAbility = 4,
    }
}
