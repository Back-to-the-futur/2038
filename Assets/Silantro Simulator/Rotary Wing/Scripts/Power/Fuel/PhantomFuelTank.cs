using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class PhantomFuelTank : MonoBehaviour
{
    //--------------------------------------- Selectibles
    public enum TankType { Main, Auxillary }
    public TankType tankType = TankType.Main;

    public enum FuelType { JetB, JetA1, JP6, JP8, AVGas100, AVGas100LL, AVGas82UL }
    public FuelType fuelType = FuelType.JetB;

	public enum TankPosition { Left, Right, Center }
	public TankPosition tankPosition = TankPosition.Center;

	public enum FuelUnit { Kilogram, Pounds, Liters, Gallon }
    public FuelUnit fuelUnit = FuelUnit.Kilogram;
    public PhantomController controller;



    // ------------------------------------Variables
    public float Capacity = 100;                               //Maximum amount of fuel the tank can carry
    public float CurrentAmount;                                //Current amount of fuel in the tank
    public float actualAmount;                                 //Factor used for fuel conversion based on assigned unit
    public bool attached = true;                               //Is the tank attached to the aircraft
    float fuelFactor;



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void Start() { ConvertFuel(); CurrentAmount = actualAmount; }





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void ConvertFuel()
	{
		if (fuelUnit == FuelUnit.Gallon)
		{
			fuelFactor = 0.79f;
		}
		if (fuelUnit == FuelUnit.Kilogram)
		{
			fuelFactor = 1f;
		}
		if (fuelUnit == FuelUnit.Liters)
		{
			if (fuelType == FuelType.JetA1)
			{
				fuelFactor = 0.79f;
			}
			if (fuelType == FuelType.JetB)
			{
				fuelFactor = 0.781f;
			}
			if (fuelType == FuelType.JP6)
			{
				fuelFactor = 0.81f;
			}
			if (fuelType == FuelType.JP8)
			{
				fuelFactor = 0.804f;
			}
			if (fuelType == FuelType.AVGas100)
			{
				fuelFactor = 0.721f;
			}
			if (fuelType == FuelType.AVGas100LL)
			{
				fuelFactor = 0.769f;
			}
			if (fuelType == FuelType.AVGas82UL)
			{
				fuelFactor = 0.730f;
			}
		}
		if (fuelUnit == FuelUnit.Pounds)
		{
			fuelFactor = 0.454f;
		}
		//
		actualAmount = Capacity * fuelFactor;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//EDITOR UPDATE
	public void OnDrawGizmos()
	{
		ConvertFuel();
		//DRAW IDENTIFIER
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(this.transform.position, (this.transform.up * 2f + this.transform.position));
	}


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (CurrentAmount < 0f)
		{
			CurrentAmount = 0f;
		}
	}
}





#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomFuelTank))]
public class PhantomFuelTankEditor : Editor
{
	Color backgroundColor;
	Color silantroColor = new Color(1.0f, 0.40f, 0f);
	Color fuelColor = Color.white;
	PhantomFuelTank tank;

	
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void OnEnable() { tank = (PhantomFuelTank)target; }



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public override void OnInspectorGUI()
	{
		backgroundColor = GUI.backgroundColor;
		//DrawDefaultInspector ();
		serializedObject.Update();


		GUILayout.Space(3f);
		GUI.color = silantroColor;
		EditorGUILayout.HelpBox("Tank Configuration", MessageType.None);
		GUI.color = backgroundColor;
		GUILayout.Space(3f);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("tankType"), new GUIContent("Type"));

		if (tank.tankType == PhantomFuelTank.TankType.Main)
		{
			GUILayout.Space(3f);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("tankPosition"), new GUIContent("Position"));
		}

		GUILayout.Space(10f);
		GUI.color = silantroColor;
		EditorGUILayout.HelpBox("Fuel Configuration", MessageType.None);
		GUI.color = backgroundColor;
		GUILayout.Space(3f);

		if (tank.fuelType == PhantomFuelTank.FuelType.AVGas100) { fuelColor = Color.green; }
		else if (tank.fuelType == PhantomFuelTank.FuelType.AVGas100LL) { fuelColor = Color.cyan; }
		else if (tank.fuelType == PhantomFuelTank.FuelType.AVGas82UL) { fuelColor = Color.red; }
        else { fuelColor = Color.white; }

		GUI.color = fuelColor;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("fuelType"), new GUIContent("Fuel Type"));
		GUI.color = backgroundColor;

		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("fuelUnit"), new GUIContent("Fuel Unit"));
		GUILayout.Space(3f); 
		EditorGUILayout.PropertyField(serializedObject.FindProperty("Capacity"), new GUIContent("Capacity"));
		GUILayout.Space(5f);
		EditorGUILayout.LabelField("Actual Capacity", tank.actualAmount.ToString("0.00") + " kg");
		GUILayout.Space(10f);
		GUI.color = silantroColor;
		EditorGUILayout.HelpBox("Fuel Display", MessageType.None);
		GUI.color = backgroundColor;
		GUILayout.Space(3f);
		EditorGUILayout.LabelField("Current Amount", tank.CurrentAmount.ToString("0.00") + " kg");


		serializedObject.ApplyModifiedProperties();
	}
}
#endif