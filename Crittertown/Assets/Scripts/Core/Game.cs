using UnityEngine;
using System.Collections;
using System;

public class Game : SVBLM.Core.Singleton<Game> {
	public static new Camera camera {
		get {
			return Camera.main;
		}
		private set {}
	}


	#region UI
	public static bool UIEnabled = true;
	#endregion




	#region promode
	public static bool SupportsImageEffects() { return SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures; }
	#endregion
}
