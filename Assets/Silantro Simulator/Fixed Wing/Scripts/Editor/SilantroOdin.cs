using UnityEngine;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class Odin : MonoBehaviour {

	public class SilantroMenu
	{
		// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		[MenuItem("Oyedoyin/Fixed Wing/Download")]
		private static void AddMotorEngine()
		{
			Application.OpenURL("https://assetstore.unity.com/packages/tools/physics/silantro-flight-simulator-toolkit-128025");
		}
	}
}
