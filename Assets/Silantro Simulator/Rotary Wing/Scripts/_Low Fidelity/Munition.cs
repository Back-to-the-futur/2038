using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Oyedoyin.Rotary.LowFidelity
{
	public class Munition : MonoBehaviour
	{

		//--------------------- Selectibles
		public enum BurnType { Regressive, Neutral, Progressive }
		public BurnType burnType = BurnType.Neutral;
		public enum MunitionType { Guided, UnGuided }
		public MunitionType munitionType = MunitionType.Guided;
		public enum MunitionForm { SecantOgive, TangentOgive, RoundNose }
		public MunitionForm munitionForm = MunitionForm.RoundNose;


		//--------------------- Connections
		public Rigidbody launcher;
		public Rigidbody munition;
		public Transform motor;
		public AnimationCurve burnCurve;
		public ParticleSystem exhaustSmoke;
		public ParticleSystem exhaustFlame;
		ParticleSystem.EmissionModule smokeModule;
		ParticleSystem.EmissionModule flameModule;
		public GameObject explosionPrefab;
		public Transform m_target;



		//--------------------- Data
		public float factor, fireDuration = 5f, burnTime;
		float activeTime;
		public float maximumSmokeEmissionValue = 50f;
		public float maximumFlameEmissionValue = 50f;


		public float munitionWeight = 20f;
		public float munitionDiameter = 1f;
		public float munitionLength = 1f;
		public float munitionSpeed, munitionMach;
		public float maximumSpeed = 1;
		public float munitionThrust = 10000f;
		public float maximumRange = 1000f;
		public float distanceTravelled;
		public float distanceToTarget;
		public float thrust;
		public float acceleration;
		public float lockDirection;
		public float minimumLockDirection = 0.4f;


		public float proximity = 100f;//Distance to target
		bool lostTarget;
		public bool armed;
		bool exploded;
		public bool active;
		public bool seeking;

		//--------------------- Drag
		public float airDensity, soundSpeed;
		public float dragCoefficient;
		public float surfaceArea;
		public float skinningRatio;
		public float drag;
		public float baseCoefficient;



		//--------------------- Sound
		public AudioClip motorSound;
		AudioSource boosterSound;
		public float maximumPitch = 1.2f;
		Vector3 lastPosition;



		//--------------------- Navigation
		public float navigationConstant = 3f;
		public float maximumTurnRate = 150f;
		public Vector3 sightLine;
		public Vector3 bodyRate;





		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		//SMART/GUIDED MUNITIONS
		/// <summary>
		/// Fires guided munition.
		/// </summary>
		/// <param name="markedTarget">locked target from the radar.</param>
		/// <param name="ID">tracking ID for the locked target</param>
		/// <param name="mode">fire mode for the missiles; 1: Drop, 2: Tube, 3: Trapeze Launch</param>
		public void FireMunition(Transform markedTarget, string ID)
		{
			Vector3 dropVelocity = Vector3.zero;
			if (launcher != null) { dropVelocity = launcher.velocity; }

			active = true;
			burnTime = 0.0f;
			munition.transform.parent = null;
			munition.isKinematic = false;
			munition.useGravity = false;
			munition.velocity = dropVelocity;

			if (munitionType == MunitionType.Guided)
			{
				m_target = markedTarget;
				seeking = true;
				StartCoroutine(TimeStep(0.3f));
			}
		}



		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Releases the bomb or rocket.
		/// </summary>
		public void ReleaseMunition()
		{
			Vector3 dropVelocity = Vector3.zero;
			if (launcher != null) { dropVelocity = launcher.velocity; }

			active = true;
			burnTime = 0.0f;
			munition.transform.parent = null;
			munition.isKinematic = false;
			munition.useGravity = false;
			munition.velocity = dropVelocity;
			armed = true;
		}








		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		public void InitializeMunition()
		{
			munition = GetComponent<Rigidbody>();
			if (munition == null) { Debug.Log("Rigidbody for munition is missing " + transform.name); return; }
			munition.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			munition.mass = munitionWeight;
			munition.isKinematic = true;
			lastPosition = transform.position;


			if (exhaustSmoke != null) { smokeModule = exhaustSmoke.emission; smokeModule.rateOverTime = 0.0f; }
			if (exhaustFlame != null) { flameModule = exhaustFlame.emission; flameModule.rateOverTime = 0.0f; }
			if (motorSound) { Handler.SetupSoundSource(this.transform, motorSound, "Booster Sound", 80f, true, true, out boosterSound); }
			PlotDataSet(); armed = false; active = false;


			// ---------------------------------- Drag Settings
			if (munitionForm == MunitionForm.RoundNose) { skinningRatio = 0.95f; baseCoefficient = 0.0235f; }
			if (munitionForm == MunitionForm.SecantOgive) { skinningRatio = 0.913f; baseCoefficient = 0.0171f; }
			if (munitionForm == MunitionForm.TangentOgive) { skinningRatio = 0.914f; baseCoefficient = 0.0165f; }
			surfaceArea = skinningRatio * 2f * Mathf.PI * (munitionDiameter / 2f) * munitionLength;
		}







		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		void FixedUpdate()
		{
			if (munition.isKinematic && active && armed) { munition.isKinematic = false; }
			if (munition.useGravity && active && armed) { munition.useGravity = false; }


			if (active)
			{
				burnTime += Time.deltaTime;
				activeTime = burnTime / fireDuration;
				if (armed) { if (m_target != null) { distanceToTarget = Vector3.Distance(transform.position, m_target.position); } }
				factor = burnCurve.Evaluate(activeTime);
				thrust = munitionThrust * factor;
				acceleration = thrust / munitionWeight;
				if (thrust < 0) { thrust = 0f; }
				if (burnTime > fireDuration) { active = false; }


				// ------------------------------- Sound
				boosterSound.volume = factor;
				boosterSound.pitch = maximumPitch * factor;


				// ------------------------------- Effects
				if (!smokeModule.enabled && exhaustSmoke != null) { smokeModule = exhaustSmoke.emission; }
				if (!flameModule.enabled && exhaustFlame != null) { flameModule = exhaustFlame.emission; }
				if (exhaustFlame) { flameModule.rateOverTime = maximumFlameEmissionValue * factor; }
				if (exhaustSmoke) { smokeModule.rateOverTime = maximumSmokeEmissionValue * factor; }
			}


			//----------------------------------- Data
			munitionSpeed = munition.velocity.magnitude;
			if (munitionSpeed > 1)
			{
				float altitude = munition.gameObject.transform.position.y * 3.28084f;
				float kelvinTemperatrue;
				float a = 0.0000004f * altitude * altitude;
				float b = (0.0351f * altitude);
				float ambientPressure = (a - b + 1009.6f) / 10f;
				float a1 = 0.000000003f * altitude * altitude;
				float a2 = 0.0021f * altitude;
				float ambientTemperature = a1 - a2 + 15.443f;
				kelvinTemperatrue = ambientTemperature + 273.15f;
				airDensity = (ambientPressure * 1000f) / (287.05f * kelvinTemperatrue);
				soundSpeed = Mathf.Pow((1.2f * 287f * (273.15f + ambientTemperature)), 0.5f);
				munitionMach = (munitionSpeed) / soundSpeed;
			}






			// ----------------------------- Forces
			if (munition != null)
			{
				if (active)
				{
					float trueSpeed = soundSpeed * maximumSpeed;
					float dynamicForce = 0.5f * airDensity * trueSpeed * trueSpeed * surfaceArea;
					dragCoefficient = thrust / dynamicForce;
				}
				else { dragCoefficient = baseCoefficient; }
				if (float.IsNaN(dragCoefficient) || float.IsInfinity(dragCoefficient)) { dragCoefficient = 0.01f; }
				drag = 0.5f * airDensity * dragCoefficient * munitionSpeed * munitionSpeed * surfaceArea;
				distanceTravelled += Vector3.Distance(transform.position, lastPosition);
				lastPosition = transform.position;


				Vector3 thrustForce = transform.forward * thrust;
				if (!float.IsNaN(thrust) && !float.IsInfinity(thrust)) { munition.AddForce(thrustForce, ForceMode.Force); }
				Vector3 dragForce = munition.velocity.normalized * -drag;
				if (!float.IsNaN(drag) && !float.IsInfinity(drag)) { munition.AddForce(dragForce, ForceMode.Force); }
			}
		}
		// ---------------------------------------
		void LateUpdate() { if (armed) { EvaluateMunition(); } }







		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		void OnDrawGizmos() { PlotDataSet(); }
		void PlotDataSet()
		{
			burnCurve = new AnimationCurve();

			//PROGRESSIVE BURN
			if (burnType == BurnType.Progressive)
			{
				burnCurve.AddKey(new Keyframe(0, 0));
				burnCurve.AddKey(new Keyframe(0.041055718f, 0.102848764f));
				burnCurve.AddKey(new Keyframe(0.065982405f, 0.195789207f));
				burnCurve.AddKey(new Keyframe(0.089442815f, 0.311990335f));
				burnCurve.AddKey(new Keyframe(0.11143695f, 0.421551817f));
				burnCurve.AddKey(new Keyframe(0.126099707f, 0.524493136f));
				burnCurve.AddKey(new Keyframe(0.148093842f, 0.644021395f));
				burnCurve.AddKey(new Keyframe(0.171554252f, 0.766867041f));
				burnCurve.AddKey(new Keyframe(0.212609971f, 0.85643164f));
				burnCurve.AddKey(new Keyframe(0.269794721f, 0.916042322f));
				burnCurve.AddKey(new Keyframe(0.348973607f, 0.949001861f));
				burnCurve.AddKey(new Keyframe(0.423753666f, 0.978653754f));
				burnCurve.AddKey(new Keyframe(0.498533724f, 1.008305648f));
				burnCurve.AddKey(new Keyframe(0.57771261f, 1.044587446f));
				burnCurve.AddKey(new Keyframe(0.658357771f, 1.084186631f));
				burnCurve.AddKey(new Keyframe(0.739002933f, 1.117141298f));
				burnCurve.AddKey(new Keyframe(0.799120235f, 1.140197387f));
				burnCurve.AddKey(new Keyframe(0.857771261f, 1.15329157f));
				burnCurve.AddKey(new Keyframe(0.895894428f, 1.0568194f));
				burnCurve.AddKey(new Keyframe(0.920821114f, 0.917201703f));
				burnCurve.AddKey(new Keyframe(0.939882698f, 0.797537047f));
				burnCurve.AddKey(new Keyframe(0.957478006f, 0.64465467f));
				burnCurve.AddKey(new Keyframe(0.972140762f, 0.498426555f));
				burnCurve.AddKey(new Keyframe(0.983870968f, 0.342241405f));
				burnCurve.AddKey(new Keyframe(0.992668622f, 0.202677293f));
				burnCurve.AddKey(new Keyframe(0.998533724f, 0.086378738f));
				burnCurve.AddKey(new Keyframe(1f, 0f));
			}

			//NEUTRAL BURN
			if (burnType == BurnType.Neutral)
			{
				burnCurve.AddKey(new Keyframe(0f, 0f));
				burnCurve.AddKey(new Keyframe(0.011540828f, 0.128099174f));
				burnCurve.AddKey(new Keyframe(0.021510337f, 0.289256198f));
				burnCurve.AddKey(new Keyframe(0.036367648f, 0.47107438f));
				burnCurve.AddKey(new Keyframe(0.046363904f, 0.648760331f));
				burnCurve.AddKey(new Keyframe(0.059603092f, 0.830578512f));
				burnCurve.AddKey(new Keyframe(0.076031721f, 0.983471074f));
				burnCurve.AddKey(new Keyframe(0.142508492f, 1.066115702f));
				burnCurve.AddKey(new Keyframe(0.215324026f, 1.066115702f));
				burnCurve.AddKey(new Keyframe(0.297821552f, 1.049586777f));
				burnCurve.AddKey(new Keyframe(0.381943887f, 1.037190083f));
				burnCurve.AddKey(new Keyframe(0.454739362f, 1.024793388f));
				burnCurve.AddKey(new Keyframe(0.537250261f, 1.016528926f));
				burnCurve.AddKey(new Keyframe(0.618143037f, 1.008264463f));
				burnCurve.AddKey(new Keyframe(0.697404317f, 0.991735537f));
				burnCurve.AddKey(new Keyframe(0.771817914f, 0.979338843f));
				burnCurve.AddKey(new Keyframe(0.841377143f, 0.966942149f));
				burnCurve.AddKey(new Keyframe(0.893076841f, 0.917355372f));
				burnCurve.AddKey(new Keyframe(0.92032416f, 0.756198347f));
				burnCurve.AddKey(new Keyframe(0.936251304f, 0.599173554f));
				burnCurve.AddKey(new Keyframe(0.952198508f, 0.454545455f));
				burnCurve.AddKey(new Keyframe(0.971335152f, 0.280991736f));
				burnCurve.AddKey(new Keyframe(0.987289042f, 0.140495868f));
				burnCurve.AddKey(new Keyframe(1.001651555f, 0f));
			}


			//REGRESSIVE
			if (burnType == BurnType.Regressive)
			{
				burnCurve.AddKey(new Keyframe(0.005592615f, 0.006872852f));
				burnCurve.AddKey(new Keyframe(0.027852378f, 0.113402062f));
				burnCurve.AddKey(new Keyframe(0.045896022f, 0.23024055f));
				burnCurve.AddKey(new Keyframe(0.062505415f, 0.371134021f));
				burnCurve.AddKey(new Keyframe(0.079114807f, 0.512027491f));
				burnCurve.AddKey(new Keyframe(0.094280324f, 0.683848797f));
				burnCurve.AddKey(new Keyframe(0.106663971f, 0.841924399f));
				burnCurve.AddKey(new Keyframe(0.127489484f, 0.972508591f));
				burnCurve.AddKey(new Keyframe(0.15672317f, 1.099656357f));
				burnCurve.AddKey(new Keyframe(0.190221106f, 1.182130584f));
				burnCurve.AddKey(new Keyframe(0.249059074f, 1.171821306f));
				burnCurve.AddKey(new Keyframe(0.302323679f, 1.140893471f));
				burnCurve.AddKey(new Keyframe(0.36541627f, 1.092783505f));
				burnCurve.AddKey(new Keyframe(0.436897783f, 1.054982818f));
				burnCurve.AddKey(new Keyframe(0.505582989f, 1.013745704f));
				burnCurve.AddKey(new Keyframe(0.588302675f, 0.951890034f));
				burnCurve.AddKey(new Keyframe(0.649994706f, 0.903780069f));
				burnCurve.AddKey(new Keyframe(0.727083273f, 0.862542955f));
				burnCurve.AddKey(new Keyframe(0.805582027f, 0.81443299f));
				burnCurve.AddKey(new Keyframe(0.856088827f, 0.75257732f));
				burnCurve.AddKey(new Keyframe(0.902446889f, 0.652920962f));
				burnCurve.AddKey(new Keyframe(0.930636172f, 0.525773196f));
				burnCurve.AddKey(new Keyframe(0.943404853f, 0.408934708f));
				burnCurve.AddKey(new Keyframe(0.958979468f, 0.288659794f));
				burnCurve.AddKey(new Keyframe(0.975978708f, 0.151202749f));
				burnCurve.AddKey(new Keyframe(0.999985561f, 0f));
			}
		}
		IEnumerator WaitForMomentumShead(Vector3 collisionPoint, Quaternion collisionRotation) { yield return new WaitForSeconds(0.02f); Explode(collisionPoint, collisionRotation); }
		void OnTriggerEnter(Collider other) { if (other.transform != launcher.transform && munition.velocity.magnitude > 40f) { WaitForMomentumShead(transform.position, Quaternion.identity); } }
		IEnumerator TimeStep(float time) { yield return new WaitForSeconds(time); armed = true; }




		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		private void EvaluateMunition()
		{
			if (munitionType == MunitionType.Guided)
			{
				// ------------------ Out of Range
				if (m_target == null && distanceTravelled > maximumRange) { Explode(transform.position, Quaternion.identity); }
				// ------------------ Proximity
				if (distanceTravelled > 50f && distanceToTarget > 0 && distanceToTarget < proximity && armed) { Explode(transform.position, Quaternion.identity); }




				// ------------------ Navigation
				if (seeking)
				{
					Vector3 targetDirection = (m_target.transform.position - munition.transform.position).normalized;
					lockDirection = Vector3.Dot(targetDirection, transform.forward);
					if (lockDirection < minimumLockDirection) { seeking = false; }
					Vector3 prevSightLine = sightLine;
					sightLine = m_target.position - transform.position;
					Vector3 δLOS = sightLine - prevSightLine;
					δLOS = δLOS - Vector3.Project(δLOS, sightLine);
					bodyRate = Time.fixedDeltaTime * sightLine + δLOS * navigationConstant + Time.fixedDeltaTime * bodyRate * navigationConstant / 2;
					bodyRate = Vector3.ClampMagnitude(bodyRate * acceleration, acceleration);
					Quaternion targetRotation = Quaternion.LookRotation(bodyRate, transform.up);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * maximumTurnRate);
				}
			}
		}





		// ----------------------------------------------------------------------------------------------------------------------------------------------------------
		public void Explode(Vector3 collisionPosition, Quaternion collisionRotation)
		{
			if (explosionPrefab != null && !exploded)
			{
				GameObject explosion = Instantiate(explosionPrefab, collisionPosition, collisionRotation);
				explosion.SetActive(true);
				explosion.GetComponentInChildren<AudioSource>().Play();
				Rigidbody explosionBody = explosion.GetComponent<Rigidbody>();
				if (explosionBody != null)
				{
					explosionBody.mass = munitionWeight;
					explosionBody.drag = 1f;
					explosionBody.velocity = munition.velocity;
				}
				exploded = true;
			}
			Destroy(gameObject);
		}
	}
}