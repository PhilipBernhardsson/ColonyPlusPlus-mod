﻿using Pipliz.JSON;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.Classes.Managers
{
    public static class NPCManager
    {

        private static Dictionary<int, Data.NPCData> NPCDataList = new Dictionary<int, Data.NPCData>();

        public static int baseXP = 10;
        public static int maxLevel = 25;
        public static float XPMultiplier = 2;
        public static float EfficiencyPerLevel = 0.02f;

        public static void initialise()
        {
            loadNPCData();
            baseXP = ConfigManager.getConfigInt("jobs.baseXP");
            maxLevel = ConfigManager.getConfigInt("jobs.maxLevel");
            XPMultiplier = ConfigManager.getConfigFloat("jobs.xpMultiplier");
            EfficiencyPerLevel = ConfigManager.getConfigFloat("jobs.efficiencyPerLevel");

            Utilities.WriteLog(String.Format("NPC Config: baseXP: {0}, maxLevel: {1}, xpMultiplier: {2}, efficiencyPerLevel: {3}",baseXP, maxLevel, XPMultiplier, EfficiencyPerLevel));
        }

        public static void registerAllJobs()
        {
            removeBaseJobs();

            //REIMPLEMENTED BASE JOBS
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Grinder>("grindstone");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Minter>("mint");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Merchant>("shop");
            //Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Merchant>("tailorshop");
            //Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Technologist>("technologisttable");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Crafter>("workbench");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Smelter>("furnace");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.BaseJobs.Baker>("oven");

            // CUSTOM JOBS
            //CraftingJobs
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.CraftingJob.Blacksmith>("anvil");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.CraftingJob.Carpenter>("sawmill");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.CraftingJob.ChickenPluckerJob>("chickencoop");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.CraftingJob.StoneMason>("masontable");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.CraftingJob.WellOperator>("well");
            //FueledCraftingJobs
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.FueledCraftingJob.PotteryJob>("potterytable");
            //DefenseJobs
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.Defense.QuiverJobT2>("quivert2");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.Register<Classes.BlockJobs.Defense.BaseQuiver>("quiver");

        }

        public static void removeBaseJobs()
        {
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("grindstone");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("mint");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("shop");
            //Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("tailorshop");
            //Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("technologisttable");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("workbench");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("furnace");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("oven");
            Pipliz.APIProvider.Jobs.BlockJobManagerTracker.ClearType("quiver");
        }

        public static Data.NPCData getNPCData(int id, Players.Player owner)
        {
            if (NPCDataList.ContainsKey(id)) {
                return NPCDataList[id];
            } else
            {
                Data.NPCData d = new Data.NPCData(owner);
                NPCDataList.Add(id, d);
                return d;
            }
        }

        public static void updateNPCData(int id, Data.NPCData d)
        {
            NPCDataList[id] = d;
        }

        public static void removeNPCData(int id)
        {
            if(NPCDataList.ContainsKey(id))
            {
                NPCDataList.Remove(id);
            }
        }

        public static void saveNPCData()
        {
            try
            {
                string jSONPath = GetJSONPath();
                Utilities.MakeDirectoriesIfNeeded(jSONPath);
                if (NPCDataList.Count == 0)
                {
                    File.Delete(jSONPath);
                }
                else
                {

                    // make a JSON node
                    JSONNode rootnode = new JSONNode(NodeType.Array);

                    // then go through stuff
                    foreach (int npcID in NPCDataList.Keys)
                    {
                        Data.NPCData npcData = NPCDataList[npcID];

                        // build a child node
                        JSONNode child = new JSONNode(NodeType.Object);

                        // Create the JSON
                        child.SetAs("id", npcID);
                        child.SetAs("owner", npcData.owner.ID.steamID);
                        child.SetAs("xpdata", npcData.XPData.toJSON());

                        rootnode.AddToArray(child);
                    }

                    Pipliz.JSON.JSON.Serialize(jSONPath, rootnode);

                }
            }
            catch (Exception exception2)
            {
                Utilities.WriteLog("Exception in saving all NPC Data:" + exception2.Message);
            }
        }

        public static void loadNPCData()
        {
            try
            {

                JSONNode array;
                if (Pipliz.JSON.JSON.Deserialize(GetJSONPath(), out array, false))
                {

                    if (array != null)
                    {
                        foreach (JSONNode node in array.LoopArray())
                        {
                            try
                            {
                               
                                int npcID = node["id"].GetAs<int>();
                                Players.Player owner = Players.GetPlayer(new NetworkID(new CSteamID(node["owner"].GetAs<ulong>())));


                                //Utilities.WriteLog("Loaded NPC: " + npcID);

                                if (!NPCDataList.ContainsKey(npcID))
                                {
                                    // doesn't exist, add it!
                                    Data.NPCData npcData = new Data.NPCData(owner);
                                    JSONNode xpdata = node["xpdata"].GetAs<JSONNode>();

                                    if (xpdata.ChildCount > 0)
                                    {
                                        foreach (KeyValuePair<string, JSONNode> current in xpdata.LoopObject())
                                        {
                                            npcData.XPData.setXP(current.Key, current.Value.GetAs<ushort>());
                                            //Utilities.WriteLog("Added XP: " + current.Key + " value: " + current.Value.GetAs<ushort>());
                                        }
                                    }
                                    npcData.XPData.recalculateAllLevels();

                                    NPCDataList.Add(npcID, npcData);

                                }



                            }
                            catch (Exception exception)
                            {
                                Utilities.WriteLog("Exception loading NPC data;" + exception.Message);
                            }
                        }

                        Utilities.WriteLog("Loaded NPCData");
                    }
                    else
                    {
                        Utilities.WriteLog("Loading NPC data returned 0 results");
                    }
                }
                else
                {
                    Utilities.WriteLog("Found no NPC Data (read error?)");
                }

            }
            catch (Exception exception2)
            {
                Utilities.WriteLog("Exception in loading NPC data:" + exception2.Message);
            }
        }

        private static string GetJSONPath()
        {
            return "gamedata/savegames/" + ServerManager.WorldName + "/cppnpcxp.json";
        }

    }

}
