﻿using Permissions;
using System;
using ChatCommands;
using NPC;
using Permissions;
using Pipliz;
using Pipliz.Chatting;
using System;

namespace ColonyPlusPlus.Classes.ChatCommands
{
    class ChunkCommands : IChatCommand
    {
        public bool IsCommand(string chatItem) =>
            (chatItem.StartsWith("/chunk"));

        public bool TryDoCommand(NetworkID id, string chatItem)
        {
            if (chatItem.StartsWith("/chunk"))
            {
                string[] s = chatItem.Split(' ');
                if(s[1] == "claim")
                {

                    return this.ProcessClaimChunk(id, chatItem);
                }
                else if(s[1] == "unclaim")
                {
                    return this.ProcessUnclaimChunk(id, chatItem);
                }
                else if (s[1] == "delete")
                {
                    return this.ProcessDeleteChunk(id, chatItem);
                }
            }
            return false;
        }

        private bool ProcessClaimChunk(NetworkID id, string chatItem)
        {
            if (PermissionsManager.CheckAndWarnPermission(id, "chunk.claim"))
            {
                // get the current chunk
                Players.Player p = Players.GetPlayer(id);

                int playerX = Pipliz.Math.RoundToInt(p.Position.x);
                int playerY = Pipliz.Math.RoundToInt(p.Position.y);
                int playerZ = Pipliz.Math.RoundToInt(p.Position.z);

                Vector3Int position = new Vector3Int(playerX, playerY, playerZ);

                if (Managers.WorldManager.claimChunk(position, p.ID))
                {
                    Vector3Int chunkPos = position.ToChunk();
                    int owned = Managers.WorldManager.getOwnedChunkCount(p.ID);

                    Chat.Send(id, String.Format("Claimed chunk: {0}, {1}, {2}", chunkPos.x, chunkPos.y, chunkPos.z), ChatSenderType.Server);
                    Chat.Send(id, String.Format("You now own {0} chunks.", owned), ChatSenderType.Server);
                }
                else
                {
                    // chunk already owned
                    Chat.Send(id, "Unable to claim chunk", ChatSenderType.Server);
                }



            }


            return true;
        }

        private bool ProcessUnclaimChunk(NetworkID id, string chatItem)
        {
            if (PermissionsManager.CheckAndWarnPermission(id, "chunk.claim"))
            {
                // get the current chunk
                Players.Player p = Players.GetPlayer(id);

                int playerX = Pipliz.Math.RoundToInt(p.Position.x);
                int playerY = Pipliz.Math.RoundToInt(p.Position.y);
                int playerZ = Pipliz.Math.RoundToInt(p.Position.z);

                Vector3Int position = new Vector3Int(playerX, playerY, playerZ);

                if (Managers.WorldManager.unclaimChunk(position, p.ID))
                {
                    Vector3Int chunkPos = position.ToChunk();
                    int owned = Managers.WorldManager.getOwnedChunkCount(p.ID);

                    Chat.Send(id, String.Format("Unclaimed chunk: {0}, {1}, {2}", chunkPos.x, chunkPos.y, chunkPos.z), ChatSenderType.Server);
                    Chat.Send(id, String.Format("You now own {0} chunks.", owned), ChatSenderType.Server);
                }
                else
                {
                    // chunk already owned
                    Chat.Send(id, "Unable to unclaim chunk", ChatSenderType.Server);
                }



            }


            return true;
        }

        private bool ProcessDeleteChunk(NetworkID id, string chatItem)
        {
            if (PermissionsManager.CheckAndWarnPermission(id, "chunk.delete"))
            {
                // get the current chunk
                Players.Player p = Players.GetPlayer(id);

                int playerX = Pipliz.Math.RoundToInt(p.Position.x);
                int playerY = Pipliz.Math.RoundToInt(p.Position.y);
                int playerZ = Pipliz.Math.RoundToInt(p.Position.z);

                Vector3Int position = new Vector3Int(playerX, playerY, playerZ);

                if (Managers.WorldManager.unclaimChunk(position, p.ID))
                {
                    Vector3Int chunkPos = position.ToChunk();
                    int owned = Managers.WorldManager.getOwnedChunkCount(p.ID);

                    Chat.Send(id, String.Format("Unclaimed chunk: {0}, {1}, {2}", chunkPos.x, chunkPos.y, chunkPos.z), ChatSenderType.Server);
                    Chat.Send(id, String.Format("You now own {0} chunks.", owned), ChatSenderType.Server);
                }
                else
                {
                    // chunk already owned
                    Chat.Send(id, "Unable to unclaim chunk", ChatSenderType.Server);
                }



            }


            return true;
        }
    }
}
