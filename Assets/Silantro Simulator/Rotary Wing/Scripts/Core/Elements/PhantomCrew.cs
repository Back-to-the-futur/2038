using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class PhantomCrew : MonoBehaviour
{
    //--------------------------------------- Selectibles
    public enum Position { Internal, External }
    public Position position = Position.Internal;

    public enum Designation { Pilot, CoPilot, Gunner, ReconOfficer }
    public Designation designation = Designation.Pilot;

    public enum ControlType { ThirdPerson, FirstPerson }
    public ControlType controlType = ControlType.ThirdPerson;


    //--------------------------------------- Data
    public float weight;
    public Transform head;
    PhantomController controller;
    public GameObject body;
    public bool canEnter = false, isClose = false;
    public float maxRayDistance = 2f;




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//ENTER 
	public void SendEntryData()
	{
		if (isClose && canEnter)
		{
			//PLAYER INFO
			if (controller != null)
			{
				controller.player = this.gameObject;
				if (controlType == ControlType.FirstPerson)
				{
					controller.playerType = PhantomController.PlayerType.FirstPerson;
				}
				if (controlType == ControlType.ThirdPerson)
				{
					controller.playerType = PhantomController.PlayerType.ThirdPerson;
				}
				//SEND ACCEPT
				controller.EnterAircraft();
			}
		}
	}


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//DISPLAY ENTERY INFORMATION
	void OnGUI()
	{
		if (position == Position.External)
		{
			if (isClose && canEnter)
			{
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 100), "Press F to Enter");
			}
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		if (position == Position.External && controlType == ControlType.FirstPerson)
		{
			GameObject mainCameraObject = Camera.main.gameObject;
			if (mainCameraObject != null)
			{
				mainCameraObject.SetActive(true);
			}
		}
	}




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnDrawGizmos()
    {
        //DRAW IDENTIFIER
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, (this.transform.up * 2f + this.transform.position));
    }




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//CHECK AIRCRAFT DISTANCE
	void Update()
	{
		if (position == Position.External)
		{
			//SEND CHECK DATA
			CheckAircraftState();
			//ENTER
			if (Input.GetKeyDown(KeyCode.F)) { SendEntryData(); }
		}
	}



	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void CheckAircraftState()
	{
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		RaycastHit aircraft;

		if (Physics.Raycast(head.position, direction, out aircraft, maxRayDistance))
		{
			//COLLECT AIRCRAFT CONTROLLER
			controller = aircraft.transform.gameObject.GetComponent<PhantomController>();

			//PROCESS IF CONTROLLER IS AVAILABLE
			if (controller != null) { if (!controller.pilotOnboard) { isClose = true; } canEnter = true; }
			else { isClose = false; canEnter = false; }
		}

		else { isClose = false; canEnter = false; }
	}
}
