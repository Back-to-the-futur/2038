using Oyedoyin;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif




/// <summary>
///
/// 
/// Use:		 Handles the wheel collider(s) operations and states e.g Rotating, Tracking with the selected wheel mesh and braking.		 
/// </summary>
/// 



public class PhantomGearSystem : MonoBehaviour
{
	public List<WheelSystem> wheelSystem = new List<WheelSystem>();
	public WheelCollider[] wheelColliders;
	public bool showWheels = true, evaluate;


	//-------------------------------------------------CONNECTIONs
	public PhantomController controller;
	public Rigidbody helicopter;


	//---------------------------------------------------BRAKE
	public enum BrakeState { Engaged, Disengaged }
	public BrakeState brakeState = BrakeState.Engaged;
	public float brakeInput;
	public float brakeTorque = 10000f; //Nm



	//--------------------------------------------------STEERING AXLE CONFIG
	public Transform steeringAxle;
	public enum RotationAxis { X, Y, Z }
	public RotationAxis rotationAxis = RotationAxis.X;
	Vector3 steerAxis; Quaternion baseAxleRotation;
	public bool invertAxleRotation;
	public float rudderInput, currentSteerAngle;
	public float maximumSteerAngle = 40f;
	float rumbleLimit;


	//------------------------------------------------WHEEL RUMBLE
	public float maximumRumbleVolume = 1f, currentVolume, currentPitch;
	public AudioSource soundSource, brakeSource; public AudioClip groundRoll;
	public AudioClip brakeEngage, brakeRelease;
	bool initialized; float helicopterSpeed;


	//--------------------------------------------WHEEL SYSTEM
	[System.Serializable]
	public class WheelSystem
	{
		//--------------PROPERTIES
		public string Identifier; public WheelCollider collider; public Transform wheelModel;
		public enum WheelRotationAxis { X, Y, Z }
		public float wheelRPM;
		public WheelRotationAxis rotationWheelAxis = WheelRotationAxis.X; public bool steerable;
		//-------------STORAGE
		[HideInInspector] public Vector3 initialWheelPosition;[HideInInspector] public Quaternion initialWheelRotation;
	}





