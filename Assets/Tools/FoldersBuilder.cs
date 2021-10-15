#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class FoldersBuilder
    {
        [MenuItem("MagicQuick / Create Project Folders")]
        public static void CreateProjectFolders()
        {
            string[] folderNames =
            {
                "ART", 
                "ART/Animations","ART/Asset Store", "ART/Animators", "ART/Audio", "ART/Audio/Music", "ART/Audio/SoundEffects",
                "ART/Models", "ART/Materials", "ART/Prefabs", "ART/Textures","ART/Scenes", "ART/Shaders", "ART/ShaderGraph", "ART/Scripts",
                "ART/Timelines", "ART/Signals",
                
                "Code",
                "Code/Infrastructure", "Code/Services", 
                "Code/UI", "Code/UI/Controllers", "Code/UI/Views",
                
                "Materials",
                "Prefabs",
                "Resources",
                
                "Scenes",

                "UI",
                "UI/Animations", "UI/Animators", "UI/Audio", "UI/Animations", "UI/Materials", "UI/Textures", "UI/Fonts", "UI/Sprites"
                
            };

            foreach (var item in folderNames)
            {
                CreateFolder(Application.dataPath, "MagicQuick", item);
            }

            CreateFolder(Application.dataPath, "Standard Assets");

            AssetDatabase.Refresh();
        }

        private static void CreateFolder(params string[] paths)
        {
            string path = string.Join("/", paths);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
#endif