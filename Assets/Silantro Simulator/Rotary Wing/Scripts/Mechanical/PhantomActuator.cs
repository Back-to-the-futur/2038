using Oyedoyin;
using UnityEngine;
using System.Collections.Generic;





/// <summary>
///
/// 
/// Use:		 Handles the movement of actuator surfaces based on the imported animation sequence		 
/// </summary>
/// 


[HelpURL("https://youtu.be/cwLsm8w8tGg")]
public class PhantomActuator : MonoBehaviour
{
	//------------------------------------------------------------------------MOVEMENT
	public float currentActuationLevel, targetActuationLevel, actuationSpeed = 0.2f;


	//------------------------------------------------------------------------ANIMATION
	public Animator actuatorAnimator;
	public string animationName = "Component Animation";
	public int animationLayer = 0;

	public List<PhantomBulb> landingBulbs;

	//------------------------------------------------------------------------DRAG
	public bool generatesDrag;
	public float dragFactor = 0.01f, currentDragFactor;
	public bool invertMotion, engaged, invertDrag;
	public float multiplier = 1f;
	public enum ActuatorState { Engaged, Disengaged }
	public ActuatorState actuatorState = ActuatorState.Disengaged;
	public enum ActuatorMode { DefaultOpen, DefaultClose }
	public ActuatorMode actuatorMode = ActuatorMode.DefaultClose;
	public enum ActuatorType { LandingGear, Canopy, Door, Custom, GunCover }
	public ActuatorType actuatorType = ActuatorType.LandingGear;


	//------------------------------------------------------------------------SOUND
	public enum SoundType { Simple, Complex }
	public SoundType soundType = SoundType.Complex;
	public AudioClip EngageClip, disengageClip;
	public AudioClip EngageLoopClip, EngageEndClip;
	public AudioSource EngageLoopPoint, EngageEndPoint;
	public AudioSource actuationSoundPoint;

	public bool initialized;
	public bool evaluate;


	/// <summary>
	/// For testing purposes only
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void Start()
	{
		if (evaluate) { InitializeActuator(); }
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeActuator()
	{
		if (actuatorAnimator != null)
		{
			if (actuatorMode == ActuatorMode.DefaultOpen) { actuatorState = ActuatorState.Engaged; }
			if (EngageLoopClip) { Handler.SetupSoundSource(this.transform, EngageLoopClip, "Loop Point", 150f, true, false, out EngageLoopPoint); EngageLoopPoint.volume = 1f; }
			if (EngageEndClip) { Handler.SetupSoundSource(this.transform, EngageEndClip, "Clip Point", 150f, false, false, out EngageEndPoint); EngageEndPoint.volume = 1f; }
			if (EngageClip) { Handler.SetupSoundSource(this.transform, EngageClip, "Clip Point", 150f, false, false, out actuationSoundPoint); actuationSoundPoint.volume = 1f; }
			initialized = true;
		}
		else { Debug.LogError("Animator for " + transform.name + " has not been assigned"); return; }
		if (EngageEndClip == null && EngageClip == null && EngageLoopClip == null && disengageClip == null) { Debug.LogError("Audio Clips for " + transform.name + " has not been assigned"); return; }
	}



	// ---------------------------------------------------CONTROLS-------------------------------------------------------------------------------------------------------
	// ----------------------------OPEN
	public void EngageActuator()
	{
		if (initialized && !engaged)
		{
			if (soundType == SoundType.Complex) { if (EngageLoopPoint.isPlaying) { EngageLoopPoint.Stop(); } if (EngageEndPoint.isPlaying) { EngageEndPoint.Stop(); } }
			if (invertMotion)
			{
				targetActuationLevel = 1; if (currentActuationLevel < 0.01f)
				{
					engaged = true; if (soundType == SoundType.Simple)
					{
						if (EngageClip)
						{
							actuationSoundPoint.PlayOneShot(EngageClip);
						}
					}
				}
			}
			else
			{
				targetActuationLevel = 0; if (currentActuationLevel > 0.99f)
				{
					engaged = true; if (soundType == SoundType.Simple) { if (EngageClip) { actuationSoundPoint.PlayOneShot(EngageClip); } }
				}
			}
		}
	}


	// ----------------------------CLOSE
	public void DisengageActuator()
	{
		if (initialized && !engaged)
		{
			if (soundType == SoundType.Complex) { if (EngageLoopPoint.isPlaying) { EngageLoopPoint.Stop(); } if (EngageEndPoint.isPlaying) { EngageEndPoint.Stop(); } }
			if (invertMotion)
			{
				targetActuationLevel = 0; if (currentActuationLevel > 0.99f)
				{
					engaged = true; if (soundType == SoundType.Simple)
					{
						if (disengageClip)
						{
							actuationSoundPoint.PlayOneShot(disengageClip);
						}
					}
				}
			}
			else
			{
				targetActuationLevel = 1; if (currentActuationLevel < 0.01f)
				{
					engaged = true;
					if (soundType == SoundType.Simple) { if (disengageClip) { actuationSoundPoint.PlayOneShot(disengageClip); } }
				}
			}
			if (actuatorType == ActuatorType.LandingGear && landingBulbs != null) { foreach (PhantomBulb bulb in landingBulbs) { if (bulb.state == PhantomBulb.CurrentState.On) { bulb.SwitchOff(); } } }
		}
	}








	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (initialized)
		{
			//ADJUST CONTROL VARIABLE
			if (currentActuationLevel != targetActuationLevel) { currentActuationLevel = Mathf.MoveTowards(currentActuationLevel, targetActuationLevel, Time.deltaTime * actuationSpeed * multiplier); }

			if (invertDrag) { if (generatesDrag) { currentDragFactor = (1 - currentActuationLevel) * dragFactor; } }
			else { if (generatesDrag) { currentDragFactor = (currentActuationLevel) * dragFactor; } }

			//--------------------------------------ANIMATE
			if (actuatorAnimator != null) { actuatorAnimator.Play(animationName, animationLayer, currentActuationLevel); }

			//--------------------------------------SOUND
			if (engaged) { AnalyseSound(targetActuationLevel); }
		}
	}




