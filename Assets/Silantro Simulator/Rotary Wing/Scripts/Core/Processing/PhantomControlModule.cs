using Oyedoyin;
using Oyedoyin.Rotary.LowFidelity;
using UnityEngine;
using System;



/// <summary>
/// Handles core helicopter data collection and processing
/// </summary>
/// <remarks>
/// This component will collect the data required by the helicopter and process them. It also handles the step calculation of the helicopter
/// center of gravity based on the position and weight of the helicopter components
/// </remarks>

public class PhantomControlModule : MonoBehaviour
{

    // ------------------------------------- Connections
    public PhantomController controller;
    public Rigidbody helicopter;
    public Transform emptyCenterOfMass;
    public Transform currentCOM;
    public Vector3 baseCenter, centerOfMass;
    public Rigidbody samplehelicopter;



    // ------------------------------------- Base Data
    public float currentAltitude;
    public float headingDirection;
    public float driftSpeed;
    public float forwardSpeed;
    public float maxAngularSpeed = 5f;
    public float rotationDrag = 2f;

    // ------------------------------------- Weights
    public float emptyWeight;
    public float munitionLoad;
    public float componentLoad;
    public float fuelLoad;
    public float totalWeight;
    public int munitionCount;
    public Vector3 deviation;


    // ------------------------------------- Environmental Data
    public float ambientTemperature;
    public float ambientPressure;
    public float airDensity = 1.225f;
    public float viscocity = 0.1f;



    // ------------------------------------- Dynamics
    public Vector3 earthLinearVelocity, bodyLinearVelocity;
    public Vector3 earthAngularVelocity, bodyAngularVelocity;
    public float verticalSpeed;
    public float pitchRate;
    public float rollRate;
    public float yawRate;
    public float turnRate;
    public float gForce;
    float intermediateValueBuf;
    public float smoothGSpeed = 0.04f;
    public float smoothRateSpeed = 0.05f;
    public float sideSlip, alpha;
    public float bankAngle, soundSpeed;
    public bool initialized;
    public float machSpeed;


    // ---------------------------- Inertia
    [Tooltip("Resistance to movement on the pitch axis")] public float xInertia = 10000;
    [Tooltip("Resistance to movement in the roll axis")] public float yInertia = 5000;
    [Tooltip("Resistance to movement in the yaw axis")] public float zInertia = 8000;
    public Vector3 baseInertiaTensor;
    public Vector3 inertiaTensor;
    public Quaternion baseTensorRotation;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InitializeCore()
    {
        //----------------------------COG
        GameObject core = new GameObject("_current_cog"); core.transform.parent = transform;
        core.transform.localPosition = Vector3.zero; currentCOM = core.transform;
        if (emptyCenterOfMass == null) { emptyCenterOfMass = this.transform; }
        helicopter.maxAngularVelocity = maxAngularSpeed;
        emptyCenterOfMass.name = "_empty_cog";

        //----------------------------BALANCE
        baseInertiaTensor = helicopter.inertiaTensor;
        baseTensorRotation = helicopter.inertiaTensorRotation;
        inertiaTensor = new Vector3(xInertia, yInertia, zInertia);
        helicopter.inertiaTensor = inertiaTensor;
        helicopter.angularDrag = rotationDrag;
        initialized = true;
    }






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public float helicopterWeight;
    public bool applyMagic;
    public void SilantroMagic(float weight)
    {
        float x = 8.840f * weight; float y = 3.315f * weight; float z = 5.525f * weight;
        xInertia = x; yInertia = y; zInertia = z;
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (initialized)
        {
            //-----------------DATA
            if (helicopter != null) { ProcessData(); }

            //----------------COG
            if (helicopter != null && controller != null) { ProcessCOG(); }
        }
    }





