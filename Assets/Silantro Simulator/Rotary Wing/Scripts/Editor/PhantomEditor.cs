using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif





// --------------------------------------------- Shaft
#region TurboShaft
#if UNITY_EDITOR
// --------------------------------------------- Turboshaft
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomTurboShaft))]
public class PhantomTurboShaftEditor : Editor
{

    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomTurboShaft prop;
    SerializedProperty core;
    public int toolbarTab;
    public string currentTab;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { prop = (PhantomTurboShaft)target; core = serializedObject.FindProperty("core"); }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUILayout.Space(2f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Identifier", MessageType.None);
        GUI.color = backgroundColor;
        EditorGUILayout.PropertyField(core.FindPropertyRelative("engineIdentifier"), new GUIContent(" "));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("enginePosition"), new GUIContent("Position"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("engineNumber"), new GUIContent("Number"));



        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Properties", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("engineDiameter"), new GUIContent("Engine Diameter"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("intakePercentage"), new GUIContent("Intake Ratio"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Intake Diameter", prop.diffuserDrawDiameter.ToString("0.000") + " m");

        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustPercentage"), new GUIContent("Exhaust Ratio"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Exhaust Diameter", prop.exhaustDrawDiameter.ToString("0.000") + " m");

        GUILayout.Space(8f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("intakeType"), new GUIContent("Intake Type"));
        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Power Turbine RPM", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("functionalRPM"), new GUIContent(" "));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("intakeFactor"), new GUIContent("Intake Factor"));
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Airflow", prop.ma.ToString("0.00") + " kg/s");
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("OverSpeed Allowance (%)", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("overspeedAllowance"), new GUIContent(" "));
        GUILayout.Space(4f);
        EditorGUILayout.LabelField("Maximum RPM", prop.core.maximumRPM.ToString("0.0") + " RPM");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Minimum RPM", prop.core.minimumRPM.ToString("0.0") + " RPM");
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("baseCoreAcceleration"), new GUIContent("Core Acceleration"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Core RPM", prop.core.coreRPM.ToString("0.0") + " RPM");


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Thermodynamic Properties", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("πc"), new GUIContent("Core Pressure Ratio"));
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Pressure Drop (%)", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pcc"), new GUIContent("Compressor"));
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Turbine Inlet Temperature (°K)", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TIT"), new GUIContent(" "));
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("PSFC ", prop.PSFC.ToString("0.00") + " lb/hp.hr");



        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Efficiency Configuration (%)", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nd"), new GUIContent("Diffuser"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nc"), new GUIContent("Compressor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nb"), new GUIContent("Burner"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nt"), new GUIContent("Turbine"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nn"), new GUIContent("Nozzle"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ng"), new GUIContent("Gear"));





        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Connections", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("intakePoint"), new GUIContent("Intake Point"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exitPoint"), new GUIContent("Exhaust Point"));


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("soundMode"), new GUIContent("Mode"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorMode"), new GUIContent("Cabin Sounds"));
        GUILayout.Space(5f);
        if (prop.core.soundMode == PhantomEngineCore.SoundMode.Basic)
        {
            if (prop.core.interiorMode == PhantomEngineCore.InteriorMode.Off)
            {
                EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Ignition Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Shutdown Sound"));
            }
            else
            {
                toolbarTab = GUILayout.Toolbar(toolbarTab, new string[] { "Exterior Sounds", "Interior Sounds" });
                GUILayout.Space(5f);
                switch (toolbarTab)
                {
                    case 0: currentTab = "Exterior Sounds"; break;
                    case 1: currentTab = "Interior Sounds"; break;
                }
                switch (currentTab)
                {
                    case "Exterior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Ignition Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Shutdown Sound"));
                        break;

                    case "Interior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionInterior"), new GUIContent("Interior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorIdle"), new GUIContent("Interior Idle"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownInterior"), new GUIContent("Interior Shutdown"));
                        break;
                }
            }
        }
        else
        {
            GUILayout.Space(3f);
            if (prop.core.interiorMode == PhantomEngineCore.InteriorMode.Off)
            {
                EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Exterior Ignition"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("frontIdle"), new GUIContent("Front Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("sideIdle"), new GUIContent("Side Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Rear Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Exterior Shutdown"));
            }
            else
            {
                toolbarTab = GUILayout.Toolbar(toolbarTab, new string[] { "Exterior Sounds", "Interior Sounds" });
                GUILayout.Space(5f);
                switch (toolbarTab)
                {
                    case 0: currentTab = "Exterior Sounds"; break;
                    case 1: currentTab = "Interior Sounds"; break;
                }
                switch (currentTab)
                {
                    case "Exterior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Exterior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("frontIdle"), new GUIContent("Front Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("sideIdle"), new GUIContent("Side Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Rear Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Exterior Shutdown"));
                        break;

                    case "Interior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionInterior"), new GUIContent("Interior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorIdle"), new GUIContent("Interior Idle"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownInterior"), new GUIContent("Interior Shutdown"));
                        break;
                }
            }
        }



        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Effects Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("exhaustSmoke"), new GUIContent("Exhaust Smoke"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("smokeEmissionLimit"), new GUIContent("Maximum Emission"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("exhaustDistortion"), new GUIContent("Exhaust Distortion"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("distortionEmissionLimit"), new GUIContent("Maximum Emission"));



        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Output", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Core Power", (prop.core.corePower * prop.core.coreFactor * 100f).ToString("0.00") + " %");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Brake Power", prop.brakePower.ToString("0.0") + " Hp");


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Core
#region Core
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomControlModule))]
public class PhantomControlModuleEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomControlModule core;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { core = (PhantomControlModule)target; }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("COG Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("emptyCenterOfMass"), new GUIContent("Empty COG"));
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Deviation", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        string longLoad;
        string latLoad;
        if(core.deviation.z > 0.01f){ longLoad = "Forward"; }
        else if(core.deviation.z < -0.01f) { longLoad = "Rear"; }
        else { longLoad = "Neutral"; }

        if (core.deviation.x > 0.01f) { latLoad = "Right"; }
        else if (core.deviation.x < -0.01f) { latLoad = "Left"; }
        else { latLoad = "Neutral"; }
        EditorGUILayout.LabelField("Longitudinal", core.deviation.z.ToString("0.00") + " m" + " " + longLoad);
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Lateral", core.deviation.x.ToString("0.00") + " m" + " " + latLoad);
       
        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Intertia Tensor Data", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("xInertia"), new GUIContent("Pitch Inertia"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("zInertia"), new GUIContent("Roll Inertia"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("yInertia"), new GUIContent("Yaw Inertia"));



        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Limits", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationDrag"), new GUIContent("Angular Drag"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxAngularSpeed"), new GUIContent("Rate Limit (rad/s)"));
        float rateLimit = core.maxAngularSpeed * Mathf.Rad2Deg;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField(" ", rateLimit.ToString("0.0") + " °/s");



        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Performance Data", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Basic", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Forward Airspeed", (core.forwardSpeed * Oyedoyin.MathBase.toKnots).ToString("0.0") + " knots");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Drift Speed", (core.driftSpeed * Oyedoyin.MathBase.toKnots).ToString("0.0") + " knots");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Mach", core.machSpeed.ToString("0.00"));

        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Altitude", (core.currentAltitude * Oyedoyin.MathBase.toFt).ToString("0.0") + " feet");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Vertical Speed", (core.verticalSpeed * Oyedoyin.MathBase.toFtMin).ToString("0.0") + " ft/min");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Heading Direction", core.headingDirection.ToString("0.0") + " °");


        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Advanced", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("G-Load", core.gForce.ToString("0.0"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Pitch Rate", core.pitchRate.ToString("0.0") + " °/s");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Roll Rate", core.rollRate.ToString("0.0") + " °/s");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Yaw Rate", core.yawRate.ToString("0.0") + " °/s");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Turn Rate", core.turnRate.ToString("0.0") + " °/s");


        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Low Pass Filter", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("smoothGSpeed"), new GUIContent("G Smooth Speed"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("smoothRateSpeed"), new GUIContent("Rate Smooth Speed"));


        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Ambient", MessageType.None);
        GUI.color = backgroundColor; GUILayout.Space(2f);
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Air Density", core.airDensity.ToString("0.000") + " kg/m3");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Temperature", core.ambientTemperature.ToString("0.0") + " °C");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Pressure", core.ambientPressure.ToString("0.0") + " kPa");

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Camera
#region Camera
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomCamera))]
public class PhantomCameraEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomCamera camera;
    public int toolbarTab; public string currentTab;

    private void OnEnable() { camera = (PhantomCamera)target; }

    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();


        GUILayout.Space(4f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Camera Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraType"));

        if (camera.cameraType == PhantomCamera.CameraType.Helicopter)
        {
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Functionality Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraFocus"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attachment"), new GUIContent("Attachment"));

            GUILayout.Space(10f);
            toolbarTab = GUILayout.Toolbar(toolbarTab, new string[] { "Exterior Camera", "Interior Camera" });
            switch (toolbarTab)
            {
                case 0: currentTab = "Exterior Camera"; break;
                case 1: currentTab = "Interior Camera"; break;
            }


            switch (currentTab)
            {
                case "Exterior Camera":
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("normalExterior"), new GUIContent("Exterior Camera"));
                    GUILayout.Space(5f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Free Camera Settings", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("focusPoint"));
                    GUILayout.Space(5f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Camera Movement", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    serializedObject.FindProperty("elevationSensitivity").floatValue = EditorGUILayout.Slider("Elevation Sensitivity", serializedObject.FindProperty("elevationSensitivity").floatValue, 0f, 5f);
                    GUILayout.Space(3f);
                    serializedObject.FindProperty("azimuthSensitivity").floatValue = EditorGUILayout.Slider("Azimuth Sensitivity", serializedObject.FindProperty("azimuthSensitivity").floatValue, 0f, 5f);
                    GUILayout.Space(5f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Camera Positioning", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    camera.maximumRadius = EditorGUILayout.FloatField("Camera Distance", camera.maximumRadius);



                    if (camera.attachment != PhantomCamera.CameraAttachment.None)
                    {
                        GUILayout.Space(10f);
                        GUI.color = Color.white;
                        EditorGUILayout.HelpBox("Exterior Attachment (e.g Pilot Body)", MessageType.None);
                        GUI.color = backgroundColor;
                        GUILayout.Space(3f);
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("exteriorObject"), new GUIContent(" "));
                    }

                    break;
                case "Interior Camera":
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("normalInterior"), new GUIContent("Interior Camera"));
                    GUILayout.Space(5f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Camera Movement", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    serializedObject.FindProperty("mouseSensitivity").floatValue = EditorGUILayout.Slider("Mouse Sensitivity", serializedObject.FindProperty("mouseSensitivity").floatValue, 0f, 100f);
                    GUILayout.Space(5f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Camera Positioning", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    serializedObject.FindProperty("clampAngle").floatValue = EditorGUILayout.Slider("View Angle", serializedObject.FindProperty("clampAngle").floatValue, 10f, 210f);


                    GUILayout.Space(10f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Zoom Settings", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(5f);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("zoomEnabled"));
                    if (camera.zoomEnabled)
                    {
                        GUILayout.Space(3f);
                        serializedObject.FindProperty("maximumFOV").floatValue = EditorGUILayout.Slider("Minimum FOV", serializedObject.FindProperty("maximumFOV").floatValue, 0f, 100f);
                        GUILayout.Space(3f);
                        serializedObject.FindProperty("zoomSensitivity").floatValue = EditorGUILayout.Slider("Zoom Sensitivity", serializedObject.FindProperty("zoomSensitivity").floatValue, 0f, 20f);
                    }

                    if (camera.attachment != PhantomCamera.CameraAttachment.None && camera.attachment != PhantomCamera.CameraAttachment.ExteriorOnly)
                    {
                        GUILayout.Space(10f);
                        GUI.color = Color.white;
                        EditorGUILayout.HelpBox("Interior Attachment", MessageType.None);
                        GUI.color = backgroundColor;
                        GUILayout.Space(3f);
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("interiorObject"), new GUIContent(" "));
                    }
                    break;
            }
        }

        if (camera.cameraType == PhantomCamera.CameraType.Player)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Player Camera Settings", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("normalExterior"), new GUIContent("Camera"));
            GUILayout.Space(7f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("focusPoint"), new GUIContent("Player"));
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Camera Positioning", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("orbitDistance"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("orbitHeight"));
        }
        serializedObject.ApplyModifiedProperties();
    }

}
#endif
#endregion


