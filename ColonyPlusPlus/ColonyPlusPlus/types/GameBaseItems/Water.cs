﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.types.GameBaseItems
{
    class Water : classes.Type
    {
        public Water(string name) : base(name)
        {
            this.IsSolid = false;
            this.Register();
        }
    }
}
