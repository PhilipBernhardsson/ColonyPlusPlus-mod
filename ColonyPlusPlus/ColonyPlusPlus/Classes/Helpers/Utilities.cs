﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ColonyPlusPlus.Classes
{
    public static class Utilities
    {
        // Write a log entry
        public static void WriteLog(string message)
        {
            Pipliz.Log.Write("[ColonyPlusPlus]: " + message);
        }

        public static void WriteLog(string message, Helpers.Chat.ChatColour color, Helpers.Chat.ChatStyle style)
        {
            Pipliz.Log.Write(Helpers.Chat.buildMessage("[ColonyPlusPlus]: " + message, color, style));
        }

        public static void WriteLogError(string message)
        {
            Pipliz.Log.Write("[ColonyPlusPlus]: " + message);
        }

        public static bool ValidateIcon(string exists)
        {
            return File.Exists(Directory.GetCurrentDirectory()  + "/gamedata/textures/icons/" + exists + ".png");
        }

        public static void MakeDirectoriesIfNeeded(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

		public static string GetDebugJSONPath(string type)
		{
			return "gamedata/debug/" + type + ".json";
		}
    }
}
