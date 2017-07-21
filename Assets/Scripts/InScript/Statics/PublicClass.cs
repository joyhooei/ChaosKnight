using UnityEngine;
using System;
using System.Collections.Generic;

public static class PublicClass
{
	public static float dpp = 16.6f/Screen.width;
	public static int level=1;
	public static int type=1;
    public static float sound =0.4f, music = 0.4f;
    public static string ScenePrev = "";
    public static string SceneNext = "";
    /// <summary>
    /// indexNotify{
    ///     value 1: restart
    ///     value 2: choose Level
    ///     value 3: setting
    ///     value 4: Main menu
    /// }
    /// </summary>
    public static int indexNotify;
    public static string stringNotify; 
    public static int typeLoading = 0;
    public static StringDBs String;
    public static Language typeLanguage = Language.vi;
    //
    public static MCDb Player;
}

