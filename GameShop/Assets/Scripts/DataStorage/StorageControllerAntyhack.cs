using UnityEngine;
using System.Security.Cryptography;
using System.Text;

namespace GameShop
{
    public class StorageControllerAntyhack : MonoBehaviour
    {
        #region PublicFunctions
        public static void SetString(string key, string value)
		{
			CustomPlayerPrefs.SetString(MD5(key), Encrypt(value));
		}

		public static string GetString(string key, string defaultValue)
		{
			if (!HasKey(key))
				return defaultValue;
			try
			{
				string s = Decrypt(PlayerPrefs.GetString(MD5(key)));
				return s;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static string GetString(string key)
		{
			return GetString(key, "");
		}

		public static void SetInt(string key, int value)
		{
			CustomPlayerPrefs.SetString(MD5(key), Encrypt(value.ToString()));
		}

		public static int GetInt(string key, int defaultValue)
		{
			if (!HasKey(key))
				return defaultValue;
			try
			{
				string s = Decrypt(PlayerPrefs.GetString(MD5(key)));
				int i = int.Parse(s);
				return i;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static int GetInt(string key)
		{
			return GetInt(key, 0);
		}


		public static void SetFloat(string key, float value)
		{
			CustomPlayerPrefs.SetString(MD5(key), Encrypt(value.ToString()));
		}


		public static float GetFloat(string key, float defaultValue)
		{
			if (!HasKey(key))
				return defaultValue;
			try
			{
				string s = Decrypt(PlayerPrefs.GetString(MD5(key)));
				float f = float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
				return f;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static float GetFloat(string key)
		{
			return GetFloat(key, 0);
		}

		public static bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(MD5(key));
		}

		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		public static void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(MD5(key));
		}

		public static void Save()
		{
			PlayerPrefs.Save();
		}
        #endregion

        #region AntyHack

        private static string _secretkey = "CheckSum";
		private static byte[] _key = new byte[8] { 34, 21, 56, 12, 75, 13, 98, 12 };
		private static byte[] _iv = new byte[8] { 44, 65, 26, 94, 51, 11, 23, 120 };

		private static string Encrypt(string str)
		{
			byte[] inputbuffer = Encoding.Unicode.GetBytes(str);
			byte[] outputBuffer = DES.Create().CreateEncryptor(_key, _iv).TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			return System.Convert.ToBase64String(outputBuffer);
		}

		private static string Decrypt(string str)
		{
			byte[] inputbuffer = System.Convert.FromBase64String(str);
			byte[] outputBuffer = DES.Create().CreateDecryptor(_key, _iv).TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			return Encoding.Unicode.GetString(outputBuffer);
		}

		private static string MD5(string str)
		{
			byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(str + _key + SystemInfo.deviceUniqueIdentifier));
			string hashString = "";
			for (int i = 0; i < hashBytes.Length; i++)
			{
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
			}
			return hashString.PadLeft(32, '0');
		}
        #endregion
    }
}
