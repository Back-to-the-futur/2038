using UnityEngine;




public class PhantomTransmission : MonoBehaviour
{
    //--------------------------------------- Selectibles
    public enum SystemType { SingleEngine, MultiEngine }
    public SystemType systemType = SystemType.SingleEngine;
    public enum EngineCount { E2, E3 }
    public EngineCount engineCount = EngineCount.E2;
    public enum HelicopterType { Compound, Conventional }
    public HelicopterType helicopterType = HelicopterType.Conventional;
    public enum RotorSystem { Conventional, Tandem, Coaxial }
    public RotorSystem rotorSystem = RotorSystem.Conventional;
    public PhantomController.EngineType engineType = PhantomController.EngineType.Turboshaft;
    public enum PropellerState { Off, Engaged }
    public PropellerState propellerState = PropellerState.Off;

    //--------------------------------------- Variables
    public float totalLoad, rotorLoad, hydraulicsLoad, singleEngineLoad, totalInertia, powerLoad;
    public float availablePower, requiredPower, excessPower;
    public float internalFriction = 0.05f;
    public float norminalRPM = 1000f;
    public float Ω, Ωr, functionalRPM, δR, δΩl, δΩr;
    public bool torqueEngaged;


    public float primaryRotorRatio = 1f, primaryRotorRPM;
    public float secondaryRotorRatio = 1f, secondaryRotorRPM;
    public float appendageRotorRatio = 1, appendageRotorRPM;
    public float primaryRotorLoad, secondaryRotorLoad, appendageLoad;
    public float primaryRotorInertia, secondaryRotorInertia, appendageInertia;

    public bool brakeEngaged;
    public float brakeTorque = 100f;
    public float limitRPM;
    public float autoBrakeRPM = 1500;


    //--------------------------------------- Connections
    public PhantomController helicopter;
    public PhantomRotor primaryRotor;
    public PhantomRotor secondaryRotor;
    public PhantomRotor appendageRotor;
    public PhantomTurboShaft shaftEngineA;
    public PhantomTurboShaft shaftEngineB;
    public PhantomTurboShaft shaftEngineC;
    public PhantomPistonEngine pistonEngine;
    bool allOk;

