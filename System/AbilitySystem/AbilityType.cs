﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base2D.System.AbilitySystem
{
    [Flags]
    public enum AbilityType
    {
        CannotBreak = 1,
        CannotCancel = 2,
        CannotGetKnockDown = 4,
        CanKnockDownWithSoonRelease = 8,
        CanKnockDown = 16,
        CanCancelNoCoolDown = 32,
        CanCancelWithCoolDown = 64,
        All = ~0,
    }
}
