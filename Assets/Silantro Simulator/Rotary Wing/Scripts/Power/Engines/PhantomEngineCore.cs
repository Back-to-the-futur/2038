using Oyedoyin;
using System;
using UnityEngine;



[Serializable]
public class PhantomEngineCore
{
    //---------------------------------------VARIABLES
    public string engineIdentifier = "Default Engine";
    public float cameraSector;
    public Transform engine;
    public Transform intakePoint;
    public Transform exitPoint;

    public enum EngineNumber { N1, N2, N3 }
    public EngineNumber engineNumber = EngineNumber.N1;
    public enum EngineType { TurboShaft, Piston, Motor }
    public EngineType engineType = EngineType.Piston;
    public enum EnginePosition { Left, Right, Center }
    public EnginePosition enginePosition = EnginePosition.Center;
    public enum SoundMode { Basic, Advanced }
    public SoundMode soundMode = SoundMode.Basic;
    public enum InteriorMode { Off, Active }
    public InteriorMode interiorMode = InteriorMode.Off;

    

    //--------------------------------------SOUND
    public float sideVolume;
    public float frontVolume;
    public float backVolume;
    public float interiorVolume, exteriorVolume;
    public float overideExteriorVolume, overideInteriorVolume;
    public float overidePitch, basePitch;

    public AudioSource frontSource;
    public AudioSource sideSource;
    public AudioSource backSource;
    public AudioSource interiorSource;
    public AudioSource exteriorSource;
    public AudioSource interiorBase;

    public AudioClip frontIdle;
    public AudioClip sideIdle;
    public AudioClip backIdle;
    public AudioClip interiorIdle;
    public AudioClip ignitionInterior, ignitionExterior;
    public AudioClip shutdownInterior, shutdownExterior;



    //--------------------------------------CONNECTIONS
    public PhantomController controller;
    public PhantomTurboShaft turboshaft;
    public PhantomPistonEngine piston;
    public PhantomElectricMotor motor;




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InitializeEngineCore()
    {
        //----------------------------------------SET SOUND SOURCES
        GameObject soundPoint = new GameObject("Sources"); soundPoint.transform.parent = engine; soundPoint.transform.localPosition = Vector3.zero;
        if (soundMode == SoundMode.Advanced)
        {
            if (frontIdle) { Handler.SetupSoundSource(soundPoint.transform, frontIdle, "Front Sound Point", 150f, true, true, out frontSource); }
            if (sideIdle) { Handler.SetupSoundSource(soundPoint.transform, sideIdle, "Side Sound Point", 150f, true, true, out sideSource); }
        }
        if (backIdle) { Handler.SetupSoundSource(soundPoint.transform, backIdle, "Rear Sound Point", 150f, true, true, out backSource); }
        if (interiorIdle && interiorMode == InteriorMode.Active) { Handler.SetupSoundSource(soundPoint.transform, interiorIdle, "Interior Base Point", 80f, true, true, out interiorBase); }


        if (ignitionInterior && interiorMode == InteriorMode.Active) { Handler.SetupSoundSource(soundPoint.transform, ignitionInterior, "Interior Sound Point", 50f, false, false, out interiorSource); }
        if (ignitionExterior) { Handler.SetupSoundSource(soundPoint.transform, ignitionExterior, "Exterior Sound Point", 150f, false, false, out exteriorSource); }
        if (controller.startMode == PhantomController.StartMode.Hot) { trueCoreAcceleration = 10f; }
    }








    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //------------------------------------------------------STATE MANAGEMENT
    public enum EngineState { Off, Starting, Active }
    public EngineState CurrentEngineState;