    //---------------------------------------------------------------------COLLECT DATA
    private void ProcessData()
    {
        // ---------------- BASE
        bodyLinearVelocity = MathBase.TransformVelocityToBodyAxis(earthLinearVelocity, helicopter.transform);
        Vector3 localFlow = transform.InverseTransformDirection(helicopter.velocity);
        driftSpeed = localFlow.x;
        Vector3 linearSpeed = new Vector3(localFlow.x, localFlow.z);
        forwardSpeed = linearSpeed.magnitude;
        currentAltitude = helicopter.transform.position.y;
        headingDirection = helicopter.transform.eulerAngles.y;
        earthAngularVelocity = helicopter.angularVelocity;


        // ---------------- ----------------AMBIENT 
        float kiloMeter = MathBase.ConvertDistance(currentAltitude, "FT");
        float a = 0.0000004f * kiloMeter * kiloMeter; float b = (0.0351f * kiloMeter);
        ambientPressure = (a - b + 1009.6f) / 10f;
        float a1 = 0.000000003f * kiloMeter * kiloMeter; float a2 = 0.0021f * kiloMeter;
        ambientTemperature = a1 - a2 + 15.443f;
        float kelvinTemperatrue = ambientTemperature + 273.15f;
        airDensity = (ambientPressure * 1000f) / (287.05f * kelvinTemperatrue);
        viscocity = 1.458f / (1000000) * Mathf.Pow(kelvinTemperatrue, 1.5f) * (1 / (kelvinTemperatrue + 110.4f));


        // ---------------- PERFORMANCE
        Vector3 localVelocity = transform.InverseTransformDirection(helicopter.velocity);
        Vector3 localAngularVelocity = transform.InverseTransformDirection(helicopter.angularVelocity);
        pitchRate = (float)Math.Round((-localAngularVelocity.x * Mathf.Rad2Deg), 2);
        yawRate = (float)Math.Round((localAngularVelocity.y * Mathf.Rad2Deg), 2);
        rollRate = (float)Math.Round((-localAngularVelocity.z * Mathf.Rad2Deg), 2);
        verticalSpeed = (float)Math.Round(helicopter.velocity.y, 2);

        // ---------------- TURN RADIUS
        float turnRadius = (Mathf.Approximately(localAngularVelocity.x, 0.0f)) ? float.MaxValue : localVelocity.z / localAngularVelocity.x;
        float turnForce = (Mathf.Approximately(turnRadius, 0.0f)) ? 0.0f : (localVelocity.z * localVelocity.z) / turnRadius;
        float baseG = turnForce / -9.81f; baseG += transform.up.y * (Physics.gravity.y / -9.81f);
        float targetG = (baseG * smoothGSpeed) + (intermediateValueBuf * (1.0f - smoothGSpeed));
        intermediateValueBuf = targetG; gForce = (float)Math.Round(targetG, 1);

        bankAngle = helicopter.transform.eulerAngles.z; if (bankAngle > 180.0f) { bankAngle = -(360.0f - bankAngle); }
        turnRate = (1091f * Mathf.Tan(bankAngle * Mathf.Deg2Rad)) / MathBase.ConvertSpeed(forwardSpeed + 1, "KTS");
        soundSpeed = Mathf.Pow((1.2f * 287f * (273.15f + ambientTemperature)), 0.5f);
        machSpeed = forwardSpeed / soundSpeed;
    }