    public bool rotorOverspeed;
    public bool rotorOvertorque;





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void _checkPrerequisites()
    {
        //CHECK COMPONENTS
        if (helicopter && primaryRotor != null && secondaryRotor != null)
        {
            allOk = true;
        }
        if (helicopter.engineType == PhantomController.EngineType.Turboshaft && shaftEngineA == null)
        {
            Debug.LogError("Prerequisites not met on Helicopter " + transform.name + "....Shaft engine not connected");
            allOk = false; return;
        }
        if (helicopter.engineType == PhantomController.EngineType.Piston && pistonEngine == null)
        {
            Debug.LogError("Prerequisites not met on Helicopter " + transform.name + ".... Piston engine not connected");
            allOk = false; return;
        }
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InitializeTransmission()
    {

        _checkPrerequisites();


        if (allOk)
        {
            // ----------------------------------- Set RPMs
            if (helicopter.engineType == PhantomController.EngineType.Turboshaft) { norminalRPM = shaftEngineA.core.functionalRPM; limitRPM = shaftEngineA.core.maximumRPM; }
            if (helicopter.engineType == PhantomController.EngineType.Piston) { norminalRPM = pistonEngine.core.functionalRPM; limitRPM = pistonEngine.core.maximumRPM; }

            if (systemType == SystemType.MultiEngine && helicopter.engineType == PhantomController.EngineType.Turboshaft)
            {
                if (engineCount == EngineCount.E2) { if (shaftEngineA.core.functionalRPM != shaftEngineB.core.functionalRPM) { Debug.Log("Incompatible Dual Engines, Please check RPM Configuration"); return; } }
                if (engineCount == EngineCount.E2) { norminalRPM = (shaftEngineA.core.functionalRPM + shaftEngineB.core.functionalRPM) / 2f; limitRPM = shaftEngineA.core.maximumRPM; }
                if (engineCount == EngineCount.E3) { if (shaftEngineA.core.functionalRPM != shaftEngineB.core.functionalRPM || (shaftEngineA.core.functionalRPM != shaftEngineC.core.functionalRPM) || (shaftEngineB.core.functionalRPM != shaftEngineC.core.functionalRPM)) { Debug.Log("Incompatible Engines, Please check RPM Configuration"); return; } }
                if (engineCount == EngineCount.E3) { norminalRPM = (shaftEngineA.core.functionalRPM + shaftEngineB.core.functionalRPM + shaftEngineC.core.functionalRPM) / 2f; limitRPM = shaftEngineA.core.maximumRPM; }
            }

        
            // ----------------------------------- RPM Ratios
            primaryRotorRatio = norminalRPM / primaryRotor.funcionalRPM; primaryRotor.coreRatio = primaryRotorRatio;
            secondaryRotorRatio = norminalRPM / secondaryRotor.funcionalRPM; secondaryRotor.coreRatio = secondaryRotorRatio;
            if (appendageRotor != null) { appendageRotorRatio = norminalRPM / appendageRotor.funcionalRPM; appendageRotor.coreRatio = appendageRotorRatio; }


            // ----------------------------------- Base Inertia
            primaryRotorInertia = Mathf.Abs(primaryRotor.Inertia) / (primaryRotorRatio * primaryRotorRatio);
            secondaryRotorInertia = Mathf.Abs(secondaryRotor.Inertia) / (secondaryRotorRatio * secondaryRotorRatio);
            appendageInertia = 0f;
            if (helicopterType == HelicopterType.Compound && appendageRotor != null) { appendageInertia = Mathf.Abs(appendageRotor.Inertia) / (appendageRotorRatio * appendageRotorRatio); }
            totalInertia = primaryRotorInertia + secondaryRotorInertia + appendageInertia;
            if (float.IsNaN(totalInertia) || float.IsInfinity(totalInertia)) { totalInertia = 0.0f; }

        }
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if (allOk)
        {
            // --------------------------------------------------- Engine Input
            if (systemType == SystemType.SingleEngine)
            {
                if (helicopter.engineType == PhantomController.EngineType.Turboshaft) { functionalRPM = shaftEngineA.core.coreRPM; availablePower = shaftEngineA.Pt; }
                if (helicopter.engineType == PhantomController.EngineType.Piston) { functionalRPM = pistonEngine.core.coreRPM; availablePower = pistonEngine.Pb; }

                if (totalLoad > 1)
                {
                    if (helicopter.engineType == PhantomController.EngineType.Turboshaft) { shaftEngineA.core.torqueEngaged = torqueEngaged; shaftEngineA.core.inputLoad = totalLoad; }
                    if (helicopter.engineType == PhantomController.EngineType.Piston) { pistonEngine.core.torqueEngaged = torqueEngaged; pistonEngine.core.inputLoad = totalLoad; }
                }
            }

            if (systemType == SystemType.MultiEngine && helicopter.engineType == PhantomController.EngineType.Turboshaft)
            {
                if (engineCount == EngineCount.E2)
                {
                    if (shaftEngineA.core.active && shaftEngineB.core.active) { functionalRPM = ((shaftEngineA.core.coreRPM * 2f) + (shaftEngineB.core.coreRPM * 2f)) / 4f; }
                    else { functionalRPM = ((shaftEngineA.core.coreRPM * 2f) + (shaftEngineB.core.coreRPM * 2f)) / 2f; }
                    availablePower = shaftEngineB.Pt + shaftEngineA.Pt;


                    if (totalLoad > 1)
                    {
                        if (shaftEngineA.core.active && shaftEngineB.core.active) { singleEngineLoad = totalLoad / 2f; } else { singleEngineLoad = totalLoad; }
                        if (shaftEngineA.core.active) { shaftEngineA.core.torqueEngaged = torqueEngaged; shaftEngineA.core.inputLoad = singleEngineLoad; }
                        if (shaftEngineB.core.active) { shaftEngineB.core.torqueEngaged = torqueEngaged; shaftEngineB.core.inputLoad = singleEngineLoad; }
                    }
                }
            }

            if (float.IsNaN(availablePower) || float.IsInfinity(availablePower)) { availablePower = 0.0f; }
            if (totalLoad < 5) { powerLoad = (30f * availablePower * 1000) / (functionalRPM * Mathf.PI); }
            else { powerLoad = totalLoad; }
            if (float.IsNaN(totalLoad) || float.IsInfinity(totalLoad)) { totalLoad = 0.0f; }
            if (float.IsNaN(powerLoad) || float.IsInfinity(powerLoad)) { powerLoad = 0.0f; }
            EstimateTransmissionLoad();

            δR = functionalRPM / norminalRPM;
            Ωr = functionalRPM / 9.5492966f;
            δΩl = totalLoad / totalInertia * Time.fixedDeltaTime;
            δΩr = powerLoad / totalInertia * Time.fixedDeltaTime;
            if (float.IsNaN(δΩl) || float.IsInfinity(δΩl)) { Ω = 0.0f; }
            if (autoBrakeRPM > 1e-5 && Ω < autoBrakeRPM / 9.5492966 && Ωr < Ω) { brakeEngaged = true; } else { brakeEngaged = false; }
            if (float.IsNaN(Ωr) || float.IsInfinity(Ωr)) { Ωr = 0.0f; }
            if (availablePower > 10f && δR > 0.5f) { torqueEngaged = true; } else { torqueEngaged = false; }


            if (totalLoad < 0) { totalLoad = 0f; }
            if (hydraulicsLoad < 0) { hydraulicsLoad = 0f; }
            if (functionalRPM < 0) { functionalRPM = 0f; }
            if (Ωr < 0) { Ωr = 0f; }
            if (Ω < 0) { Ω = 0f; }



            if (torqueEngaged)
            {
                if (helicopter.startMode == PhantomController.StartMode.Hot || helicopter.quickStart == PhantomFlightComputer.ControlState.Active) { Ω = Ωr; }
                else { Ω += δΩr; }
                if (Ω > Ωr) { Ω = Ωr; }
            }
            else
            {
                Ω -= δΩl;
                if (Ω < 0) { Ω = 0f; }
                if (Ωr < 0.99)
                {
                    torqueEngaged = false;
                    totalLoad = 0;
                    totalInertia = 0;
                }
            }
            if (float.IsNaN(Ω) || float.IsInfinity(Ω)) { Ω = 0.0f; }



            // --------------------------------------------------- Send
            primaryRotorRPM = (Ω * 9.5492966f) / primaryRotorRatio; if (primaryRotor != null) { primaryRotor.coreRPM = primaryRotorRPM; }
            secondaryRotorRPM = (Ω * 9.5492966f) / secondaryRotorRatio; if (secondaryRotor != null) { secondaryRotor.coreRPM = secondaryRotorRPM; }
            appendageRotorRPM = (Ω * 9.5492966f) / appendageRotorRatio; if (helicopterType == HelicopterType.Compound && appendageRotor != null) { appendageRotor.coreRPM = appendageRotorRPM; }
            requiredPower = (totalLoad * Ω) / 1000f;
            excessPower = availablePower - requiredPower;
            if (excessPower < 0 && torqueEngaged) { rotorOvertorque = true; } else { rotorOvertorque = false; }
            if(primaryRotor.υM > 0.92f || secondaryRotor.υM > 0.92f) { rotorOverspeed = true; } else { rotorOverspeed = false; }
        }
    }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void EstimateTransmissionLoad()
    {
        // --------------------------------------------------- Load
        hydraulicsLoad = internalFriction * Ω;
        primaryRotorLoad = Mathf.Abs(primaryRotor.Torque) / primaryRotorRatio;
        secondaryRotorLoad = Mathf.Abs(secondaryRotor.Torque) / secondaryRotorRatio;
        appendageLoad = 0f;
        if (helicopterType == HelicopterType.Compound && appendageRotor != null) { appendageLoad = Mathf.Abs(appendageRotor.Torque) / appendageRotorRatio; }
        rotorLoad = primaryRotorLoad + secondaryRotorLoad;
        float brakeLoad = (brakeEngaged && Ω > 0 ? brakeTorque : 0);
        totalLoad = rotorLoad + hydraulicsLoad + appendageLoad + brakeLoad;
        if (float.IsNaN(totalLoad) || float.IsInfinity(totalLoad)) { totalLoad = 0.0f; }


        // --------------------------------------------------- Inertia
        primaryRotorInertia = Mathf.Abs(primaryRotor.Inertia) / (primaryRotorRatio * primaryRotorRatio);
        secondaryRotorInertia = Mathf.Abs(secondaryRotor.Inertia) / (secondaryRotorRatio * secondaryRotorRatio);
        appendageInertia = 0f;
        if (helicopterType == HelicopterType.Compound && appendageRotor != null) { appendageInertia = Mathf.Abs(appendageRotor.Inertia) / (appendageRotorRatio * appendageRotorRatio); }
        totalInertia = primaryRotorInertia + secondaryRotorInertia + appendageInertia;
        if (float.IsNaN(totalInertia) || float.IsInfinity(totalInertia)) { totalInertia = 0.0f; }
    }
}
