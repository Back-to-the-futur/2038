#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Oyedoyin;

public class PhantomESC : MonoBehaviour
{
    // ------------------------------------------- Components
    public PhantomRotor frontLeftRotor;
    public PhantomRotor frontRightRotor;
    public PhantomRotor rearLeftRotor;
    public PhantomRotor rearRightRotor;

    public PhantomElectricMotor frontLeftMotor;
    public PhantomElectricMotor frontRightMotor;
    public PhantomElectricMotor rearLeftMotor;
    public PhantomElectricMotor rearRightMotor;

    public PhantomBattery FLBattery;
    public PhantomBattery FRBattery;
    public PhantomBattery RLBattery;
    public PhantomBattery RRBattery;

    public PhantomController controller;


    // ------------------------------------------- Control
    bool initialized;
    public float m_roll;
    public float m_pitch;
    public float m_yaw;
    public float m_throttle;

    public float m_esc_1;
    public float m_esc_2;
    public float m_esc_3;
    public float m_esc_4;

    public float m_factor = 10f, m_throttle_factor;
    public Vector3 m_pitch_gain;
    public Vector3 m_roll_gain;
    public Vector3 m_yaw_gain;

    public float m_inflow_command;
    public float m_inflow_throttle = 0.5f;
    public float m_command;
    public float m_ux;



    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InitializeESC()
    {
        frontLeftMotor.battery = FLBattery;
        frontRightMotor.battery = FRBattery;
        rearLeftMotor.battery = RLBattery;
        rearRightMotor.battery = RRBattery;
        initialized = true;
    }




    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {

        if (initialized)
        {
            m_pitch = -controller.flightComputer.processedPitch;
            m_roll = -controller.flightComputer.processedRoll;
            m_yaw = -controller.flightComputer.processedYaw;
            m_throttle = controller.flightComputer.processedThrottle;

            if (controller.core.currentAltitude > 5)
            {
                m_inflow_command = ((Mathf.Abs(controller.flightComputer.commandPitchInput) * 3f) + (Mathf.Abs(controller.flightComputer.commandRollInput) * 3f) + (Mathf.Abs(controller.flightComputer.commandYawInput) * 3f)) / 3f;
                if (m_inflow_command > 1) { m_inflow_command = 1; }
                m_ux = (0.5f - m_throttle) / 0.5f; if (m_ux < 0) { m_ux = 0f; }
                m_command = m_inflow_throttle * m_inflow_command * m_ux;
            }
            else { m_command = 0f; }


            if (m_throttle > 0.8f) { m_throttle = 0.8f; }
            m_throttle_factor = ((m_factor - 1) / 0.8f * m_throttle) + 1f;
            m_esc_1 = (m_throttle * 2) - m_pitch + m_roll - m_yaw;
            m_esc_2 = (m_throttle * 2) + m_pitch + m_roll + m_yaw;
            m_esc_3 = (m_throttle * 2) + m_pitch - m_roll - m_yaw;
            m_esc_4 = (m_throttle * 2) - m_pitch - m_roll + m_yaw;

            float m_f = (m_throttle * 0.8f) + 1;
            m_esc_1 /= m_f;
            m_esc_2 /= m_f;
            m_esc_3 /= m_f;
            m_esc_4 /= m_f;

            controller.flightComputer.rollRateSolver.Kp = m_roll_gain.x / m_throttle_factor;
            controller.flightComputer.rollRateSolver.Kd = m_roll_gain.z / m_throttle_factor;
            controller.flightComputer.pitchRateSolver.Kp = m_pitch_gain.x / m_throttle_factor;
            controller.flightComputer.pitchRateSolver.Kd = m_pitch_gain.z / m_throttle_factor;
            controller.flightComputer.yawRateSolver.Kp = m_yaw_gain.x / m_throttle_factor;
            controller.flightComputer.yawRateSolver.Kd = m_yaw_gain.z / m_throttle_factor;

            m_esc_1 = Mathf.Clamp((m_esc_1 + 0.0f), 0.05f, 1.0f);
            m_esc_2 = Mathf.Clamp((m_esc_2 + 0.0f), 0.05f, 1.0f);
            m_esc_3 = Mathf.Clamp((m_esc_3 + 0.0f), 0.05f, 1.0f);
            m_esc_4 = Mathf.Clamp((m_esc_4 + 0.0f), 0.05f, 1.0f);

            rearRightMotor.controlInput = m_esc_1;
            frontRightMotor.controlInput = m_esc_2;
            frontLeftMotor.controlInput = m_esc_3;
            rearLeftMotor.controlInput = m_esc_4;

            frontLeftRotor.coreRPM = frontLeftMotor.coreRPM;
            frontRightRotor.coreRPM = frontRightMotor.coreRPM;
            rearLeftRotor.coreRPM = rearLeftMotor.coreRPM;
            rearRightRotor.coreRPM = rearRightMotor.coreRPM;
        }
    }
}
