using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class PlayerPrefsWindow : EditorWindow
{
    #region PrivateStructs
    private IPrefsTable[] tables =
    {
        new PrefsTable<int>(PlayerPrefs.GetInt),
        new PrefsTable<float>(PlayerPrefs.GetFloat),
        new PrefsTable<string>(PlayerPrefs.GetString),
    };
    private interface IPrefsTable
    {
        string Type { get; }
        object Get(string key);
    }

    private class PrefsTable<T> : IPrefsTable
    {
        public Func<string, T> Getter;

        public PrefsTable(Func<string, T> getter)
        {
            Getter = getter;
        }

        public string Type
        {
            get
            {
                return TypeWrapper.TypeWrap[typeof(T)] + "Keys";
            }
        }

        public object Get(string key)
        {
            return Getter(key);
        }
    }

    private static class TypeWrapper
    {
        public static Dictionary<Type, string> TypeWrap = new Dictionary<System.Type, string>();

        static TypeWrapper()
        {
            TypeWrap.Add(typeof(int), "Int");
            TypeWrap.Add(typeof(string), "String");
            TypeWrap.Add(typeof(float), "Float");
        }
    }
    #endregion

    #region MonoBehFunctions
    private void OnGUI()
    {
        if (GUILayout.Button("Clear Prefs"))
        {
            PlayerPrefs.DeleteAll();
        }

        GUILayout.BeginHorizontal();
        {

            for (int i = 0; i < tables.Length; i++)
            {
                var table = tables[i];

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginArea(new Rect(i * 200, 25, 200, 400));
                    GUILayout.Label(table.Type + ":");
                    if (PlayerPrefs.HasKey(table.Type))
                    {
                        DrawKeys(PlayerPrefs.GetString(table.Type).Split(','), table.Get);
                    }
                    GUILayout.EndArea();
                }
                GUILayout.EndVertical();

            }
        }
        GUILayout.EndHorizontal();
    }
    #endregion

    #region PrivateFunctions
    private void DrawKeys(string[] keys, Func<string, object> getter)
    {
        foreach (var key in keys)
        {
            GUILayout.Label(string.Format("{0} : {1}", key, getter(key).ToString()));
        }
    }
    #endregion

    #region PublicFunctions
    [MenuItem("Window/Player Prefs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsWindow));
    }
    #endregion
}

#endif