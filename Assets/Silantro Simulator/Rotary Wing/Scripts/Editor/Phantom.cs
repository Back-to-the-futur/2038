using UnityEngine;
using Oyedoyin;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif



public class Phantom : MonoBehaviour
{



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Aerofoil System/Sponson", false, 500)]
	private static void AddSponsonSystem()
	{
		GameObject wing;//EditorSceneManager.MarkSceneDirty (Selection.activeGameObject .gameObject.scene);
		if (Selection.activeGameObject != null)
		{
			wing = new GameObject("Default Right Wing");
			wing.transform.parent = Selection.activeGameObject.transform;
			wing.transform.localPosition = new Vector3(0, 0, 0);
		}
		else
		{
			wing = new GameObject("Default Right Wing");
			GameObject parent = new GameObject("Aerodynamics");
			wing.transform.parent = parent.transform;
		}
		EditorSceneManager.MarkSceneDirty(wing.gameObject.scene);
		PhantomAerofoil wingAerofoil = wing.AddComponent<PhantomAerofoil>();
		wingAerofoil.foilSubdivisions = 5; wingAerofoil.aerofoilType = PhantomAerofoil.AerofoilType.Sponson;
		SilantroAirfoil wng = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		wingAerofoil.rootAirfoil = wng;
		wingAerofoil.tipAirfoil = wng;
	}


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Aerofoil System/Stabilizer/Plain", false, 600)]
	private static void AddPlainSystem()
	{
		GameObject wing;
		if (Selection.activeGameObject != null)
		{
			wing = new GameObject("Default Right Stabilizer");
			wing.transform.parent = Selection.activeGameObject.transform;
			wing.transform.localPosition = new Vector3(0, 0, 0);

		}
		else
		{
			wing = new GameObject("Default Right Stabilizer");
			GameObject parent = new GameObject("Aerodynamics");
			wing.transform.parent = parent.transform;
		}
		EditorSceneManager.MarkSceneDirty(wing.gameObject.scene);
		PhantomAerofoil wingAerofoil = wing.AddComponent<PhantomAerofoil>(); wingAerofoil.foilSubdivisions = 4; wingAerofoil.aerofoilType = PhantomAerofoil.AerofoilType.Stabilator; wingAerofoil.stabType = PhantomAerofoil.StabilatorType.Plain;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		wingAerofoil.rootAirfoil = start;
		wingAerofoil.tipAirfoil = start;
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Aerofoil System/Stabilizer/Stabilator", false, 700)]
	private static void AddStabiatorSystem()
	{
		GameObject wing;
		if (Selection.activeGameObject != null)
		{
			wing = new GameObject("Default Right Stabilator");
			wing.transform.parent = Selection.activeGameObject.transform;
			wing.transform.localPosition = new Vector3(0, 0, 0);

		}
		else
		{
			wing = new GameObject("Default Right Stabilator");
			GameObject parent = new GameObject("Aerodynamics");
			wing.transform.parent = parent.transform;
		}
		EditorSceneManager.MarkSceneDirty(wing.gameObject.scene);
		PhantomAerofoil wingAerofoil = wing.AddComponent<PhantomAerofoil>(); wingAerofoil.foilSubdivisions = 4; wingAerofoil.aerofoilType = PhantomAerofoil.AerofoilType.Stabilator; wingAerofoil.stabType = PhantomAerofoil.StabilatorType.Elevator;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		wingAerofoil.rootAirfoil = start;
		wingAerofoil.tipAirfoil = start;
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Aerofoil System/Fin/Plain", false, 800)]
	private static void AddFinPlainSystem()
	{
		GameObject wing;
		if (Selection.activeGameObject != null)
		{
			wing = new GameObject("Default Fin");
			wing.transform.parent = Selection.activeGameObject.transform; wing.transform.localPosition = new Vector3(0, 0, 0);
		}
		else
		{
			wing = new GameObject("Default Fin"); GameObject parent = new GameObject("Aerodynamics"); wing.transform.parent = parent.transform;
		}
		EditorSceneManager.MarkSceneDirty(wing.gameObject.scene); wing.transform.eulerAngles = new Vector3(0, 0, 90);
		PhantomAerofoil wingAerofoil = wing.AddComponent<PhantomAerofoil>(); wingAerofoil.foilSubdivisions = 4; wingAerofoil.aerofoilType = PhantomAerofoil.AerofoilType.Fin; wingAerofoil.finType = PhantomAerofoil.FinType.Plain;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		wingAerofoil.rootAirfoil = start;
		wingAerofoil.tipAirfoil = start;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Aerofoil System/Fin/Rudder", false, 900)]
	private static void AddRudderSystem()
	{
		GameObject wing;
		if (Selection.activeGameObject != null)
		{
			wing = new GameObject("Default Rudder");
			wing.transform.parent = Selection.activeGameObject.transform; wing.transform.localPosition = new Vector3(0, 0, 0);
		}
		else
		{
			wing = new GameObject("Default Rudder"); GameObject parent = new GameObject("Aerodynamics"); wing.transform.parent = parent.transform;
		}
		EditorSceneManager.MarkSceneDirty(wing.gameObject.scene); wing.transform.eulerAngles = new Vector3(0, 0, 90);
		PhantomAerofoil wingAerofoil = wing.AddComponent<PhantomAerofoil>(); wingAerofoil.foilSubdivisions = 4; wingAerofoil.aerofoilType = PhantomAerofoil.AerofoilType.Fin; wingAerofoil.finType = PhantomAerofoil.FinType.Rudder;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		wingAerofoil.rootAirfoil = start;
		wingAerofoil.tipAirfoil = start;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Avionic System/Cockpit/Dial", false, 1000)]
	private static void AddDial()
	{
		GameObject panel;
		GameObject dial;
		if (Selection.activeGameObject != null && Selection.activeGameObject.name == "Avionics")
		{
			panel = new GameObject("Control Panel");
			panel.transform.parent = Selection.activeGameObject.transform;
			dial = new GameObject("Default Dial");
			dial.transform.parent = panel.transform;
			dial.AddComponent<PhantomDial>();
		}
		else
		{
			Debug.Log("Please Select 'Avionics' GameObject within the Aircraft, or create one if its's missing");
		}
	}


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Avionic System/Cockpit/Lever", false, 1100)]
	private static void AddLever()
	{
		GameObject panel;
		GameObject lever;
		if (Selection.activeGameObject != null && Selection.activeGameObject.name == "Avionics")
		{
			panel = new GameObject("Control Panel");
			panel.transform.parent = Selection.activeGameObject.transform;
			lever = new GameObject("Default Lever");
			lever.transform.parent = panel.transform;
			lever.AddComponent<PhantomLever>();
		}
		else
		{
			Debug.Log("Please Select 'Avionics' GameObject within the Aircraft, or create one if its's missing");
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Avionic System/Control Module", false, 1200)]
	private static void AddControlModule()
	{
		GameObject CoreControl;
		CoreControl = new GameObject("Core Control");
		//
		GameObject defaultCG;
		defaultCG = new GameObject("Empty Gravity Center");
		//
		defaultCG.transform.parent = CoreControl.transform;
		CoreControl.transform.parent = CoreControl.transform;
		PhantomControlModule COG = CoreControl.AddComponent<PhantomControlModule>();
		COG.emptyCenterOfMass = defaultCG.transform;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Avionic System/Radar/Civillian", false, 1300)]
	private static void AddCivillianRadar()
	{
		GameObject radome;
		if (Selection.activeGameObject != null)
		{
			radome = new GameObject("Radar");
			radome.transform.parent = Selection.activeGameObject.transform;
			PhantomRadar radar = radome.AddComponent<PhantomRadar>();
			PhantomTransponder ponder = radome.GetComponent<PhantomTransponder>();
			if(ponder == null) { ponder = radome.AddComponent<PhantomTransponder>(); }
			radar.radarType = PhantomRadar.RadarType.Civilian;
			ponder.silantroTag = PhantomTransponder.SilantroTag.Helicopter;
			//Load Textures
			Texture2D background = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Radar Background.png", typeof(Texture2D));
			Texture2D compass = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Radar Needle.png", typeof(Texture2D));
			Texture2D aircraft = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Icons/Aircraft.png", typeof(Texture2D));
			ponder.silantroTexture = aircraft;
			radar.background = background; radar.compass = compass;
			EditorSceneManager.MarkSceneDirty(radome.gameObject.scene);
		}
		else
		{
			Debug.Log("Please Select 'Avionics' GameObject within the Aircraft, or create one if its's missing");
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Avionic System/Radar/Military", false, 1400)]
	private static void AddMilitaryRadar()
	{
		GameObject radome;
		if (Selection.activeGameObject != null)
		{
			radome = new GameObject("Radar");
			radome.transform.parent = Selection.activeGameObject.transform;
			PhantomRadar radar = radome.AddComponent<PhantomRadar>();
			PhantomTransponder ponder = radome.GetComponent<PhantomTransponder>();
			if (ponder == null) { ponder = radome.AddComponent<PhantomTransponder>(); }
			radar.radarType = PhantomRadar.RadarType.Military;
			ponder.silantroTag = PhantomTransponder.SilantroTag.Helicopter;
			//Load Textures
			Texture2D background = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Radar Background.png", typeof(Texture2D));
			Texture2D compass = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Radar Needle.png", typeof(Texture2D));
			Texture2D aircraft = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Icons/Aircraft.png", typeof(Texture2D));
			Texture2D view = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Target InView.png", typeof(Texture2D));
			Texture2D locked = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Target Locked.png", typeof(Texture2D));
			Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Locked Icon.png", typeof(Texture2D));
			Texture2D selection = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Textures/Radar/Selection B.png", typeof(Texture2D));
			ponder.silantroTexture = aircraft;
			radar.background = background;
			radar.compass = compass;

			radar.TargetLockedTexture = locked;
			radar.TargetLockOnTexture = view;
			radar.selectedTargetTexture = selection; 
			radar.lockedTargetTexture = icon;
			EditorSceneManager.MarkSceneDirty(radome.gameObject.scene);
		}
		else
		{
			Debug.Log("Please Select 'Avionics' GameObject within the Aircraft, or create one if its's missing");
		}
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Power System/Engines/Piston Engine", false, 1500)]
	private static void AddPistonEngine()
	{
		if (Selection.activeGameObject != null)
		{
			GameObject exit = new GameObject();
			exit.name = "_exhaust_point";
			exit.transform.parent = Selection.activeGameObject.transform; exit.transform.localPosition = new Vector3(0, 0, -1);

			GameObject effects = new GameObject("_engine_effects");
			effects.transform.parent = Selection.activeGameObject.transform;
			effects.transform.localPosition = new Vector3(0, 0, -2);

			Selection.activeGameObject.name = "Default Piston Engine";
			EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.scene);
			Rigidbody parent = Selection.activeGameObject.transform.root.gameObject.GetComponent<Rigidbody>();
			if (parent == null) { Debug.Log("Engine is not parented to an Aircraft!! Create a default Rigidbody is you're just testing the Engine"); }
			PhantomPistonEngine prop = Selection.activeGameObject.AddComponent<PhantomPistonEngine>();
			prop.exitPoint = exit.transform;
			
			
			GameObject smoke = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Effects/Engine/Exhaust Smoke.prefab", typeof(GameObject));
			GameObject smokeEffect = Instantiate(smoke, effects.transform.position, Quaternion.Euler(0, -180, 0), effects.transform);
			GameObject distortion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Effects/Engine/Engine Distortion.prefab", typeof(GameObject));
			GameObject distortionEffect = Instantiate(distortion, effects.transform.position, Quaternion.Euler(0, -180, 0), effects.transform);
			
			AudioClip start = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Piston/Exterior/Exterior Piston Start.wav", typeof(AudioClip));
			AudioClip stop = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Piston/Exterior/Exterior Piston Stop.wav", typeof(AudioClip));
			AudioClip run = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Piston/Exterior/Exterior Piston Idle.wav", typeof(AudioClip));
			prop.core = new PhantomEngineCore
			{
				exhaustSmoke = smokeEffect.GetComponent<ParticleSystem>(),
				exhaustDistortion = distortionEffect.GetComponent<ParticleSystem>(),
				distortionEmissionLimit = 20f,
				ignitionExterior = start,
				shutdownExterior = stop,
				backIdle = run
			};
		}
		else
		{
			Debug.Log("Please Select GameObject to add Engine to..");
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Power System/Engines/TurboShaft Engine", false, 1600)]
	private static void AddTurboshaftEngine()
	{
		if (Selection.activeGameObject != null)
		{
			GameObject exit = new GameObject();
			exit.name = "_exhaust_point";
			exit.transform.parent = Selection.activeGameObject.transform; exit.transform.localPosition = new Vector3(0, 0, -1);

			GameObject intake = new GameObject();
			intake.name = "_intake_point";
			intake.transform.parent = Selection.activeGameObject.transform; intake.transform.localPosition = new Vector3(0, 0, 1);

			GameObject effects = new GameObject("_engine_effects");
			effects.transform.parent = Selection.activeGameObject.transform;
			effects.transform.localPosition = new Vector3(0, 0, -2);

			Selection.activeGameObject.name = "Default TurboShaft Engine";
			EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.scene);
			Rigidbody parent = Selection.activeGameObject.transform.root.gameObject.GetComponent<Rigidbody>();
			if (parent == null) { Debug.Log("Engine is not parented to an Aircraft!! Create a default Rigidbody is you're just testing the Engine"); }
			PhantomTurboShaft prop = Selection.activeGameObject.AddComponent<PhantomTurboShaft>();
			prop.exitPoint = exit.transform;
			prop.intakePoint = intake.transform;

			GameObject smoke = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Effects/Engine/Exhaust Smoke.prefab", typeof(GameObject));
			GameObject smokeEffect = Instantiate(smoke, effects.transform.position, Quaternion.Euler(0, -180, 0), effects.transform);
			GameObject distortion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Effects/Engine/Engine Distortion.prefab", typeof(GameObject));
			GameObject distortionEffect = Instantiate(distortion, effects.transform.position, Quaternion.Euler(0, -180, 0), effects.transform);

			AudioClip start = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Turbine/Exterior/Exterior Turbine Start.wav", typeof(AudioClip));
			AudioClip stop = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Turbine/Exterior/Exterior Turbine Stop.wav", typeof(AudioClip));
			AudioClip run = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Sounds/Engines/Turbine/Exterior/Exterior Turbine Idle.wav", typeof(AudioClip));
			prop.core = new PhantomEngineCore
			{
				exhaustSmoke = smokeEffect.GetComponent<ParticleSystem>(),
				exhaustDistortion = distortionEffect.GetComponent<ParticleSystem>(),
				distortionEmissionLimit = 20f,
				ignitionExterior = start,
				shutdownExterior = stop,
				backIdle = run
			};
		}
		else
		{
			Debug.Log("Please Select GameObject to add Engine to..");
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Power System/Fuel Tanks/Main", false, 1700)]
	private static void AddInternalTank()
	{
		GameObject tank;
		if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<PhantomDistributor>())
		{
			tank = new GameObject();
			tank.transform.parent = Selection.activeGameObject.transform; tank.transform.localPosition = new Vector3(0, 0, 0);
		}
		else
		{
			tank = new GameObject();
		}
		EditorSceneManager.MarkSceneDirty(tank.gameObject.scene);
		tank.name = "Main Fuel Tank";
		PhantomFuelTank fuelTank = tank.AddComponent<PhantomFuelTank>(); fuelTank.Capacity = 1000f; fuelTank.tankType = PhantomFuelTank.TankType.Main;
		EditorSceneManager.MarkSceneDirty(tank.gameObject.scene);
	}








	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Power System/Fuel Tanks/Auxillary", false, 1800)]
	private static void AddExternalTank()
	{
		GameObject tank;
		if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<PhantomDistributor>())
		{
			tank = new GameObject();
			tank.transform.parent = Selection.activeGameObject.transform; tank.transform.localPosition = new Vector3(0, 0, 0);
		}
		else
		{
			tank = new GameObject();
		}
		tank.name = "Auxillary Fuel Tank";
		EditorSceneManager.MarkSceneDirty(tank.gameObject.scene);
		PhantomFuelTank fuelTank = tank.AddComponent<PhantomFuelTank>(); fuelTank.Capacity = 400f; fuelTank.tankType = PhantomFuelTank.TankType.Auxillary;
		EditorSceneManager.MarkSceneDirty(tank.gameObject.scene);
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Rotor System/Tandem", false, 1900)]
	private static void AddTandemRotor()
	{
		GameObject rotorSystem;
		rotorSystem = new GameObject("_rotors");
		GameObject forwardRotor = new GameObject("Forward Rotor"); 
		GameObject backwardRotor = new GameObject("Backward Rotor");
		forwardRotor.transform.parent = rotorSystem.transform; 
		forwardRotor.transform.localPosition = new Vector3(0, 0, 4); 
		backwardRotor.transform.parent = rotorSystem.transform;
		backwardRotor.transform.localPosition = new Vector3(0, 0, -4);
		PhantomRotor ForwardRotor = forwardRotor.AddComponent<PhantomRotor>(); 
		PhantomRotor BackwardRotor = backwardRotor.AddComponent<PhantomRotor>(); 
		ForwardRotor.rotorType = PhantomRotor.RotorType.MainRotor;
		ForwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Tandem; ForwardRotor.tandemPosition = PhantomRotor.TandemPosition.Forward;
		BackwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Tandem; BackwardRotor.tandemPosition = PhantomRotor.TandemPosition.Rear;
		BackwardRotor.rotorDirection = PhantomRotor.RotationDirection.CCW;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		ForwardRotor.rootAirfoil = start; ForwardRotor.drawFoils = true;
		ForwardRotor.tipAirfoil = start; BackwardRotor.drawFoils = true;
		BackwardRotor.rootAirfoil = start;
		BackwardRotor.tipAirfoil = start;
		ForwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
		BackwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Rotor System/Coaxial ", false, 2000)]
	private static void AddCoaxialRotor()
	{
		GameObject rotorSystem;
		rotorSystem = new GameObject("_rotors");
		GameObject forwardRotor = new GameObject("Upper Rotor");
		GameObject backwardRotor = new GameObject("Lower Rotor");
		forwardRotor.transform.parent = rotorSystem.transform; 
		forwardRotor.transform.localPosition = new Vector3(0, 1, 0);
		backwardRotor.transform.parent = rotorSystem.transform;
		backwardRotor.transform.localPosition = new Vector3(0, -1, 0);
		PhantomRotor ForwardRotor = forwardRotor.AddComponent<PhantomRotor>();
		PhantomRotor BackwardRotor = backwardRotor.AddComponent<PhantomRotor>();
		ForwardRotor.rotorType = PhantomRotor.RotorType.MainRotor;
		ForwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Coaxial; ForwardRotor.rotorPosition = PhantomRotor.CoaxialPosition.Top;
		BackwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Coaxial; BackwardRotor.rotorPosition = PhantomRotor.CoaxialPosition.Bottom;
		BackwardRotor.rotorDirection = PhantomRotor.RotationDirection.CCW;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		ForwardRotor.rootAirfoil = start;
		ForwardRotor.tipAirfoil = start; ForwardRotor.drawFoils = true;
		BackwardRotor.rootAirfoil = start; BackwardRotor.drawFoils = true;
		BackwardRotor.tipAirfoil = start;
		ForwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
		BackwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Rotor System/Syncrocopter ", false, 2100)]
	private static void AddSyncroRotor()
	{
		GameObject rotorSystem;
		rotorSystem = new GameObject("_rotors");
		GameObject forwardRotor = new GameObject("Left Rotor"); 
		GameObject backwardRotor = new GameObject("Right Rotor"); 
		forwardRotor.transform.parent = rotorSystem.transform; 
		forwardRotor.transform.localPosition = new Vector3(0, 1, 0);
		backwardRotor.transform.parent = rotorSystem.transform;
		backwardRotor.transform.localPosition = new Vector3(0, -1, 0);
		PhantomRotor ForwardRotor = forwardRotor.AddComponent<PhantomRotor>(); 
		PhantomRotor BackwardRotor = backwardRotor.AddComponent<PhantomRotor>();
		ForwardRotor.rotorType = PhantomRotor.RotorType.MainRotor;
		ForwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Syncrocopter; ForwardRotor.syncroPosition = PhantomRotor.SyncroPosition.Left;
		BackwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Syncrocopter; BackwardRotor.syncroPosition = PhantomRotor.SyncroPosition.Right;
		BackwardRotor.rotorDirection = PhantomRotor.RotationDirection.CCW;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		ForwardRotor.rootAirfoil = start;
		ForwardRotor.tipAirfoil = start; ForwardRotor.drawFoils = true;
		BackwardRotor.rootAirfoil = start; BackwardRotor.drawFoils = true;
		BackwardRotor.tipAirfoil = start;
		ForwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
		BackwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Rotor System/Conventional/Main Rotor", false, 2200)]
	private static void AddMainRotor()
	{
		GameObject rotorSystem;
		rotorSystem = new GameObject("_rotors");
		GameObject forwardRotor = new GameObject("Main Rotor"); 
		forwardRotor.transform.parent = rotorSystem.transform; 
		forwardRotor.transform.localPosition = new Vector3(0, 1, 0);
		PhantomRotor ForwardRotor = forwardRotor.AddComponent<PhantomRotor>(); 
		ForwardRotor.rotorType = PhantomRotor.RotorType.MainRotor;
		ForwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Conventional;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		ForwardRotor.rootAirfoil = start; ForwardRotor.drawFoils = true;
		ForwardRotor.tipAirfoil = start;
		ForwardRotor.effectState = PhantomRotor.GroundEffectState.Consider;
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	[MenuItem("Oyedoyin/Rotary Wing/Rotor System/Conventional/Tail Rotor", false, 2300)]
	private static void AddTailRotor()
	{
		GameObject rotorSystem; 
		rotorSystem = new GameObject("_rotors");
		GameObject backwardRotor = new GameObject("Tail Rotor"); 
		backwardRotor.transform.parent = rotorSystem.transform; 
		backwardRotor.transform.localPosition = new Vector3(0, 0, -4);
		PhantomRotor BackwardRotor = backwardRotor.AddComponent<PhantomRotor>(); 
		BackwardRotor.rotorType = PhantomRotor.RotorType.TailRotor; BackwardRotor.rotorConfiguration = PhantomRotor.RotorConfiguration.Conventional;
		SilantroAirfoil start = (SilantroAirfoil)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Airfoils/NACA 0012.asset", typeof(SilantroAirfoil));
		BackwardRotor.rootAirfoil = start; BackwardRotor.drawFoils = true;
		BackwardRotor.tipAirfoil = start; BackwardRotor.soundState = PhantomRotor.SoundState.Silent;
	}







	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Create Internals", false, 2400)]
	public static void Helper1()
	{
		GameObject aircraft;
		if (Selection.activeGameObject != null)
		{
			aircraft = Selection.activeGameObject;
			EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.gameObject.scene);
			aircraft.name = "Default Helicopter";

			//Setup the controller
			Rigidbody sRigidbody = aircraft.GetComponent<Rigidbody>();
			if (sRigidbody == null) { sRigidbody = aircraft.AddComponent<Rigidbody>(); }
			sRigidbody.mass = 1000f;

			CapsuleCollider sCollider = aircraft.GetComponent<CapsuleCollider>();
			if (sCollider == null) { aircraft.AddComponent<CapsuleCollider>(); }

			PhantomController sController = aircraft.GetComponent<PhantomController>();
			if (sController == null) { aircraft.AddComponent<PhantomController>(); }


			GameObject core = new GameObject("_core");
			GameObject aerodynamics = new GameObject("_dynamics");
			GameObject power = new GameObject("_power");
			GameObject structure = new GameObject("_structure");
			GameObject avionics = new GameObject("_avionics");
			GameObject computer = new GameObject("_computer");
			GameObject transmission = new GameObject("_transmission");
			GameObject engine = new GameObject("_engine");
			GameObject cog = new GameObject("_empty_cog");


			GameObject body = new GameObject("Body");
			GameObject actuators = new GameObject("_actuators");
			GameObject cameraSystem = new GameObject("_cameras");
			GameObject focusPoint = new GameObject("Camera Focus Point");
			GameObject incamera = new GameObject("Interior Camera");
			GameObject outcamera = new GameObject("Exterior Camera");

			GameObject lights = new GameObject("_lights");
			
			Transform aircraftParent = aircraft.transform;
			Vector3 defaultPosition = new Vector3(0, 0, 0);

			core.transform.parent = aircraftParent; core.transform.localPosition = defaultPosition;
			cog.transform.parent = core.transform; cog.transform.localPosition = defaultPosition;
			aerodynamics.transform.parent = aircraftParent; aerodynamics.transform.localPosition = defaultPosition;
			power.transform.parent = aircraftParent; power.transform.localPosition = defaultPosition;
			structure.transform.parent = aircraftParent; structure.transform.localPosition = defaultPosition;

			transmission.transform.parent = power.transform; transmission.transform.localPosition = defaultPosition;
			engine.transform.parent = power.transform; engine.transform.localPosition = defaultPosition;

			body.transform.parent = structure.transform; body.transform.localPosition = defaultPosition;
			avionics.transform.parent = aircraftParent; avionics.transform.localPosition = defaultPosition;
			actuators.transform.parent = avionics.transform; actuators.transform.localPosition = defaultPosition;
			cameraSystem.transform.parent = avionics.transform; cameraSystem.transform.localPosition = defaultPosition;
			
			
			lights.transform.parent = avionics.transform; lights.transform.localPosition = defaultPosition;
			incamera.transform.parent = cameraSystem.transform; incamera.transform.localPosition = defaultPosition;
			outcamera.transform.parent = cameraSystem.transform; outcamera.transform.localPosition = defaultPosition;
			focusPoint.transform.parent = cameraSystem.transform; focusPoint.transform.localPosition = defaultPosition;
			computer.transform.parent = aircraftParent; computer.transform.localPosition = defaultPosition;

			GameObject bulb_caseR = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Avionics/Lights/Right Navigation.prefab", typeof(GameObject));
			GameObject bulbR = Instantiate(bulb_caseR, lights.transform.position, Quaternion.identity, lights.transform);
			bulbR.transform.localPosition = new Vector3(2, 0, 0);

			GameObject bulb_caseL = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Silantro Simulator/Rotary Wing/Prefabs/Avionics/Lights/Left Navigation.prefab", typeof(GameObject));
			GameObject bulbL = Instantiate(bulb_caseL, lights.transform.position, Quaternion.identity, lights.transform);
			bulbL.transform.localPosition = new Vector3(-2, 0, 0);

			//ADD CAMERAS
			Camera interior = incamera.AddComponent<Camera>(); incamera.AddComponent<AudioListener>(); Camera exterior = outcamera.AddComponent<Camera>(); outcamera.AddComponent<AudioListener>();
			PhantomCamera view = cameraSystem.AddComponent<PhantomCamera>();
			view.normalExterior = exterior; view.normalInterior = interior; view.focusPoint = focusPoint.transform;
			computer.AddComponent<PhantomFlightComputer>();
			PhantomControlModule module = core.AddComponent<PhantomControlModule>();module.emptyCenterOfMass = cog.transform;
			transmission.AddComponent<PhantomTransmission>();
		}
		else
		{
			Debug.Log("Please Select Aircraft GameObject to Setup..");
		}
	} 




	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Setup Input", false, 2500)]
	public static void InitializeQuick()
	{
		try
		{
			Handler.ControlAxis(new Handler.Axis() { name = "---------------- Buttons", positiveButton = "-", gravity = 0f, sensitivity = 0f, type = 0, descriptiveName = "Key 01" }, false);
			Handler.ControlAxis(new Handler.Axis() { name = "Start Engine", positiveButton = "f1", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 02" }, false);
			Handler.ControlAxis(new Handler.Axis() { name = "Stop Engine", positiveButton = "f2", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 05" }, false);

            Handler.ControlAxis(new Handler.Axis() { name = "Parking Brake", positiveButton = "x", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 08" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Brake Lever", positiveButton = "space", gravity = 0.0f, sensitivity = 0.6f, type = 0, descriptiveName = "Key 09" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Actuate Gear", positiveButton = "0", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 10" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "LightSwitch", positiveButton = "v", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 11" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Fire", positiveButton = "left ctrl", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 12" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Target Up", positiveButton = "m", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 13" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Target Down", positiveButton = "n", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 14" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Target Lock", positiveButton = "numlock", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 15" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Propeller Engage", positiveButton = "y", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 16" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Weapon Select", positiveButton = "q", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 17" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Weapon Release", positiveButton = "z", gravity = 0.5f, sensitivity = 1f, type = 0, descriptiveName = "Key 18" }, false);

			Handler.ControlAxis(new Handler.Axis() { name = "---------------- Keyboard Axes", positiveButton = "-", gravity = 0f, sensitivity = 0f, type = 0, descriptiveName = "Key 32" }, false);
			Handler.ControlAxis(new Handler.Axis() { name = "Throttle", positiveButton = "1", negativeButton = "2", gravity = 0.0f, sensitivity = 0.4f, type = 0, dead = 0.001f, descriptiveName = "Key 33" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Roll", positiveButton = "d", negativeButton = "a", altPositiveButton = "right", altNegativeButton = "left", gravity = 0.6f, sensitivity = 0.65f, type = 0, dead = 0.001f, descriptiveName = "Key 34" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Pitch", positiveButton = "w", negativeButton = "s", altPositiveButton = "up", altNegativeButton = "down", gravity = 0.6f, sensitivity = 0.7f, type = 0, dead = 0.001f, descriptiveName = "Key 35" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Rudder", positiveButton = "e", negativeButton = "q", gravity = 1f, sensitivity = 0.9f, type = 0, dead = 0.001f, descriptiveName = "Key 36" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Collective", positiveButton = "right alt", negativeButton = "left alt", gravity = 0.0f, sensitivity = 0.25f, type = 0, dead = 0.001f, descriptiveName = "Key 37" }, false);
            Handler.ControlAxis(new Handler.Axis() { name = "Propeller", positiveButton = "]", negativeButton = "[", gravity = 0.0f, sensitivity = 0.35f, type = 0, dead = 0.001f, descriptiveName = "Key 38" }, false);

			Handler.ControlAxis(new Handler.Axis() { name = "---------------- Joystick Axes", positiveButton = "-", gravity = 0f, sensitivity = 0f, type = 0, descriptiveName = "Key 39" }, false);
			Handler.ControlAxis(new Handler.Axis() { name = "Throttle", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 3, dead = 0.001f, invert = true, descriptiveName = "Key 40" }, true);
            Handler.ControlAxis(new Handler.Axis() { name = "Roll", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 0, dead = 0.001f, descriptiveName = "Key 41" }, true);
            Handler.ControlAxis(new Handler.Axis() { name = "Pitch", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 1, dead = 0.001f, descriptiveName = "Key 42" }, true);
            Handler.ControlAxis(new Handler.Axis() { name = "Rudder", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 2, dead = 0.001f, descriptiveName = "Key 43" }, true);
            Handler.ControlAxis(new Handler.Axis() { name = "Collective", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 4, dead = 0.001f, descriptiveName = "Key 44" }, true);
            Handler.ControlAxis(new Handler.Axis() { name = "Propeller", positiveButton = " ", negativeButton = " ", gravity = 1.0f, sensitivity = 1.0f, type = 2, axis = 5, dead = 0.001f, descriptiveName = "Key 45" }, true);

            Debug.Log("Input Setup Successful!");
		}
		catch
		{
			Debug.LogError("Failed to apply input manager bindings.");
		}
	}
	


	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Help/Forum Page", false, 2700)]
	public static void ForumPage()
	{
		Application.OpenURL("https://forum.unity.com/threads/released-silantro-helicopter-simulator.673468/");
	}
	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Help/Youtube Channel", false, 2800)]
	public static void YoutubeChannel()
	{
		Application.OpenURL("https://www.youtube.com/channel/UCYXrhYRzY11qokg59RFg7gQ/videos");
	}
	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Help/Update Log", false, 2900)]
	public static void UpdateLog()
	{
		Application.OpenURL("https://drive.google.com/file/d/1O0nL547y56B31uOocDSucq_tthqItBE9/view0");
	}
	[MenuItem("Oyedoyin/Rotary Wing/Miscellaneous/Help/Report Bug_Contact Developer", false, 3000)]
	public static void Contact()
	{
		Application.OpenURL("mailto:" + "silantrosimulator@gmail.com" + "?subject:" + "Silantro Bug" + "&body:" + " ");
	}
	[MenuItem("Oyedoyin/Rotary Wing/Drag System/Create Panel/Fuselage", false, 3100)]
	public static void CreateBaseFuselarge()
	{
		GameObject aircraft;
		if (Selection.activeGameObject != null)
		{
			aircraft = Selection.activeGameObject;
			EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.gameObject.scene);
			GameObject dragPanel = new GameObject("Fuselage Panel");
			dragPanel.transform.parent = aircraft.transform;
			dragPanel.transform.localPosition = Vector3.zero;

			//PLACE MARKERS
			PhantomBody fuselagePanel = dragPanel.AddComponent<PhantomBody>();
			fuselagePanel.AddElement();
			fuselagePanel.AddSupplimentElement(-1); fuselagePanel.AddSupplimentElement(-2);

		}
		else
		{
			Debug.Log("Please select a valid aircraft GameObject");
		}
	}
	[MenuItem("Oyedoyin/Rotary Wing/Drag System/Create Panel/Tail Boom", false, 3200)]
	public static void CreateTailBoom()
	{
		GameObject aircraft;
		if (Selection.activeGameObject != null)
		{
			aircraft = Selection.activeGameObject;
			EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.gameObject.scene);
			GameObject dragPanel = new GameObject("Boom Panel");
			dragPanel.transform.parent = aircraft.transform;
			dragPanel.transform.localPosition = Vector3.zero;

			//PLACE MARKERS
			PhantomBody fuselagePanel = dragPanel.AddComponent<PhantomBody>();
			fuselagePanel.AddElement(); fuselagePanel.maximumDiameter = 2f;
			fuselagePanel.AddSupplimentElement(-1); fuselagePanel.AddSupplimentElement(-2);

		}
		else
		{
			Debug.Log("Please select a valid aircraft GameObject");
		}
	}
	[MenuItem("Oyedoyin/Rotary Wing/Drag System/Tutorial", false, 3300)]
	public static void DragTutorial()
	{
		Application.OpenURL("https://www.youtube.com/watch?v=CvsMZcuUjmg");
	}
} 