    //--------------------------------------VARIABLES
    [Tooltip("Engine throttle up and down speed.")] [Range(0.01f, 1f)] public float baseCoreAcceleration = 0.25f;
    public float corePower, trueCoreAcceleration;
    public bool start, shutdown, clutching, active;
    public float coreRPM, factorRPM, norminalRPM, functionalRPM = 1000f;
    public float controlInput;
    public float idlePercentage = 10f, coreFactor, fuelFactor;
    [Tooltip("Percentage of RPM allowed over or under the functional RPM.")] [Range(0, 10)] public float overspeedAllowance = 3f;
    public float pitchTarget;
    public float pitchFactor, totalLoad, engineLoad = 10f;
    public float inputLoad, shaftRPM, powerInput;
    public bool torqueEngaged;
    public float minimumRPM = 900, maximumRPM = 1000;
    public float inertia;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void EvaluateRPMLimits()
    {
        minimumRPM = (1 - (overspeedAllowance / 100)) * functionalRPM;
        maximumRPM = (1 + (overspeedAllowance / 100)) * functionalRPM;
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void StartEngine()
    {
        if (controller != null && controller.isControllable)
        {
            //MAKE SURE SOUND IS SET PROPERLY
            if (backIdle == null || ignitionExterior == null || shutdownExterior == null)
            {
                Debug.Log("Engine " + engine.name + " cannot start due to incorrect Audio configuration");
            }
            else
            {
                //MAKE SURE THERE IS FUEL TO START THE ENGINE
                if (controller && controller.fuelLevel > 1f)
                {
                    //ACTUAL START ENGINE
                    if (controller.startMode == PhantomController.StartMode.Cold)
                    {
                        start = true;
                    }
                    if (controller.startMode == PhantomController.StartMode.Hot)
                    {
                        //JUMP START ENGINE
                        active = true;
                        StateActive(); clutching = false; CurrentEngineState = EngineState.Active;
                    }
                }
                else
                {
                    Debug.Log("Engine " + engine.name + " cannot start due to low fuel");
                }
            }
        }
    }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ShutDownEngine() { shutdown = true; }


    //-------------------------------------------------------ENGINE CORE
    void AnalyseCore()
    {

        // ----------------- Check Control state
        if (controller.startMode != PhantomController.StartMode.Hot)
        {
            if (controller.flightComputer != null && corePower > 0.95f && CurrentEngineState == EngineState.Active && controller.core.currentAltitude > 15f)
            {
                if (controller.flightComputer.autoThrottle == PhantomFlightComputer.ControlState.Active) { trueCoreAcceleration = 10f; }
            }
            else { trueCoreAcceleration = baseCoreAcceleration; }
        }


        if (active && controller.fuelLevel < 5) { ShutDownEngine(); }
        if (active && controller.isControllable == false) { ShutDownEngine(); }


        //------------------ POWER
        if (active) { if (corePower < 1f) { corePower += Time.fixedDeltaTime * trueCoreAcceleration; } }
        else if (corePower > 0f) { corePower -= Time.fixedDeltaTime * trueCoreAcceleration; }
        if (controlInput > 1) { controlInput = 1f; }
        if (corePower > 1) { corePower = 1f; }
        if (!active && corePower < 0) { corePower = 0f; }
        if (active && controller.fuelExhausted) { shutdown = true; }
        fuelFactor = 1f;


        //------------------ FUEL
        if (active && controller.lowFuel)
        {
            float startRange = 0.2f; float endRange = 0.85f; float cycleRange = (endRange - startRange) / 2f;
            float offset = cycleRange + startRange; fuelFactor = offset + Mathf.Sin(Time.time * 3f) * cycleRange;
        }

        //------------------- STATES
        switch (CurrentEngineState) { case EngineState.Off: StateOff(); break; case EngineState.Starting: StateStart(); break; case EngineState.Active: StateActive(); break; }


        //------------------- TORQUE 
        totalLoad = engineLoad + inputLoad + 0.0001f;
        shaftRPM = (30f * powerInput * 1000) / (totalLoad * 3.142f);
        shaftRPM = Mathf.Clamp(shaftRPM, 0, maximumRPM);


        //------------------- RPM
        if (active) { factorRPM = Mathf.Lerp(factorRPM, norminalRPM, trueCoreAcceleration * Time.fixedDeltaTime * 2); }
        else { factorRPM = Mathf.Lerp(factorRPM, 0, trueCoreAcceleration * Time.fixedDeltaTime * 2f); }
        float limitRPM = (functionalRPM * (100 + overspeedAllowance)) / 100f;
        if (factorRPM > limitRPM) { factorRPM = limitRPM; }

        coreRPM = factorRPM * corePower * fuelFactor;
        coreFactor = coreRPM / functionalRPM;

        //-------------------SOUND
        if (controller.cameraState == PhantomCamera.CameraState.Exterior) { overideExteriorVolume = corePower * 0.6f; overideInteriorVolume = 0f; }
        if (controller.cameraState == PhantomCamera.CameraState.Interior) { overideInteriorVolume = corePower; overideExteriorVolume = 0f; }
    }





    //------------------------------------------------ANALYSE SOUND SECTOR
    void AnalyseSound()
    {

        if (controller.cameraState == PhantomCamera.CameraState.Exterior)
        {
            //RESET
            interiorVolume = 0f; exteriorVolume = 1f;

            if (soundMode == SoundMode.Advanced && controller.view != null)
            {
                //------------------------------------------FRONT || RIGHT
                if (cameraSector > 0 && cameraSector < 90) { frontVolume = cameraSector / 90f; sideVolume = 1 - frontVolume; backVolume = 0f; }

                //------------------------------------------FRONT || LEFT
                if (cameraSector >= 90 && cameraSector < 180) { sideVolume = (cameraSector - 90) / 90f; frontVolume = 1 - sideVolume; backVolume = 0f; }

                //------------------------------------------BACK || LEFT
                if (cameraSector >= 180 && cameraSector < 270) { backVolume = (cameraSector - 180) / 90f; sideVolume = 1 - backVolume; frontVolume = 0f; }

                //------------------------------------------BACK || RIGHT
                if (cameraSector >= 270 && cameraSector < 360) { sideVolume = (cameraSector - 270) / 90f; backVolume = 1 - sideVolume; frontVolume = 0f; }
            }
            else { backVolume = 1f; }
        }
        else { backVolume = sideVolume = frontVolume = 0f; interiorVolume = 1f; exteriorVolume = 0f; }

        //-------------------PITCH
        float speedFactor = ((coreRPM + (controller.helicopter.velocity.magnitude * 1.943f) + 10f) - functionalRPM * (idlePercentage / 100f)) / (functionalRPM - functionalRPM * (idlePercentage / 100f));

        pitchTarget = 0.35f + (0.7f * speedFactor);
        if (fuelFactor < 1) { overidePitch = pitchTarget; } else { overidePitch = fuelFactor * Mathf.Lerp(overidePitch, pitchTarget, Time.fixedDeltaTime * 0.5f); }
        pitchTarget *= fuelFactor; backSource.pitch = overidePitch;
        if (interiorMode == InteriorMode.Active && interiorBase != null) { interiorBase.pitch = overidePitch; }
        if (soundMode == SoundMode.Advanced) { frontSource.pitch = pitchTarget; sideSource.pitch = pitchTarget; }


        //-------------------SET VOLUMES
        backSource.volume = overideExteriorVolume * backVolume; if (soundMode == SoundMode.Advanced) { frontSource.volume = overideExteriorVolume * frontVolume; sideSource.volume = overideExteriorVolume * sideVolume; }
        exteriorSource.volume = exteriorVolume; if (interiorMode == InteriorMode.Active && interiorBase != null && interiorSource != null)
        {
            interiorSource.volume = interiorVolume;
            if (controller != null && controller.view != null) { interiorBase.volume = overideInteriorVolume * controller.view.maximumInteriorVolume; } else { interiorBase.volume = overideInteriorVolume; }
        }
    }





    //--------------------------------------------------------ENGINE STATES
    public void StateActive()
    {
        if (exteriorSource.isPlaying) { exteriorSource.Stop(); }
        if (interiorSource != null && interiorSource.isPlaying) { interiorSource.Stop(); }

        //------------------STOP ENGINE
        if (shutdown)
        {
            exteriorSource.clip = shutdownExterior; exteriorSource.Play();
            if (interiorSource != null) { interiorSource.clip = shutdownInterior; interiorSource.Play(); }
            CurrentEngineState = EngineState.Off;
            active = false;

            if (engineType == EngineType.TurboShaft) { turboshaft.ReturnIgnitionCall(); }
            if (engineType == EngineType.Piston) { piston.ReturnIgnitionCall(); }
        }

        //------------------RUN
        if (torqueEngaged) { norminalRPM = (functionalRPM * (idlePercentage / 100f)) + (shaftRPM - (functionalRPM * (idlePercentage / 100f))) * controlInput; }
        else { norminalRPM = (functionalRPM * (idlePercentage / 100f)) + (maximumRPM - (functionalRPM * (idlePercentage / 100f))) * controlInput; }
    }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void StateStart()
    {
        if (clutching) { if (!exteriorSource.isPlaying) { CurrentEngineState = EngineState.Active; clutching = false; StateActive(); } }
        else { exteriorSource.Stop(); if (interiorSource != null) { interiorSource.Stop(); } CurrentEngineState = EngineState.Off; }

        //------------------RUN
        norminalRPM = functionalRPM * (idlePercentage / 100f);
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void StateOff()
    {
        if (exteriorSource.isPlaying && corePower < 0.01f) { exteriorSource.Stop(); }
        if (interiorSource != null && interiorSource.isPlaying && corePower < 0.01f) { interiorSource.Stop(); }


        //------------------START ENGINE
        if (start)
        {
            exteriorSource.clip = ignitionExterior; exteriorSource.Play();
            if (interiorSource != null) { interiorSource.clip = ignitionInterior; interiorSource.Play(); }
            CurrentEngineState = EngineState.Starting; clutching = true;
            active = true;
            if (engineType == EngineType.TurboShaft) { turboshaft.ReturnIgnitionCall(); }
            if (engineType == EngineType.Piston) { piston.ReturnIgnitionCall(); }
        }


        //------------------RUN
        norminalRPM = 0f;
    }








    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //------------------------------------------------------EFFECT MANAGEMENT
    public ParticleSystem exhaustSmoke;
    public ParticleSystem.EmissionModule smokeModule;
    public ParticleSystem exhaustDistortion;
    ParticleSystem.EmissionModule distortionModule;
    public float smokeEmissionLimit = 50f;
    public float distortionEmissionLimit = 20f;



    //-------------------------------------------------------ENGINE EFFECTS
    void AnalyseEffects()
    {
        // Collect Modules
        if (!smokeModule.enabled && exhaustSmoke != null) { smokeModule = exhaustSmoke.emission; }
        if (!distortionModule.enabled && exhaustDistortion != null) { distortionModule = exhaustDistortion.emission; }


        // Control Amount
        if (smokeModule.enabled) { smokeModule.rateOverTime = smokeEmissionLimit * coreFactor; }
        if (distortionModule.enabled) { distortionModule.rateOverTime = distortionEmissionLimit * coreFactor; }
    }










    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void UpdateCore()
    {
        //-----------------//SOUND
        AnalyseSound();

        //-----------------//CORE
        AnalyseCore();

        //----------------//EFFECTS
        AnalyseEffects();
    }
}