// --------------------------------------------- Rotor
#region Rotor
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomRotor))]
public class PhantomRotorEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomRotor rotor;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { rotor = (PhantomRotor)target; }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Rotor Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorConfiguration"), new GUIContent("Rotor Type"));
        GUILayout.Space(5f);

        // ------------------------------------------ Conventional
        if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Conventional)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorType"), new GUIContent(" "));
            GUILayout.Space(3f);
            if (rotor.rotorType == PhantomRotor.RotorType.TailRotor)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("tailRotorType"), new GUIContent(" "));
            }
        }

        // ------------------------------------------ Coaxial
        if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Coaxial)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorPosition"), new GUIContent(" "));
        }

        // ------------------------------------------ Tandem
        if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Tandem)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tandemPosition"), new GUIContent(" "));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tandemAnalysis"), new GUIContent("Analysis Method"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("kov"), new GUIContent("kov"));
        }

        // ------------------------------------------ Intermeshing
        if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Syncrocopter)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("syncroPosition"), new GUIContent(" "));
        }


        // ------------------------------------------ Propeller
        if(rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Propeller)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("propellerMode"), new GUIContent(" "));
        }


        if (rotor.rotorType != PhantomRotor.RotorType.TailRotor && rotor.propellerMode != PhantomRotor.PropellerMode.Horizontal)
        {
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------
            GUILayout.Space(10f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Ground Effect Component", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("effectState"), new GUIContent(" "));
            if (rotor.effectState == PhantomRotor.GroundEffectState.Consider)
            {
                GUILayout.Space(5f);
                SerializedProperty layerMask = serializedObject.FindProperty("groundLayer");
                EditorGUILayout.PropertyField(layerMask);
                GUILayout.Space(5f);
                EditorGUILayout.LabelField("Lift Percentage", (rotor.δT * 100f).ToString("0.00") + " %");
            }
        }


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorRadius"), new GUIContent("Rotor Radius (m)"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorHeadRadius"), new GUIContent("Rotor Head (m)"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Nb"), new GUIContent("Blade Count"));

        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Blade Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flappingState"), new GUIContent("Flapping State"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twistType"), new GUIContent("Twist"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("surfaceFinish"), new GUIContent("Surface Finish"));


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Weight", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weightUnit"), new GUIContent("Unit"));
        if (rotor.weightUnit == PhantomRotor.WeightUnit.Pounds)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bladeMass"), new GUIContent("Mass (lbs)"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Blade Mass", (rotor.actualWeight).ToString("0.00") + " kg");
        }
        else
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bladeMass"), new GUIContent("Mass (kg)"));
        }


        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        if (rotor.twistType != PhantomRotor.TwistType.Constant)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bladeWashout"), new GUIContent("Washout (°)"));
        }
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bladeChord"), new GUIContent("Blade Chord"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("re"), new GUIContent("Hinge Ratio"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rootCutOut"), new GUIContent("Root Cutout %"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rootDeviation"), new GUIContent("Root Deviation"));


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Blade Data", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Blade Radius", (rotor.bladeRadius).ToString("0.000") + " m");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Root Cut", (rotor.rootcut).ToString("0.000") + " m");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Hinge Offset", (rotor.hingeOffset).ToString("0.000") + " m");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Aspect Ratio", (rotor.aspectRatio).ToString("0.000"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Rotational Inertia", (rotor.J).ToString("0.00") + " kg/m2");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Flapping Inertia", (rotor.Iβ).ToString("0.00") + " kg/m2");


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Airfoil Component", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rootAirfoil"), new GUIContent("Root Airfoil"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tipAirfoil"), new GUIContent("Tip Airfoil"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("drawFoils"), new GUIContent("Draw Foil"));


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Control Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Collective", MessageType.None);
        GUI.color = backgroundColor;

        if (rotor.rotorType == PhantomRotor.RotorType.TailRotor)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumCollective"), new GUIContent("Max Collective (L)"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumCollective"), new GUIContent("Min Collective (R)"));
        }
        if (rotor.rotorType == PhantomRotor.RotorType.MainRotor)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumCollective"), new GUIContent("Maximum Collective"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumCollective"), new GUIContent("Minimum Collective"));

            if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Coaxial || rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Syncrocopter)
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumYawCollective"), new GUIContent("Maximum Yaw Collective"));
            }
            if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Tandem)
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitchCollective"), new GUIContent("Pitch Collective"));
            }
        }



        if (rotor.rotorType != PhantomRotor.RotorType.TailRotor && rotor.rotorConfiguration != PhantomRotor.RotorConfiguration.Propeller)
        {
            if (rotor.rotorConfiguration == PhantomRotor.RotorConfiguration.Tandem)
            {
                GUILayout.Space(3f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Cyclic", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRollCyclic"), new GUIContent("Maximum Roll Cyclic (R)"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumRollCyclic"), new GUIContent("Minimum Roll Cyclic (L)"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumYawCyclic"), new GUIContent("Maximum Yaw Cyclic"));

            }
            else
            {
                GUILayout.Space(3f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Cyclic", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitchCyclic"), new GUIContent("Maximum Pitch Cyclic (F)"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumPitchCyclic"), new GUIContent("Minimum Pitch Cyclic (B)"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRollCyclic"), new GUIContent("Maximum Roll Cyclic (R)"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumRollCyclic"), new GUIContent("Minimum Roll Cyclic (L)"));

            }
        }


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Dynamic Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("funcionalRPM"), new GUIContent("Functional RPM"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorDirection"), new GUIContent("Rotation Direction"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Core RPM", rotor.coreRPM.ToString("0.0") + " RPM");


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Audio Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundState"), new GUIContent("State"));
        if (rotor.soundState == PhantomRotor.SoundState.Active)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bladeChop"), new GUIContent("Rotor Sound"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitch"), new GUIContent("Maximum Pitch"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interiorVolume"), new GUIContent("Interior Volume"));
        }



        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Visuals Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("visualType"), new GUIContent(" "));
        if (rotor.visualType == PhantomRotor.VisulType.Complete)
        {
            GUILayout.Space(2f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Blurred Rotor Materials", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            SerializedProperty bmaterials = serializedObject.FindProperty("blurredRotor");
            GUIContent barrelLabel = new GUIContent("Material Count");
            EditorGUILayout.PropertyField(bmaterials.FindPropertyRelative("Array.size"), barrelLabel);
            GUILayout.Space(3f);
            for (int i = 0; i < bmaterials.arraySize; i++)
            {
                GUIContent label = new GUIContent("Material " + (i + 1).ToString());
                EditorGUILayout.PropertyField(bmaterials.GetArrayElementAtIndex(i), label);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Normal Rotor Materials", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            SerializedProperty nmaterials = serializedObject.FindProperty("normalRotor");
            GUIContent nbarrelLabel = new GUIContent("Material Count");
            EditorGUILayout.PropertyField(nmaterials.FindPropertyRelative("Array.size"), nbarrelLabel);
            GUILayout.Space(3f);
            for (int i = 0; i < nmaterials.arraySize; i++)
            {
                GUIContent label = new GUIContent("Material " + (i + 1).ToString());
                EditorGUILayout.PropertyField(nmaterials.GetArrayElementAtIndex(i), label);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        if (rotor.visualType == PhantomRotor.VisulType.Partial)
        {
            GUILayout.Space(2f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Blurred Rotor Materials", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            SerializedProperty bmaterials = serializedObject.FindProperty("blurredRotor");
            GUIContent barrelLabel = new GUIContent("Material Count");
            EditorGUILayout.PropertyField(bmaterials.FindPropertyRelative("Array.size"), barrelLabel);
            GUILayout.Space(3f);
            for (int i = 0; i < bmaterials.arraySize; i++)
            {
                GUIContent label = new GUIContent("Material " + (i + 1).ToString());
                EditorGUILayout.PropertyField(bmaterials.GetArrayElementAtIndex(i), label);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Output", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("ϴ0", (rotor.ϴ0 * Mathf.Rad2Deg).ToString("0.00") + " °");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("αR", (rotor.αR.R).ToString("0.00") + " °");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("αT", (rotor.αT.R).ToString("0.00") + " °");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("υM", (rotor.υM).ToString("0.000") + " M");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("CT", (rotor.CT.R).ToString("0.00000")); 
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("CQ", (rotor.CQ.R).ToString("0.00000"));
        //GUILayout.Space(3f);
        //EditorGUILayout.LabelField("CH", (rotor.CH).ToString("0.00000"));
        //GUILayout.Space(3f);
        //EditorGUILayout.LabelField("CY", (rotor.CY).ToString("0.00000"));

        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Forces", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);

        EditorGUILayout.LabelField("Thrust", rotor.Thrust.ToString("0.00") + " N");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Torque", rotor.Torque.ToString("0.00") + " Nm");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Inertia", rotor.Inertia.ToString("0.00") + " kg/m2");
        if (rotor.rotorType == PhantomRotor.RotorType.MainRotor && rotor.rotorConfiguration != PhantomRotor.RotorConfiguration.Propeller)
        {
            if (rotor.rotorConfiguration != PhantomRotor.RotorConfiguration.Tandem)
            {
                GUILayout.Space(3f);
                EditorGUILayout.LabelField("Pitch Moment", rotor.TorqueForce.x.ToString("0.00") + " Nm");
            }
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Roll Moment", rotor.TorqueForce.z.ToString("0.00") + " Nm");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Aerofoil
#region Aerofoil
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomAerofoil))]
public class PhantomAerofoilEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomAerofoil foil;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { foil = (PhantomAerofoil)target; }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();
        GUI.color = silantroColor;


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Foil Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("aerofoilType"), new GUIContent("Type"));

        if (foil.aerofoilType == PhantomAerofoil.AerofoilType.Fin)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("finType"), new GUIContent(" "));
        }
        if (foil.aerofoilType == PhantomAerofoil.AerofoilType.Stabilator)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stabType"), new GUIContent(" "));
        }
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("surfaceFinish"), new GUIContent("Surface Finish"));


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Airfoil Component", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rootAirfoil"), new GUIContent("Root Airfoil"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tipAirfoil"), new GUIContent("Tip Airfoil"));


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Foil Dimensions", MessageType.None); GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Sweep", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sweepDirection"), new GUIContent("Sweep Direction"));
        if (foil.sweepDirection != PhantomAerofoil.SweepDirection.Unswept)
        {
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("aerofoilSweepAngle"), new GUIContent("Sweep Angle"));
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("ɅLE", foil.leadingEdgeSweep.ToString("0.00") + " °");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Correction Factor", foil.sweepCorrectionFactor.ToString("0.000"));
        }


        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Structure", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taperPercentage"), new GUIContent("Taper Percentage"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("foilSubdivisions"), new GUIContent("Panel Subdivisions"));



        GUILayout.Space(15f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(1f);
        EditorGUILayout.LabelField("Root Chord", foil.foilRootChord.ToString("0.00") + " m");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Tip Chord", foil.foilTipChord.ToString("0.00") + " m");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Aspect Ratio", foil.aspectRatio.ToString("0.000"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Span Efficiency", foil.spanEfficiency.ToString("0.00") + " %");


        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Draw Options", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("drawFoils"), new GUIContent("Draw Foils"));


        if (foil.aerofoilType == PhantomAerofoil.AerofoilType.Stabilator && foil.stabType != PhantomAerofoil.StabilatorType.Plain)
        {
            GUILayout.Space(15f);
            GUI.color = foil.controlColor;
            EditorGUILayout.HelpBox("Stabilator Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("positiveLimit"), new GUIContent("Positive Limit"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("negativeLimit"), new GUIContent("Negative Limit"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actuationSpeed"), new GUIContent("Actuation Speed (°/s)"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Deflection", foil.controlDeflection.ToString("0.00") + " °");

            if (foil.stabType == PhantomAerofoil.StabilatorType.Elevon)
            {
                GUILayout.Space(20f);
                GUI.color = foil.controlColor;
                EditorGUILayout.HelpBox("Stabilator Chord Ratios (xc/c)", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(4f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("controlRootChord"), new GUIContent("Root Chord"));
                GUILayout.Space(5f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("controlTipChord"), new GUIContent("Tip Chord"));


                GUILayout.Space(10f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Stabilator Panels", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(4f);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                SerializedProperty boolsControl = serializedObject.FindProperty("controlSections");
                for (int ci = 0; ci < boolsControl.arraySize; ci++)
                {
                    GUIContent labelControl = new GUIContent();
                    if (ci == 0)
                    {
                        labelControl = new GUIContent("Root Panel: ");
                    }
                    else if (ci == boolsControl.arraySize - 1)
                    {
                        labelControl = new GUIContent("Tip Panel: ");
                    }
                    else
                    {
                        labelControl = new GUIContent("Panel: " + (ci + 1).ToString());
                    }
                    EditorGUILayout.PropertyField(boolsControl.GetArrayElementAtIndex(ci), labelControl);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

            }

            GUILayout.Space(10f);
            GUI.color = foil.controlColor;
            EditorGUILayout.HelpBox("Model Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(5f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlSurfaceModel"), new GUIContent(""));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deflectionAxis"), new GUIContent(""));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deflectionDirection"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();
        }

        if (foil.aerofoilType == PhantomAerofoil.AerofoilType.Fin && foil.finType == PhantomAerofoil.FinType.Rudder)
        {
            GUILayout.Space(15f);
            GUI.color = foil.controlColor;
            EditorGUILayout.HelpBox("Rudder Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("positiveLimit"), new GUIContent("Positive Limit"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("negativeLimit"), new GUIContent("Negative Limit"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actuationSpeed"), new GUIContent("Actuation Speed (°/s)"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Deflection", foil.controlDeflection.ToString("0.00") + " °");


            GUILayout.Space(20f);
            GUI.color = foil.controlColor;
            EditorGUILayout.HelpBox("Rudder Chord Ratios (xc/c)", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(4f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlRootChord"), new GUIContent("Root Chord"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlTipChord"), new GUIContent("Tip Chord"));


            GUILayout.Space(10f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Rudder Panels", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(4f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            SerializedProperty boolsControl = serializedObject.FindProperty("controlSections");
            for (int ci = 0; ci < boolsControl.arraySize; ci++)
            {
                GUIContent labelControl = new GUIContent();
                if (ci == 0)
                {
                    labelControl = new GUIContent("Root Panel: ");
                }
                else if (ci == boolsControl.arraySize - 1)
                {
                    labelControl = new GUIContent("Tip Panel: ");
                }
                else
                {
                    labelControl = new GUIContent("Panel: " + (ci + 1).ToString());
                }
                EditorGUILayout.PropertyField(boolsControl.GetArrayElementAtIndex(ci), labelControl);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();


            GUILayout.Space(10f);
            GUI.color = foil.controlColor;
            EditorGUILayout.HelpBox("Model Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(5f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlSurfaceModel"), new GUIContent(""));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deflectionAxis"), new GUIContent(""));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deflectionDirection"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Output Data", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Lift", foil.TotalLift.ToString("0.0") + " N");
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Drag", foil.TotalDrag.ToString("0.0") + " N");

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Piston
#region Piston
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomPistonEngine))]
public class PhantomPistonEngineEditor : Editor
{

    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomPistonEngine piston;
    SerializedProperty core;
    public int toolbarTab;
    public string currentTab;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { piston = (PhantomPistonEngine)target; core = serializedObject.FindProperty("core"); }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();

        GUILayout.Space(2f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Identifier", MessageType.None);
        GUI.color = backgroundColor;
        EditorGUILayout.PropertyField(core.FindPropertyRelative("engineIdentifier"), new GUIContent(" "));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("enginePosition"), new GUIContent("Position"));


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Properties", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stroke"), new GUIContent("Stroke (In)"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bore"), new GUIContent("Bore (In)"));
        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Displacement", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("displacement"), new GUIContent(" "));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("displacementUnit"), new GUIContent(" "));



        GUILayout.Space(10f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("compressionRatio"), new GUIContent("Compression Ratio"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfCylinders"), new GUIContent("No of Cylinders"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("functionalRPM"), new GUIContent("Functional RPM"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("baseCoreAcceleration"), new GUIContent("Core Acceleration"));

        GUILayout.Space(10f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exitPoint"), new GUIContent("Exhaust Point"));


        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Thermodynamic Properties", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Exhaust Gas Residual (%)", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("residue"), new GUIContent(" "));
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Mechanical Efficiency", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nm"), new GUIContent("Mechanical Efficiency"));
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Air-Fuel Ratio", piston.AF.ToString("0.00"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("PSFC ", piston.PSFC.ToString("0.00") + " lb/hp.hr");

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Thermodynamic Performance", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("showPerformance"), new GUIContent("Show"));
        if (piston.showPerformance)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Module Performance", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Expansion Work", (piston.W3_4).ToString("0.00") + " kJ");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Compression Work", (piston.W1_2).ToString("0.00") + " kJ");

            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Flows Rates", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Intake Air", piston.ma.ToString("0.00") + " kg/s");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Fuel", piston.Mf.ToString("0.00") + " kg/s");
        }


        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("soundMode"), new GUIContent("Mode"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorMode"), new GUIContent("Cabin Sounds"));
        GUILayout.Space(5f);
        if (piston.core.soundMode == PhantomEngineCore.SoundMode.Basic)
        {
            if (piston.core.interiorMode == PhantomEngineCore.InteriorMode.Off)
            {
                EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Ignition Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Shutdown Sound"));
            }
            else
            {
                toolbarTab = GUILayout.Toolbar(toolbarTab, new string[] { "Exterior Sounds", "Interior Sounds" });
                GUILayout.Space(5f);
                switch (toolbarTab)
                {
                    case 0: currentTab = "Exterior Sounds"; break;
                    case 1: currentTab = "Interior Sounds"; break;
                }
                switch (currentTab)
                {
                    case "Exterior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Ignition Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Shutdown Sound"));
                        break;

                    case "Interior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionInterior"), new GUIContent("Interior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorIdle"), new GUIContent("Interior Idle"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownInterior"), new GUIContent("Interior Shutdown"));
                        break;
                }
            }
        }
        else
        {
            GUILayout.Space(3f);
            if (piston.core.interiorMode == PhantomEngineCore.InteriorMode.Off)
            {
                EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Exterior Ignition"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("frontIdle"), new GUIContent("Front Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("sideIdle"), new GUIContent("Side Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Rear Idle Sound"));
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Exterior Shutdown"));
            }
            else
            {
                toolbarTab = GUILayout.Toolbar(toolbarTab, new string[] { "Exterior Sounds", "Interior Sounds" });
                GUILayout.Space(5f);
                switch (toolbarTab)
                {
                    case 0: currentTab = "Exterior Sounds"; break;
                    case 1: currentTab = "Interior Sounds"; break;
                }
                switch (currentTab)
                {
                    case "Exterior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionExterior"), new GUIContent("Exterior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("frontIdle"), new GUIContent("Front Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("sideIdle"), new GUIContent("Side Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("backIdle"), new GUIContent("Rear Idle Sound"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownExterior"), new GUIContent("Exterior Shutdown"));
                        break;

                    case "Interior Sounds":
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("ignitionInterior"), new GUIContent("Interior Ignition"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("interiorIdle"), new GUIContent("Interior Idle"));
                        GUILayout.Space(2f);
                        EditorGUILayout.PropertyField(core.FindPropertyRelative("shutdownInterior"), new GUIContent("Interior Shutdown"));
                        break;
                }
            }
        }



        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Effects Configuration", MessageType.None);
        GUI.color = backgroundColor;
        EditorGUILayout.PropertyField(core.FindPropertyRelative("exhaustSmoke"), new GUIContent("Exhaust Smoke"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("smokeEmissionLimit"), new GUIContent("Maximum Emission"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("exhaustDistortion"), new GUIContent("Exhaust Distortion"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(core.FindPropertyRelative("distortionEmissionLimit"), new GUIContent("Maximum Emission"));



        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Engine Output", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Core Power", (piston.core.corePower * piston.core.coreFactor * 100f).ToString("0.00") + " %");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Brake Power", piston.brakePower.ToString("0.0") + " Hp");


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Gear
#region Wheels
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomGearSystem))]
public class PhantomGearSystemEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomGearSystem structBase;

    int listSize;
    SerializedProperty wheelList;

    private static GUIContent deleteButton = new GUIContent("Remove", "Delete");
    private static GUILayoutOption buttonWidth = GUILayout.Width(60f);




    //------------------------------------------------------------------------
    private SerializedProperty showWheelCommand;
    private SerializedProperty steeringAxle;
    private SerializedProperty rotationAxis;
    private SerializedProperty invertAxleRotation;
    private SerializedProperty maximumSteerAngle;

    private SerializedProperty brakeTorque;
    private SerializedProperty groundRollExterior;
    private SerializedProperty brakeEngage;
    private SerializedProperty brakeRelease;
    private SerializedProperty maximumRumbleVolume;



    //------------------------------------------------------------------------
    void OnEnable()
    {
        structBase = (PhantomGearSystem)target;
        wheelList = serializedObject.FindProperty("wheelSystem");


        showWheelCommand = serializedObject.FindProperty("showWheels");
        steeringAxle = serializedObject.FindProperty("steeringAxle");
        rotationAxis = serializedObject.FindProperty("rotationAxis");
        maximumSteerAngle = serializedObject.FindProperty("maximumSteerAngle");
        invertAxleRotation = serializedObject.FindProperty("invertAxleRotation");
        brakeTorque = serializedObject.FindProperty("brakeTorque");
        groundRollExterior = serializedObject.FindProperty("groundRoll");
        brakeEngage = serializedObject.FindProperty("brakeEngage");
        brakeRelease = serializedObject.FindProperty("brakeRelease");
        maximumRumbleVolume = serializedObject.FindProperty("maximumRumbleVolume");
    }


    //-------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Wheel Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(5f);

        if (wheelList != null) { EditorGUILayout.LabelField("Wheel Count", wheelList.arraySize.ToString()); }
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(showWheelCommand);

        if (structBase.showWheels)
        {
            GUILayout.Space(5f);
            if (GUILayout.Button("Create Wheel")) { structBase.wheelSystem.Add(new PhantomGearSystem.WheelSystem()); }

            //--------------------------------------------WHEEL ELEMENTS
            if (wheelList != null)
            {
                GUILayout.Space(2f);
                //DISPLAY WHEEL ELEMENTS
                for (int i = 0; i < wheelList.arraySize; i++)
                {
                    SerializedProperty reference = wheelList.GetArrayElementAtIndex(i);
                    SerializedProperty Identifier = reference.FindPropertyRelative("Identifier");
                    SerializedProperty collider = reference.FindPropertyRelative("collider");
                    SerializedProperty wheelModel = reference.FindPropertyRelative("wheelModel");
                    SerializedProperty steerable = reference.FindPropertyRelative("steerable");
                    SerializedProperty rotationAxis = reference.FindPropertyRelative("rotationWheelAxis");

                    GUI.color = new Color(1, 0.8f, 0);
                    EditorGUILayout.HelpBox("Wheel : " + (i + 1).ToString(), MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(Identifier);
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(collider);
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(wheelModel);
                    GUILayout.Space(3f);
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Operational Properties", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(rotationAxis);
                    GUILayout.Space(3f);
                    EditorGUILayout.PropertyField(steerable);
                    GUILayout.Space(3f);
                    EditorGUILayout.LabelField("RPM", structBase.wheelSystem[i].wheelRPM.ToString("0") + " RPM");

                    GUILayout.Space(3f);
                    if (GUILayout.Button(deleteButton, EditorStyles.miniButtonRight, buttonWidth))
                    {
                        structBase.wheelSystem.RemoveAt(i);
                    }
                    GUILayout.Space(5f);
                }
            }
        }



        //-------------------------------------------------------------STEERING
        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Steering Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(steeringAxle);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(rotationAxis);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(invertAxleRotation);




        //------------------------LIMITS
        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Steering Limits", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(maximumSteerAngle);
        GUILayout.Space(3f);
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Steer Angle", structBase.currentSteerAngle.ToString("0.0"));

        //-------------------------------------------------------------BRAKING
        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Brake Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Engaged", structBase.brakeState.ToString());
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(brakeTorque);


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(groundRollExterior);
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(brakeEngage);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(brakeRelease);
        GUILayout.Space(3f);
        maximumRumbleVolume.floatValue = EditorGUILayout.Slider("Volume Limit", maximumRumbleVolume.floatValue, 0f, 1f);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Crew
#region Crew
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomCrew))]
public class PhantomCrewEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1f, 0.4f, 0);


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector(); 
        serializedObject.Update();

        PhantomCrew crew = (PhantomCrew)target;
        GUILayout.Space(3f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Crew Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("position"), new GUIContent("Position"));
        if (crew.position == PhantomCrew.Position.Internal)
        {
            GUILayout.Space(3f);
            crew.designation = (PhantomCrew.Designation)EditorGUILayout.EnumPopup("Designation", crew.designation);
            GUILayout.Space(5f);
            crew.weight = EditorGUILayout.FloatField("Weight", crew.weight);
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("body"), new GUIContent("Body"));
        }
        if (crew.position == PhantomCrew.Position.External)
        {
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Player Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlType"), new GUIContent("Player Type"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxRayDistance"), new GUIContent("Sight Distance"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("head"), new GUIContent("Pilot Head"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// --------------------------------------------- Transponder
#region Transponder
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomTransponder))]
public class PhantomTransponderEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    private SerializedProperty text;


    private void OnEnable()
    {
        text = serializedObject.FindProperty("silantroTexture");
    }


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        PhantomTransponder mark = (PhantomTransponder)target;

        serializedObject.Update();
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Definition", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("silantroTag"), new GUIContent(" "));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("radarSignature"), new GUIContent("RCS"));
        GUILayout.Space(3f);
        text.objectReferenceValue = EditorGUILayout.ObjectField("Radar Texture", text.objectReferenceValue, typeof(Texture2D), true) as Texture2D;

        GUILayout.Space(5f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sensor Data", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Is Tracked", mark.isTracked.ToString());
        if (mark.isTracked)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Target ID", mark.AssignedID.ToString());
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Tracker ID", mark.TrackingID.ToString());
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Is Locked", mark.isLockedOn.ToString());
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------- Lever
#region Lever
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomLever))]
public class PhantomLeverEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomLever lever;


    //------------------------------------------------------------------------
    private SerializedProperty leverType;
    private SerializedProperty stickType;
    private SerializedProperty leverObj;
    private SerializedProperty yokeObj;

    private SerializedProperty pitchRotationAxis;
    private SerializedProperty MaximumPitchDeflection;
    private SerializedProperty pitchAxis;

    private SerializedProperty rollRotationAxis;
    private SerializedProperty MaximumRollDeflection;
    private SerializedProperty rollAxis;



    private void OnEnable()
    {
        lever = (PhantomLever)target;

        leverType = serializedObject.FindProperty("leverType");
        stickType = serializedObject.FindProperty("stickType");
        leverObj = serializedObject.FindProperty("lever");
        yokeObj = serializedObject.FindProperty("yoke");

        pitchRotationAxis = serializedObject.FindProperty("pitchRotationAxis");
        MaximumPitchDeflection = serializedObject.FindProperty("MaximumPitchDeflection");
        pitchAxis = serializedObject.FindProperty("pitchDirection");

        rollRotationAxis = serializedObject.FindProperty("rollRotationAxis");
        MaximumRollDeflection = serializedObject.FindProperty("MaximumRollDeflection");
        rollAxis = serializedObject.FindProperty("rollDirection");
    }


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUILayout.Space(2f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Lever Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(leverType, new GUIContent(" "));


        // -------------------------------------------------------- Control Stick
        if (lever.leverType == PhantomLever.LeverType.Stick)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(stickType, new GUIContent("Control Type"));
            GUILayout.Space(5f);
            if (lever.stickType == PhantomLever.StickType.Joystick)
            {
                GUILayout.Space(5f);
                EditorGUILayout.PropertyField(leverObj, new GUIContent("Stick"));
            }
            else
            {
                GUILayout.Space(5f);
                EditorGUILayout.PropertyField(leverObj, new GUIContent("Yoke Stick"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(yokeObj, new GUIContent("Yoke Wheel"));
            }

            GUILayout.Space(10f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(pitchRotationAxis, new GUIContent("Pitch Rotation Axis"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(MaximumPitchDeflection, new GUIContent("Max Pitch Deflection"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(pitchAxis, new GUIContent("Pitch Direction"));

            GUILayout.Space(8f);
            EditorGUILayout.PropertyField(rollRotationAxis, new GUIContent("Roll Rotation Axis"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(MaximumRollDeflection, new GUIContent("Max Roll Deflection"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(rollAxis, new GUIContent("Roll Direction"));
        }



        // -------------------------------------------------------- Collective
        if (lever.leverType == PhantomLever.LeverType.Collective)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(leverObj, new GUIContent("Collective Lever"));
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("direction"), new GUIContent("Rotation Direction"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumDeflection"), new GUIContent("Maximum Deflection"));
        }




        // -------------------------------------------------------- Throttle
        if (lever.leverType == PhantomLever.LeverType.Throttle)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lever"), new GUIContent("Throttle Handle"));
            GUILayout.Space(2f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
            GUI.color = backgroundColor;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("direction"), new GUIContent("Rotation Direction"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumDeflection"), new GUIContent("Maximum Deflection"));
        }




        // -------------------------------------------------------- Rudder Pedals
        if (lever.leverType == PhantomLever.LeverType.Pedal)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pedalType"), new GUIContent("Pedal Type"));

            if (lever.pedalType == PhantomLever.PedalType.Hinged)
            {
                GUILayout.Space(10f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Right Pedal Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightPedal"), new GUIContent("Right Pedal"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightRotationAxis"), new GUIContent("Rotation Axis"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightDirection"), new GUIContent("Rotation Deflection"));


                GUILayout.Space(5f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Left Pedal Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftPedal"), new GUIContent("Left Pedal"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftRotationAxis"), new GUIContent("Rotation Axis"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftDirection"), new GUIContent("Rotation Deflection"));

                GUILayout.Space(10f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pedalMode"), new GUIContent("Clamped Together"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumDeflection"), new GUIContent("Maximum Deflection"));
            }

            if (lever.pedalType == PhantomLever.PedalType.Sliding)
            {
                GUILayout.Space(10f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Right Pedal Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightPedal"), new GUIContent("Right Pedal"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightRotationAxis"), new GUIContent("Sliding Axis"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightDirection"), new GUIContent("Sliding Deflection"));


                GUILayout.Space(5f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Left Pedal Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftPedal"), new GUIContent("Left Pedal"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftRotationAxis"), new GUIContent("Sliding Axis"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftDirection"), new GUIContent("Sliding Deflection"));


                GUILayout.Space(10f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Sliding Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumSlidingDistance"), new GUIContent("Sliding Distance (cm)"));
            }
        }



        if (lever.leverType == PhantomLever.LeverType.GearIndicator)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(leverObj, new GUIContent("Lever Handle"));
            GUILayout.Space(10f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
            GUI.color = backgroundColor;

            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("direction"), new GUIContent("Rotation Direction"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumDeflection"), new GUIContent("Maximum Deflection"));


            GUILayout.Space(10f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Bulb Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulbMaterial"), new GUIContent("Bulb Material"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumBulbEmission"), new GUIContent("Maximum Emission"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------- Actuator
#region Actuator
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomActuator))]
public class PhantomActuatorEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomActuator actuator;


    //------------------------------------------------------------------------
    private SerializedProperty animator;
    private SerializedProperty animationLayer;
    private SerializedProperty animationName;
    private SerializedProperty actuationSpeed;
    private SerializedProperty invertMotion;
    private SerializedProperty dragFactor;
    private SerializedProperty generatesDrag;
    private SerializedProperty invertDrag;
    private SerializedProperty soundType;


    private SerializedProperty engageClip;
    private SerializedProperty disengageClip;
    private SerializedProperty engageLoopClip;
    private SerializedProperty engageEndClip;
    private SerializedProperty type;
    private SerializedProperty Mode;

    void OnEnable()
    {
        actuator = (PhantomActuator)target;

        animator = serializedObject.FindProperty("actuatorAnimator");
        animationLayer = serializedObject.FindProperty("animationLayer");
        animationName = serializedObject.FindProperty("animationName");
        actuationSpeed = serializedObject.FindProperty("actuationSpeed");
        invertMotion = serializedObject.FindProperty("invertMotion");
        dragFactor = serializedObject.FindProperty("dragFactor");
        generatesDrag = serializedObject.FindProperty("generatesDrag");
        invertDrag = serializedObject.FindProperty("invertDrag");
        soundType = serializedObject.FindProperty("soundType");

        engageClip = serializedObject.FindProperty("EngageClip");
        disengageClip = serializedObject.FindProperty("disengageClip");
        engageLoopClip = serializedObject.FindProperty("EngageLoopClip");
        engageEndClip = serializedObject.FindProperty("EngageEndClip");
        type = serializedObject.FindProperty("actuatorType");
        Mode = serializedObject.FindProperty("actuatorMode");
    }

    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();

        GUI.color = Color.white;
        EditorGUILayout.HelpBox("State", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        if (actuator.evaluate) { if (GUILayout.Button("Finish Evaluation")) { actuator.evaluate = false; } silantroColor = Color.red; }
        if (!actuator.evaluate) { if (GUILayout.Button("Evaluate")) { actuator.evaluate = true; } silantroColor = new Color(1, 0.4f, 0); }
        if (actuator.evaluate)
        {
            GUILayout.Space(5f);
            if (GUILayout.Button("Engage")) { actuator.EngageActuator(); }
            GUILayout.Space(2f);
            if (GUILayout.Button("DisEngage")) { actuator.DisengageActuator(); }
        }

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(type);

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Animation Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(animator);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(animationLayer);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(animationName);

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Actuation Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(Mode);
        GUILayout.Space(3f);
        actuationSpeed.floatValue = EditorGUILayout.Slider("Actuation Speed", actuationSpeed.floatValue, 0f, 1f);


        if (actuator.actuatorType == PhantomActuator.ActuatorType.Custom || actuator.actuatorType == PhantomActuator.ActuatorType.GunCover)
        {
            GUILayout.Space(3f);
            serializedObject.FindProperty("multiplier").floatValue = EditorGUILayout.Slider("Multiplier", serializedObject.FindProperty("multiplier").floatValue, 0f, 10f);
        }

        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Actuation level", (actuator.currentActuationLevel * 100f).ToString("0.00") + " %");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Actuation State", actuator.actuatorState.ToString());
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(invertMotion);

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(soundType);
        GUILayout.Space(5f);
        if (actuator.soundType == PhantomActuator.SoundType.Simple)
        {
            EditorGUILayout.PropertyField(engageClip);
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(disengageClip);
        }
        if (actuator.soundType == PhantomActuator.SoundType.Complex)
        {
            EditorGUILayout.PropertyField(engageLoopClip);
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(engageEndClip);
        }

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Drag Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(generatesDrag);
        if (actuator.generatesDrag)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(dragFactor);
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Factor", actuator.currentDragFactor.ToString("0.000"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(invertDrag);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------- Readout
#region Readout
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomReadout))]
public class PhantomReadoutEditor : Editor
{
    Color backgroundColor; Color silantroColor = new Color(1, 0.4f, 0);
    PhantomReadout dial;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnEnable() { dial = (PhantomReadout)target; }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();

        GUI.color = silantroColor; EditorGUILayout.HelpBox("Connected Aircraft", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controller"), new GUIContent(" "));
        GUILayout.Space(3f); GUI.color = silantroColor; EditorGUILayout.HelpBox("Dial Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialType"), new GUIContent(" "));
        GUILayout.Space(5f);
        if (dial.dialType == PhantomReadout.DialType.Altitude)
        {
            GUILayout.Space(3f); GUI.color = Color.white;
            EditorGUILayout.HelpBox("Altimeter Type", MessageType.None);
            GUI.color = backgroundColor; GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("altimeterType"), new GUIContent(" "));
        }

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Dial 'Per Digit' Translation", MessageType.None); GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitOneTranslation"), new GUIContent("Digit One"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitTwoTranslation"), new GUIContent("Digit Two"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitThreeTranslation"), new GUIContent("Digit Three"));

        if (dial.dialType == PhantomReadout.DialType.Altitude && dial.altimeterType == PhantomReadout.AltimeterType.FourDigit)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("digitFourTranslation"), new GUIContent("Digit Four"));
        }


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Digit Containers", MessageType.None); GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitOneContainer"), new GUIContent("Digit One Container"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitTwoContainer"), new GUIContent("Digit Two Container"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("digitThreeContainer"), new GUIContent("Digit Three Container"));

        if (dial.dialType == PhantomReadout.DialType.Altitude && dial.altimeterType == PhantomReadout.AltimeterType.FourDigit)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("digitFourContainer"), new GUIContent("Digit Four Container"));
        }

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Digit Display", MessageType.None); GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.LabelField("Current Value", dial.digitFour.ToString("0") + "| " + dial.digitThree.ToString("0") + "| " + dial.digitTwo.ToString("0") + "| " + dial.digitOne.ToString("0"));


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Dial Face Settings", MessageType.None); GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("needle"), new GUIContent("Needle"));

        if (dial.dialType == PhantomReadout.DialType.Airspeed)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"), new GUIContent("Maximum Speed"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------- Instrument
#region Instrument
#if UNITY_EDITOR
// ---------------------------------------------------- Instrument
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomInstrument))]
public class PhantomInstrumentEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomInstrument dial;

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { dial = (PhantomInstrument)target; }


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Connection", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controller"), new GUIContent(" "));

        GUILayout.Space(5f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Display Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("displayType"), new GUIContent(" "));
        GUILayout.Space(3f);

        // -------------------------------- Altimeterr
        if (dial.displayType == PhantomInstrument.DisplayType.Speedometer)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Speedometer Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speedType"), new GUIContent(" "));
        }

        // -------------------------------- Fuel
        if (dial.displayType == PhantomInstrument.DisplayType.FuelQuantity)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Fuel Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fuelType"), new GUIContent(" "));
        }


        // ------------------------------- RPM
        if (dial.displayType == PhantomInstrument.DisplayType.Tachometer)
        {
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Engine Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent(" "));
            GUILayout.Space(5f);

            if (dial.engineType == PhantomInstrument.EngineType.ElectricMotor)
            {
                //EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent(" "));
            }
            if (dial.engineType == PhantomInstrument.EngineType.TurboShaft)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaft"), new GUIContent(" "));
            }
            if (dial.engineType == PhantomInstrument.EngineType.PistonEngine)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("piston"), new GUIContent(" "));
            }
        }



        if (dial.displayType == PhantomInstrument.DisplayType.Horizon)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Roll Rotation Amounts", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumRoll"), new GUIContent("Minimum Roll"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRoll"), new GUIContent("Maximum Roll"));


            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Pitch Deflection Amounts", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumPitch"), new GUIContent("Minimum Pitch"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitch"), new GUIContent("Maximum Pitch"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("movementFactor"), new GUIContent("Movement Factor"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connections", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchTape"), new GUIContent("Pitch Tape"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rollAnchor"), new GUIContent("Roll Anchor"));
        }
        else if (dial.displayType == PhantomInstrument.DisplayType.Temperature)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Movement Amounts", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumTemperaturePosition"), new GUIContent("Minimum Position"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumTemperaturePosition"), new GUIContent("Maximum Position"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Data Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inputFactor"), new GUIContent("Multiplier"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumValue"), new GUIContent("Minimum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"), new GUIContent("Maximum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Amount", dial.currentValue.ToString("0.00"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connections", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TemperatureNeedle"), new GUIContent("Temperature Tape"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("valueOutput"), new GUIContent("Text Display"));
        }
        else
        {

            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Rotation Amounts", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumRotation"), new GUIContent("Minimum Angle"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRotation"), new GUIContent("Maximum Angle"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Data Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inputFactor"), new GUIContent("Multiplier"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumValue"), new GUIContent("Minimum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"), new GUIContent("Maximum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Amount", dial.currentValue.ToString("0.00"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connections", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("needle"), new GUIContent("Needle"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("valueOutput"), new GUIContent("Text Display"));
        }


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Explosion
#region Explosion
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomExplosion))]
public class PhantomExplosionEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);

    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();



        GUILayout.Space(3f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Properties", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionForce"), new GUIContent("Explosion Force"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionRadius"), new GUIContent("Effective Radius"));


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Light Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightIntensity"), new GUIContent("Maximum Intensity"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exposureTime"), new GUIContent("Exposure Time"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("LightCurve"), new GUIContent("Decay Curve"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Extension
#region Extension
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomExtension))]
public class PhantomExtensionEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();
        PhantomExtension extension = (PhantomExtension)target;
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("function"), new GUIContent("Function"));


        //1. SOUND SYSTEM
        if (extension.function == PhantomExtension.Function.CaseSound || extension.function == PhantomExtension.Function.ImpactSound)
        {
            GUILayout.Space(5f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Sounds", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(5f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUIContent soundLabel = new GUIContent("Sound Clips");
            SerializedProperty muzs = this.serializedObject.FindProperty("sounds");
            EditorGUILayout.PropertyField(muzs.FindPropertyRelative("Array.size"), soundLabel);
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Clips", MessageType.None);
            GUI.color = backgroundColor;
            for (int i = 0; i < muzs.arraySize; i++)
            {
                GUIContent label = new GUIContent("Clip " + (i + 1).ToString());
                EditorGUILayout.PropertyField(muzs.GetArrayElementAtIndex(i), label);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Settings", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundRange"), new GUIContent("Range"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundVolume"), new GUIContent("Volume"));

            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("CleanUp Timer", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyTime"), new GUIContent("Destroy Time"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contact"), new GUIContent("Collision Destroy"));
        }

        //2. CLEANUP
        if (extension.function == PhantomExtension.Function.CleanUp)
        {
            GUILayout.Space(3f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Timer", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyTime"), new GUIContent("Destroy Time"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contact"), new GUIContent("Collision Destroy"));
        }
        //3. STARTER
        if (extension.function == PhantomExtension.Function.Starter)
        {
            GUILayout.Space(3f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connected Helicopter", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controller"), new GUIContent(" "));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Transmission
#region Transmission
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomTransmission))]
public class PhantomTransmissionEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomTransmission gearBox;
    GUIContent label;
    float cutoutLimit;
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        gearBox = (PhantomTransmission)target;
    }
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();

        //SELECTIBLES
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Helicopter Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("helicopterType"), new GUIContent(" "));
        GUILayout.Space(5f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Rotor Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotorSystem"), new GUIContent("Rotor Type"));
        GUILayout.Space(5f);





        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Power Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("systemType"), new GUIContent("System Type"));
        GUILayout.Space(3f);
        if (gearBox.systemType == PhantomTransmission.SystemType.SingleEngine)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent("Engine Type"));
            GUILayout.Space(3f);
            if (gearBox.engineType == PhantomController.EngineType.Piston)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pistonEngine"), new GUIContent(" "));
            }
            if (gearBox.engineType == PhantomController.EngineType.Turboshaft)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineA"), new GUIContent(" "));
            }
        }
        if (gearBox.systemType == PhantomTransmission.SystemType.MultiEngine)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("engineCount"), new GUIContent("Count"));

            if (gearBox.engineCount == PhantomTransmission.EngineCount.E2)
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineA"), new GUIContent("Engine A"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineB"), new GUIContent("Engine B"));
            }
            if (gearBox.engineCount == PhantomTransmission.EngineCount.E3)
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineA"), new GUIContent("Engine A"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineB"), new GUIContent("Engine B"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaftEngineC"), new GUIContent("Reserve Engine"));
            }
        }




        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("GearBox Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        // gearBox.torqueSystem = (PhantomTransmission.TorqueSystem)EditorGUILayout.EnumPopup("Torque System", gearBox.torqueSystem);
        GUILayout.Space(3f);
        gearBox.internalFriction = EditorGUILayout.FloatField("Internal Friction", gearBox.internalFriction);
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Core RPM", gearBox.functionalRPM.ToString("0.00") + " RPM");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Hydraulic Load", gearBox.hydraulicsLoad.ToString("0.00") + " Nm");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Rotor Load", gearBox.rotorLoad.ToString("0.00") + " Nm");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Total Load", gearBox.totalLoad.ToString("0.00") + " Nm");

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Performance Monitor", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Rotor Tip Speeds", MessageType.None);
        GUI.color = backgroundColor;
        float pt = 0;if (gearBox.primaryRotor != null) { pt = gearBox.primaryRotor.υM; }
        float st = 0; if (gearBox.secondaryRotor != null) { st = gearBox.secondaryRotor.υM; }
        float at = 0; if (gearBox.appendageRotor != null) { at = gearBox.appendageRotor.υM; }


        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Primary Rotor", pt.ToString("0.000"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Secondary Rotor", st.ToString("0.000"));
        if (gearBox.helicopterType == PhantomTransmission.HelicopterType.Compound)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Appendage Rotor", at.ToString("0.000"));
        }


        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Power Management", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Available Power", gearBox.availablePower.ToString("0.00") + " kW");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Requested Power", gearBox.requiredPower.ToString("0.00") + " kW");
        Color powerColor = Color.white;
        if (gearBox.excessPower > 0) { powerColor = Color.green; }
        if (gearBox.excessPower < 0) { powerColor = Color.red; }

        GUI.color = powerColor;
        EditorGUILayout.HelpBox("Power Deficit: " + gearBox.excessPower.ToString("0.00") + " kW", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        //gearBox.overTorqueWarning = EditorGUILayout.ObjectField("Overtorque Warning", gearBox.overTorqueWarning, typeof(AudioClip), true) as AudioClip;



        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Controller
#region Controller
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomController))]
public class PhantomControllerEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomController controller;
    SerializedProperty model;
    SerializedProperty fuel;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        controller = (PhantomController)target;
        model = serializedObject.FindProperty("model");
        fuel = serializedObject.FindProperty("fuelSystem");
    }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Aircraft Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Label", controller.transform.gameObject.name);
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("craftMode"), new GUIContent("Aircraft Type"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent("Engine Type"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("torqueState"), new GUIContent("Torque State"));




        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Control Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controlType"), new GUIContent("Control"));
        if (controller.controlType == PhantomController.ControlType.Internal)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Cockpit Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Pilot Onboard", controller.pilotOnboard.ToString());
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("canvasDisplay"), new GUIContent("Display Canvas"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interiorPilot"), new GUIContent("Interior Pilot"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("getOutPosition"), new GUIContent("Exit Point"));
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox(" ", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
        }

        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inputType"), new GUIContent("Input"));

        if (controller.inputType == PhantomController.InputType.VR)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("VR Levers", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("controlStick"), new GUIContent("Cyclic Stick"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("throttleLever"), new GUIContent("Throttle Lever"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("collectiveLever"), new GUIContent("Collective Lever"));
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox(" ", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
        }


        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startMode"), new GUIContent("Start Mode"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("quickStart"), new GUIContent("Quick Start"));

        if (controller.startMode == PhantomController.StartMode.Hot)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Instantenous Start (Speed = m/s, Altitude = m)", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startSpeed"), new GUIContent("Start Speed"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startAltitude"), new GUIContent("Start Altitude"));
        }




        GUILayout.Space(15f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Weight Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("emptyWeight"), new GUIContent("Empty Weight"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumWeight"), new GUIContent("Maximum Weight"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Current Weight", controller.currentWeight.ToString("0.0") + " kg");

        if (controller.engineType != PhantomController.EngineType.Electric)
        {

            if (controller.engineType == PhantomController.EngineType.Piston)
            {
                if (controller.gasType == PhantomController.GasType.AVGas100) { controller.combustionEnergy = 42.8f; }
                if (controller.gasType == PhantomController.GasType.AVGas100LL) { controller.combustionEnergy = 43.5f; }
                if (controller.gasType == PhantomController.GasType.AVGas82UL) { controller.combustionEnergy = 43.6f; }
            }
            else
            {
                if (controller.jetType == PhantomController.JetType.JetB) { controller.combustionEnergy = 42.8f; }
                if (controller.jetType == PhantomController.JetType.JetA1) { controller.combustionEnergy = 43.5f; }
                if (controller.jetType == PhantomController.JetType.JP6) { controller.combustionEnergy = 43.02f; }
                if (controller.jetType == PhantomController.JetType.JP8) { controller.combustionEnergy = 43.28f; }
            }
            controller.combustionEnergy *= 1000f;


            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Fuel Configuration", MessageType.None);
            GUI.color = backgroundColor;
            if (controller.engineType == PhantomController.EngineType.Piston)
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("gasType"), new GUIContent("Fuel Type"));
            }
            else
            {
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("jetType"), new GUIContent("Fuel Type"));

            }
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Q ", controller.combustionEnergy.ToString("0.0") + " KJ");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Fuel Level", controller.fuelLevel.ToString("0.00") + " kg");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Flow Rate", controller.totalConsumptionRate.ToString("0.000") + " kg/s");


            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(fuel.FindPropertyRelative("fuelSelector"), new GUIContent("Tank Selector"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(fuel.FindPropertyRelative("usageFactor"), new GUIContent("Update Function"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Motor
#region Motor
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomElectricMotor))]
public class PhantomElectricMotorEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomElectricMotor motor;
    public int toolbarTab;
    public string currentTab;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { motor = (PhantomElectricMotor)target; }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Motor Specifications", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ratedVoltage"), new GUIContent("Rated Voltage"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Kv"), new GUIContent("Kv"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("windingResistance"), new GUIContent("Winding Resistance"));
        GUILayout.Space(8f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ratedRPM"), new GUIContent("Rated RPM"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("baseCoreAcceleration"), new GUIContent("Motor Acceleration"));


        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Power Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(8f);
        EditorGUILayout.LabelField("Input Voltage", motor.Vrms.ToString("0.0") + " Volts");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Input Current", motor.motorCurrent.ToString("0.0") + " Amps");


        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("runningSound"), new GUIContent("Running Sound"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitch"), new GUIContent("Maximum Pitch"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumVolume"), new GUIContent("Maximum Volume"));




        GUILayout.Space(25f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Performance", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Motor State", motor.motorState.ToString());
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Motor Power", (motor.electricalPowerConsumed).ToString("0.00") + " kW");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Shaft Horsepower", motor.mechanicalPowerDelivered.ToString("0.0") + " Hp");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Core Speed", motor.coreRPM.ToString("0.0") + " RPM");


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Munition
#region Munition
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomMunition))]
public class PhantomMunitionEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomMunition munition;

    //--------------------------------------------------------------------------------------------------------
    private void OnEnable() { munition = (PhantomMunition)target; }


    //--------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector(); 
        serializedObject.Update();

        EditorGUILayout.LabelField("Identifier", munition.Identifier);
        GUILayout.Space(3f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Munition Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionType"), new GUIContent(" "));


        if (munition.munitionType == PhantomMunition.MunitionType.Rocket || munition.munitionType == PhantomMunition.MunitionType.Missile)
        {
            GUILayout.Space(8f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Dynamic Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("surfaceFinish"), new GUIContent("Surface Finish"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dragMode"), new GUIContent("Drag Estimation"));
            GUILayout.Space(5f);
            if (munition.dragMode == PhantomMunition.DragMode.Clamped)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumMachSpeed"), new GUIContent("Maximum Mach Speed"));
            }
            if (munition.dragMode == PhantomMunition.DragMode.Free)
            {
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Base Drag Coefficient", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("baseDragCoefficient"), new GUIContent(" "));
                GUILayout.Space(3f);
                EditorGUILayout.LabelField("Total Cd", munition.dragCoefficient.ToString("0.000"));
            }
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Speed", munition.machSpeed.ToString("0.00") + " Mach");
        }

        GUILayout.Space(10f);

        //1. -------------------------------------------- Rocket
        if (munition.munitionType == PhantomMunition.MunitionType.Rocket)
        {
            GUILayout.Space(2f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Rocket Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rocketType"), new GUIContent("Mode"));
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Warhead Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fireMode"), new GUIContent("Fire Mode"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionPrefab"), new GUIContent("Explosion Prefab"));


            if (munition.explosionPrefab != null)
            {
                PhantomExplosion explosive = munition.explosionPrefab.GetComponent<PhantomExplosion>();
                if (explosive != null)
                {
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Performance", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    GUILayout.Space(3f);
                    EditorGUILayout.LabelField("Explosive Force", explosive.explosionForce.ToString("0.0") + " N");
                    GUILayout.Space(1f);
                    EditorGUILayout.LabelField("Explosive Radius", explosive.explosionRadius.ToString("0") + " m");
                }
            }

            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Detonation System", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fuzeType"), new GUIContent("Fuze Type"));
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Armed", munition.armed.ToString());
            GUILayout.Space(3f);
            //IMPACT
            if (munition.fuzeType == PhantomMunition.FuzeType.MK193Mod0)
            {
                EditorGUILayout.LabelField("Detonation Mechanism", "Nose Impact");
                GUILayout.Space(2f);
            }
            //TIME
            else if (munition.fuzeType == PhantomMunition.FuzeType.MK352)
            {
                EditorGUILayout.LabelField("Detonation Mechanism", "Mechanical Timer");
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("timer"), new GUIContent("Trigger Timer"));
            }
            else if (munition.fuzeType == PhantomMunition.FuzeType.M423)
            {
                EditorGUILayout.LabelField("Detonation Mechanism", "Proximity");
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("proximity"), new GUIContent("Trigger Distance"));
                if (munition.target != null)
                {
                    GUILayout.Space(2f);
                    EditorGUILayout.LabelField("Target", munition.target.name);
                }
            }
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRange"), new GUIContent("Maximum Range"));
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Rocket Dimensions", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionDiameter"), new GUIContent("Diameter"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionLength"), new GUIContent("Length"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionWeight"), new GUIContent("Weight"));
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Propulsion", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("motorEngine"), new GUIContent("Rocket Motor"));
            GUILayout.Space(3f);
        }

        //2. -------------------------------------------- Bullet
        if (munition.munitionType == PhantomMunition.MunitionType.Bullet)
        {
            GUILayout.Space(3f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Bullet Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ammunitionType"), new GUIContent("Ammunition Type"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ammunitionForm"), new GUIContent("Ammunition Form"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletfuseType"), new GUIContent("Fuze Type"));
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("System Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mass"), new GUIContent("Mass"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("caseLength"), new GUIContent("Case Length"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("overallLength"), new GUIContent("Overall Length"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("diameter"), new GUIContent("Diameter"));
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Performance Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ballisticVelocity"), new GUIContent("Ballistic Velocity"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyTime"), new GUIContent("Destroy Time"));
            if (munition.ammunitionType == PhantomMunition.AmmunitionType.HEI)
            {
                GUILayout.Space(10f);
                GUI.color = silantroColor;
                EditorGUILayout.HelpBox("Explosive Configuration", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionPrefab"), new GUIContent("Explosion Prefab"));
                if (munition.explosionPrefab != null)
                {
                    PhantomExplosion explosive = munition.explosionPrefab.GetComponent<PhantomExplosion>();
                    if (explosive != null)
                    {
                        GUI.color = Color.white;
                        EditorGUILayout.HelpBox("Performance", MessageType.None);
                        GUI.color = backgroundColor;
                        GUILayout.Space(3f);
                        EditorGUILayout.LabelField("Explosive Force", explosive.explosionForce.ToString("0.0") + " N");
                        GUILayout.Space(1f);
                        EditorGUILayout.LabelField("Explosive Radius", explosive.explosionRadius.ToString("0") + " m");
                    }
                }
            }
        }

        //3. -------------------------------------------- Missile
        if (munition.munitionType == PhantomMunition.MunitionType.Missile)
        {
            GUILayout.Space(2f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Missile Configuration", MessageType.None);
            GUI.color = backgroundColor;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("missileType"), new GUIContent("Mode"));
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Warhead Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fireMode"), new GUIContent("Fire Mode"));
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionPrefab"), new GUIContent("Explosion Prefab"));
            if (munition.explosionPrefab != null)
            {
                PhantomExplosion explosive = munition.explosionPrefab.GetComponent<PhantomExplosion>();
                if (explosive != null)
                {
                    GUI.color = Color.white;
                    EditorGUILayout.HelpBox("Performance", MessageType.None);
                    GUI.color = backgroundColor;
                    GUILayout.Space(2f);
                    GUILayout.Space(3f);
                    EditorGUILayout.LabelField("Explosive Force", explosive.explosionForce.ToString("0.0") + " N");
                    GUILayout.Space(1f);
                    EditorGUILayout.LabelField("Explosive Radius", explosive.explosionRadius.ToString("0") + " m");
                }
            }

            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Detonation System", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Armed State", munition.armed.ToString());
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Current Speed", munition.speed.ToString() + " m/s");
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("detonationType"), new GUIContent("Detonation Type"));
            if (munition.detonationType == PhantomMunition.DetonationType.Proximity)
            {
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("proximity"), new GUIContent("Trigger Distance"));
                if (munition.target)
                {
                    GUILayout.Space(2f);
                    EditorGUILayout.LabelField("Distance To Target", munition.distanceToTarget.ToString("0.00") + " m");
                }
                if (munition.target != null)
                {
                    EditorGUILayout.LabelField("Current Target", munition.target.name);
                }
                else
                {
                    EditorGUILayout.LabelField("Current Target", "Null");
                }
            }
            if (munition.detonationType == PhantomMunition.DetonationType.Timer)
            {
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("timer"), new GUIContent("Trigger Timer"));
            }
            if (munition.detonationType == PhantomMunition.DetonationType.Impact)
            {
                GUILayout.Space(2f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("speedThreshhold"), new GUIContent("Trigger Speed"));
            }
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("Distance Traveled", munition.distanceTraveled.ToString("0.0") + " m");
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRange"), new GUIContent("Maximum Range"));
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Missile Dimensions", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionDiameter"), new GUIContent("Diameter"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionLength"), new GUIContent("Length"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionWeight"), new GUIContent("Weight"));
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Propulsion", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("motorEngine"), new GUIContent("Rocket Motor"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Sensor Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Weapon ID", munition.WeaponID);

            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Target Configuration", MessageType.None);
            GUI.color = backgroundColor;
            if (munition.target)
            {
                GUILayout.Space(5f);
                EditorGUILayout.LabelField("Target", munition.target.name);
                GUILayout.Space(3f);
                EditorGUILayout.LabelField("Target Distance", munition.distanceToTarget.ToString("0.0") + " m");
                GUILayout.Space(3f);
                EditorGUILayout.LabelField("Target Direction", munition.viewDirection.ToString());
            }
            else
            {
                GUILayout.Space(3f);
                EditorGUILayout.LabelField("Target", "Unassigned");
            }
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Target ID", munition.targetID);
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Seeking", munition.seeking.ToString());
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Rocket Motor
#region Rocket Motor
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomRocketMotor))]
[CanEditMultipleObjects]
public class PhantomRocketMotorEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomRocketMotor motor;

    //------------------------------------------------------------------------
    private SerializedProperty coneLength;
    private SerializedProperty fuelLength;

    // ------------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        motor = (PhantomRocketMotor)target;

        coneLength = serializedObject.FindProperty("exhaustConeLength");
        fuelLength = serializedObject.FindProperty("fuelLength");
    }


    // ------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();


        GUILayout.Space(3f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Motor Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nozzleDiameterPercentage"), new GUIContent("Nozzle Ratio"));

        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Nozzle Diameter", motor.nozzleDiameter.ToString("0.00") + " m");
        GUILayout.Space(1f);
        EditorGUILayout.LabelField("Nozzle Area", motor.demoArea.ToString("0.00") + " m2");
        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Fuel Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        coneLength.floatValue = EditorGUILayout.Slider("Exhaust Cone Length", coneLength.floatValue, 0f, motor.overallLength / 2f);
        GUILayout.Space(2f);
        fuelLength.floatValue = EditorGUILayout.Slider("Fuel Length", fuelLength.floatValue, 0f, motor.overallLength);



        GUILayout.Space(8f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Point Pressures in kPa", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("chamberPressure"), new GUIContent("Chamber Pressure"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exitPressure"), new GUIContent("Exit Pressure"));


        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Fuel Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("solidFuelType"), new GUIContent("Fuel Type"));
        GUILayout.Space(1f);
        EditorGUILayout.LabelField("Tc", motor.combustionTemperature.ToString("0") + " °K");
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireDuration"), new GUIContent("Fire Duration"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("burnType"), new GUIContent("Burn Type"));
        GUILayout.Space(2f);
        EditorGUILayout.CurveField("Thrust Curve", motor.burnCurve);

        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Rocket Effects", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustSmoke"), new GUIContent("Exhaust Smoke"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumSmokeEmissionValue"), new GUIContent("Maximum Emission"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustFlame"), new GUIContent("Exhaust Flame"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumFlameEmissionValue"), new GUIContent("Maximum Emission"));


        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("motorSound"), new GUIContent("Booster Sound"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitch"), new GUIContent("Maximum Pitch"));


        GUILayout.Space(20f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Output", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Thrust Generated", motor.Thrust.ToString("0.00") + " N");


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Radar
#region Radar
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomRadar))]
public class PhantomRadarEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomRadar radar;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnEnable() { radar = (PhantomRadar)target; }



    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector(); 
        serializedObject.Update();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Functionality", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("radarType"), new GUIContent("Type"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("radarMode"), new GUIContent("Mode"));

        if (radar.radarMode == PhantomRadar.RadarMode.Connected)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("supportRadar"), new GUIContent(" "));
        }




        // ----------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Radar Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("range"), new GUIContent("Effective Range"));

        if (radar.radarType == PhantomRadar.RadarType.Military)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumWeaponsRange"), new GUIContent("Weapons Range"));
        }
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("criticalSignature"), new GUIContent("Critical RCS"));
        GUILayout.Space(5f);
        SerializedProperty layerMask = serializedObject.FindProperty("checkLayers");
        EditorGUILayout.PropertyField(layerMask);
        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Ping Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pingRate"), new GUIContent("Ping Rate"));
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Last Ping", (radar.pingTime).ToString("0.000") + " s");
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("displayBounds"), new GUIContent("Display Extents"));



        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Object Identification", MessageType.None);
        GUI.color = backgroundColor;
        if (radar.radarType == PhantomRadar.RadarType.Military)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectFilter"), new GUIContent("Target Filter"));
        }
        if (radar.visibleObjects != null)
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("Visible Objects", radar.visibleObjects.Length.ToString());
        }
        if (radar.filteredObjects != null)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Filtered Objects", radar.filteredObjects.Count.ToString());
        }
        if (radar.radarType == PhantomRadar.RadarType.Military)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Selected Target", radar.currentTarget.name);
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Locked Target", radar.lockedTarget.name);
        }




        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Display Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("size"), new GUIContent("Radar Size"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("objectScale"), new GUIContent("Object Scale"));
        GUILayout.Space(5f);
        serializedObject.FindProperty("radarSkin").objectReferenceValue = EditorGUILayout.ObjectField("GUI Skin", serializedObject.FindProperty("radarSkin").objectReferenceValue, typeof(GUISkin), true) as GUISkin;
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotate"), new GUIContent("Rotate"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("markTargets"), new GUIContent("Mark Objects"));


        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Texture Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        serializedObject.FindProperty("background").objectReferenceValue = EditorGUILayout.ObjectField("Radar Background", serializedObject.FindProperty("background").objectReferenceValue, typeof(Texture), true) as Texture;
        GUILayout.Space(5f);
        serializedObject.FindProperty("compass").objectReferenceValue = EditorGUILayout.ObjectField("Compass", serializedObject.FindProperty("compass").objectReferenceValue, typeof(Texture), true) as Texture;
        if (radar.radarType == PhantomRadar.RadarType.Military)
        {
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Radar Screen Icons", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            serializedObject.FindProperty("selectedTargetTexture").objectReferenceValue = EditorGUILayout.ObjectField("Selected Target", serializedObject.FindProperty("selectedTargetTexture").objectReferenceValue, typeof(Texture2D), true) as Texture2D;
            GUILayout.Space(5f);
            serializedObject.FindProperty("lockedTargetTexture").objectReferenceValue = EditorGUILayout.ObjectField("Locked Target", serializedObject.FindProperty("lockedTargetTexture").objectReferenceValue, typeof(Texture2D), true) as Texture2D;
            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Camera Screen Icons", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            serializedObject.FindProperty("TargetLockOnTexture").objectReferenceValue = EditorGUILayout.ObjectField("Selected Target", serializedObject.FindProperty("TargetLockOnTexture").objectReferenceValue, typeof(Texture2D), true) as Texture2D;
            GUILayout.Space(5f);
            serializedObject.FindProperty("TargetLockedTexture").objectReferenceValue = EditorGUILayout.ObjectField("Locked Target", serializedObject.FindProperty("TargetLockedTexture").objectReferenceValue, typeof(Texture2D), true) as Texture2D;
        }




        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Color Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Transparency"), new GUIContent("Transparency"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("generalColor"), new GUIContent("General Color"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("labelColor"), new GUIContent("Label Color"));

        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Camera Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        serializedObject.FindProperty("targetCamera").objectReferenceValue = EditorGUILayout.ObjectField("Radar Camera", serializedObject.FindProperty("targetCamera").objectReferenceValue, typeof(Camera), true) as Camera;
        if (radar.radarType == PhantomRadar.RadarType.Military)
        {
            GUILayout.Space(3f);
            serializedObject.FindProperty("lockedTargetCamera").objectReferenceValue = EditorGUILayout.ObjectField("Locked Camera", serializedObject.FindProperty("lockedTargetCamera").objectReferenceValue, typeof(Camera), true) as Camera;
        }



        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("View Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraHeight"), new GUIContent("Camera Height"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraDistance"), new GUIContent("Camera Distance"));


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Gun
#region Gun
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomMachineGun))]
public class PhantomMachineGunEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomMachineGun gun;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { gun = (PhantomMachineGun)target; }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector(); 
        serializedObject.Update();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("System Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletType"), new GUIContent("Bullet Type"));


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Ballistic Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gunWeight"), new GUIContent("Weight"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("barrelLength"), new GUIContent("Barrel Length"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleVelocity"), new GUIContent("Muzzle Velocity"));
        GUILayout.Space(7f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("range"), new GUIContent("Maximum Range"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rateOfFire"), new GUIContent("Rate of Fire"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Actual Rate", gun.actualRate.ToString("0.0000"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Fire Timer", gun.fireTimer.ToString("0.0000"));

        GUILayout.Space(3f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Recoil Effect", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Recoil Force", gun.projectileForce.ToString("0.00") + " N");
        GUILayout.Space(3f);
        serializedObject.FindProperty("damperStrength").floatValue = EditorGUILayout.Slider("Damper", serializedObject.FindProperty("damperStrength").floatValue, 0f, 100f);


        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Ammo Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("unlimitedAmmo"), new GUIContent("Infinite Ammo"));
        if (!gun.unlimitedAmmo)
        {
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ammoCapacity"), new GUIContent("Capacity"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Ammo", gun.currentAmmo.ToString());
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Drum Weight", gun.drumWeight.ToString() + " kg");
        }
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ejectShells"), new GUIContent("Release Shells"));
        if (gun.ejectShells)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shellEjectPoint"), new GUIContent("Release Point"));
        }



        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Accuracy Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("accuracy"), new GUIContent("Accuracy"));
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Current Accuracy", gun.currentAccuracy.ToString("0.00"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("advancedSettings"), new GUIContent("Advanced Settings"));

        if (gun.advancedSettings)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("accuracyDrop"), new GUIContent("Drop Per Shot"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("accuracyRecover"), new GUIContent("Recovery Per Shot"));
        }


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Bullet Settings", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        if (gun.bulletType == PhantomMachineGun.BulletType.Rigidbody)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ammunition"), new GUIContent("Bullet"));
        }
        else
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage"));
        }
        GUILayout.Space(5f);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        SerializedProperty muzs = this.serializedObject.FindProperty("muzzles");
        GUIContent barrelLabel = new GUIContent("Barrel Count");
        EditorGUILayout.PropertyField(muzs.FindPropertyRelative("Array.size"), barrelLabel);
        GUILayout.Space(5f);
        for (int i = 0; i < muzs.arraySize; i++)
        {
            GUIContent label = new GUIContent("Barrel " + (i + 1).ToString());
            EditorGUILayout.PropertyField(muzs.GetArrayElementAtIndex(i), label);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        GUILayout.Space(10f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Revolver", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("barrel"), new GUIContent("Revolver"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationDirection"), new GUIContent("Rotation Direction"));

        if (gun.barrel != null)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Barrel RPM", gun.currentRPM.ToString("0.0") + " RPM");
        }



        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Effects Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleFlash"), new GUIContent("Muzzle Flash"));
        if (gun.ejectShells)
        {
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletCase"), new GUIContent("Bullet Case"));
        }
        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Impact Effects", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("groundHit"), new GUIContent("Ground Hit"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("metalHit"), new GUIContent("Metal Hit"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("woodHit"), new GUIContent("Wood Hit"));



        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireLoopSound"), new GUIContent("Fire Loop Sound"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireEndSound"), new GUIContent("Fire End Sound"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundVolume"), new GUIContent("Sound Volume"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundRange"), new GUIContent("Sound Range"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Armament
#region Armament
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomArmament))]
public class PhantomArmamentEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomArmament carrier;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        carrier = (PhantomArmament)target;
    }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();



        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("System Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Payload", carrier.weaponsLoad.ToString("0.0") + " kg");
        int mun = 0;
        if (carrier.munitions != null) { mun = carrier.munitions.Length; }
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("Munition Count", mun.ToString());
        GUILayout.Space(8f);
        if (!carrier.setMissile)
        {
            if (GUILayout.Button("Configure Missile"))
            {
                carrier.setMissile = true;
            }
        }
        if (carrier.setMissile)
        {
            if (GUILayout.Button("Hide Missile Board"))
            {
                carrier.setMissile = false;
            }
            GUILayout.Space(8f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Missile Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            int count = 0;
            if (carrier.missiles != null) { count = carrier.missiles.Count; }
            if (carrier.lowMissiles != null) { count = carrier.lowMissiles.Count; }
            EditorGUILayout.LabelField("Missile Count", count.ToString());
            if (carrier.AAMS != null)
            {
                GUILayout.Space(2f);
                EditorGUILayout.LabelField("AAM Count", carrier.AAMS.Count.ToString());
            }
            if (carrier.AGMS != null)
            {
                GUILayout.Space(2f);
                EditorGUILayout.LabelField("AGM Count", carrier.AGMS.Count.ToString());
            }

            GUILayout.Space(8f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Launcher Sound", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fireSound"), new GUIContent("Fire Sound"));
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fireVolume"), new GUIContent("Fire Volume"));
        }

        //2. ROCKETS
        GUILayout.Space(10f);
        if (!carrier.setRocket)
        {
            if (GUILayout.Button("Configure Rockets"))
            {
                carrier.setRocket = true;
            }
        }
        if (carrier.setRocket)
        {
            if (GUILayout.Button("Hide Rocket Board"))
            {
                carrier.setRocket = false;
            }
            GUILayout.Space(8f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Rocket Configuration", MessageType.None);
            GUI.color = backgroundColor;
            if (carrier.rockets != null)
            {
                GUILayout.Space(2f);
                EditorGUILayout.LabelField("Rocket Count", carrier.rockets.Count.ToString());
            }
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rateOfFire"), new GUIContent("Rate of Fire"));
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("AROF", carrier.actualFireRate.ToString("0.00"));
        }


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- ESC
#region ESC
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomESC))]
public class PhantomESCEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomESC esc;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { esc = (PhantomESC)target; }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();



        EditorGUILayout.HelpBox("Forward Left Components", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontLeftRotor"), new GUIContent("Rotor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontLeftMotor"), new GUIContent("Motor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("FLBattery"), new GUIContent("Battery"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("RPM", esc.frontLeftRotor.coreRPM.ToString("0.0") + " RPM");


        GUILayout.Space(3f);
        EditorGUILayout.HelpBox("Forward Right Components", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontRightRotor"), new GUIContent("Rotor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontRightMotor"), new GUIContent("Motor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("FRBattery"), new GUIContent("Battery"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("RPM", esc.frontRightRotor.coreRPM.ToString("0.0") + " RPM");


        GUILayout.Space(3f);
        EditorGUILayout.HelpBox("Rear Left Components", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rearLeftRotor"), new GUIContent("Rotor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rearLeftMotor"), new GUIContent("Motor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("RLBattery"), new GUIContent("Battery"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("RPM", esc.rearLeftRotor.coreRPM.ToString("0.0") + " RPM");


        GUILayout.Space(3f);
        EditorGUILayout.HelpBox("Rear Right Components", MessageType.None);
        GUI.color = backgroundColor;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rearRightRotor"), new GUIContent("Rotor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rearRightMotor"), new GUIContent("Motor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("RRBattery"), new GUIContent("Battery"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("RPM", esc.rearRightRotor.coreRPM.ToString("0.0") + " RPM");


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Control Signals", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Motor 1", (1000 + esc.m_esc_1 * 1000).ToString("0.0") + " µs");
        EditorGUILayout.LabelField("Motor 2", (1000 + esc.m_esc_2 * 1000).ToString("0.0") + " µs");
        EditorGUILayout.LabelField("Motor 3", (1000 + esc.m_esc_3 * 1000).ToString("0.0") + " µs");
        EditorGUILayout.LabelField("Motor 4", (1000 + esc.m_esc_4 * 1000).ToString("0.0") + " µs");


        GUILayout.Space(15f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Control Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_inflow_throttle"), new GUIContent("Balance Throttle"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_factor"), new GUIContent("TPA Factor"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_pitch_gain"), new GUIContent("Pitch Gain"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_roll_gain"), new GUIContent("Roll Gain"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_yaw_gain"), new GUIContent("Yaw Gain"));


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Battery
#region Battery
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomBattery))]
public class PhantomBatteryEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomBattery battery;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { battery = (PhantomBattery)target; }



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Specifications", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("capacity"), new GUIContent("Capacity"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("seriesCellResistance"), new GUIContent("Cell Resistance"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("standbyDischargeCurrent"), new GUIContent("Standby Current"));
        GUILayout.Space(5f);
        GUI.color = Color.white;
        EditorGUILayout.HelpBox("Internals", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("batteryType"), new GUIContent("Battery Type"));

        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cellCount"), new GUIContent("Cell Count"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Nominal Cell Voltage", battery.nominalCellVoltage.ToString() + " Volts");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Current Cell Voltage", battery.currentCellVolage.ToString("0.0") + " Volts");


        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Performance", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Output Voltage", battery.outputVoltage.ToString("0.0") + " Volts");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Output Current", battery.outputCurrent.ToString("0.0") + " Amps");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Current Power", battery.currentCapacity.ToString("0.0") + " Ah");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Battery Level", battery.SoC.ToString("0.00") + " %");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Effective Power", (battery.availablePower / 1000).ToString("0.0") + " kWh");


        GUILayout.Space(20f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Display", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Current State", battery.state.ToString());
        if (battery.state == PhantomBattery.State.Charging)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Charging Current", battery.chargingCurrent.ToString("0.0") + " Amps");
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Charging Time", battery.timeRemaining.ToString("0.0") + " Hours");
        }
        else if (battery.state == PhantomBattery.State.Discharging)
        {
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Time Remaining", battery.timeRemaining.ToString("0.0") + " Hours");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- Dial
#region Dial
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomDial))]
public class PhantomDialEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomDial dial;

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { dial = (PhantomDial)target; }


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();



        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Dial Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialType"), new GUIContent(" "));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationMode"), new GUIContent("Rotation Mode"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("needleType"), new GUIContent("Needle Type"));


        // -------------------------------- Altimeterr
        if (dial.dialType == PhantomDial.DialType.Speed)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Speedometer Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speedType"), new GUIContent(" "));
        }

        // -------------------------------- Fuel
        if (dial.dialType == PhantomDial.DialType.Fuel)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Fuel Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fuelType"), new GUIContent(" "));
        }

        if (dial.dialType == PhantomDial.DialType.Stabilator)
        {
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Connected Stabilator", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stabilator"), new GUIContent(" "));
        }


        // ------------------------------- RPM
        if (dial.dialType == PhantomDial.DialType.RPM)
        {
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Engine Type", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent(" "));
            GUILayout.Space(5f);

            if (dial.engineType == PhantomDial.EngineType.ElectricMotor)
            {
                //EditorGUILayout.PropertyField(serializedObject.FindProperty("engineType"), new GUIContent(" "));
            }
            if (dial.engineType == PhantomDial.EngineType.TurboShaft)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shaft"), new GUIContent(" "));
            }
            if (dial.engineType == PhantomDial.EngineType.PistonEngine)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("piston"), new GUIContent(" "));
            }
        }



        // --------------------------------------------------------------------------------------------------------------------
        if (dial.dialType != PhantomDial.DialType.ArtificialHorizon)
        {

            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Rotation Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationAxis"), new GUIContent("Rotation Axis"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("direction"), new GUIContent("Rotation Direction"));

            if (dial.needleType == PhantomDial.NeedleType.Dual)
            {
                GUILayout.Space(5f);
                GUI.color = Color.white;
                EditorGUILayout.HelpBox("Support Needle", MessageType.None);
                GUI.color = backgroundColor;
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("supportRotationAxis"), new GUIContent("Rotation Axis"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("supportDirection"), new GUIContent("Rotation Direction"));
            }


            GUILayout.Space(5f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Rotation Amounts", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MinimumAngleDegrees"), new GUIContent("Minimum Angle"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MaximumAngleDegrees"), new GUIContent("Maximum Angle"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Data Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MinimumValue"), new GUIContent("Minimum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MaximumValue"), new GUIContent("Maximum Value"));
            GUILayout.Space(3f);
            EditorGUILayout.LabelField("Current Amount", dial.currentValue.ToString("0.00"));


            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connections", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);


            if (dial.needleType == PhantomDial.NeedleType.Dual)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Needle"), new GUIContent("Main Needle"));
                GUILayout.Space(3f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("supportNeedle"), new GUIContent("Support Needle"));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Needle"), new GUIContent("Needle"));
            }
        }




        else if (dial.dialType == PhantomDial.DialType.ArtificialHorizon)
        {
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Deflection Configuration", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchRotationAxis"), new GUIContent("Pitch Rotation Axis"));
            GUILayout.Space(2f);
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchDirection"), new GUIContent("Pitch Rotation Direction"));

            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("yawRotationAxis"), new GUIContent("Yaw Rotation Axis"));
            GUILayout.Space(2f);
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("yawDirection"), new GUIContent("Yaw Rotation Direction"));

            GUILayout.Space(5f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rollRotationAxis"), new GUIContent("Roll Rotation Axis"));
            GUILayout.Space(2f);
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rollDirection"), new GUIContent("Roll Rotation Direction"));

            GUILayout.Space(15f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Connections", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            dial.Needle = EditorGUILayout.ObjectField("Ball Indicator", dial.Needle, typeof(Transform), true) as Transform;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion


// ---------------------------------------------------- FCS
#region Flight Computer
#if UNITY_EDITOR
[CustomEditor(typeof(PhantomFlightComputer))]
public class PhantomFlightComputerEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1.0f, 0.40f, 0f);
    PhantomFlightComputer computer;
    //SerializedProperty brain;
    //SerializedProperty tracker;


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        computer = (PhantomFlightComputer)target;
        //brain = serializedObject.FindProperty("brain");
        //tracker = brain.FindPropertyRelative("tracker");
    }


    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();
        computer._plotInputCurve();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Control Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("operationMode"), new GUIContent("Mode"));
        GUILayout.Space(5f);



        if (computer.operationMode != PhantomFlightComputer.AugmentationType.Autonomous)
        {
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Input Tuning", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Pitch", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchDeadZone"), new GUIContent("Dead Zone"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchScale"), new GUIContent("Curvature"));
            GUILayout.Space(3f);
            EditorGUILayout.CurveField("Curve", computer.pitchInputCurve);

            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Roll", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rollDeadZone"), new GUIContent("Dead Zone"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rollScale"), new GUIContent("Curvature"));
            GUILayout.Space(3f);
            EditorGUILayout.CurveField("Curve", computer.rollInputCurve);


            GUILayout.Space(3f);
            GUI.color = Color.white;
            EditorGUILayout.HelpBox("Yaw", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(2f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("yawDeadZone"), new GUIContent("Dead Zone"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("yawScale"), new GUIContent("Curvature"));
            GUILayout.Space(3f);
            EditorGUILayout.CurveField("Curve", computer.yawInputCurve);


            GUILayout.Space(20f);
            GUI.color = silantroColor;
            if (GUILayout.Button("Configure Gains"))
            {
                //Selection.activeGameObject = computer.gainBoard.gameObject;
            }
            GUI.color = backgroundColor;


            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
#endregion


// ---------------------------------------------------- Gun Control
#region Gun Control
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PhantomGunControl))]
public class PhantomGunControlEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    PhantomGunControl gunControl;

    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable() { gunControl = (PhantomGunControl)target; }


    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector ();
        serializedObject.Update();


        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Connections", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("connectedGun"), new GUIContent("Gun"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalPivot"), new GUIContent("Horizontal Pivot"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalPivot"), new GUIContent("Vertical Pivot"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleCenter"), new GUIContent("Muzzle Center"));
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Locked Target", gunControl.lockedTarget.ToString());

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Limits", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalLimit"), new GUIContent("Horizontal Limits"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalLimit"), new GUIContent("Vertical Limits"));
        GUILayout.Space(5f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Speeds", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalSpeed"), new GUIContent("Horizontal Speed"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalSpeed"), new GUIContent("Vertical Speed"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion



// ---------------------------------------------------- Missile
#region Missile
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Oyedoyin.Rotary.LowFidelity.Munition))]
public class MissileEditor : Editor
{
    Color backgroundColor;
    Color silantroColor = new Color(1, 0.4f, 0);
    Oyedoyin.Rotary.LowFidelity.Munition munition;