	/// <summary>
	/// For testing purposes only
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void Start()
	{
		if (evaluate) { InitializeStruct(); }
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeStruct()
	{
		if (helicopter != null)
		{
			foreach (WheelSystem system in wheelSystem)
			{
				if (system.wheelModel != null)
				{
					system.initialWheelPosition = system.wheelModel.transform.localPosition;
					system.initialWheelRotation = system.wheelModel.transform.localRotation;
				}
			}

			//----------------COLLECT AXLE DATA
			if (rotationAxis == RotationAxis.X) { steerAxis = new Vector3(1, 0, 0); }
			else if (rotationAxis == RotationAxis.Y) { steerAxis = new Vector3(0, 1, 0); }
			else if (rotationAxis == RotationAxis.Z) { steerAxis = new Vector3(0, 0, 1); }
			steerAxis.Normalize(); if (steeringAxle != null) { baseAxleRotation = steeringAxle.localRotation; }

			wheelColliders = helicopter.gameObject.GetComponentsInChildren<WheelCollider>();

			//--------------SETUP GROUND ROLL
			if (groundRoll != null) { Handler.SetupSoundSource(transform, groundRoll, "Struct Sound Point", 150f, true, true, out soundSource); }
			if (brakeRelease) { Handler.SetupSoundSource(transform, brakeEngage, "Brake Sound Point", 150f, false, false, out brakeSource); brakeSource.volume = maximumRumbleVolume; }
			initialized = true;
		}
		else { return; }
	}



	float actualSteer;
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void Update()
	{
		if (initialized)
		{
			//--------------------------INPUTS
			currentSteerAngle = rudderInput * maximumSteerAngle;
			currentSteerAngle = Mathf.Clamp(currentSteerAngle, -maximumSteerAngle, maximumSteerAngle);


			if (invertAxleRotation) { currentSteerAngle *= -1f; }
			if (helicopter != null) { helicopterSpeed = helicopter.velocity.magnitude; }


			//--------------------- Brake Force
			if (controller.helicopter != null)
			{
				Vector3 brakeForce = -controller.helicopter.velocity * brakeInput * brakeTorque;
				controller.helicopter.AddForce(brakeForce, ForceMode.Force);
			}




			foreach (WheelSystem system in wheelSystem)
			{
				//----------------BRAKE
				BrakingSystem(system);
				//---------------SEND ROTATION DATA

				RotateWheel(system.wheelModel, system);

				if (system.collider.isGrounded)
				{
					//---------------SEND ALIGNMENT DATA
					WheelAllignment(system, system.wheelModel);
				}

				//-----------------RETURN TO BASE POINT
				if (!system.collider.isGrounded && helicopter != null && helicopter.transform.position.y > 5)
				{
					system.wheelModel.transform.localPosition = system.initialWheelPosition;
					system.wheelModel.transform.localRotation = system.initialWheelRotation;
				}


				//------------------STEERING
				if (system.collider.isGrounded) { actualSteer = currentSteerAngle; } else { actualSteer = 0f; }
				if (system.steerable && system.collider != null) { system.collider.steerAngle = actualSteer; }
				if (steeringAxle != null) { steeringAxle.localRotation = baseAxleRotation; steeringAxle.Rotate(steerAxis, actualSteer); }
			}


			//---------------------WHEEL SOUND
			if (soundSource != null) { PlayRumbleSound(); }
		}
	}











	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	// ---------------------------------------------------CONTROL FUNCTIONS--------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------



	//--------------------------------------------------ACTIVATE BRAKES
	public void EngageBrakes()
	{
		if (helicopter != null && initialized)
		{
			if (brakeState != BrakeState.Engaged)
			{
				brakeState = BrakeState.Engaged;
				if (brakeSource != null)
				{
					if (brakeSource.isPlaying) { brakeSource.Stop(); }
					brakeSource.PlayOneShot(brakeEngage);
				}
				else { Debug.LogError("Brake sounds for" + transform.name + " has not been assigned"); }
			}
		}
		if (helicopter == null) { Debug.LogError("Aircraft for " + transform.name + " has not been assigned"); }
	}


	//--------------------------------------------------RELEASE BRAKES
	public void ReleaseBrakes()
	{
		if (helicopter != null && initialized)
		{
			if (brakeState != BrakeState.Disengaged)
			{
				brakeState = BrakeState.Disengaged;
				if (brakeSource != null)
				{
					if (brakeSource.isPlaying) { brakeSource.Stop(); }
					brakeSource.PlayOneShot(brakeRelease);
				}
				else { Debug.LogError("Brake sounds for " + transform.name + " has not been assigned"); }
			}
		}
		if (helicopter == null) { Debug.LogError("Aircraft for " + transform.name + " has not been assigned"); }
	}



	//-------------------------------------------------- TOGGLE BRAKES
	public void ToggleBrakes()
	{
		if (helicopter != null && initialized)
		{
			if (brakeState != BrakeState.Disengaged) { ReleaseBrakes(); }
			else { EngageBrakes(); }
		}
		if (helicopter == null) { Debug.LogError("Aircraft for " + transform.name + " has not been assigned"); }
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	// ---------------------------------------------------CALL FUNCTIONS--------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------







	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//ROTATE WHEEL
	void RotateWheel(Transform wheel, WheelSystem system)
	{
		if (system.collider != null && system.collider.isGrounded)
		{
			float circumfrence = 2f * Mathf.PI * system.collider.radius; float speed = helicopterSpeed * 60f;
			system.wheelRPM = speed / circumfrence;
		}
		else { system.wheelRPM = 0f; }

		if (wheel != null)
		{
			if (system.rotationWheelAxis == WheelSystem.WheelRotationAxis.X) { wheel.Rotate(new Vector3(system.wheelRPM * Time.deltaTime, 0, 0)); }
			if (system.rotationWheelAxis == WheelSystem.WheelRotationAxis.Y) { wheel.Rotate(new Vector3(0, system.wheelRPM * Time.deltaTime, 0)); }
			if (system.rotationWheelAxis == WheelSystem.WheelRotationAxis.Z) { wheel.Rotate(new Vector3(0, 0, system.wheelRPM * Time.deltaTime)); }
		}
	}






	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	////ALLIGN WHEEL TO COLLIDER
	void WheelAllignment(WheelSystem system, Transform wheel)
	{
		if (wheel != null)
		{
			RaycastHit hit; WheelHit CorrespondingGroundHit;

			//-------------------------------------------------------------------------------------
			if (system.collider != null)
			{
				Vector3 ColliderCenterPoint = system.collider.transform.TransformPoint(system.collider.center); system.collider.GetGroundHit(out CorrespondingGroundHit);

				if (Physics.Raycast(ColliderCenterPoint, -system.collider.transform.up, out hit, (system.collider.suspensionDistance + system.collider.radius) * transform.localScale.y))
				{
					wheel.position = hit.point + (system.collider.transform.up * system.collider.radius) * transform.localScale.y;
					float extension = (-system.collider.transform.InverseTransformPoint(CorrespondingGroundHit.point).y - system.collider.radius) / system.collider.suspensionDistance;
					Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point + system.collider.transform.up, extension <= 0.0 ? Color.magenta : Color.white);
					Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point - system.collider.transform.forward * CorrespondingGroundHit.forwardSlip * 2f, Color.green);
					Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point - system.collider.transform.right * CorrespondingGroundHit.sidewaysSlip * 2f, Color.red);
				}
				else
				{
					wheel.transform.position = Vector3.Lerp(wheel.transform.position, ColliderCenterPoint - (system.collider.transform.up * system.collider.suspensionDistance) * transform.localScale.y, Time.deltaTime * 10f);
				}
			}
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//BRAKE
	void BrakingSystem(WheelSystem wheel)
	{
		if (brakeInput < 0) { brakeInput = 0; }

		//------------------------CALCULATE BRAKE LEVER TORQUE
		float actualTorque = 0f;

		//------------------------PARKING BRAKE
		if (wheel != null && wheel.collider != null && !wheel.steerable)
		{
			if (brakeState == BrakeState.Engaged) { wheel.collider.brakeTorque = brakeTorque + actualTorque; wheel.collider.motorTorque = 0; }
			else { wheel.collider.motorTorque = 10; if (actualTorque > 10) { wheel.collider.brakeTorque = actualTorque; } else { wheel.collider.brakeTorque = 0f; } }
		}
	}






	//------------------------------------------------------------------------------------------
	public bool GroundCheck()

	{
		for (int i = 0; i < wheelColliders.Length; i++) { if (wheelColliders[i].isGrounded == true) { return true; } }
		return false;
	}




	//------------------------------------------------------------------------------------------
	void PlayRumbleSound()
	{
		if (wheelSystem[0].collider != null && wheelSystem[1].collider != null && groundRoll != null)
		{
			if (wheelSystem[0].collider.isGrounded && wheelSystem[1].collider.isGrounded)
			{

				//-----------------------SET PARAMETERS
				if (controller != null && controller.view != null)
				{
					if (controller.view.cameraState == PhantomCamera.CameraState.Exterior) { rumbleLimit = maximumRumbleVolume; }
					else if (controller.view.cameraState == PhantomCamera.CameraState.Interior) { rumbleLimit = 0.3f * maximumRumbleVolume; }
				}
				else { rumbleLimit = maximumRumbleVolume; }

				currentPitch = helicopterSpeed / 50f; currentVolume = helicopterSpeed / 20f;
				currentVolume = Mathf.Clamp(currentVolume, 0, maximumRumbleVolume); currentPitch = Mathf.Clamp(currentPitch, 0, 1f);
				soundSource.volume = Mathf.Lerp(soundSource.volume, currentVolume, 0.2f);
			}
			else { soundSource.volume = Mathf.Lerp(soundSource.volume, 0f, 0.2f); }
		}
	}
}
