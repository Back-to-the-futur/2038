using UnityEngine;
using Oyedoyin;
using Oyedoyin.Rotary;
using MathNet.Numerics;
#if UNITY_EDITOR
using UnityEditor;
#endif




public class PhantomRotor : MonoBehaviour
{

    // ------------------------------ Selectibles
    public enum RotorType { MainRotor, TailRotor }
    public RotorType rotorType = RotorType.MainRotor;
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;
    public enum RotationDirection { CW, CCW }
    public RotationDirection rotorDirection = RotationDirection.CW;
    public enum TwistType { Constant, Upward, Downward }
    public TwistType twistType = TwistType.Constant;
    public enum BladeFlappingState { Dynamic, Static, Inactive }
    public BladeFlappingState flappingState = BladeFlappingState.Static;
    public enum GroundEffectState { Neglect, Consider }
    public GroundEffectState effectState = GroundEffectState.Neglect;
    public enum RotorConfiguration { Tandem, Coaxial, Conventional, Syncrocopter, Propeller }
    public RotorConfiguration rotorConfiguration = RotorConfiguration.Conventional;
    public enum TandemAnalysisMethod { MT1, MT2, Harris }
    public TandemAnalysisMethod tandemAnalysis = TandemAnalysisMethod.MT1;
    public enum CoaxialPosition { Top, Bottom }
    public CoaxialPosition rotorPosition = CoaxialPosition.Top;
    public enum TandemPosition { Forward, Rear }
    public TandemPosition tandemPosition = TandemPosition.Forward;
    public enum SyncroPosition { Left, Right }
    public SyncroPosition syncroPosition = SyncroPosition.Left;
    public enum TailRotorType { Fanestron, Conventional }
    public TailRotorType tailRotorType = TailRotorType.Conventional;
    public enum SoundState { Active, Silent }
    public SoundState soundState = SoundState.Active;
    public enum VisulType { Default, Partial, Complete }
    public VisulType visualType = VisulType.Default;
    public enum WeightUnit { Kilogram, Pounds }
    public WeightUnit weightUnit = WeightUnit.Kilogram;
    public enum PropellerMode { Vertical, Horizontal}
    public PropellerMode propellerMode = PropellerMode.Vertical;
    public enum SurfaceFinish { SmoothPaint, PolishedMetal, ProductionSheetMetal, MoldedComposite, PaintedAluminium }
    public SurfaceFinish surfaceFinish = SurfaceFinish.PaintedAluminium;




    // ------------------------------ Blade Parameters
    public SilantroAirfoil rootAirfoil;
    public SilantroAirfoil tipAirfoil;
    public float bladeMass = 20f;
    public float bladeRadius;
    public float hingeOffset;
    [Range(0, 1f)] public float rotorHeadRadius = 0.1f;
    [Range(-0.3f, 0.3f)] public float rootDeviation = 0.0f;
    [Range(0, 0.4f)] public float rootCutOut = 0.01f;
    public float rootcut = 0.01f;
    [Range(0, 0.2f)] public float re = 0.01f;
    [Range(0, 1.5f)] public float bladeChord = 0.1f;
    public float γ, ke;
    [Range(0, 20)] public float bladeWashout = 0f;
    [HideInInspector] public int bladeSubdivisions = 4;
    [HideInInspector] public float rootDeflection;
    public float spanEfficiency;
    public float aspectRatio;
    public float direction;
    public float weightFactor, actualWeight;


    // ------------------------------ Blade Motion
    public float β0;
    public float β1s;
    public float β1c;
    public float Ωmax;


    // ------------------------------ Inflow
    Complex32[] xtx, kpx, kix, CLx, stx;
    Complex32 tsum, dsum, δxt, δxd, CTt, CTi, cqi, CQnot, λ, CL, CD;
    public float CLαr, B;
    public cfloat CTtip, λr, δCT, swirl;
    public float CLα, αMax, υTip, υM;
    public cfloat[] υi, υt, λt, α, CLf, CDf;
    public cfloat αR, αT;
    public bool debug, assigned;

    // ------------------------------ Rotor Parameters
    public float rotorRadius = 1f;
    [Range(2, 8)] public int Nb = 2;
    public float funcionalRPM = 1500f;
    public float coreRPM;
    public float Ω, coreRatio;
    public float solidity;
    public float ϴ0, ϴy, ϴr, ϴf, ϴs;
    public float ϴ1c;
    public float ϴ1s;
    public float ϴtw;
    public float J;
    public float Iβ = 300f;
    public float rotorArea;
    public float h = 1000f;
    public float Mβ1c, Rβ1s, µx, µz;
    public float k;
    public int subdivision = 6;
    public Vector3 υl, wl;
    public bool drawFoils;
    public Vector3 groundAxis = new Vector3(0.0f, -1.0f, 0.0f);
    public LayerMask groundLayer;
    public int rotorSubdivision = 8;
    public AnimationCurve swirlCorrection;
    public AnimationCurve powerCorrection;
    public AnimationCurve thrustCorrection;
    public AnimationCurve kov;
    public AnimationCurve groundCorrection;
    public float δf, δT = 1, δv = 1, δQ = 1;
    protected float Mw;
    public float lx, ly, υz, υy;


