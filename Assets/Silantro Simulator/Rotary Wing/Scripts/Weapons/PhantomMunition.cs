using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhantomMunition : MonoBehaviour
{

    // ---------------------------------------- Selectibles
    public enum MunitionType { Missile, Rocket, Bullet }
    public MunitionType munitionType = MunitionType.Rocket;
    public enum RocketType { Guided, Unguided }
    public RocketType rocketType = RocketType.Unguided;
    public enum TriggerMechanism { Proximity, ImpactForce }
    public TriggerMechanism triggerMechanism = TriggerMechanism.ImpactForce;
    public enum MissileType { ASM, AAM, SAM }
    public MissileType missileType = MissileType.ASM;
    public enum DetonationType { Proximity, Impact, Timer }
    public DetonationType detonationType = DetonationType.Impact;
    public enum FireMode { ForwardFiring, PointFiring }
    public FireMode fireMode = FireMode.ForwardFiring;
    public enum AmmunitionType { AP, HEI, FMJ, Tracer }
    public AmmunitionType ammunitionType = AmmunitionType.Tracer;
    public enum FuzeType { MK352, M423, MK193Mod0, }
    public FuzeType fuzeType = FuzeType.M423;
    public enum BulletFuzeType { M1032, ME091 }
    public BulletFuzeType bulletfuseType = BulletFuzeType.M1032;
    public enum DragMode { Free, Clamped }
    public DragMode dragMode = DragMode.Clamped;
    public enum SurfaceFinish { SmoothPaint, PolishedMetal, ProductionSheetMetal, MoldedComposite, PaintedAluminium }
    public SurfaceFinish surfaceFinish = SurfaceFinish.PaintedAluminium;
    public enum AmmunitionForm
    {
        SecantOgive,//0.171
        TangentOgive,//0.165
        RoundNose,//0.235
        FlatNose,//0.316
        Spitzer//0.168
    }
    public AmmunitionForm ammunitionForm = AmmunitionForm.RoundNose;


    // ----------------------------------- Data
    public string Identifier;
    public float mass;
    public float caseLength;
    public float overallLength;
    public float diameter;
    float area;


    public float munitionDiameter = 1f;
    public float munitionLength = 1f;
    public float munitionWeight = 5f;
    public float maximumRange = 1000f;
    public float distanceTraveled;
    public float activeTime;

    public float timer = 10f;
    public float selfDestructTimer;
    public float triggerTimer;
    public float proximity = 100f;//Distance to target
    bool lostTarget;
    public bool armed;
    bool exploded;

    public float detonationDistance = 100f;
    public float speedThreshhold = 10;


    public float CDCoefficient;
    public float surfaceArea;
    public float percentageSkinning = 70f;
    public float dragForce;
    public float fillingWeight = 10f;
    public bool falling;
    public float speed;
    public float distanceToTarget;
    public float fallTime;

    public float ballisticVelocity;
    float currentVelocity;
    public float currentEnergy;
    public float drag;
    public float skinningRatio;
    public float dragCoefficient;
    public float machSpeed;
    public float airDensity;
    private float viscocity;
    public float altitude;
    public float destroyTime;
    float bullettimer;

    public float damage;
    float damageMulitplier;
    float damageFactor;//f-a
    float damageCompiler;//a
    Vector3 dropVelocity;
    Quaternion collisionRotation;
    Vector3 contactPosition;

    public float skinDragCoefficient;
    public float k, totalDrag, RE;
    public float maximumMachSpeed = 1f;
    public float baseDragCoefficient = 0.05f;
    AnimationCurve frictionDragCurve;
    RaycastHit hit;
    bool initialized;



    // ----------------------------------- Connections
    public PhantomController controller;
    public PhantomRocketMotor motorEngine;
    public Collider ammunitionCasing;
    public Rigidbody munition;
    public Rigidbody helicopter;
    public Vector3 ejectionPoint;
    public Transform target;



    // ----------------------------------- Effects
    public GameObject muzzleFlash;
    public GameObject groundHit;
    public GameObject metalHit;
    public GameObject woodHit;//ADD MORE
    public GameObject explosionPrefab;
    private float soundSpeed;
    private float ambientTemperature;
    private float ambientPressure;






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Fires the bullet.
    /// </summary>
    /// <param name="muzzleVelocity"> muzzle/exit velocity of the gun.</param>
    /// <param name="parentVelocity">velocity vector of the parent aircraft.</param>
    public void FireBullet(float muzzleVelocity, Vector3 parentVelocity)
    {
        //DETERMINE INITIAL SPEED
        float startingSpeed;
        if (muzzleVelocity > ballisticVelocity) { startingSpeed = muzzleVelocity; }
        else { startingSpeed = ballisticVelocity; }

        //ADD BASE SPEED
        Vector3 ejectVelocity = transform.forward * startingSpeed;
        Vector3 resultantVelocity = ejectVelocity + parentVelocity;
        //RELEASE BULLET
        munition.velocity = resultantVelocity;
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Releases the bomb or rocket.
    /// </summary>
    public void ReleaseMunition()
    {
        //GET VELOCITY FROM PARENT
        if (helicopter != null)
        {
            dropVelocity = helicopter.velocity;
        }
        //LAUNCH ROCKET
        if (munitionType == MunitionType.Rocket)
        {
            munition.transform.parent = null;
            munition.isKinematic = false;
            munition.velocity = dropVelocity;
            motorEngine.FireRocket();
            StartCoroutine(TimeStep(0.3f));
        }
    }






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
        //GET VELOCITY FROM PARENT
        if (helicopter != null) { dropVelocity = helicopter.velocity; }
        //LAUNCH ROCKET
        if (munitionType == MunitionType.Rocket)
        {
            if (rocketType == RocketType.Guided)
            {
                //FIRE
                motorEngine.FireRocket();
                munition.transform.parent = null;
                munition.isKinematic = false;
                munition.velocity = dropVelocity;
                StartCoroutine(TimeStep(0.3f));
                //SET TARGET DATA
                target = markedTarget;
                targetID = ID;
                target = markedTarget;
                //ACTIVATE SEEKER
                seeking = true;
                active = true;
            }
        }
        //LAUNCH MISSILE
        if (munitionType == MunitionType.Missile)
        {
            //FIRE
            motorEngine.FireRocket();
            munition.transform.parent = null;
            munition.isKinematic = false;
            munition.velocity = dropVelocity;
            StartCoroutine(TimeStep(0.8f));
            //SET TARGET DATA
            target = markedTarget;
            targetID = ID;
            target = markedTarget;
            //DISABLE GRAVITY
            munition.useGravity = false;
            //ACTIVATE SEEKER
            seeking = true;
            active = true;
        }
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //ACTIVATE AFTER DROP
    IEnumerator WaitForDrop(Transform markedTarget, string ID)
    {
        yield return new WaitForSeconds(1f);
        ////FIRE
        motorEngine.FireRocket();
        StartCoroutine(TimeStep(0.8f));

        //SET TARGET DATA
        target = markedTarget;
        targetID = ID;

        target = markedTarget;
        //DISABLE GRAVITY
        munition.useGravity = false;

        //ACTIVATE SEEKER
        seeking = true;
        active = true;
    }


    //CLEAR AIRCRAFT BEFORE ARMING
    IEnumerator TimeStep(float time)
    {
        yield return new WaitForSeconds(time);
        armed = true;
    }






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //SETUP WEAPON
    public void InitializeMunition()
    {
        //GET COMPONENTS
        munition = GetComponent<Rigidbody>();
        Collider munitionCollider = GetComponentInChildren<Collider>();
        Collider[] helicopterColliders = controller.transform.root.GetComponentsInChildren<Collider>();
        foreach (Collider collider in helicopterColliders)
        {
            Physics.IgnoreCollision(munitionCollider, collider, true);
        }

        //SET FINISH FACTOR
        if (surfaceFinish == SurfaceFinish.MoldedComposite) { k = 0.17f; }
        if (surfaceFinish == SurfaceFinish.PaintedAluminium) { k = 3.33f; }
        if (surfaceFinish == SurfaceFinish.PolishedMetal) { k = 0.50f; }
        if (surfaceFinish == SurfaceFinish.ProductionSheetMetal) { k = 1.33f; }
        if (surfaceFinish == SurfaceFinish.SmoothPaint) { k = 2.08f; }

        frictionDragCurve = new AnimationCurve();
        frictionDragCurve.AddKey(new Keyframe(1000000000, 1.5f));
        frictionDragCurve.AddKey(new Keyframe(100000000, 2.0f));
        frictionDragCurve.AddKey(new Keyframe(10000000, 2.85f));
        frictionDragCurve.AddKey(new Keyframe(1000000, 4.1f));
        frictionDragCurve.AddKey(new Keyframe(100000, 7.0f));


        //SETUP MISSILES-ROCKETS-BOMBS
        if (munitionType != MunitionType.Bullet)
        {
            //SET ROCKET MOTOR PROPERTIES
            if (motorEngine != null)
            {
                motorEngine.w = munitionWeight;
                motorEngine.weapon = munition;
                motorEngine.InitializeRocket();
            }
        }

        //SET FACTORS
        if (munition != null)
        {
            if (munitionType != MunitionType.Bullet)
            {
                munition.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                munition.mass = munitionWeight;
                munition.isKinematic = true;
            }
            else { munition.mass = ((mass * 0.0648f) / 1000f); }

        }
        else
        {
            Debug.Log("Rigidbody for munition is missing " + transform.name);
        }
        armed = false;
        surfaceArea = 2f * Mathf.PI * (munitionDiameter / 2f) * munitionLength;



        if (munitionType == MunitionType.Bullet)
        {
            //SET AERODYNAMIC PROPERTIES
            if (ammunitionForm == AmmunitionForm.FlatNose)
            {
                skinningRatio = 0.99f;
                dragCoefficient = 0.316f;
            }
            else if (ammunitionForm == AmmunitionForm.SecantOgive)
            {
                skinningRatio = 0.913f;
                dragCoefficient = 0.171f;
            }
            else if (ammunitionForm == AmmunitionForm.RoundNose)
            {
                skinningRatio = 0.95f;
                dragCoefficient = 0.235f;
            }
            else if (ammunitionForm == AmmunitionForm.TangentOgive)
            {
                skinningRatio = 0.914f;
                dragCoefficient = 0.165f;
            }
            else if (ammunitionForm == AmmunitionForm.Spitzer)
            {
                skinningRatio = 0.921f;
                dragCoefficient = 0.168f;
            }
            //
            ammunitionCasing = GetComponent<Collider>();
            if (munition == null)
            {
                Debug.Log("Bullet " + transform.name + " rigidbody has not been assigned");
            }
            else
            {
                if (ammunitionCasing == null)
                {
                    Debug.Log("Ammunition cannot work without a collider");
                }
                else
                {
                    float radius = diameter / 2000f;
                    area = Mathf.PI * radius * radius;
                }
            }
            //
            float a = Random.Range(78, 98);
            float f = Random.Range(27, 43);
            damageCompiler = a;
            damageFactor = f - a;
        }
        initialized = true;
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public float EstimateRe(float inputSpeed)
    {
        float superRe = 0f;
        float Re1 = (airDensity * inputSpeed * munitionLength) / viscocity; float Re2;
        if (machSpeed < 0.9f) { Re2 = 38.21f * Mathf.Pow(((munitionLength * 3.28f) / (k / 100000)), 1.053f); }
        else { Re2 = 44.62f * Mathf.Pow(((munitionLength * 3.28f) / (k / 100000)), 1.053f) * Mathf.Pow(machSpeed, 1.16f); }
        superRe = Mathf.Min(Re1, Re2); RE = superRe;
        return superRe;
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public float EstimateSkinDragCoefficient(float velocity)
    {
        float Recr = EstimateRe(velocity);
        float baseCf = frictionDragCurve.Evaluate(Recr) / 1000f;

        //WRAPPING CORRECTION
        float Cfa = baseCf * (0.0025f * (munitionLength / munitionDiameter) * Mathf.Pow(Recr, -0.2f));
        //SUPERVELOCITY CORRECTION
        float Cfb = baseCf * Mathf.Pow((munitionDiameter / munitionLength), 1.5f);
        //PRESSURE CORRECTION
        float Cfc = baseCf * 7 * Mathf.Pow((munitionDiameter / munitionLength), 3f);
        float actualCf = 1.03f * (baseCf + Cfa + Cfb + Cfc);
        return actualCf;
    }







    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnCollisionEnter(Collision col)
    {
        if (controller != null && col.gameObject != controller.gameObject && initialized)
        {
            //GET CONTACT POINT
            ContactPoint contact = col.contacts[0];
            collisionRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);


            //FIRE TYPE
            if (fireMode == FireMode.PointFiring) { contactPosition = contact.point; }
            if (fireMode == FireMode.ForwardFiring) { if (Physics.Raycast(transform.position, munition.velocity, out hit)) { contactPosition = hit.point; } }

            //1. ---------------------------------------------------------------------------ROCKET
            if (munitionType == MunitionType.Rocket)
            {
                //TRIGGER WITH IMPACT
                if (armed && fuzeType == FuzeType.MK193Mod0)
                {
                    Explode("Rocket Collision--Fuze MK193", contactPosition);
                }
                //DESTROY IF IMPACT IS STRONG ENOUGH
                if (armed && munition.velocity.magnitude > 5)
                {
                    StartCoroutine(WaitForMomentumShead("Rocket Collision Base", contactPosition));
                }
            }



            //3.---------------------------------------------------------------------------- MISSILE
            if (munitionType == MunitionType.Missile)
            {
                //TRIGGER WITH IMPACT
                if (armed && detonationType == DetonationType.Impact && col.relativeVelocity.magnitude > speedThreshhold)
                {
                    StartCoroutine(WaitForMomentumShead("Missile Impact Fuze", contactPosition));
                }
                //DESTROY IF IMPACT IS STRONG ENOUGH
                if (armed && col.relativeVelocity.magnitude > 5)
                {
                    StartCoroutine(WaitForMomentumShead("Missile Base Fuze", contactPosition));
                }
            }



            //4. -------------------------------------------------------------------------------------------BULLET
            if (munitionType == MunitionType.Bullet)
            {
                if (ammunitionType == AmmunitionType.HEI)
                {
                    Explode("Bullet Collision--HEI", contactPosition);
                }
                if (ammunitionType == AmmunitionType.AP)
                {
                    damageMulitplier = 5f;
                }
                if (ammunitionType == AmmunitionType.FMJ)
                {
                    damageMulitplier = 1.56f;
                }
                if (ammunitionType == AmmunitionType.Tracer)
                {
                    damageMulitplier = 1.65f;
                }


                //------------------------------DISTANCE FALLOFF
                float distance = Vector3.Distance(this.transform.position, ejectionPoint);
                float mix = (((distance - 10f) * damageFactor) / 1990) + damageCompiler;
                float actualDamage = damage * damageMulitplier * (mix / 100f);
                //APPLY
                if (actualDamage > 0)
                {
                    col.collider.gameObject.SendMessage("SilantroDamage", actualDamage, SendMessageOptions.DontRequireReceiver);
                }
                //EFFECT
                if (col.collider.tag == "Ground" && groundHit != null)
                {
                    Instantiate(groundHit, col.contacts[0].point, Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
                }
                if (col.collider.tag == "Wood" && woodHit != null)
                {
                    Instantiate(woodHit, col.contacts[0].point, Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
                }
                if (col.collider.tag == "Metal" && metalHit != null)
                {
                    Instantiate(metalHit, col.contacts[0].point, Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
                }


                Destroy(gameObject);
            }
        }
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //SIMULATE ACTIVATION LATENCY
    IEnumerator WaitForMomentumShead(string actionLabel, Vector3 collisionPoint)
    {
        yield return new WaitForSeconds(0.02f);
        Explode(actionLabel, collisionPoint);
    }





    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    //WARHEAD ACTIVATION
    public void Explode(string actionLabel, Vector3 collisionPosition)
    {
        if (explosionPrefab != null && !exploded)
        {
            GameObject explosion = Instantiate(explosionPrefab, collisionPosition, collisionRotation);
            explosion.SetActive(true);
            explosion.GetComponentInChildren<AudioSource>().Play();
            if(explosion.GetComponent<Rigidbody>() != null) { explosion.GetComponent<Rigidbody>().velocity = munition.velocity; }
            exploded = true;
        }
        Destroy(gameObject);
    }






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void FixedUpdate()
    {

        if (initialized)
        {
            if (munitionType == MunitionType.Missile || munitionType == MunitionType.Rocket)
            {
                //CALCULATE DRAG COEFFICIENT
                if (dragMode == DragMode.Clamped)
                {
                    float trueSpeed = soundSpeed * maximumMachSpeed;
                    float dynamicForce = 0.5f * airDensity * trueSpeed * trueSpeed * surfaceArea;
                    dragCoefficient = motorEngine.Thrust / dynamicForce;
                    if (motorEngine.Thrust < 1 && motorEngine.active) { dragMode = DragMode.Free; baseDragCoefficient = 0.005f; }
                }
                if (dragMode == DragMode.Free)
                {
                    skinDragCoefficient = EstimateSkinDragCoefficient(speed);
                    dragCoefficient = skinDragCoefficient + baseDragCoefficient;
                }

                //Drag
                if (float.IsNaN(dragCoefficient) || float.IsInfinity(dragCoefficient)) { dragCoefficient = 0.01f; }
                if (machSpeed > maximumMachSpeed) { dragMode = DragMode.Clamped; }
                dragForce = 0.5f * airDensity * dragCoefficient * speed * speed * surfaceArea;

                if (dragForce > 0)
                {
                    Vector3 Force = transform.forward * -dragForce;
                    if (!float.IsNaN(drag) && !float.IsInfinity(drag)) { munition.AddForce(Force, ForceMode.Force); }
                }
            }



            if (active && munitionType == MunitionType.Missile)
            {
                //TRACK TARGET
                if (target != null) { distanceToTarget = Vector3.Distance(this.transform.position, target.position); }

                //SEND CALCULATION DATA
                if (munition != null) { CalculateData(); }

                if (seeking && target != null)
                {
                    SilantroNavigation();
                }
            }



            //-----------------------------------------------------------------------------BULLET
            if (drag > 0 && munitionType == MunitionType.Bullet)
            {
                Vector3 Force = transform.forward * -drag;
                if (!float.IsNaN(drag) && !float.IsInfinity(drag)) { munition.AddForce(Force, ForceMode.Force); }
            }




            //------------------------------------------------------------------------------------------------------------------------GENERAL
            if (armed)
            {
                speed = munition.velocity.magnitude;
                activeTime += Time.deltaTime;
                distanceTraveled += speed * Time.deltaTime;
                if (target != null) { distanceToTarget = Vector3.Distance(transform.position, target.position); }

                //SEND DATA
                CalculateData();
            }


            //------------------------------------------------------------------------------------------------------------------------BULLET
            if (munitionType == MunitionType.Bullet)
            {
                bullettimer += Time.deltaTime;
                if (bullettimer > destroyTime && bulletfuseType == BulletFuzeType.M1032)
                {
                    Destroy(gameObject);
                }
            }



            //OTHERS
            if (armed)
            {
                if (explosionPrefab != null && target != null)
                {
                    PhantomExplosion explosion = explosionPrefab.GetComponent<PhantomExplosion>();
                    if (explosion != null)
                    {
                        if (distanceToTarget < (0.6f * explosion.explosionRadius)) { Explode("Missile Proximity Fuze", transform.position); }
                    }
                }



                //1.------------------------------------------------------------------------------------------------------------------ROCKET
                if (munitionType == MunitionType.Rocket)
                {
                    //TIMER FUZE
                    if (fuzeType == FuzeType.MK352)
                    {
                        triggerTimer += Time.deltaTime;
                        if (triggerTimer > timer)
                        {
                            Explode("Rocket Timer--Fuze MK352", transform.position);
                        }
                    }
                    //PROXIMITY FUZE
                    if (fuzeType == FuzeType.M423)
                    {
                        //ACTIVATE IF TARGET IS WITHIN RANGE
                        if (target != null)
                        {
                            if (distanceToTarget < proximity)
                            {
                                Explode("Rocket Proximity--Fuze M423", transform.position);
                            }
                        }
                    }
                    //RESET TIMER
                    if (target)
                    {
                        selfDestructTimer = 0f;
                    }
                    ////DESTROY IF TARGET IS NULL
                    if (rocketType == RocketType.Guided && seeking && target == null)
                    {
                        {   //DESTROY AFTER 5 Seconds IF TARGET IS NULL
                            selfDestructTimer += Time.deltaTime;
                            if (selfDestructTimer > 5)
                            {
                                Explode("Rocket Self Destruct", transform.position);
                            }
                        }
                    }
                }



                //3.------------------------------------------------------------------------------------------------------------------------------- MISSILE
                if (munitionType == MunitionType.Missile)
                {

                    //-----------------------------------------------TIMER FUZE
                    if (detonationType == DetonationType.Timer)
                    {
                        triggerTimer += Time.deltaTime;
                        if (triggerTimer > timer)
                        {
                            Explode("Missile Timer Fuze", transform.position);
                        }
                    }


                    //-----------------------------------------------PROXIMITY FUZE
                    if (detonationType == DetonationType.Proximity)
                    {
                        //ACTIVATE IF TARGET IS WITHIN RANGE
                        if (target != null)
                        {
                            if (distanceToTarget < proximity)
                            {
                                Explode("Missile Proximity Fuze", transform.position);
                            }
                        }
                    }


                    //----------------------------------------------RESET TIMER
                    if (target) { selfDestructTimer = 0f; }


                    ////--------------------------------------------DESTROY IF TARGET IS NULL
                    if (seeking && target == null)
                    {
                        {
                            //DESTROY AFTER 5 Seconds IF TARGET IS NULL
                            selfDestructTimer += Time.deltaTime;
                            if (selfDestructTimer > 5) { Explode("Missile Self Destruct Fuze", transform.position); }
                        }
                    }
                }


                //---------------------------------------------------------------------------------------------------------DESTROY IF OUT OF RANGE

            }
        }
    }





   


    public float currentSpeed;
    public float currentAltitude;
    float altimeter; float a; float b;
    public float viewDirection;
    public float minimumLockDirection = 0.5f;
    public bool seeking;
    public string WeaponID = "Unassigned";
    public string targetID = "Unassigned";
    public bool active;



    public float maxTurnRate = 180.0f;
    public float nc = 3.0f;
    public float turnRamp = 3.0f;
    public float turnRate = 0.0f;
    public float thrust = 0.0f;
    Vector3 los;  // line of sight
    Vector3 acceleration;
   

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void CalculateData()
    {
        //CALCULATE BASE DATA
        currentAltitude = munition.gameObject.transform.position.y * 3.28084f;//Convert altitude from meter to feet
        currentSpeed = munition.velocity.magnitude * 1.944f;//Speed in knots
                                                            //CALCULATE DENSITY
        float kelvinTemperatrue;
        kelvinTemperatrue = ambientTemperature + 273.15f;
        airDensity = (ambientPressure * 1000f) / (287.05f * kelvinTemperatrue);
        //CALCULATE AMBIENT DATA
        float a1; float a2;
        a1 = 0.000000003f * currentAltitude * currentAltitude;
        a2 = 0.0021f * currentAltitude; ambientTemperature = a1 - a2 + 15.443f;
        //CALCULATE PRESSURE
        a = 0.0000004f * currentAltitude * currentAltitude;
        b = (0.0351f * currentAltitude);
        ambientPressure = (a - b + 1009.6f) / 10f;
        //CALCULATE MACH SPEED
        soundSpeed = Mathf.Pow((1.2f * 287f * (273.15f + ambientTemperature)), 0.5f);
        machSpeed = (currentSpeed / 1.944f) / soundSpeed;
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void SilantroNavigation()
    {
        //1. STOP SEEKING IF TARGET IS OUT OF VIEW
        Vector3 targetDirection = (target.transform.position - munition.transform.position).normalized;
        viewDirection = Vector3.Dot(targetDirection, transform.forward);
        //if (viewDirection < minimumLockDirection) { seeking = false; Target = null; Debug.Log("Missile " + munition.transform.name + " " + munition.computer.WeaponID + " lost track of target. Reason: Target out of navigation range"); }

        //2. STOP SEEKING IF TARGET IS BEHIND THE MISSILE
        if (Vector3.Dot(targetDirection, transform.forward) < 0) { seeking = false; target = null; Debug.Log("Missile " + munition.transform.name + " " + WeaponID + " lost track of target. Reason: Target overshoot"); }


        if (target != null)
        {
            thrust = motorEngine.Thrust / munitionWeight;
            if (thrust < 0) { thrust = 0f; }
            if (float.IsNaN(thrust) || float.IsInfinity(thrust)) { thrust = 0; }
            if (turnRate < maxTurnRate)
            {
                float increase = Time.fixedDeltaTime * maxTurnRate / turnRamp;
                turnRate = Mathf.Min(turnRate + increase, maxTurnRate);
            }


            Vector3 prevLos = los;
            los = target.position - transform.position;
            Vector3 dLos = los - prevLos;
            dLos = dLos - Vector3.Project(dLos, los);
            acceleration = Time.fixedDeltaTime * los + dLos * nc + Time.fixedDeltaTime * acceleration * nc / 2;
            acceleration = Vector3.ClampMagnitude(acceleration * thrust, thrust);
            if (float.IsNaN(acceleration.magnitude) || float.IsInfinity(acceleration.magnitude)) { acceleration = Vector3.zero; }
            Quaternion targetRotation = Quaternion.LookRotation(acceleration, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * turnRate);
            //munition.AddForce(transform.forward * acceleration.magnitude, ForceMode.Acceleration);
        }
    }
}