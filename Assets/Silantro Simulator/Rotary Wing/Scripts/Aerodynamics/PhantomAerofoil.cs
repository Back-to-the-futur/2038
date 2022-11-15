using Oyedoyin;
using Oyedoyin.Rotary;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
///
/// 
/// Use:		 Handles the calculation/analysis of aerodynamic force and moments on the helicopter
/// </summary>


[RequireComponent(typeof(BoxCollider))]
public class PhantomAerofoil : MonoBehaviour
{


	// ------------------------------ Selectibles
	public enum AerofoilType { Sponson, Stabilator, Fin }
	public AerofoilType aerofoilType = AerofoilType.Sponson;
	public enum StabilatorType { Plain, Elevator, Elevon }
	public StabilatorType stabType = StabilatorType.Plain;
	public enum FinType { Plain, Rudder }
	public FinType finType = FinType.Plain;
	public enum SweepDirection { Unswept, Forward, Backward }
	public SweepDirection sweepDirection = SweepDirection.Unswept;
	public enum SurfaceFinish { SmoothPaint, PolishedMetal, ProductionSheetMetal, MoldedComposite, PaintedAluminium }
	public SurfaceFinish surfaceFinish = SurfaceFinish.PaintedAluminium;
	

	// ------------------------------ Surface Movement
	public enum RotationAxis { X, Y, Z }
	public RotationAxis deflectionAxis = RotationAxis.X;
	public enum DeflectionDirection { CW, CCW }
	public DeflectionDirection deflectionDirection;



	// ------------------------------ Connections
	public Transform controlSurfaceModel;
	public SilantroAirfoil rootAirfoil, tipAirfoil;
	public PhantomController controller;
	public PhantomControlModule coreSystem;
	public Rigidbody helicopter; 
	public BoxCollider foilCollider;
	public PhantomAerofoil foil;
	public AnimationCurve effectivenessPlot;



	// ------------------------------ Variables
	[Range(3, 10)] public int foilSubdivisions = 5;
	[Range(0, 90f)] public float aerofoilSweepAngle;
	[Range(0, 95f)] public float taperPercentage;
	public float foilDeflection;
	public float viscocity = 0.000014607f, k, machSpeed = 0f;
	public float spanEfficiency = 0.9f;
	public float sweepCorrectionFactor = 1f;
	public Vector3 controlAxis;
	public Quaternion baseModelRotation;
	public float taperRatio, aspectRatioCorrection;
	public float foilTipLeadingExtension, foilTipTrailingExtension;
	public string sweepString;
	public Color controlColor;
	public float actuationSpeed = 50f;
	float temp;


	// ------------------------------ Shape Dimensions
	public float foilLength, foilWidth, quaterSweep, tipCenterSweep;
	public float sweepAngle, aspectRatio, foilSpan, foilRootChord, foilTipChord, leadingEdgeSweep;
	public float foilArea, foilMeanChord, foilWettedArea;
	[HideInInspector] public Vector3 rootChordCenter, tipChordCenter, quaterRootChordPoint, quaterTipChordPoint;
	public Vector3 RootChordLeading, TipChordLeading, RootChordTrailing, TipChordTrailing, baseTip;
	public Vector3 baseTipLeading, baseTipTrailing;
	public float rootAirfoilArea, tipAirfoilArea;
	public List<Vector3> tipPoints, rootPoints;
	public Vector3 panelWorldFlow, panelLocalFlow;


	// ------------------------------ Control
	[Range(0, 100f)]
	public float controlRootChord = 10f, controlTipChord = 10f;
	public float controlArea;
	public float controlSpan;
	public float correctedDeflection;
	public bool[] controlSections;
	public float controlDeflection, positiveLimit = 30f, negativeLimit = 20f;
	float controlActuatorDeflection, baseDeflection;


	// ----------------------------- Inputs
	public float pitchInput, rollInput, yawInput;
	private float baseInput;


	// ------------------------------ Control Bools
	public bool drawFoils = true;
	//public bool drawSplits;
	public float[] panelAOA;