    //--------------------------------------------------------------------------------------------------------
    private void OnEnable() { munition = (Oyedoyin.Rotary.LowFidelity.Munition)target; }


    //--------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        backgroundColor = GUI.backgroundColor;
        //DrawDefaultInspector();
        serializedObject.Update();

        EditorGUILayout.LabelField("Identifier", munition.gameObject.name);
        GUILayout.Space(3f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Munition Type", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionType"), new GUIContent("Type"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionForm"), new GUIContent("Form"));



        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Munition Dimensions", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionDiameter"), new GUIContent("Diameter"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionLength"), new GUIContent("Length"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionWeight"), new GUIContent("Weight"));
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumSpeed"), new GUIContent("Mach Speed"));


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Motor Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("burnType"), new GUIContent("Burn Type"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireDuration"), new GUIContent("Burn Time"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("burnCurve"), new GUIContent("Burn Curve"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("munitionThrust"), new GUIContent("Mean Thrust"));


        if (munition.munitionType == Oyedoyin.Rotary.LowFidelity.Munition.MunitionType.Guided)
        {
            GUILayout.Space(10f);
            GUI.color = silantroColor;
            EditorGUILayout.HelpBox("Navigation", MessageType.None);
            GUI.color = backgroundColor;
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumRange"), new GUIContent("Maximum Range"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("navigationConstant"), new GUIContent("Navigation Gain"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumTurnRate"), new GUIContent("Turn Rate"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumLockDirection"), new GUIContent("Minimum Lock"));
            GUILayout.Space(3f);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("proximity"), new GUIContent("Proximity"));
            GUILayout.Space(3f);
            string tgt = "null";
            if (munition.m_target != null) { tgt = munition.m_target.name; }
            EditorGUILayout.LabelField("Target", tgt);
        }

        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Rocket Effects", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustSmoke"), new GUIContent("Exhaust Smoke"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumSmokeEmissionValue"), new GUIContent("Maximum Emission"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustFlame"), new GUIContent("Exhaust Flame"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumFlameEmissionValue"), new GUIContent("Maximum Emission"));
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("explosionPrefab"), new GUIContent("Explosion Prefab"));


        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Sound Configuration", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("motorSound"), new GUIContent("Booster Sound"));
        GUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumPitch"), new GUIContent("Maximum Pitch"));




        GUILayout.Space(10f);
        GUI.color = silantroColor;
        EditorGUILayout.HelpBox("Output", MessageType.None);
        GUI.color = backgroundColor;
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Isp", munition.acceleration.ToString("0.00") + " /s");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Thrust", munition.thrust.ToString("0.00") + " N");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Drag", munition.drag.ToString("0.00") + " N");
        GUILayout.Space(3f);
        EditorGUILayout.LabelField("Speed", munition.munitionMach.ToString("0.00") + " Mach");


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion