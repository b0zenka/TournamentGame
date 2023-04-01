using TournamentPlayer;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tournament
{
    public static class LoadAssembly
    {
        /// <summary>
        /// Funkcja walidująca ścieżki do plików
        /// </summary>
        /// <param name="paths">Lista ścieżek do plików</param>
        /// <remarks>
        /// Scieżki niepoprawne zostaną usunięte z listy
        /// </remarks>
        public static void ValidatePaths<T>(List<string> paths)
             where T : IPlayer
        {
            paths.RemoveAll(path => !IsValid<T>(path));
        }

        public static string[] GetPlayerNames<T>(string[] paths)
        where T : IPlayer
        {
            string[] playerNames = new string[paths.Length];

            for (int i = 0; i < playerNames.Length; i++)
            {
                playerNames[i] = LoadInstance<T>(paths[i]).Name;
            }

            return playerNames;
        }

        public static bool IsValid<T>(string path)
            where T : IPlayer
        {
            if (!File.Exists(path)) return false;
            if (Path.GetExtension(path) != ".dll") return false;

            Assembly assembly = Assembly.LoadFrom(path);

            foreach (var fileType in assembly.GetTypes())
            {
                if (string.IsNullOrEmpty(fileType.FullName)) continue;
                if (!fileType.GetInterfaces().Contains(typeof(T))) continue;

                return true;
            }

            return false;
        }

        public static T LoadInstance<T>(string path)
            where T : IPlayer
        {
            if (!IsValid<T>(path)) throw new InvalidCastException();

            Assembly assembly = Assembly.LoadFrom(path);

            if (assembly == null) return default;

            foreach (Type fileType in assembly.GetTypes())
            {
                if (string.IsNullOrEmpty(fileType.FullName)) continue;
                if (!fileType.GetInterfaces().Contains(typeof(T))) continue;

                var type = assembly.GetType(fileType.FullName);
                if (type == null) continue;

                return (T)Activator.CreateInstance(type);
            }

            return default;
        }
    }
}