	// ------------------------------ Output
	public float maximumAOA, stallAOA;
	public float TotalLift, TotalDrag, TotalMoment, TotalSkinDrag;
	public float TotalBaseDrag, TotalControlDrag;


 


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    bool allOk;
	protected void _checkPrerequisites()
	{
		// ------------------------------ Check Components
		if (helicopter != null && coreSystem != null && rootAirfoil != null && tipAirfoil != null) { allOk = true; }
		else if (helicopter == null) { Debug.LogError("Prerequisites not met on " + aerofoilType.ToString() + " " + transform.name + "....helicopter rigidbody not assigned"); allOk = false; }
		else if (coreSystem == null) { Debug.LogError("Prerequisites not met on " + aerofoilType.ToString() + " " + transform.name + "....control module not assigned"); allOk = false; }
		else if (rootAirfoil == null) { Debug.LogError("Prerequisites not met on " + aerofoilType.ToString() + " " + transform.name + "....Root airfoil has not been assigned"); allOk = false; }
		else if (tipAirfoil == null) { Debug.LogError("Prerequisites not met on " + aerofoilType.ToString() + " " + transform.name + "....Tip airfoil has not been assigned"); allOk = false; }
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public void InitializeFoil()
	{

		//----------------------------
		_checkPrerequisites();


		if (allOk)
		{
			stallAOA = 90f;
			// ------------------------------ Surface Finish
			if (surfaceFinish == SurfaceFinish.MoldedComposite) { k = 0.17f; }
			if (surfaceFinish == SurfaceFinish.PaintedAluminium) { k = 3.33f; }
			if (surfaceFinish == SurfaceFinish.PolishedMetal) { k = 0.50f; }
			if (surfaceFinish == SurfaceFinish.ProductionSheetMetal) { k = 1.33f; }
			if (surfaceFinish == SurfaceFinish.SmoothPaint) { k = 2.08f; }
			sweepString = sweepDirection.ToString();
			if (rootAirfoil != null && tipAirfoil != null)
			{
				if (rootAirfoil.stallAngle < stallAOA) { stallAOA = rootAirfoil.stallAngle; }
				if (tipAirfoil.stallAngle < stallAOA) { stallAOA = tipAirfoil.stallAngle; }
				if (stallAOA == 0 || stallAOA > 90) { stallAOA = 15f; }
			}

			// ------------------------------ Calculate Dimensions
			AnalyseDimensions();

			// ------------------------------ Control
			controlAxis = Handler.EstimateModelProperties(deflectionDirection.ToString(), deflectionAxis.ToString());
			if (controlSurfaceModel != null) { baseModelRotation = controlSurfaceModel.transform.localRotation; }
		}
	}


	


	/// <summary>
	/// Recalculate vector points for the aerofoil shape based on transform position and set variables
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void AnalyseStructure()
	{

		// ---------------------------------- Estimate Efficiency
		float eA = Mathf.Pow((Mathf.Tan(quaterSweep * Mathf.Deg2Rad)), 2);
		float eB = 4f + ((aspectRatio * aspectRatio) * (1 + eA));
		spanEfficiency = 2 / (2 - aspectRatio + Mathf.Sqrt(eB));
		sweepCorrectionFactor = Mathf.Cos(quaterSweep * Mathf.Deg2Rad);
		aspectRatio = (foilSpan * foilSpan) / (foilArea);

		// ---------------------------------- Base Factors
		Vector3 scaleFactor, supremeFactor, RootScale;
		float parentLength, parentWidth, combinedScale, wingTaper;
		if (helicopter != null) { RootScale = helicopter.transform.localScale; } else { RootScale = transform.root.localScale; }
		foilLength = transform.localScale.x; foilWidth = transform.localScale.z;
		wingTaper = 100 - taperPercentage; parentLength = RootScale.x; parentWidth = RootScale.z;


		// ----------------------------------- Dimension Ratios
		scaleFactor = transform.forward * (parentWidth * foilWidth * 0.5f); taperRatio = wingTaper / 100f;
		supremeFactor = transform.forward * ((parentWidth * foilWidth * 0.5f) * (wingTaper / 100));
		combinedScale = RootScale.magnitude * transform.localScale.magnitude;
		MathBase.EstimateFoilShapeProperties("Untwisted", sweepString, 0, aerofoilSweepAngle, out temp, out sweepAngle);


		// ------------------------------------- Structure Points
		rootChordCenter = transform.position - (transform.right * (parentLength * foilLength * 0.5f));
		tipChordCenter = (transform.position + (transform.right * (parentLength * foilLength * 0.5f))) + (transform.forward * (sweepAngle / 90) * combinedScale);
		RootChordLeading = rootChordCenter + scaleFactor; RootChordTrailing = rootChordCenter - scaleFactor;
		baseTipLeading = tipChordCenter + supremeFactor; TipChordLeading = baseTipLeading + (transform.right * foilTipLeadingExtension);
		baseTipTrailing = tipChordCenter - supremeFactor; TipChordTrailing = baseTipTrailing - (foilTipTrailingExtension * transform.right);
		baseTip = (transform.position + (transform.right * (parentLength * foilLength * 0.5f))) + transform.forward * ((parentWidth * foilWidth * 0.5f));
	}







	// <summary>
	/// Calculate wing dimensions needed for analysis
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void AnalyseDimensions()
	{
		// ---------------------------------- Base Variables
		float tanc4 = Mathf.Tan((leadingEdgeSweep * Mathf.Deg2Rad)) + ((foilRootChord / (4 * foilSpan)) * (taperRatio - 1));
		quaterSweep = Mathf.Atan(tanc4) * Mathf.Rad2Deg;
		float tanc2 = tanc4 - ((2 / aspectRatio) * (0.25f * ((1 - taperRatio) / (1 + taperRatio))));
		tipCenterSweep = Mathf.Atan(tanc2) * Mathf.Rad2Deg;
		float tipDistance = Vector3.Distance(baseTip, TipChordLeading);
		leadingEdgeSweep = Mathf.Abs(Mathf.Atan(tipDistance / foilLength) * Mathf.Rad2Deg);
		aspectRatioCorrection = aspectRatio / (aspectRatio + 2 * (aspectRatio + 4) / (aspectRatio + 2));


		// ------------------------------------ Calculate Dimensions
		foilRootChord = Vector3.Distance(RootChordTrailing, RootChordLeading);
		foilTipChord = Vector3.Distance(TipChordLeading, TipChordTrailing);
		foilMeanChord = MathBase.EstimateMeanChord(foilRootChord, foilTipChord);
		foilArea = MathBase.EstimatePanelArea(foilSpan, foilRootChord, foilTipChord);
		foilSpan = Mathf.Abs(foilLength);


		if (rootAirfoil && tipAirfoil) { float meanThickness = (rootAirfoil.maximumThickness + tipAirfoil.maximumThickness) / 2f;
		foilWettedArea = foilArea * (1.977f + (0.52f * meanThickness)); }
		quaterRootChordPoint = MathBase.EstimateSectionPosition(RootChordLeading, RootChordTrailing, 0.25f);
		quaterTipChordPoint = MathBase.EstimateSectionPosition(TipChordLeading, TipChordTrailing, 0.25f);
	}




	/// <summary>
	/// Process control inputs into surface deflection
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void AnalyseControl()
	{
		if (finType == FinType.Rudder || stabType == StabilatorType.Elevon || stabType == StabilatorType.Elevator)
		{
			float flashRate = 1 / 40f;
			effectivenessPlot = MathBase.PlotControlEffectiveness();

			// ---------------------------------- Collect Inputs
			if (finType == FinType.Rudder) { baseInput = -yawInput; }
			if (stabType == StabilatorType.Elevator)
			{
				float corePitch = pitchInput; if (transform.localScale.x < 0) { corePitch *= -1; }
				baseInput = corePitch;
			}
			if (stabType == StabilatorType.Elevon)
			{
				float basePitch = pitchInput; float baseRoll = rollInput; if (transform.localScale.x < 0) { basePitch *= -1; }
				baseInput = ((basePitch * 2f) + (baseRoll * 2f)) / 2f;
			}

			// ----------------------------------- Clamp
			if (baseInput > 1) { baseInput = 1f; }
			if (baseInput < -1) { baseInput = -1f; }


			// ---------------------------------- Deflect
			if (transform.localScale.x < 0)
			{
				baseDeflection = baseInput > 0f ? baseInput * negativeLimit : baseInput * positiveLimit;
				controlActuatorDeflection = baseDeflection;
				if (controlActuatorDeflection > negativeLimit) { controlActuatorDeflection = negativeLimit; }
				if (controlActuatorDeflection < -positiveLimit) { controlActuatorDeflection = -positiveLimit; }
			}
			if (transform.localScale.x > 0)
			{
				baseDeflection = baseInput > 0f ? baseInput * positiveLimit : baseInput * negativeLimit;
				controlActuatorDeflection = baseDeflection;
				if (controlActuatorDeflection > positiveLimit) { controlActuatorDeflection = positiveLimit; }
				if (controlActuatorDeflection < -negativeLimit) { controlActuatorDeflection = -negativeLimit; }
			}
			controlDeflection = Mathf.MoveTowards(controlDeflection, controlActuatorDeflection, actuationSpeed * flashRate);
			if (controlSurfaceModel) { controlSurfaceModel.transform.localRotation = baseModelRotation; controlSurfaceModel.transform.Rotate(controlAxis, controlDeflection); }
			
			// ------------------------------------ Foil Correction
			if (transform.localScale.x < 0) { correctedDeflection = controlDeflection; }
			if (transform.localScale.x > 0) { correctedDeflection = -controlDeflection; }
		}
	}







	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void FixedUpdate()
	{
		if (allOk && controller.isControllable)
		{
			// --------------------- Shape
			AnalyseStructure();

			// --------------------- Control
			AnalyseControl();

			// --------------------- Forces
			AnalyseForces();
		}
	}





	public float panelControlLiftCoefficient = 0f;
	/// <summary>
	/// Process aerodynamic forces based on set variables
	/// </summary>
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void AnalyseForces()
	{
		// ------------------------------------- Reset Variabless
		TotalBaseDrag = TotalControlDrag = TotalDrag = TotalLift = TotalSkinDrag = TotalMoment = 0f;


		// ------------------------------------- Panel Iteration
		for (int p = 0; p < foilSubdivisions; p++)
		{
			// ------------------------ Factors
			float currentPanel = (float)p; float nextPanel = (float)(p + 1);
			float panelCount = (float)foilSubdivisions;


			// ------------------------ Variables
			float currentPanelFactor = currentPanel / panelCount; float nextPanelFactor = nextPanel / panelCount;
			float α, panelDistance, panelLiftCoefficient, panelDragCoefficient, panelMomentCoefficient, dynamicPressure;
			float panelArea, panelTipChord, panelRootChord, panelMeanChord;

			// ------------------------Points
			Vector3 LeadEdgeRight, LeadEdgeLeft, TrailEdgeRight, TrailEdgeLeft, geometricCenter, circulation;
			Vector3 panelLiftDirection, panelDragDirection, panelCoefficients, PanelCenter, leftExtension, rightExtension;

			LeadEdgeRight = MathBase.EstimateSectionPosition(RootChordLeading, baseTipLeading, nextPanelFactor);
			LeadEdgeLeft = MathBase.EstimateSectionPosition(RootChordLeading, baseTipLeading, currentPanelFactor);
			TrailEdgeLeft = MathBase.EstimateSectionPosition(RootChordTrailing, baseTipTrailing, currentPanelFactor);
			TrailEdgeRight = MathBase.EstimateSectionPosition(RootChordTrailing, baseTipTrailing, nextPanelFactor);
			if (foil.finType == FinType.Rudder || foil.stabType == StabilatorType.Elevator || foil.stabType == StabilatorType.Elevon)
			{
				MathR.EstimateControlExtension(LeadEdgeLeft, TrailEdgeRight, TrailEdgeLeft, LeadEdgeRight, controlRootChord, controlTipChord, controlDeflection, out leftExtension, out rightExtension);
				TrailEdgeLeft = MathBase.EstimateSectionPosition(TrailEdgeLeft, LeadEdgeLeft, (controlRootChord * 0.01f)) + leftExtension;
				TrailEdgeRight = MathBase.EstimateSectionPosition(TrailEdgeRight, LeadEdgeRight, (controlTipChord * 0.01f)) + rightExtension;

			}
			geometricCenter = MathBase.EstimateGeometricCenter(LeadEdgeRight, TrailEdgeRight, LeadEdgeLeft, TrailEdgeLeft);
			PanelCenter = MathBase.EstimateSectionPosition(LeadEdgeLeft, LeadEdgeRight, 0.5f) - MathBase.EstimateSectionPosition(TrailEdgeLeft, TrailEdgeRight, 0.5f); PanelCenter.Normalize();


			// ----------- Extract alpha and calculate lift Direction
			Vector3 sectionRootCenter = MathBase.EstimateSectionPosition(LeadEdgeLeft, TrailEdgeLeft, 0.5f);
			Vector3 sectionTipCenter = MathBase.EstimateSectionPosition(LeadEdgeRight, TrailEdgeRight, 0.5f);
			float sectionTaper = Vector3.Distance(LeadEdgeRight, TrailEdgeRight) / Vector3.Distance(LeadEdgeLeft, TrailEdgeLeft);
			float sectionSpan = Vector3.Distance(sectionRootCenter, sectionTipCenter);
			float factor = ((sectionSpan / 6) * ((1 + (2 * sectionTaper)) / (1 + sectionTaper))) / sectionSpan;
			Vector3 yMGCTop = MathBase.EstimateSectionPosition(TrailEdgeLeft, LeadEdgeLeft, (1 - factor));
			Vector3 yMGCBottom = MathBase.EstimateSectionPosition(TrailEdgeRight, LeadEdgeRight, (1 - factor));


			panelWorldFlow = (helicopter.GetPointVelocity(geometricCenter) + controller.baseWind); panelLocalFlow = -panelWorldFlow;
			circulation = Vector3.Cross(helicopter.angularVelocity.normalized, (geometricCenter - helicopter.worldCenterOfMass).normalized);
			circulation *= -((helicopter.angularVelocity.magnitude) * (geometricCenter - helicopter.worldCenterOfMass).magnitude);
			panelLocalFlow += circulation;
			Vector3 parallelFlow = (transform.right * (Vector3.Dot(transform.right, panelLocalFlow)));
			panelLocalFlow -= parallelFlow; Vector3 normalWind = panelLocalFlow.normalized;
			α = Mathf.Acos(Vector3.Dot(PanelCenter, -normalWind)) * Mathf.Rad2Deg;
			panelLiftDirection = Vector3.Cross(PanelCenter, (yMGCBottom - yMGCTop).normalized);
			panelLiftDirection.Normalize(); if (transform.localScale.x < 0.0f) { panelLiftDirection = -panelLiftDirection; }
			if (Vector3.Dot(panelLiftDirection, normalWind) < 0.0f) { α = -α; }
			panelDragDirection = panelLocalFlow;

			//---- Filter
			if (α > 179) { α = 179; }
			if (α < -179) { α = -179; }
			if (float.IsNaN(α)) { α = 0f; }
			if (float.IsInfinity(α)) { α = 0f; }
			if (α > maximumAOA) { maximumAOA = α; }
			//panelAOA[p] = α;


			// ---------------------------------- Panel Dynamics
			panelArea = MathBase.EstimatePanelSectionArea(LeadEdgeLeft, LeadEdgeRight, TrailEdgeLeft, TrailEdgeRight);
			panelDistance = Vector3.Distance(RootChordTrailing, TrailEdgeLeft) / foilSpan;
			dynamicPressure = 0.5f * coreSystem.airDensity * panelLocalFlow.sqrMagnitude;
			panelTipChord = Vector3.Distance(LeadEdgeRight, TrailEdgeRight);
			panelRootChord = Vector3.Distance(LeadEdgeLeft, TrailEdgeLeft);
			panelMeanChord = MathBase.EstimateMeanChord(panelRootChord, panelTipChord);

			
			if (foil.finType == FinType.Rudder || foil.stabType == StabilatorType.Elevator || foil.stabType == StabilatorType.Elevon)
			{
				float panelLiftSlope = MathBase.EstimateEffectiveValue(rootAirfoil.centerLiftSlope, tipAirfoil.centerLiftSlope, panelDistance, 0, foilSpan);
				if (panelLiftSlope < 1) { panelLiftSlope = 6.28f; }
				float effectiveControlChord = MathBase.EstimateEffectiveValue(controlRootChord, controlTipChord, panelDistance, 0, controlSpan);
				float θf = Mathf.Acos(2 * (effectiveControlChord / 100) - 1);
				float τ = 1 - (θf - Mathf.Sin(θf)) / Mathf.PI;
				float numericDeflection = Mathf.Abs(controlDeflection);
				float ƞ = effectivenessPlot.Evaluate(numericDeflection);
				panelControlLiftCoefficient = Mathf.Abs(panelLiftSlope) * τ * ƞ * numericDeflection * Mathf.Deg2Rad * -Mathf.Sign(controlDeflection);
			}

			panelCoefficients = MathBase.EvaluateCoefficients(rootAirfoil, tipAirfoil, α, panelDistance, foilSpan);
			float baseLiftCoefficient = panelCoefficients.x;
			float baseDragCoefficient = panelCoefficients.y;
			float baseMomentCoefficient = panelCoefficients.z;


			panelLiftCoefficient = baseLiftCoefficient;// + panelControlLiftCoefficient;
			panelDragCoefficient = baseDragCoefficient;
			panelMomentCoefficient = baseMomentCoefficient;


			// ---------------------------------- Panel Forces
			float panelLift = sweepCorrectionFactor * panelArea * panelLiftCoefficient * dynamicPressure; TotalLift += panelLift;
			float panelDrag = panelArea * panelDragCoefficient * dynamicPressure; TotalDrag += panelDrag;
			float panelMoment = dynamicPressure * panelArea * panelMeanChord * panelMeanChord * panelMomentCoefficient; TotalMoment += panelMoment;
			Vector3 panelTorque = Vector3.Cross(PanelCenter, panelLiftDirection.normalized); panelTorque.Normalize(); panelTorque *= panelMoment;
			panelAOA[p] = panelDrag;

			// ---------------------------------- Apply
			if (!float.IsNaN(panelLift) && !float.IsInfinity(panelLift)) { helicopter.AddForceAtPosition(panelLiftDirection * panelLift, transform.position, ForceMode.Force); }
			if (!float.IsNaN(panelDrag) && !float.IsInfinity(panelDrag)) { helicopter.AddForceAtPosition(panelDragDirection * panelDrag, transform.position, ForceMode.Force); }
			if (!float.IsNaN(panelMoment) && !float.IsInfinity(panelMoment)) { helicopter.AddTorque(panelTorque, ForceMode.Force); }
		}
	}






#if UNITY_EDITOR
	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void OnDrawGizmosSelected()
	{
		if (foil == null) { foil = this.GetComponent<PhantomAerofoil>(); }
		if (sweepString != sweepDirection.ToString()) { sweepString = sweepDirection.ToString(); }

		// ------------------------ Base Shape
		AnalyseDimensions(); MathR.FoilDesign.ShapeAerofoil(foil, true);
	}
 


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnDrawGizmos()
	{
		// ------------------------ Collect Foil
		if (foil == null) { foil = this.GetComponent<PhantomAerofoil>(); }
		if (panelAOA == null || foilSubdivisions != panelAOA.Length) {panelAOA = new float[foilSubdivisions]; }

		AnalyseStructure(); MathR.FoilDesign.ShapeAerofoil(foil, false);

		// ------------------------ Advanced Visuals
		if (foil.finType == FinType.Rudder || foil.stabType == StabilatorType.Elevator || foil.stabType == StabilatorType.Elevon)
		{ MathR.FoilDesign.MapSurface(foil); }
	}
#endif
}
