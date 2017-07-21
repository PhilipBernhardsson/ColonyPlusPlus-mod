﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.types.GameBase.Items
{
    class Wheat : classes.Type
    {
        public Wheat(string name) : base(name)
        {
            this.NutritionalValue = 0.4f;
            this.MaxStackSize = 600;
            this.Register();
        }
    }
}