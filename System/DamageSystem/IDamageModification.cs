﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base2D.System.DamageSystem
{
    public interface IDamageModification<T>
    {
        int Id { get; set; }
        string Name { get; set; }
        float Chance { get; set; }
        List<T> Stacks { get; set; }
        StackType StackType { get; }

        void Trigger(DamageInfo info);
    }
}
