﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColonyPlusPlus.classes;

namespace ColonyPlusPlus.classes
{
    public static class TypeManager
    {
        public static void registerTypes()
        {

            // test dirt!
            types.newdirt newdirt = new types.newdirt("newdirt");

        }

        public static void registerTrackedTypes()
        {
            /*
             *  ItemTypesServer.RegisterType("ExampleBlock1",
                new ItemTypesServer.ItemActionBuilder()
                .SetOnAdd(ExampleClassCodeManager.OnAdd)
                .SetOnRemove(ExampleClassCodeManager.OnRemove)
                .SetOnChange(ExampleClassCodeManager.OnChange)
                .SetChangeTypes("ExampleBlock1", "ExampleBlock2")
            );

            ItemTypesServer.RegisterParent("ExampleBlock2", "ExampleBlock1"); 
            */
        }
    }
}