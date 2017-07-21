using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class DataRader {

	public static string GetPath()
	{
		return null;
	}
	public static string GetInPath()
	{
		#if UNITY_EDITOR
		return Application.dataPath + "/Resources/";
		#elif UNITY_ANDROID
		return Application.persistentDataPath + "/";
		#elif UNITY_IPHONE
		return Application.persistentDataPath + "/";
		#else
		return Application.dataPath +"/";
		#endif
		}


	public static void Save(string path, object Player)
	{
		var serializer = new XmlSerializer(Player.GetType());
        if (File.Exists(GetInPath() + path + ".xml"))
        {
            File.Delete(GetInPath() + path + ".xml");
        }
		FileStream stream = new FileStream(GetInPath() + path + ".xml", FileMode.OpenOrCreate);
		serializer.Serialize (stream, Player);
		stream.Close ();
	}


	public static T Load<T>(string path)
	{
		var serializer = new XmlSerializer(typeof(T));
		if (File.Exists (GetInPath () + path + ".xml")) {
		var stream = new FileStream(GetInPath () + path + ".xml", FileMode.Open);
			var u = serializer.Deserialize (stream);
			stream.Close();
			return  (T)Convert.ChangeType (u, typeof(T));
		} else {
			TextAsset Texts = (TextAsset)Resources.Load<TextAsset> (path);
			TextReader t = new StringReader (Texts.text);
			var u = serializer.Deserialize (t);
			t.Close ();
			return  (T)Convert.ChangeType (u, typeof(T));
		}
	}

}
