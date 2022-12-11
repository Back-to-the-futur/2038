using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class PhantomCargo : MonoBehaviour
{
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public float weight;


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnDrawGizmos()
	{
		//DRAW IDENTIFIER
		Gizmos.color = Color.grey;
		Gizmos.DrawSphere(transform.position, 0.1f);
		Gizmos.color = Color.grey;
		Gizmos.DrawLine(this.transform.position, (this.transform.up * 2f + this.transform.position));
	}
}





#if UNITY_EDITOR
[CustomEditor(typeof(PhantomCargo))]
public class PhantomCargoEditor : Editor
{
	Color backgroundColor;
	Color silantroColor = new Color(1f, 0.4f, 0);
	

	public override void OnInspectorGUI()
	{
		backgroundColor = GUI.backgroundColor;
		//DrawDefaultInspector(); 
		serializedObject.Update();
	
	
		GUILayout.Space(3f);
		GUI.color = silantroColor;
		EditorGUILayout.HelpBox("Cargo Configuration", MessageType.None);
		GUI.color = backgroundColor;
		GUILayout.Space(3f);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("weight"), new GUIContent("Weight"));
		serializedObject.ApplyModifiedProperties();
	}
}
#endif