    //-------------------------------------------------CENTER OF GRAVITY CALCULATION
    void ProcessCOG()
    {
        //COLLECT WEIGHT DATA
        emptyWeight = controller.emptyWeight; totalWeight = emptyWeight; fuelLoad = componentLoad = 0f; munitionCount = 0; munitionLoad = 0;

        //PROCESS
        centerOfMass = helicopter.transform.TransformDirection(emptyCenterOfMass.position) * emptyWeight;

        //-------------FUEL EFFECT
        if (controller.fuelTanks.Length > 0)
        {
            foreach (PhantomFuelTank tank in controller.fuelTanks)
            {
                if (tank != null)
                {
                    Vector3 tankPosition = tank.transform.position; fuelLoad += tank.CurrentAmount; totalWeight += tank.CurrentAmount;
                    centerOfMass += helicopter.transform.TransformDirection(tankPosition) * tank.CurrentAmount;
                }
            }
        }


        //--------------CREW EFFECT
        if (controller.crew.Length > 0)
        {
            foreach (PhantomCrew component in controller.crew)
            {
                if (component != null)
                {
                    Vector3 loadPosition = component.transform.position; componentLoad += component.weight; totalWeight += component.weight;
                    centerOfMass += helicopter.transform.TransformDirection(loadPosition) * component.weight;
                }
            }
        }



        //--------------LOAD EFFECT
        if (controller.cargo.Length > 0)
        {
            foreach (PhantomCargo component in controller.cargo)
            {
                if (component != null)
                {
                    Vector3 loadPosition = component.transform.position; componentLoad += component.weight; totalWeight += component.weight;
                    centerOfMass += helicopter.transform.TransformDirection(loadPosition) * component.weight;
                }
            }
        }



        //--------------MUNITION EFFECT
        if (controller.hardpoints != null && controller.hardpoints.munitions != null)
        {
            foreach (PhantomMunition munition in controller.hardpoints.munitions)
            {
                if (munition != null)
                {
                    Vector3 loadPosition = munition.transform.position; munitionLoad += munition.munitionWeight; totalWeight += munition.munitionWeight;
                    centerOfMass += helicopter.transform.TransformDirection(loadPosition) * munition.munitionWeight;
                    munitionCount += 1;
                }
            }
            munitionLoad += controller.hardpoints.bulletLoad;
        }



        //-------------- LOW MUNITION EFFECT
        if (controller.hardpoints != null && controller.hardpoints.lowMunition != null)
        {
            foreach (Munition munition in controller.hardpoints.lowMunition)
            {
                if (munition != null)
                {
                    Vector3 loadPosition = munition.transform.position; 
                    munitionLoad += munition.munitionWeight; totalWeight += munition.munitionWeight;
                    centerOfMass += helicopter.transform.TransformDirection(loadPosition) * munition.munitionWeight;
                    munitionCount += 1;
                }
            }
        }


        //---------------APPLY
        if (totalWeight > 0) { centerOfMass /= (totalWeight); } else { centerOfMass /= emptyWeight; }
        if(centerOfMass != Vector3.negativeInfinity && centerOfMass != Vector3.positiveInfinity)
        {
            currentCOM.position = helicopter.transform.InverseTransformDirection(centerOfMass);
            helicopter.centerOfMass = currentCOM.localPosition;
        }
        deviation = currentCOM.localPosition - emptyCenterOfMass.localPosition;


        // ----------------------------- Weight
        controller.currentWeight = totalWeight;
        helicopter.mass = totalWeight;
    }




    //-----------------------------------------------DRAW COG POSITION
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; if (emptyCenterOfMass != null) { Gizmos.DrawSphere(emptyCenterOfMass.position, 0.2f); Gizmos.DrawLine(emptyCenterOfMass.position, (emptyCenterOfMass.transform.up * 3f + emptyCenterOfMass.position)); }
        if (emptyCenterOfMass == null && currentCOM == null) { Gizmos.DrawSphere(this.transform.position, 0.2f); Gizmos.DrawLine(this.transform.position, (this.transform.transform.up * 3f + this.transform.position)); }
        Gizmos.color = Color.red; if (currentCOM != null) { Gizmos.DrawSphere(currentCOM.position, 0.2f); Gizmos.DrawLine(currentCOM.position, (currentCOM.transform.up * 3f + currentCOM.position)); }
        Gizmos.color = Color.green; if (helicopter != null) { Gizmos.DrawLine(helicopter.transform.position, (helicopter.velocity.normalized * 5f + helicopter.position)); }
    }
}
