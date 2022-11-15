using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PhantomBladeMagic : MonoBehaviour { }



#if UNITY_EDITOR
[CustomEditor(typeof(PhantomBladeMagic))]
public class PhantomBladeMagicEditor : Editor
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