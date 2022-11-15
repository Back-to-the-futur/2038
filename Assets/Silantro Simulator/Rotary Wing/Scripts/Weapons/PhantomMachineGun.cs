using Oyedoyin;
using UnityEngine;






public class PhantomMachineGun : MonoBehaviour
{

	// ----------------------------------------------- Selectibles
	public enum BulletType { Raycast, Rigidbody }
	public BulletType bulletType;
	public enum RotationAxis { X, Y, Z }
	public RotationAxis rotationAxis = RotationAxis.X;
	public enum RotationDirection { CW, CCW }
	public RotationDirection rotationDirection = RotationDirection.CCW;



	// ----------------------------------------------- Variables
	public float rateOfFire = 500;
	public float actualRate;
	public float fireTimer;
	public float accuracy = 80f;
	public float currentAccuracy;
	public float accuracyDrop = 0.2f;
	public float accuracyRecover = 0.5f;
	float acc;

	public float muzzleVelocity = 500;
	public float barrelLength = 2f;
	public float gunWeight;
	public float drumWeight;
	public float damage;
	private float barrelRPM;
	public float currentRPM;
	public Vector3 baseVelocity;


	public float projectileForce;
	public float damperStrength = 90f;
	public int ammoCapacity = 1000;
	public int currentAmmo;
	public bool unlimitedAmmo;
	private int muzzle = 0;
	public bool advancedSettings;
	public float range = 1000f;
	public float rangeRatio = 1f;


	private float shellSpitForce = 1.5f;
	private float shellForceRandom = 1.5f;
	private float shellSpitTorqueX = 0.5f;
	private float shellSpitTorqueY = 0.5f;
	private float shellTorqueRandom = 1.0f;
	public bool ejectShells = false;
	public bool canFire = true;
	public bool running;




	// ----------------------------------------------- Connections
	public PhantomController controller;
	public GameObject ammunition;
	GameObject currentBullet;
	public Transform barrel;
	public GameObject bulletCase;
	public Transform shellEjectPoint;
	public Transform[] muzzles;
	private Transform currentMuzzle;


	// ----------------------------------------------- Sounds
	public AudioClip fireLoopSound;
	public AudioClip fireEndSound;
	public float soundVolume = 0.75f;
	public AudioSource gunLoopSource, gunEndSource;
	float bulletMass;
	public float soundRange = 150f;


	// ---------------------------------------------- Effects
	public GameObject muzzleFlash;
	public GameObject groundHit;
	public GameObject metalHit;
	public GameObject woodHit;//ADD MORE





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//FIRE FUNCTION
	public void FireGun()
	{
		if (canFire) { if ((fireTimer > (actualRate))) { Fire(); } }
		//OFFLINE
		else { Debug.Log("Gun System Offline"); }
	}