    public float soundFactor;
    public AudioSource soundPoint;
    public AudioClip bladeChop;
    public float maximumPitch = 1.0f;
    public float interiorVolume = 0.2f;
    public float rotorVolume, rotorPitch;
    public float fA;
    public System.Collections.Generic.List<Vector3> fL;

    // ------------------------------ Limits
    public float minimumCollective = 1f;
    public float maximumCollective = 10f;
    public float minimumPitchCyclic = 10;
    public float maximumPitchCyclic = 10f;
    public float minimumRollCyclic = 10f;
    public float maximumRollCyclic = 10f;
    public float maximumYawCollective = 5f;
    public float maximumYawCyclic = 5f;
    public float maximumPitchCollective = 5f;
    private float ϴmax, ϴmin, ϴymax, ϴfmax, ϴsmax;
    private float ϴ1sMax, ϴ1cMax;
    private float ϴ1sMin, ϴ1cMin;


    // ------------------------------ Connections
    public PhantomRotor rotor;
    public PhantomController controller;
    public Material[] normalRotor;
    public Material[] blurredRotor;
    public Color blurredRotorColor;
    public Color normalRotorColor;
    public float alphaSettings;
    public float normalBalance = 0.2f;
    public Transform forcePoint; Quaternion baseRotation;
    public Vector3 forceDirection;

    // ------------------------------ Input;
    public float pitchInput;
    public float rollInput;
    public float collectiveInput;
    public float yawInput;
    public float propellerInput;


    // ------------------------------ Vectors
    [HideInInspector] public Vector3 rootLeadingEdge, tipLeadingEdge, rootTrailingEdge, tipTrailingEdge;
    [HideInInspector] public Vector3 quaterRootChordPoint, quaterTipChordPoint, skewDistance;



