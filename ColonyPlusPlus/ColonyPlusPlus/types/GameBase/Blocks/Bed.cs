﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.types.GameBase.Blocks
{
    class Bed : classes.Type
    {
        public Bed(string name) : base(name)
        {
            this.OnPlaceAudio = "woodPlace";
            this.OnRemoveAudio = "woodDeleteLight";
            this.IsAutoRotatable = true;
            this.IsSolid = false;
            this.NeedsBase = true;
            this.NPCLimit = 0;
            this.IsPlaceable = true;
            this.RotatablexPlus = "bedx+";
            this.RotatablexMinus = "bedx-";
            this.RotatablezPlus = "bedz+";
            this.RotatablezMinus = "bedz-";
            this.Register();
        }
    }

    class BedxPlus : classes.Type
    {
        public BedxPlus(string name) : base(name)
        {
            this.ParentType = "bed";
            this.Mesh = "bedx+.obj";
            this.Register();
        }
    }
    class BedxMinus : classes.Type
    {
        public BedxMinus(string name) : base(name)
        {
            this.ParentType = "bed";
            this.Mesh = "bedx-.obj";
            this.Register();
        }
    }
    class BedzPlus : classes.Type
    {
        public BedzPlus(string name) : base(name)
        {
            this.ParentType = "bed";
            this.Mesh = "bedz+.obj";
            this.Register();
        }
    }
    class BedzMinus : classes.Type
    {
        public BedzMinus(string name) : base(name)
        {
            this.ParentType = "bed";
            this.Mesh = "bedz-.obj";
            this.Register();
        }
    }
}
