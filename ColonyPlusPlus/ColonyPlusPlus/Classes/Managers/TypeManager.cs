﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColonyPlusPlus.Classes;

namespace ColonyPlusPlus.Classes.Managers
{
    public static class TypeManager
    {

        public static List<GrowableType> GrowableTypesTracker = new List<GrowableType>();

        public static List<string> AddedTypes = new List<string>();
        public static List<string> CreativeAddedTypes = new List<string>();
        public static List<Type> ActionTypeRegistry = new List<Type>(); 

        // using a prebuilt list of croptypes
        public static void registerTrackedTypes()
        {
            // loop through each growable type
            foreach(GrowableType gt in GrowableTypesTracker)
            {
                // register each crop with our custom crop actions
                

                ItemTypesServer.RegisterOnAdd(gt.TypeName, gt.OnAddAction);
                ItemTypesServer.RegisterOnRemove(gt.TypeName, gt.OnRemoveAction);
                ItemTypesServer.RegisterOnChange(gt.TypeName, gt.OnChangeAction);
            }

            Utilities.WriteLog("Registered Crop Actions");
        }

        public static void registerActionableTypes()
        {
            if(ActionTypeRegistry.Count > 0)
            {
                foreach (Type t in ActionTypeRegistry)
                {
                    t.RegisterActionCallback();
                }
            }
            
        }

        public static void registerActionableTypeCallback(Type t)
        {
            ActionTypeRegistry.Add(t);
        }

        

        // Register the crop in the growable Types list.
        public static void registerCrop(GrowableType classInstance)
        {
            GrowableTypesTracker.Add(classInstance);
        }
    }
}
