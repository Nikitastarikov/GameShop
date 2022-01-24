using UnityEngine;

public static class CustomPlayerPrefs
{
    #region PrivateFunctions
    private static void SaveKey(string key, string type)
    {
        if (PlayerPrefs.HasKey(type))
        {
            string keys = PlayerPrefs.GetString(type);
            keys += "," + key;
            PlayerPrefs.SetString(type, keys);
        }
        else
        {
            PlayerPrefs.SetString(type, key);
        }
    }
    #endregion

    #region PublicFunctions
    public static void SetFloat(string key, float value)
    {
        SaveKey(key, "FloatKeys");
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SetInt(string key, int value)
    {
        SaveKey(key, "IntKeys");
        PlayerPrefs.SetInt(key, value);
    }

    public static void SetString(string key, string value)
    {
        SaveKey(key, "StringKeys");
        PlayerPrefs.SetString(key, value);
    }
    #endregion
}