	bool allOk;
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	protected void _checkPrerequisites()
	{
		//CHECK COMPONENTS
		if (controller != null && fireLoopSound != null && fireEndSound != null)
		{
			allOk = true;
		}
		else if (controller == null)
		{
			Debug.LogError("Prerequisites not met on " + transform.name + "....Aircraft rigidbody not assigned");
			allOk = false;
		}
		else if (fireEndSound == null)
		{
			Debug.LogError("Prerequisites not met on " + transform.name + "....fire end clip not assigned");
			allOk = false;
		}
		else if (fireLoopSound == null)
		{
			Debug.LogError("Prerequisites not met on " + transform.name + "....fire loop clip not assigned");
			allOk = false;
		}
	}







	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeGun()
	{


		//----------------------------
		_checkPrerequisites();


		if (allOk)
		{
			//SETUP FIRE RATE
			if (rateOfFire > 0)
			{
				float secFireRate = rateOfFire / 60f;//FROM RPM TO RPS
				actualRate = 1.0f / secFireRate;
			}
			else { actualRate = 0.01f; }
			fireTimer = 0.0f;

			// -------------------------------------------------- Base
			currentAmmo = ammoCapacity; barrelRPM = rateOfFire; currentAccuracy = accuracy;
			CountBullets();


			if (fireLoopSound) { Handler.SetupSoundSource(this.transform, fireLoopSound, "Loop Sound Point", soundRange, true, false, out gunLoopSource); gunLoopSource.volume = soundVolume; }
			if (fireEndSound) { Handler.SetupSoundSource(this.transform, fireEndSound, "End Sound Point", soundRange, false, false, out gunEndSource); gunEndSource.volume = soundVolume; }
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void CountBullets()
	{
		// --------------------------------------------------CALCULATE BULLET DRUM WEIGHT
		if (bulletType == BulletType.Rigidbody)
		{
			if (ammunition != null && ammunition.GetComponent<PhantomMunition>() != null)
			{
				bulletMass = ammunition.GetComponent<PhantomMunition>().mass;
			}
			if (ammunition == null) { Debug.Log("Gun " + transform.name + " ammunition gameobject has not been assigned"); return; }
			if (ammunition.GetComponent<PhantomMunition>() == null) { Debug.Log("Gun " + transform.name + " bullet gameobject is invalid, use the prefabs in the Prefabs/Sample/Ammunition/Bullets folder"); }
		}
		else
		{
			bulletMass = 150f;
		}
		drumWeight = currentAmmo * ((bulletMass * 0.0648f) / 1000f);
		if (currentAmmo > 0) { canFire = true; }
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//SOUND
	void Update()
	{
		if (allOk)
		{
			if (running && currentAmmo <= 0) { gunLoopSource.Stop(); gunEndSource.PlayOneShot(fireEndSound); running = false; }

			if (fireLoopSound != null && fireEndSound != null && canFire)
			{
				if (running && gunLoopSource != null && !gunLoopSource.isPlaying) { gunLoopSource.Play(); }
				if (!running && fireLoopSound != null && gunLoopSource.isPlaying) { gunLoopSource.Stop(); gunEndSource.PlayOneShot(fireEndSound); }
				if (controller.hardpoints != null && controller.hardpoints.currentWeapon != "Gun")
				{
					if (gunEndSource.isPlaying) { gunEndSource.Stop(); }
					if (gunLoopSource.isPlaying) { gunLoopSource.Stop(); }
				}
			}
		}
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//REFRESH
	void LateUpdate()
	{
		fireTimer += Time.deltaTime;
		//CLAMP RPM
		if (currentRPM <= 0f) { currentRPM = 0f; }
		//LERP ACCURACY
		currentAccuracy = Mathf.Lerp(currentAccuracy, accuracy, accuracyRecover * Time.deltaTime);
		//CLAMP AMMO
		if (currentAmmo < 0) { currentAmmo = 0; }
		if (currentAmmo == 0) { canFire = false; }
		//CLAMP ROTATION
		if (currentRPM < 0) { currentRPM = 0; }
		if (currentRPM > barrelRPM) { currentRPM = barrelRPM; }

		//ROTATE BARREL
		if (barrel)
		{
			//ANTICLOCKWISE
			if (rotationDirection == RotationDirection.CCW)
			{
				if (rotationAxis == RotationAxis.X) { barrel.Rotate(new Vector3(currentRPM * Time.deltaTime, 0, 0)); }
				if (rotationAxis == RotationAxis.Y) { barrel.Rotate(new Vector3(0, currentRPM * Time.deltaTime, 0)); }
				if (rotationAxis == RotationAxis.Z) { barrel.Rotate(new Vector3(0, 0, currentRPM * Time.deltaTime)); }
			}
			//CLOCKWISE
			if (rotationDirection == RotationDirection.CW)
			{
				if (rotationAxis == RotationAxis.X) { barrel.Rotate(new Vector3(-1f * currentRPM * Time.deltaTime, 0, 0)); }
				if (rotationAxis == RotationAxis.Y) { barrel.Rotate(new Vector3(0, -1f * currentRPM * Time.deltaTime, 0)); }
				if (rotationAxis == RotationAxis.Z) { barrel.Rotate(new Vector3(0, 0, -1f * currentRPM * Time.deltaTime)); }
			}
		}
		//
		//REV GUN UP AND DOWN
		if (running)
		{
			currentRPM = Mathf.Lerp(currentRPM, barrelRPM, Time.deltaTime * 0.5f);
		}
		else
		{
			currentRPM = Mathf.Lerp(currentRPM, 0f, Time.deltaTime * 0.5f);
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//ACTUAL FIRE
	void Fire()
	{
		//MAKE SURE THEIR IS A BARREL TO FIRE FROM
		if (muzzles.Length > 0)
		{
			// --------------------------------------------------SELECT A MUZZLE
			muzzle += 1;
			if (muzzle > (muzzles.Length - 1)) { muzzle = 0; }
			currentMuzzle = muzzles[muzzle]; fireTimer = 0f;
			if (controller.helicopter != null) { baseVelocity = controller.helicopter.velocity; }

			// --------------------------------------------------REDUCE AMMO COUNT
			if (!unlimitedAmmo) { currentAmmo--; }
			CountBullets();

			// --------------------------------------------------FIRE DIRECTION AND ACCURACY
			Vector3 direction = currentMuzzle.forward;
			Ray rayout = new Ray(currentMuzzle.position, direction);
			RaycastHit hitout;
			if (Physics.Raycast(rayout, out hitout, range / rangeRatio)) { acc = 1 - ((hitout.distance) / (range / rangeRatio)); }
			// --------------------------------------------------VARY ACCURACY
			float accuracyVary = (100 - currentAccuracy) / 1000;
			direction.x += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
			direction.y += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
			direction.z += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
			currentAccuracy -= accuracyDrop;
			if (currentAccuracy <= 0.0f) currentAccuracy = 0.0f;
			//
			Quaternion muzzleRotation = Quaternion.LookRotation(direction);

			//1. FIRE RIGIDBODY AMMUNITION
			if (bulletType == BulletType.Rigidbody)
			{
				//SHOOT RIGIDBODY
				currentBullet = Instantiate(ammunition, currentMuzzle.position, muzzleRotation) as GameObject;
				PhantomMunition munition = currentBullet.GetComponent<PhantomMunition>();
				if (munition != null)
				{
					munition.controller = controller;
					munition.InitializeMunition();
					munition.ejectionPoint = this.transform.position;
					munition.FireBullet(muzzleVelocity, baseVelocity);
					munition.woodHit = woodHit;
					munition.metalHit = metalHit;
					munition.groundHit = groundHit;
				}
			}

			//2. FIRE RAYCAST AMMUNITION
			if (bulletType == BulletType.Raycast)
			{
				//SETUP RAYCAST
				Ray ray = new Ray(currentMuzzle.position, direction);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, range / rangeRatio))
				{
					//DAMAGE
					float damageeffect = damage * acc;
					hit.collider.gameObject.SendMessage("SilantroDamage", -damageeffect, SendMessageOptions.DontRequireReceiver);
					//INSTANTIATE EFFECTS
					if (hit.collider.CompareTag("Ground") && groundHit != null) { Instantiate(groundHit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); }
					//METAL
					if (hit.collider.CompareTag("Metal") && metalHit != null) { Instantiate(metalHit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); }
					//WOOD
					if (hit.collider.CompareTag("Wood") && woodHit != null) { Instantiate(woodHit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); }
				}
			}

			// --------------------------------------------------RECOIL
			if (controller != null)
			{
				//SET BULLET WEIGHT
				float bulletWeight;
				if (bulletType == BulletType.Rigidbody)
				{
					bulletWeight = currentBullet.GetComponent<PhantomMunition>().mass;
				}
				else
				{
					bulletWeight = 150f;
				}
				float ballisticEnergy = 0.5f * ((bulletWeight * 0.0648f) / 1000f) * muzzleVelocity * muzzleVelocity * UnityEngine.Random.Range(0.9f, 1f);
				projectileForce = ballisticEnergy / barrelLength;

				//APPLY
				Vector3 recoilForce = controller.transform.forward * (-projectileForce * (1 - (damperStrength / 100f)));
				controller.helicopter.AddForce(recoilForce, ForceMode.Impulse);
			}

			// --------------------------------------------------MUZZLE FLASH
			if (muzzleFlash != null)
			{
				GameObject flash = Instantiate(muzzleFlash, currentMuzzle.position, currentMuzzle.rotation);
				flash.transform.position = currentMuzzle.position; flash.transform.parent = currentMuzzle.transform;
			}

			// --------------------------------------------------SHELLS
			if (ejectShells && bulletCase != null)
			{
				GameObject shellGO = Instantiate(bulletCase, shellEjectPoint.position, shellEjectPoint.rotation) as GameObject;
				shellGO.GetComponent<Rigidbody>().velocity = baseVelocity;
				shellGO.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shellSpitForce + UnityEngine.Random.Range(0, shellForceRandom), 0, 0), ForceMode.Impulse);
				shellGO.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(shellSpitTorqueX + UnityEngine.Random.Range(-shellTorqueRandom, shellTorqueRandom), shellSpitTorqueY + UnityEngine.Random.Range(-shellTorqueRandom, shellTorqueRandom), 0), ForceMode.Impulse);
			}
		}
		//NO AVAILABLE MUZZLE
		else { Debug.Log("Gun bullet points not setup properly"); }
	}

}
