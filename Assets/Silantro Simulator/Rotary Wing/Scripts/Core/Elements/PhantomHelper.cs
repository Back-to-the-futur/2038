using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif




[Serializable]
public class PhantomHelper
{

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InternalControlSetup(PhantomController controller)
    {
        if (!controller.getOutPosition)
        {
            GameObject getOutPos = new GameObject("Get Out Position");
            getOutPos.transform.SetParent(controller.transform);
            getOutPos.transform.localPosition = new Vector3(-3f, 0f, 0f);
            getOutPos.transform.localRotation = Quaternion.identity;
            controller.getOutPosition = getOutPos.transform;
        }
        if(controller.interiorPilot != null) { controller.interiorPilot.SetActive(false); }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void RestoreFunction(Rigidbody craft, PhantomController controller)
    {
        if (craft != null)
        {
            craft.velocity = Vector3.zero;
            craft.angularVelocity = Vector3.zero;
            craft.transform.position = controller.basePosition;
            craft.transform.rotation = controller.baseRotation;

            controller.input.TurnOffEngines();
            if (controller.gearActuator != null && controller.gearActuator.actuatorState == PhantomActuator.ActuatorState.Disengaged) { controller.gearActuator.EngageActuator(); }
        }
    }



    // -------------------------- Check Engines
    public bool CheckEngineState(PhantomEngineCore core)
    {
        bool check;
        if (core.CurrentEngineState == PhantomEngineCore.EngineState.Active) { check = true; }
        else { check = false; }
        return check;
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void PositionAircraftFunction(Rigidbody craft, PhantomController controller)
    {
        Vector3 initialPosition = craft.transform.position;
        craft.transform.position = new Vector3(initialPosition.x, controller.startAltitude, initialPosition.z);
        craft.velocity = craft.transform.forward * controller.startSpeed;
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void RefreshWeaponsFunction(PhantomController controller)
    {
        //SNAPSHOT OF CURRENT POINT
        GameObject oldPod = controller.hardpoints.gameObject; int currentWeapon = controller.hardpoints.selectedWeapon;
        GameObject newPod = UnityEngine.Object.Instantiate(controller.ArmamentsStorage, controller.hardpoints.transform.position, controller.hardpoints.transform.rotation, controller.transform);
        PhantomArmament newArmament = newPod.GetComponent<PhantomArmament>(); newPod.name = "Hardpoints";

        //REMOVE AND REPLACE
        controller.hardpoints = newArmament; newPod.SetActive(true); controller.CleanupGameobject(oldPod);
        if (controller.radar != null) { controller.hardpoints.connectedRadar = controller.radar; }

        //SET VARIABLES
        controller.hardpoints.InitializeWeapons();
        controller.hardpoints.SelectWeapon(currentWeapon);
        Debug.Log("Rearm Complete!!");
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void StartAircraftFunction(Rigidbody craft, PhantomController controller)
    {
        if (craft != null && controller.startMode == PhantomController.StartMode.Hot)
        {
            //POSITION AIRCRAFT
            controller.PositionAircraft();
            //SET ENGINE
            controller.input.TurnOnEngines();
        }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void EnterAircraftFunction(PhantomController controller)
    {
        if (!controller.opened && !controller.temp && !controller.pilotOnboard && controller.controlType == PhantomController.ControlType.Internal)
        {
            controller.opened = true; controller.temp = true;
            //OPEN CANOPY
            if (controller.canopyActuator != null && controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Disengaged)
            {
                controller.canopyActuator.EngageActuator();
            }
            //SET THINGS UP
            controller.pilotOnboard = true;
            controller.StartCoroutine(EntryProcedure(controller));
        }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ExitAircraftFunction(PhantomController controller)
    {
        if (controller.pilotOnboard && controller.controlType == PhantomController.ControlType.Internal)
        {
            //EXIT CHECK LIST
            //1. SHUT ENGINES DOWN
            controller.TurnOffEngines();
            //2. ACTIVATE BRAKE
            if (controller.gearHelper != null)  { controller.gearHelper.EngageBrakes(); }
            //3. TURN OFF LIGHTS
            controller.input.TurnOffLights();
            //OPEN CANOPY
            if (controller.canopyActuator != null && controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Disengaged)
            {
                controller.canopyActuator.EngageActuator();
            }
            //ACTUAL EXIT
            controller.pilotOnboard = false;
            controller.StartCoroutine(ExitProcedure(controller));
        }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator RecieveDelay(PhantomController controller)
    {
        yield return new WaitForSeconds(0.5f);
        //CLOSE DOOR
        if (controller.canopyActuator != null && controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Engaged) { controller.canopyActuator.DisengageActuator(); }
        BeginDisable(controller);
    }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void BeginDisable(PhantomController controller) { controller.StartCoroutine(DisableController(controller)); }
    IEnumerator DisableController(PhantomController controller)
    {
        if (controller.canopyActuator != null) { yield return new WaitUntil(() => controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Disengaged); controller.SetControlState(false); }
        else { yield return new WaitForSeconds(5f); controller.SetControlState(false); }
    }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //ENTER AIRCRAFT
    public IEnumerator EntryProcedure(PhantomController controller)
    {

        if (controller.canopyActuator != null) { yield return new WaitUntil(() => controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Engaged); }
        else { yield return new WaitForSeconds(1.5f); }

        //PLAYER STATE
        if (controller.player != null)
        {
            controller.view.ActivateExteriorCamera();
            controller.player.SetActive(false); if (controller.interiorPilot != null) { controller.interiorPilot.SetActive(true); }
            controller.player.transform.SetParent(controller.transform);
            controller.player.transform.localPosition = Vector3.zero; controller.player.transform.localRotation = Quaternion.identity;

            //ENABLE CONTROLS
            if (controller.playerType == PhantomController.PlayerType.FirstPerson)
            {
                EnableFPControls(controller);
            }
            if (controller.playerType == PhantomController.PlayerType.ThirdPerson)
            {
                controller.ThirdPersonCall();
            }
        }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //FIRST PERSON
    public void EnableFPControls(PhantomController controller)
    {
        //CLOSE CANOPY
        WaitToClose(controller);

        controller.isControllable = true;
        controller.temp = false;

        //DISABLE MAIN CAMERA USED BY PLAYER NOTE: YOU MIGHT HAVE TO SET YOUR OWN CONDITION DEPENDING ON THE PLAYER CONTROLLER
        if (controller.mainCamera != null)
        {
            controller.mainCamera.gameObject.SetActive(false);
            controller.mainCamera.enabled = false;
            controller.mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        }
        //ENABLE CANVAS
        if (controller.canvasDisplay != null)
        {
            controller.canvasDisplay.gameObject.SetActive(true);
            controller.canvasDisplay.helicopterController = controller;
        }
        Input.ResetInputAxes();
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //THIRD PERSON 
    public IEnumerator EnableTPControls(PhantomController controller)
    {
        if (controller.canopyActuator != null) { yield return new WaitUntil(() => controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Engaged); }
        else { yield return new WaitForSeconds(0.05f); }
        WaitToClose(controller);

        controller.isControllable = true;
        controller.temp = false;

        //DISABLE MAIN CAMERA USED BY PLAYER NOTE: YOU MIGHT HAVE TO SET YOUR OWN CONDITION DEPENDING ON THE PLAYER CONTROLLER
        if (controller.mainCamera != null)
        {
            controller.mainCamera.gameObject.SetActive(false);
            controller.mainCamera.enabled = false;
            controller.mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        }
        //ENABLE CANVAS
        if (controller.canvasDisplay != null)
        {
            controller.canvasDisplay.gameObject.SetActive(true);
            controller.canvasDisplay.helicopterController = controller;
        }
        Input.ResetInputAxes();
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void WaitToClose(PhantomController controller) { controller.StartCoroutine(CloseDoor(controller)); }
    public IEnumerator CloseDoor(PhantomController controller)
    {
        yield return new WaitForSeconds(0.5f);
        if (controller.canopyActuator != null && controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Engaged) { controller.canopyActuator.DisengageActuator(); }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //EXIT AIRCRAFT
    public IEnumerator ExitProcedure(PhantomController controller)
    {
        if (controller.canopyActuator != null) { yield return new WaitUntil(() => controller.canopyActuator.actuatorState == PhantomActuator.ActuatorState.Engaged); }
        else { yield return new WaitForSeconds(1.3f); }



        //PLAYER STATE
        if (controller.player != null)
        {
            if (controller.interiorPilot != null) { controller.interiorPilot.SetActive(false); }
            controller.player.transform.SetParent(null);
            controller.player.transform.position = controller.getOutPosition.position;
            controller.player.transform.rotation = controller.getOutPosition.rotation;
            controller.player.transform.rotation = Quaternion.Euler(0f, controller.player.transform.eulerAngles.y, 0f);
            controller.player.SetActive(true);
            ////SET CAMERA FOR FIRST PERson
            if (controller.playerType == PhantomController.PlayerType.FirstPerson)
            {
                controller.view.ResetCameras();
            }
            //DISABLE CANVAS
            if (controller.canvasDisplay != null)
            {
                controller.canvasDisplay.helicopterController = null;
                controller.canvasDisplay.gameObject.SetActive(false);
            }

            //ENABLE MAIN CAMERA USED BY PLAYER>>> NOTE: YOU MIGHT HAVE TO SET YOUR OWN CONDITION DEPENDING ON THE PLAYER CONTROLLER
            if (controller.mainCamera != null)
            {
                controller.mainCamera.gameObject.SetActive(true);
                controller.mainCamera.enabled = true;
                controller.mainCamera.gameObject.GetComponent<AudioListener>().enabled = true;
            }

            //
            //DISABLE 1. CAMERA
            if (controller.view != null)
            {
                controller.view.ResetCameras();
            }

            Input.ResetInputAxes();

            ////DISABLE CONTROLS
            DelayState(controller);
        }
    }

    void DelayState(PhantomController controller)
    {
        controller.StartCoroutine(RecieveDelay(controller));
    }






#if UNITY_EDITOR
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CheckInputCofig(bool truncate, PhantomController controller)
    {
        if (Application.isEditor)
        {
            controller.inputList = new List<string>();
            // ------------------------------------- Check Input Config
            var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            SerializedObject obj = new SerializedObject(inputManager);
            SerializedProperty axisArray = obj.FindProperty("m_Axes");
            if (axisArray.arraySize == 0) Debug.Log("No Axes");

            for (int i = 0; i < axisArray.arraySize; ++i) { controller.inputList.Add(axisArray.GetArrayElementAtIndex(i).displayName); }

            if (controller.inputList.Contains("Start Engine") && controller.inputList.Contains("Stop Engine") && controller.inputList.Contains("Pitch") && controller.inputList.Contains("Roll"))
            {
                controller.input.inputConfigured = true;
            }
            else
            {
                controller.input.inputConfigured = false;
                Debug.LogError("Input needs to be configured. Go to Oyedoyin/Miscellaneous/Setup Input");
                controller.allOk = false;
                if (truncate) { return; }
            }
        }
    }
#endif
}
