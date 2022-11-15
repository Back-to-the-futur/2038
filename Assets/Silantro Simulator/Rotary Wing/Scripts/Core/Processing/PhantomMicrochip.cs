using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PhantomMicrochip : MonoBehaviour { }





#if UNITY_EDITOR
[CustomEditor(typeof(PhantomMicrochip))]
public class PhantomMicrochipEditor : Editor
{
    Color backgroundColor;
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        GUI.color = Color.yellow;
        EditorGUILayout.HelpBox("Functionality has been moved, please remove", MessageType.Warning);
        GUI.color = backgroundColor;
    }
}
#endif
