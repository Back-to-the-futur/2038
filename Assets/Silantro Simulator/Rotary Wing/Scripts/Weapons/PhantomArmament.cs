using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oyedoyin.Rotary.LowFidelity;



/// <summary>
///
/// 
/// Use:		 Handles the processing and firing of the attached weapons
/// </summary>


public class PhantomArmament : MonoBehaviour
{

    // -------------------------------- Components
    public PhantomMunition[] munitions;
    public List<PhantomMunition> missiles;
    public List<PhantomMunition> rockets;
    public List<PhantomMunition> AAMS;//AIR TO AIR MISSILES
    public List<PhantomMunition> AGMS;//AIR TO GROUND MISSILES
    public PhantomRadar connectedRadar;
    public PhantomController controller;
	public PhantomMachineGun[] attachedGuns;
	public Munition[] lowMunition;
	public List<Munition> lowRockets;
	public List<Munition> lowMissiles;


	// -------------------------------- Variables
	bool canFire;
    bool canLaunch;
    public float rateOfFire = 3f;
    public float actualFireRate;
    float fireTimer;
    public float weaponsLoad;
	public float bulletLoad;
	public float viewDirection;

	// -------------------------------- Weapons
	public List<string> availableWeapons = new List<string>();
    public string currentWeapon;
    public int selectedWeapon;
    public bool setRocket;
    public bool setMissile;