	//----------------------------------------------------------------------- SOUND MANAGEMENT
	void AnalyseSound(float target)
	{
		if (target == 0) { if (currentActuationLevel > 0.05f) { if (!EngageLoopPoint.isPlaying) { EngageLoopPoint.Play(); } } else { EngageLoopPoint.Stop(); EngageEndPoint.PlayOneShot(EngageEndClip); engaged = false; AnalyseState(0); } }
		if (target == 1) { if (currentActuationLevel < 0.95f) { if (!EngageLoopPoint.isPlaying) { EngageLoopPoint.Play(); } } else { EngageLoopPoint.Stop(); EngageEndPoint.PlayOneShot(EngageEndClip); engaged = false; AnalyseState(1); } }
	}




	// ---------------------------------------------------------------------- State
	void AnalyseState(int set)
	{
		if (actuatorMode == ActuatorMode.DefaultClose)
		{
			if (set == 0) { actuatorState = ActuatorState.Disengaged; if (actuatorType == ActuatorType.LandingGear && landingBulbs != null) { foreach (PhantomBulb bulb in landingBulbs) { if (bulb.gameObject.activeSelf) { bulb.gameObject.SetActive(false); } } } }
			if (set == 1) { actuatorState = ActuatorState.Engaged; if (actuatorType == ActuatorType.LandingGear && landingBulbs != null) { foreach (PhantomBulb bulb in landingBulbs) { if (bulb.gameObject.activeSelf) { bulb.gameObject.SetActive(true); } } } }
		}
		else
		{
			if (set == 0) { actuatorState = ActuatorState.Engaged; if (actuatorType == ActuatorType.LandingGear && landingBulbs != null) { foreach (PhantomBulb bulb in landingBulbs) { if (bulb.gameObject.activeSelf) { bulb.gameObject.SetActive(true); } } } }
			if (set == 1) { actuatorState = ActuatorState.Disengaged; if (actuatorType == ActuatorType.LandingGear && landingBulbs != null) { foreach (PhantomBulb bulb in landingBulbs) { if (bulb.gameObject.activeSelf) { bulb.gameObject.SetActive(false); } } } }
		}
	}
}