    // ------------------------------ Ouput
    public float coneAngle;
    public float featherAngle, δϕ;
    public cfloat CQ0, CQI, CQIB, CT, CQ;
    public float CTσ, CH, CY;
    public float FH, FHx, FY, FYy, Thrust;
    public float Torque;
    public float Inertia;
    public Vector3 ThrustForce;
    public Vector3 TorqueForce;
    public bool allOk;






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void _checkPrerequisites()
    {
        if (controller != null && rootAirfoil != null && tipAirfoil != null) { allOk = true; }
        else if (rootAirfoil == null || tipAirfoil == null) { Debug.LogError("Prerequisites not met on Rotor" + transform.name + "....airfoil has not been assigned"); allOk = false; return; }
    }






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InitializeRotor()
    {
        _checkPrerequisites();

        // --------------------- Initialize
        if (allOk) { AnalyseRotor(); }
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void UpdateRotor()
    {
        if (allOk)
        {
            // --------------------- Control
            AnalyseControls();

            // --------------------- Forces
            AnalyseForces();

            // --------------------- Visuals
            AnalyseRotorVisuals();

            // --------------------- Sound
            AnalyseSound();
        }
    }






    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void AnalyseRotorVisuals()
    {
        if (coreRPM > 0)
        {
            if (rotorDirection == RotationDirection.CW)
            {
                if (rotationAxis == RotationAxis.X) { rotor.transform.Rotate(new Vector3(coreRPM * 5f * Time.fixedDeltaTime, 0, 0)); }
                if (rotationAxis == RotationAxis.Y) { rotor.transform.Rotate(new Vector3(0, coreRPM * 5f * Time.fixedDeltaTime, 0)); }
                if (rotationAxis == RotationAxis.Z) { rotor.transform.Rotate(new Vector3(0, 0, coreRPM * 5f * Time.fixedDeltaTime)); }
            }
            if (rotorDirection == RotationDirection.CCW)
            {
                if (rotationAxis == RotationAxis.X) { rotor.transform.Rotate(new Vector3(-1f * coreRPM * 5f * Time.fixedDeltaTime, 0, 0)); }
                if (rotationAxis == RotationAxis.Y) { rotor.transform.Rotate(new Vector3(0, -1f * coreRPM * 5f * Time.fixedDeltaTime, 0)); }
                if (rotationAxis == RotationAxis.Z) { rotor.transform.Rotate(new Vector3(0, 0, -1f * coreRPM * 5f * Time.fixedDeltaTime)); }
            }
        }

        alphaSettings = coreRPM / funcionalRPM;
        if (visualType == VisulType.Complete)
        {
            if (blurredRotor != null && normalRotor != null)
            {
                foreach (Material brotor in blurredRotor) { if (brotor != null) { brotor.color = new Color(blurredRotorColor.r, blurredRotorColor.g, blurredRotorColor.b, alphaSettings); } }
                foreach (Material nrotor in normalRotor) { if (nrotor != null) { nrotor.color = new Color(normalRotorColor.r, normalRotorColor.g, normalRotorColor.b, (1 - alphaSettings) + normalBalance); } }
            }
        }
        if (visualType == VisulType.Partial)
        {
            if (blurredRotor != null)
            {
                foreach (Material brotor in blurredRotor) { if (brotor != null) { brotor.color = new Color(blurredRotorColor.r, blurredRotorColor.g, blurredRotorColor.b, alphaSettings); } }
            }
        }
    }



    public float m_collective_bump = 0.3f;
    public float m_factor;
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void AnalyseControls()
    {
        if (rotorConfiguration != RotorConfiguration.Propeller)
        {
            // --------------------------------------------------- Differential Collective
            if (rotorConfiguration == RotorConfiguration.Coaxial || rotorConfiguration == RotorConfiguration.Syncrocopter) { ϴy = ϴymax * yawInput * direction; } else { ϴy = 0f; }
            if (rotorConfiguration == RotorConfiguration.Tandem && tandemPosition == TandemPosition.Forward) { ϴf = ϴfmax * -pitchInput; ϴs = ϴsmax * yawInput; }
            else if (rotorConfiguration == RotorConfiguration.Tandem && tandemPosition == TandemPosition.Rear) { ϴf = ϴfmax * pitchInput; ϴs = ϴsmax * -yawInput; }
            else { ϴf = 0f; ϴs = 0f; }
            if (rotorConfiguration == RotorConfiguration.Tandem) { m_factor = (collectiveInput - m_collective_bump) / (1- m_collective_bump); if (m_factor < 0) { m_factor = 0f; } 
                forcePoint.transform.localRotation = baseRotation * Quaternion.AngleAxis(-ϴs * Mathf.Rad2Deg, new Vector3(0, 0, 1)); }


            // --------------------------------------------------- Normal Collective
            if (rotorType == RotorType.MainRotor)
            {
                if (ϴf < 0) { ϴf *= m_factor; }
                ϴ0 = (ϴmin + (collectiveInput * (ϴmax - ϴmin))) + ϴy + ϴf;
                ϴ1s = pitchInput > 0f ? pitchInput * ϴ1sMin : pitchInput * ϴ1sMax;
                ϴ1c = rollInput > 0f ? rollInput * ϴ1cMin : rollInput * ϴ1cMax;
            }
            else
            {
                ϴ0 = collectiveInput > 0f ? collectiveInput * ϴmin : collectiveInput * ϴmax;
                ϴ1s = ϴ1c = 0;
            }
        }
        else
        {
            if(propellerMode == PropellerMode.Vertical) { ϴ0 = ϴmax; ϴ1s = ϴ1c = 0; }
            else
            {
                ϴ0 = propellerInput > 0f ? propellerInput * ϴmin : propellerInput * ϴmax;
                ϴ1s = ϴ1c = 0;
            }
        }

        // -------------------------------------------------- Ground Effect
        if (effectState == GroundEffectState.Consider)
        {
            Ray groundCheck = new Ray(transform.position, groundAxis);
            RaycastHit groundHit;

            if (Physics.Raycast(groundCheck, out groundHit, 1000, groundLayer))
            { h = groundHit.distance; Debug.DrawLine(transform.position, groundHit.point, Color.red); }
            if (h > 999f) { h = 999f; }
            float zR = h / bladeRadius; if (zR > 3f) { zR = 3f; }
            δT = groundCorrection.Evaluate(zR);
            controller.AGL = h;
        }
        else { δT = 1; }
    }







    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void AnalyseSound()
    {
        if (soundState == SoundState.Active && soundPoint != null)
        {
            soundFactor = coreRPM / funcionalRPM;
            rotorPitch = maximumPitch * soundFactor;
            if (controller.cameraState == PhantomCamera.CameraState.Exterior) { rotorVolume = soundFactor; } else { rotorVolume = interiorVolume * soundFactor; }

            if (soundFactor < 0.01f) { soundPoint.Stop(); }
            else
            {
                if (!soundPoint.isPlaying) { soundPoint.Play(); }
                soundPoint.pitch = rotorPitch;
                soundPoint.volume = rotorVolume;
            }
        }
    }





    public float cdw, cf, Cf;
    public float m_thickness, m_ref_area, m_wetted_area;
    public float m_surface_k;

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void AnalyseForces()
    {
        // ---------------- Base Data
        if (coreRPM > funcionalRPM * 1.02f) { coreRPM = funcionalRPM * 1.02f; }
        if (coreRPM < 0) { coreRPM = 0f; }
        Ω = (2 * 3.142f * (1 + coreRPM)) / 60f;
        if (Ω > Ωmax * 1.02f) { Ω = Ωmax * 1.02f; }
        if (Ω < 0) { Ω = 0; }
        if (float.IsNaN(Ω) || float.IsInfinity(Ω)) { Ω = 0.2f; }
        γ = CLα * controller.core.airDensity * bladeChord * Mathf.Pow(bladeRadius, 4) / Iβ;
        Mβ1c = -Iβ * MathR.Square(Ω) * 3 * re / (2 * (1 - re)) * 2;
        Rβ1s = -Iβ * MathR.Square(Ω) * 3 * re / (2 * (1 - re)) * 2;
        β0 = β1c = β1s;
        k = ϴtw * Mathf.Deg2Rad;
      


        // ---------------- Collect Velocity
        if (rotorConfiguration == RotorConfiguration.Propeller && propellerMode == PropellerMode.Vertical) { controller.GetLocalVelocity(controller.transform, out υl, out wl); }
        else { controller.GetLocalVelocity(transform, out υl, out wl); }
        υz = Mathf.Sqrt(υl.x * υl.x + υl.z * υl.z);
        υy = υl.y;
        δϕ = Mathf.Atan2(υl.x, υl.z); ;
        υTip = (bladeRadius * Ω) + υz;
        υM = υTip / controller.core.soundSpeed;

        // ------------------------------------------- Inflow
        for (int i = 0; i < subdivision + 1; i++)
        {
            float ri = (bladeRadius * rootCutOut) + ((bladeRadius * (1 - rootCutOut)) / subdivision) * (i);
            float xi = ri / bladeRadius; xtx[i] = xi;
            float Ui = xi * Ω * bladeRadius / 340f;
            float CLαd = 0f;
            if (Ui < 0.75f) { CLαd = (0.1f / Mathf.Sqrt(1 - (Ui * Ui))) - 0.01f * Ui; }
            else if (Ui > 0.75f) { CLαd = 0.677f * 0.744f * Ui; }
            ϴr = (ϴ0 + xi * k);
            CLαr = CLαd * Mathf.Rad2Deg;


            µx = υz / (Ω * ri);
            µz = υy / (Ω * ri);
            if (float.IsNaN(µx) || float.IsInfinity(µx)) { µx = 0f; }
            if (float.IsNaN(µz) || float.IsInfinity(µz)) { µz = 0f; }
            Complex32 υ = (Ω * CLαr * Nb * bladeChord) / (16 * Mathf.PI) * (-1 + Complex32.Sqrt(1 + (32 * Mathf.PI * ϴr * ri) / (CLαr * Nb * bladeChord)));
            if (υ.IsNaN() || υ.IsInfinity()) { υ = 0f; }
            Complex32 λh = (υ + υy) / (Ω * ri);
            if (λh.IsNaN() || λh.IsInfinity()) { λh = 0f; }
            if (rotorConfiguration == RotorConfiguration.Propeller && propellerMode == PropellerMode.Vertical) { λ = λh; }
            else { λ = MathR.ForwardInflow(µx, λh, µz, 1); }
            λr.R = λ.Real; λr.i = λ.Imaginary;
            if (λ.IsNaN() || λ.IsInfinity()) { λ = 0f; }
            Complex32 ϕr = Complex32.Atan(λ);
            if (ϕr.IsNaN() || ϕr.IsInfinity()) { ϕr = 0f; }
            Complex32 αr = ϴr - ϕr;
            

            // ------------------------------------- Extract Foil Coefficients
            Complex32 αs = αr * Mathf.Rad2Deg;
            if (αs.Real > 89) { αs = 89f; }
            if (αs.Real < -89) { αs = -89f; }
            if (i == 0) { αMax = αs.Real; }
            if(αs.Real > 0) { if (αs.Real > αMax) { αMax = αs.Real; } }
            else { if (αs.Real < αMax) { αMax = αs.Real; } }
            if (αs.IsNaN() || αs.IsInfinity()) { αs = 0f; }
            if (i == 0) { αR.R = αs.Real; αR.i = αs.Imaginary; }
            if (i == subdivision) { αT.R = αs.Real; αT.i = αs.Imaginary; }
            float rCL = rootAirfoil.liftCurve.Evaluate(αs.Real);
            float tCL = tipAirfoil.liftCurve.Evaluate(αs.Real);
            float rCD = rootAirfoil.dragCurve.Evaluate(αs.Real);
            float tCD = tipAirfoil.dragCurve.Evaluate(αs.Real);
            float CLfoil = MathR.EstimateEffectiveValue(rCL, tCL, xi);
            float CDfoil = MathR.EstimateEffectiveValue(rCD, tCD, xi);
            //Complex32 CLNumeric = αr * CLαr;
            //Complex32 CDNumeric = MathR.CDf((17 - 23.4f * Ui), Ui, αs);


            //-----------------------------------------Wave Drag
            float rootMcr = rootAirfoil.Mcr; float tipMcr = tipAirfoil.Mcr;
            float panelMcr = MathR.EstimateEffectiveValue(rootMcr, tipMcr, xi);
            if (panelMcr < 0.5f) { panelMcr = ((rootMcr * 2) + (tipMcr * 2)) / 2f; }
            float Mcrit = panelMcr - (0.1f * CLfoil);
            float M_Mcr = 1 - Mcrit;
            float cdw0 = 0.0264f;

            float δm = 1 - Mcrit;
            float xf = (-8f * (υM - (1 - (0.5f * δm)))) / δm;
            float fmx = Mathf.Exp(xf);
            float fm = 1 / (1 + fmx);
            float kdw = 0.5f;
            float kdwm = 0.05f;
            float dx = Mathf.Pow((Mathf.Pow((υM - kdwm), 2f) - 1), 2) + Mathf.Pow(kdw, 4);
            float correction = Mathf.Pow(Mathf.Cos(0 * Mathf.Deg2Rad), 2.5f); //Sweep correction...useful later
            cdw = (fm * cdw0 * kdw) / (Mathf.Pow(dx, 0.25f)) * correction;
            if (cdw < 0) { cdw = 0f; }


            //----------------------------------------- Skin Friction Drag
            m_ref_area = bladeRadius * bladeChord;
            m_thickness = MathR.EstimateEffectiveValue(rootAirfoil.maximumThickness, tipAirfoil.maximumThickness, xi);
            m_wetted_area = m_ref_area * (1.977f + (0.52f * m_thickness));
            cf = MathBase.EstimateSkinDragCoefficient(bladeChord, bladeChord, (MathBase.ConvertSpeed(υl.magnitude, "KTS")), controller.core.airDensity, controller.core.viscocity, υM, m_surface_k);
            Cf = (m_wetted_area / m_ref_area) * cf;
            if(Cf > CDfoil) { Cf = CDfoil; }


            //----------------------------------------- Sum
            CL = CLfoil; CLx[i] = CL;
            CD = CDfoil + cdw + Cf;



            // ------------------------------------- Debug 
            if (debug)
            {
                α[i].R = αs.Real; α[i].i = αs.Imaginary;
                CLf[i].R = CL.Real;
                CDf[i].R = CD.Real;
                λt[i].R = λ.Real; λt[i].i = λ.Imaginary;
                υi[i].R = υ.Real; υi[i].i = υ.Imaginary;
                Complex32 vt = υ + υy;
                υt[i].R = vt.Real; υt[i].i = vt.Imaginary;
            }



            // ------------------------------------- Calculate Numeric Coefficients
            Complex32 CTB = (Nb * CL * bladeChord * B * B) / (2 * Mathf.PI * bladeRadius);
            CTi = CTt - (0.5f * (((Nb * CL * bladeChord * xi * xi) / (2 * Mathf.PI * bladeRadius)) + CTB)) * (1 - B);
            CT.R = CTi.Real; CT.i = CTi.Imaginary;
            Complex32 mta = 0;
            Complex32 mtb = 0f;
            kpx[i] = (Nb * bladeChord * CD * Mathf.Pow(xi, 3)) / (2 * Mathf.PI * bladeRadius);
            kix[i] = (Nb * bladeChord * CL * Complex32.Sin(ϕr) * xi * xi * xi) / (2 * Mathf.PI * bladeRadius);
            if (i == subdivision) { for (int a = 1; a < subdivision; a++) { mta += kpx[a]; } CQnot = (0.085f / 2) * (kpx[0] + kpx[subdivision] + 2 * mta); CQ0.R = CQnot.Real; CQ0.i = CQnot.Imaginary; }
            Complex32 cqib = (Nb * bladeChord * CL * Complex32.Sin(ϕr) * B * B * B) / (2 * Mathf.PI * bladeRadius);
            CQIB.R = cqib.Real; CQIB.i = cqib.Imaginary;
            if (i == subdivision) { for (int a = 0; a < subdivision; a++) { mtb += kix[a]; } cqi = ((0.085f / 2) * (kix[0] + kix[subdivision] + 2 * mtb)) - ((0.5f) * (cqib + kix[subdivision]) * (1 - B)); CQI.R = cqi.Real; CQI.i = cqi.Imaginary; }
        }


        // ------------------------------------- Tip Correction
        δxt = (xtx[subdivision] - xtx[0]) / (subdivision); tsum = 0f;
        for (int i = 0; i < subdivision + 1; i++)
        {
            Complex32 xi = xtx[i];
            Complex32 fx = (Nb * CLx[i] * bladeChord * xi * xi) / (2 * Mathf.PI * bladeRadius);
            if (i > 0 && i < subdivision) { fx *= 2; }
            tsum += fx;
        }
        CTt = (δxt / 2) * tsum; CTtip.R = CTt.Real; CTtip.i = CTt.Imaginary; float br = CTt.Real;
        if (br < 0.006f) { B = 1 - (0.06f / Nb); }
        else if (br > 0.006f) { B = 1 - (Mathf.Sqrt(2.27f * br - 0.01f) / Nb); }
        CTσ = CT.R / solidity;


        // ------------------------------------- Torque Correction
        Complex32 δCTi = Complex32.Sqrt(2 * CTi);
        δCT.R = δCTi.Real; δCT.i = δCTi.Imaginary;
        for (int i = 0; i < subdivision + 2; i++) { if (i == 0) { stx[i] = δCTi; } else { stx[i] = xtx[i - 1]; } }
        δxt = (stx[subdivision + 1] - stx[0]) / (subdivision + 2);
        dsum = 0f;
        for (int i = 0; i < subdivision + 3; i++)
        {
            Complex32 xt = stx[0] + (δxt * (i + 1));
            Complex32 fx;
            if (i == 0) { fx = MathR.SwirlFactor(CTi, δCTi); }
            else { fx = MathR.SwirlFactor(CTi, xt); }
            if (i > 0 && i < subdivision + 1) { fx *= 2; }
            dsum += fx;
        }

        Complex32 δCQI = swirlCorrection.Evaluate(CT.R) * cqi;
        Complex32 DL = CTi * controller.core.airDensity * Complex32.Pow((Ω * bladeRadius), 2);
        Complex32 DLx = DL * (CTi / solidity);
        float δCP = powerCorrection.Evaluate(DLx.Real);
        Complex32 cq = (cqi + δCQI + CQnot) * δCP;
        CQ.R = cq.Real; CQ.i = cq.Imaginary;



        if (rotorType == RotorType.MainRotor)
        {
            // ------------------------------------- Blade Flapping
            β0 = ((2 * CTσ * γ) / (3 * CLα)) - ((3 * 9.81f * bladeRadius * bladeRadius) / 2) / Mathf.Pow((Ω * bladeRadius), 2);
            if (β0 > 0.18f) { β0 = 0.18f; }
            if (β0 < -0.08f) { β0 = -0.08f; }
            if (flappingState == BladeFlappingState.Dynamic)
            {
                β1c = ((µx * ((8 * ϴ0 + (k * 0.75f)) / 3 + (2 * λr.R))) - (ϴ1s * (1 + (3 * µx * µx) / 2))) / (1 - (0.5f * µx * µx));
                β1s = ((4 * µx * β0) + (ϴ1c * (3 * (1 + (0.5f * µx * µx))))) / (3 * (1 + (0.5f * µx * µx)));
            }
            else if (flappingState == BladeFlappingState.Static) { β1c = -ϴ1s; β1s = ϴ1c; }
            else { β1c = 0; β1s = 0; }
            if (float.IsNaN(β0) || float.IsInfinity(β0)) { β0 = 0f; }
            if (float.IsNaN(β1s) || float.IsInfinity(β1s)) { β1s = 0f; }
            if (float.IsNaN(β1c) || float.IsInfinity(β1c)) { β1c = 0f; }
        }
        else { β0 = β1s = β1c = 0f; }


        // ------------------------------------------- Longitudinal and Lateral Coefficients
        float µ = Mathf.Sqrt((µx * µx) + (µz * µz));
        Complex32 CHi = ((solidity * 0.5f * CLαr) * (ϴ0 * ((0.5f * λ * µ) - (β1c / 3)) + k * ((0.25f * λ * µ) - (β1c / 4)) + ϴ1s * ((0.25f * λ) - (µ * β1c * 0.25f)) - (ϴ1c * β0) / 6 + (3 * λ * β1c) / 4 + (β1s * β0) / 6 + (µ / 4) * ((β0 * β0) + (β1c * β1c)))) + (solidity * CLαr * µ * CD) / (4 * CLαr);
        CH = CHi.Real;

        // ------------------------------------------- Forces
        float T = CT.R * controller.core.airDensity * rotorArea * Mathf.Pow(Ω * bladeRadius, 2) * δT * δv;
        float Q = CQ.R * controller.core.airDensity * rotorArea * Mathf.Pow(Ω * bladeRadius, 2) * bladeRadius;
        if (float.IsNaN(T) || float.IsInfinity(T)) { T = 0f; }
        if (float.IsNaN(Q) || float.IsInfinity(Q)) { Q = 0f; }
        FHx = -CH * controller.core.airDensity * rotorArea * Mathf.Pow(Ω * bladeRadius, 2);
        FY = FHx * Mathf.Sin(δϕ) + FYy * Mathf.Cos(δϕ);
        FH = FHx * Mathf.Cos(δϕ) - FYy * Mathf.Sin(δϕ);

        float Fx = FH + T * Mathf.Sin(β1c) * Mathf.Cos(β1s);
        float Fy = FY - T * Mathf.Cos(β1c) * Mathf.Sin(β1s);
        float Fz = T * Mathf.Cos(β1c) * Mathf.Cos(β1s);
        if (float.IsNaN(Fx) || float.IsInfinity(Fx)) { Fx = 0f; }
        if (float.IsNaN(Fy) || float.IsInfinity(Fy)) { Fy = 0f; }
        if (float.IsNaN(Fz) || float.IsInfinity(Fz)) { Fz = 0f; }
        ThrustForce = new Vector3(-Fy, Fz, -Fx);


        // ------------------------------------------- Moments
        float Mx, My, Mz;
        if (rotorConfiguration == RotorConfiguration.Propeller)
        {
            if (propellerMode == PropellerMode.Vertical)
            {
                Mx = T * lx;
                My = -T * ly;
                Mz = Q * direction;
            }
            else { Mx = My = Mz = 0; }
        }
        else
        {
            Mx = Rβ1s * β1s - Q * Mathf.Sin(β1c) * Mathf.Cos(β1s);
            if (rotorConfiguration != RotorConfiguration.Tandem) { My = Mβ1c * β1c + Q * Mathf.Cos(β1c) * Mathf.Sin(β1s); } else { My = 0f; }
            Mz = controller.torqueState == PhantomController.TorqueState.Applied ? Q * Mathf.Cos(β1c) * Mathf.Cos(β1s) * direction : 0;
        }
        if (float.IsNaN(Mx) || float.IsInfinity(Mx)) { Mx = 0f; }
        if (float.IsNaN(My) || float.IsInfinity(My)) { My = 0f; }
        if (float.IsNaN(Mz) || float.IsInfinity(Mz)) { Mz = 0f; }
        TorqueForce = new Vector3(My, Mz, Mx);


        Thrust = T; coneAngle = β0 * Mathf.Rad2Deg;
        featherAngle = ϴ0 * Mathf.Rad2Deg;
        Torque = Q * Mathf.Cos(β1c) * Mathf.Cos(β1s) * direction;
        Inertia = J * MathR.Square(Mathf.Cos(β0));


        // ------------------------------------------- Apply Base Forces
        if (rotorConfiguration == RotorConfiguration.Tandem) { forceDirection = forcePoint.up; } else { forceDirection = transform.up; }
        if (rotorType == RotorType.MainRotor && rotorConfiguration != RotorConfiguration.Propeller) { controller.helicopter.AddForceAtPosition(Fz * forceDirection, transform.position, ForceMode.Force); }
        float δY = 1; if (controller.torqueState == PhantomController.TorqueState.Isolated) { δY = Mathf.Abs(controller.flightComputer.processedYaw); }
        if (rotorType == RotorType.TailRotor && rotorConfiguration != RotorConfiguration.Propeller) { controller.helicopter.AddForceAtPosition(-T * δY * transform.up, transform.position, ForceMode.Force); }
        if (rotorConfiguration == RotorConfiguration.Propeller && propellerMode == PropellerMode.Horizontal) { controller.helicopter.AddForceAtPosition(T * transform.up, transform.position, ForceMode.Force); }
    }







    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void AnalyseRotor()
    {

        // ------------------------------------------- Base
        if (weightUnit == WeightUnit.Kilogram) { weightFactor = 1f; }
        if (weightUnit == WeightUnit.Pounds) { weightFactor = (1 / 2.205f); }
        actualWeight = bladeMass * weightFactor;
        rotorArea = Mathf.PI * bladeRadius * bladeRadius;
        J = Nb * actualWeight * Mathf.Pow((bladeRadius * 0.55f), 2);
        float m = actualWeight / bladeRadius;
        Iβ = ((m * Mathf.Pow(bladeRadius, 3)) / 3) * Mathf.Pow((1 - re), 3);
        solidity = (Nb * bladeChord) / (Mathf.PI * bladeRadius);
        direction = rotorDirection == RotationDirection.CCW ? 1 : -1f;
        rootcut = (rotorRadius * rootCutOut) - rotorHeadRadius;
        bladeRadius = ((1 - rootCutOut) * rotorRadius) + rootcut;
        hingeOffset = re * bladeRadius; groundAxis.Normalize();
        aspectRatio = ((bladeRadius * bladeRadius) / (bladeRadius * bladeChord));
        Mw = (actualWeight * 9.8f * Mathf.Pow(bladeRadius, 2)) / 2;
        Ωmax = funcionalRPM * 0.104733f;


        // ------------------------------------- Surface Factor
        if (surfaceFinish == SurfaceFinish.MoldedComposite) { m_surface_k = 0.17f; }
        if (surfaceFinish == SurfaceFinish.PaintedAluminium) { m_surface_k = 3.33f; }
        if (surfaceFinish == SurfaceFinish.PolishedMetal) { m_surface_k = 0.50f; }
        if (surfaceFinish == SurfaceFinish.ProductionSheetMetal) { m_surface_k = 1.33f; }
        if (surfaceFinish == SurfaceFinish.SmoothPaint) { m_surface_k = 2.08f; }




        // ------------------------------------------- Rotor Config
        if (rotorConfiguration != RotorConfiguration.Conventional) { rotorType = RotorType.MainRotor; }
        if (rotorType == RotorType.TailRotor) { effectState = GroundEffectState.Neglect; flappingState = BladeFlappingState.Inactive; }
        if (rotorConfiguration == RotorConfiguration.Tandem) { MathR.PlotCorrectionFactor(tandemAnalysis, out kov); }
        if (effectState == GroundEffectState.Consider) { MathR.PlotGroundCorrection(out groundCorrection); }
        if (rotorConfiguration == RotorConfiguration.Coaxial && rotorPosition == CoaxialPosition.Bottom) { δv = 0.5616f; δQ = 1; }
        else if (rotorConfiguration == RotorConfiguration.Tandem && rotorConfiguration != RotorConfiguration.Coaxial && tandemPosition == TandemPosition.Rear) { δv = 1; float Qf = kov.Evaluate(controller.δD); δQ = (2 * Qf) - 1; }
        else { δv = 1; δQ = 1; }


        float eA = Mathf.Pow((Mathf.Tan(0f * Mathf.Deg2Rad)), 2); float eB = 4f + ((aspectRatio * aspectRatio) * (1 + eA)); spanEfficiency = 2 / (2 - aspectRatio + Mathf.Sqrt(eB));
        MathR.DrawCorrectionCurves(out swirlCorrection, out powerCorrection, out thrustCorrection);
        if (bladeChop && soundState == SoundState.Active) { Handler.SetupSoundSource(transform, bladeChop, "_rotor_sound", 80f, true, true, out soundPoint); }

        // ------------------------------------------- Tandem Config
        if (rotorConfiguration == RotorConfiguration.Tandem)
        {
            GameObject point = new GameObject("rotor_point_" + name);
            point.transform.parent = controller.core.transform; point.transform.localPosition = transform.localPosition;
            point.transform.localRotation = transform.localRotation;
            forcePoint = point.transform; baseRotation = forcePoint.localRotation;
        }


        // ------------------------------------------- Deflection Limits
        ϴmax = Mathf.Abs(maximumCollective) * Mathf.Deg2Rad;
        ϴmin = Mathf.Abs(minimumCollective) * Mathf.Deg2Rad;
        ϴ1sMax = Mathf.Abs(maximumRollCyclic) * Mathf.Deg2Rad;
        ϴ1sMin = Mathf.Abs(minimumRollCyclic) * Mathf.Deg2Rad;
        ϴ1cMax = Mathf.Abs(maximumPitchCyclic) * Mathf.Deg2Rad;
        ϴ1cMin = Mathf.Abs(minimumPitchCyclic) * Mathf.Deg2Rad;
        ϴymax = Mathf.Abs(maximumYawCollective) * Mathf.Deg2Rad;
        ϴfmax = Mathf.Abs(maximumPitchCollective) * Mathf.Deg2Rad;
        ϴsmax = Mathf.Abs(maximumYawCyclic) * Mathf.Deg2Rad;
        lx = transform.localPosition.x;
        ly = transform.localPosition.z;


        // ------------------------------------------- Initial Assumptions
        if (rootAirfoil != null && tipAirfoil != null) { float rCLα = rootAirfoil.centerLiftSlope; float tCLα = tipAirfoil.centerLiftSlope; CLα = MathR.EstimateEffectiveValue(rCLα, tCLα, 0.75f); }
        if (visualType == VisulType.Complete || visualType == VisulType.Partial)
        {
            if (blurredRotor.Length > 0 && blurredRotor[0] != null) { blurredRotorColor = blurredRotor[0].color; alphaSettings = 0; }
            if (normalRotor.Length > 0 && normalRotor[0] != null) { normalRotorColor = normalRotor[0].color; }
        }


        // ------------------------
        SetupContainers();
    }







    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void SetupContainers()
    {
        if (kix == null || kix.Length != subdivision + 1) { kix = new Complex32[subdivision + 1]; }
        if (kpx == null || kpx.Length != subdivision + 1) { kpx = new Complex32[subdivision + 1]; }
        if (CLx == null || CLx.Length != subdivision + 1) { CLx = new Complex32[subdivision + 1]; }
        if (xtx == null || xtx.Length != subdivision + 1) { xtx = new Complex32[subdivision + 1]; }
        if (stx == null || stx.Length != subdivision + 2) { stx = new Complex32[subdivision + 2]; }

        if (υt == null || υt.Length != subdivision + 1) { υt = new cfloat[subdivision + 1]; }
        if (υi == null || υi.Length != subdivision + 1) { υi = new cfloat[subdivision + 1]; }
        if (λt == null || λt.Length != subdivision + 1) { λt = new cfloat[subdivision + 1]; }
        if (α == null || α.Length != subdivision + 1) { α = new cfloat[subdivision + 1]; }
        if (CLf == null || CLf.Length != subdivision + 1) { CLf = new cfloat[subdivision + 1]; }
        if (CDf == null || CDf.Length != subdivision + 1) { CDf = new cfloat[subdivision + 1]; }
    }





#if UNITY_EDITOR
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        // ------------------------ Collect Foil
        if (rotor == null) { rotor = this.GetComponent<PhantomRotor>(); }

        // ------------------------ Base Shape
        MathR.RotorDesign.AnalyseRotorShape(rotor);

        // ------------------------
        SetupContainers();
    }
#endif
}