    public AudioClip fireSound;
    public float fireVolume = 0.7f;
    AudioSource launcherSound;




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //0. CHANGE SELECTED WEAPON
    public void ChangeWeapon()
    {
        if (controller.isControllable)
        {
            selectedWeapon += 1;
            if (selectedWeapon > (availableWeapons.Count - 1)) { selectedWeapon = 0; }
            currentWeapon = availableWeapons[selectedWeapon];
        }
    }

	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//2. SELECT WEAPON
	public void SelectWeapon(int weaponPoint)
	{
		currentWeapon = availableWeapons[weaponPoint];
	}






	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//1. ROCKET
	public void FireRocket()
	{
		if (controller != null && controller.isControllable)
		{
			if (rockets.Count > 0)
			{
				fireTimer = 0f;
				int index = UnityEngine.Random.Range(0, rockets.Count);
				if (rockets[index] != null)
				{
					rockets[index].ReleaseMunition();
					launcherSound.PlayOneShot(fireSound);
					CountOrdnance();
				}
			}
			else { Debug.Log("Rocket System Offline"); }

			// ------------------------------------------ Low Fidelity Rockets
			if(lowRockets.Count > 0)
            {
				fireTimer = 0f;
				int index = UnityEngine.Random.Range(0, rockets.Count);
				if (rockets[index] != null)
				{
					lowRockets[index].ReleaseMunition();
					launcherSound.PlayOneShot(fireSound);
					CountOrdnance();
				}
			}
			else { Debug.Log("Low Fidelity Rocket System Offline"); }
		}
		else { Debug.Log("Weapon System Offline"); }
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//2. MISSILE
	public void FireMissile()
	{
		if (controller != null && controller.isControllable)
		{
			CountOrdnance();
			//MAKE SURE MISSILES ARE AVAILABLE
			if (missiles.Count > 0)
			{
				//COLLECT TARGET DATA
				if (connectedRadar != null && connectedRadar.lockedTarget != null)
				{

					//CHECK DIRECTION
					if (viewDirection < 0.6f)
					{
						Debug.Log("Missile launch cancelled. Reason: Target out of view range");
					}
					else
					{
						//1. PROCESS AIR TARGET
						if (connectedRadar.lockedTarget.form == "Helicopter")
						{
							//SELECT AAM
							if (AAMS.Count > 0)
							{
								//SELECT RANDOM AAM
								int index = UnityEngine.Random.Range(0, AAMS.Count);//SELECT RANDOM MISSILE
								if (AAMS[index] != null)
								{
									Transform lockedTarget = connectedRadar.lockedTarget.body.transform;//SET TARGET
																										//AAMS[index].supportRadar = connectedRadar;//SET SUPPORT RADAR ?? FOR SEMI_ACTIVE GUIDANCE
									string LockedID = connectedRadar.lockedTarget.trackingID;//SET TARGET ID
									AAMS[index].target = lockedTarget;
									AAMS[index].FireMunition(lockedTarget, LockedID);//TRIGGER
								}
							}
							else if (AGMS.Count > 0)
							{
								Debug.Log("No AAM Available so AGM has been launched");
								//SELECT RANDOM AGM
								int index = UnityEngine.Random.Range(0, AGMS.Count);
								if (AGMS[index] != null)
								{
									Transform lockedTarget = connectedRadar.lockedTarget.body.transform;//SET TARGET
																										//AGMS[index].supportRadar = connectedRadar;//SET SUPPORT RADAR ?? FOR SEMI_ACTIVE GUIDANCE
									string LockedID = connectedRadar.lockedTarget.trackingID;//SET TARGET ID
									AGMS[index].target = lockedTarget;
									AGMS[index].FireMunition(lockedTarget, LockedID);//TRIGGER
								}
							}
							//PLAY SOUND
							launcherSound.PlayOneShot(fireSound);
							CountOrdnance();
						}

						//2. PROCESS GROUND TARGET
						else if (connectedRadar.lockedTarget.form == "SAM Battery" || connectedRadar.lockedTarget.form == "Truck" || connectedRadar.lockedTarget.form == "Tank")
						{
							//SELECT AGM
							if (AGMS.Count > 0)
							{
								//SELECT RANDOM AGM
								int index = UnityEngine.Random.Range(0, AGMS.Count);
								if (AGMS[index] != null)
								{
									Transform lockedTarget = connectedRadar.lockedTarget.body.transform;//SET TARGET
									AGMS[index].target = lockedTarget;
									//AGMS[index].supportRadar = connectedRadar;//SET SUPPORT RADAR ?? FOR SEMI_ACTIVE GUIDANCE
									string LockedID = connectedRadar.lockedTarget.trackingID;//SET TARGET ID
									AGMS[index].FireMunition(lockedTarget, LockedID);//TRIGGER
								}
							}
							else if (AAMS.Count > 0)
							{
								Debug.Log("No AGM Available so AAM has been launched");
								//SELECT RANDOM AAM
								int index = UnityEngine.Random.Range(0, AAMS.Count);
								if (AAMS[index] != null)
								{
									Transform lockedTarget = connectedRadar.lockedTarget.body.transform;//SET TARGET
									AAMS[index].target = lockedTarget;
									//AAMS[index].supportRadar = connectedRadar;//SET SUPPORT RADAR ?? FOR SEMI_ACTIVE GUIDANCE
									string LockedID = connectedRadar.lockedTarget.trackingID;//SET TARGET ID
									AAMS[index].FireMunition(lockedTarget, LockedID);//TRIGGER
								}
							}
							//PLAY SOUND
							launcherSound.PlayOneShot(fireSound);
							CountOrdnance();
						}
						//3.
						else { Debug.Log("Locked Target form is either null or not supported. You can add a new definition in the Armaments code"); }
					}
				}
				//NO LOCKED TARGET
				else { Debug.Log("Locked Target/Radar Unavailable"); }
			}
			//---------------------- No Normal Missiles
			else { Debug.Log("Base Missile System Offline"); }






			// ---------------------------------------------------------------------------LOW FIDELITY-------------------------------------------------------------------------------
			if (lowMissiles.Count > 0)
            {
				if (connectedRadar != null && connectedRadar.lockedTarget != null)
				{
					if (viewDirection < 0.6f)
					{
						Debug.Log("Missile launch cancelled. Reason: Target out of view range");
					}
					else
					{
						if (connectedRadar.lockedTarget.form == "Helicopter" || connectedRadar.lockedTarget.form == "SAM Battery" || connectedRadar.lockedTarget.form == "Truck" || connectedRadar.lockedTarget.form == "Tank")
						{
							int index = UnityEngine.Random.Range(0, AGMS.Count);
							Transform lockedTarget = connectedRadar.lockedTarget.body.transform;//SET TARGET
							string LockedID = connectedRadar.lockedTarget.trackingID;//SET TARGET ID
							lowMissiles[index].m_target = lockedTarget;
							lowMissiles[index].FireMunition(lockedTarget, LockedID);//TRIGGER
							launcherSound.PlayOneShot(fireSound);
							CountOrdnance();
						}
						else { Debug.Log("Locked Target form is either null or not supported. You can add a new definition in the Armaments code"); }
					}
				}
				else { Debug.Log("Locked Target/Radar Unavailable"); }
			}
			else { Debug.Log("Low Fidelity Missile System Offline"); }
		}
		else { Debug.Log("Weapon System Offline"); }
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//3. GUNS
	public void FireGuns()
	{
		if (controller != null && controller.isControllable)
		{
			if (attachedGuns.Length > 0)
			{
				bulletLoad = 0;
				foreach (PhantomMachineGun gun in attachedGuns)
				{
					gun.FireGun();
					bulletLoad += gun.drumWeight;
				}
			}
			//NO GUNS
			else { Debug.Log("Gun System Offline"); }
		}
		//NOT CONTROLLABLE
		else { Debug.Log("Weapon System Offline"); }
	}










	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeWeapons()
	{

		if (connectedRadar == null) { Debug.Log("No Radar is connected to the aircraft, some functionalities will not work"); return; }
		if (fireSound) { Oyedoyin.Handler.SetupSoundSource(transform, fireSound, "Exterior Sound Point", 100f, false, false, out launcherSound); }
		launcherSound.volume = fireVolume;
		if (rateOfFire != 0) { actualFireRate = 1.0f / rateOfFire; } else { actualFireRate = 0.01f; } fireTimer = 0.0f;


		attachedGuns = GetComponentsInChildren<PhantomMachineGun>(); bulletLoad = 0;
		foreach (PhantomMachineGun gun in attachedGuns)
		{
			if (connectedRadar != null) { gun.controller = connectedRadar.controller; }
			gun.InitializeGun();
			bulletLoad += gun.drumWeight;
		}
		if (connectedRadar != null)
		{
			munitions = connectedRadar.controller.gameObject.GetComponentsInChildren<PhantomMunition>();
			foreach (PhantomMunition munition in munitions)
			{
				munition.helicopter = controller.helicopter;
				munition.controller = controller;
				munition.InitializeMunition();
			}

			lowMunition = connectedRadar.controller.gameObject.GetComponentsInChildren<Munition>();
			foreach (Munition munition in lowMunition)
			{
				munition.launcher = controller.helicopter;
				munition.InitializeMunition();
			}
		}

		CountOrdnance();
		if (attachedGuns.Length > 0) { availableWeapons.Add("Gun"); }
		if (missiles.Count > 0 || lowMissiles.Count > 0) { availableWeapons.Add("Missile"); }
		if (rockets.Count > 0 || lowRockets.Count > 0) { availableWeapons.Add("Rocket"); }
		selectedWeapon = 0; currentWeapon = availableWeapons[selectedWeapon];
	}









	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//COUNT WEAPONS
	public void CountOrdnance()
	{
		if (connectedRadar != null)
		{
			munitions = connectedRadar.controller.gameObject.GetComponentsInChildren<PhantomMunition>();

			//RESET BUCKETS
			weaponsLoad = 0f;
			rockets = new List<PhantomMunition>();
			missiles = new List<PhantomMunition>();
			AAMS = new List<PhantomMunition>();
			AGMS = new List<PhantomMunition>();
			lowMissiles = new List<Munition>();
			lowRockets = new List<Munition>();
			
			//COUNT
			foreach (PhantomMunition munition in munitions)
			{
				weaponsLoad += munition.munitionWeight;
				//SEPARATE ROCKETS
				if (munition.munitionType == PhantomMunition.MunitionType.Rocket)
				{
					rockets.Add(munition);
				}
				//SEPARATE MISSILE
				if (munition.munitionType == PhantomMunition.MunitionType.Missile)
				{
					//CHECK IF LAUNCH SEQUENCE HAS BEEN INITIALIZED
					missiles.Add(munition);
					//BY TYPE
					if (munition.missileType == PhantomMunition.MissileType.AAM)
					{
						AAMS.Add(munition);
					}
					if (munition.missileType == PhantomMunition.MissileType.ASM)
					{
						AGMS.Add(munition);
					}
				}
			}

			lowMunition = connectedRadar.controller.gameObject.GetComponentsInChildren<Munition>();
			// -------------------- Low Fidelity Munitions
			foreach (Munition munition in lowMunition)
			{
				weaponsLoad += munition.munitionWeight;
				if (munition.munitionType == Munition.MunitionType.Guided) { lowMissiles.Add(munition); }
				if (munition.munitionType == Munition.MunitionType.UnGuided) { lowRockets.Add(munition); }
			}
		}
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//REFRESH
	void Update()
	{
		fireTimer += Time.deltaTime;
		if (connectedRadar != null && connectedRadar.lockedTarget != null && connectedRadar.lockedTarget.body != null)
		{
			Vector3 targetDirection = (connectedRadar.lockedTarget.body.transform.position - connectedRadar.transform.position).normalized;
			viewDirection = Vector3.Dot(targetDirection, connectedRadar.transform.forward);
		}
	}
}
