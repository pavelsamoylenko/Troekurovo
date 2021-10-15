﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class MissingScripts : EditorWindow
    {
        static int go_count = 0, components_count = 0, missing_count = 0;

        void OnGUI()
        {
            if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
            {
                FindInSelected();
            }
        }

        [MenuItem("MagicQuick / Tools / Find Missing Scripts Recursively")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MissingScripts));
        }

        private static void FindInSelected()
        {
            GameObject[] go = Selection.gameObjects;
            go_count = 0;

            components_count = 0;
            missing_count = 0;

            foreach (GameObject g in go)
            {
                FindInGO(g);
            }

            Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }

        private static void FindInGO(GameObject g)
        {
            go_count++;

            Component[] components = g.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                components_count++;

                if (components[i] == null)
                {
                    missing_count++;
                    string s = g.name;
                    Transform t = g.transform;

                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }

                    Debug.Log(s + " has an empty script attached in position: " + i, g);
                }
            }

            foreach (Transform childT in g.transform)
            {
                FindInGO(childT.gameObject);
            }
        }
    }
}
#endif