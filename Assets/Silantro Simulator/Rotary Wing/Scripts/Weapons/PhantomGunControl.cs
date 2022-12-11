using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PhantomGunControl : MonoBehaviour
{
	// ------------------------- Connections
	public PhantomRadar connectedRadar;
	public PhantomMachineGun connectedGun;
	public Transform verticalPivot;
	public Transform horizontalPivot;
	public Transform muzzleCenter;
	public Transform lockedTarget;
	
	
	// ------------------------- Variables
	public float horizontalSpeed = 20;
	public float verticalSpeed = 5;
	public float horizontalLimit = 50f;
	public float verticalLimit = 30f;
	Vector3 horizontalRotationAxis, initialMuzzlePosition;
	Quaternion initalHorizontalRotation, initialVerticalRotation;




#if UNITY_EDITOR
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnDrawGizmos()
	{
		// ------------------------- Draw
		if (lockedTarget != null && lockedTarget != null) { Handles.color = Color.red; Handles.DrawLine(lockedTarget.position, muzzleCenter.position); }
		if (connectedGun != null && muzzleCenter != null)
		{
			float gunRange = connectedGun.range;
			Vector3 hitPosition = muzzleCenter.position + (muzzleCenter.forward * gunRange);
			Handles.color = Color.green;
			Handles.DrawLine(hitPosition, muzzleCenter.position);
		}

		//DRAW BASE GUN VIEW
		Vector3 forwardPosition = transform.position + (transform.forward * 0.65f);
		Handles.color = Color.yellow;
		Handles.DrawLine(forwardPosition, transform.position);
	}
#endif




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeGunControl()
	{
		initalHorizontalRotation = horizontalPivot.localRotation;
		initialVerticalRotation = verticalPivot.localRotation;
		initialMuzzlePosition = muzzleCenter.position;
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (lockedTarget != null)
		{
			// ------------------------- Horizontal Rotation
			float currentAngle; Vector3 targetPosition; Quaternion targetRotation;
			if (horizontalPivot != null && horizontalLimit != 0f)
			{
				targetPosition = horizontalPivot.InverseTransformPoint(lockedTarget.position);
				currentAngle = Mathf.Atan2(targetPosition.x, targetPosition.z) * Mathf.Rad2Deg;
				if (currentAngle >= 180f) { currentAngle = 180 - currentAngle; }
				if (currentAngle <= -180f) { currentAngle = -180f + currentAngle; }
				//ROTATE HORIZONTAL
				targetRotation = horizontalPivot.rotation * Quaternion.Euler(0f, Mathf.Clamp(currentAngle, -horizontalSpeed * Time.deltaTime, horizontalSpeed * Time.deltaTime), 0f);
				if (horizontalLimit < 360f && horizontalLimit > 0f)
				{ horizontalPivot.rotation = Quaternion.RotateTowards(horizontalPivot.parent.rotation * initalHorizontalRotation, targetRotation, horizontalLimit); }
				else { horizontalPivot.rotation = targetRotation; }
			}

			// ------------------------- Vertical Rotation
			if (verticalPivot != null && verticalLimit != 0f)
			{
				targetPosition = verticalPivot.InverseTransformPoint(lockedTarget.position);
				float verticalAngle = -Mathf.Atan2(targetPosition.y, targetPosition.z) * Mathf.Rad2Deg;
				if (verticalAngle >= 180f) { verticalAngle = 180f - verticalAngle; }
				if (verticalAngle <= -180f) { verticalAngle = -180f + verticalAngle; }
				//ROTATE VERTICAL
				targetRotation = verticalPivot.rotation * Quaternion.Euler(Mathf.Clamp(verticalAngle, -verticalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime), 0f, 0f);
				if (verticalLimit < 360f && verticalLimit > 0.0f)
				{ verticalPivot.rotation = Quaternion.RotateTowards(verticalPivot.parent.rotation * initialVerticalRotation, targetRotation, verticalLimit); }
				else { verticalPivot.rotation = targetRotation; }
			}
		}
	}
}
