﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.types.items
{
    class BakedPotato : classes.Type
    {
        public BakedPotato(string name) : base(name)
        {
            this.NutritionalValue = 1.0f;
            this.Register();
        }
    }
}