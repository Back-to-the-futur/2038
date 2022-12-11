using UnityEngine;


public class PhantomBattery : MonoBehaviour
{
	//---------------------------------------------- Selectibles
	public enum BatteryType { LithiumPolymer, LithiumIon, LeadAcid, NickelCadmium, NickelMetalHydride }
	public BatteryType batteryType = BatteryType.LithiumPolymer;
	public enum State { Charging, Discharging }
	public State state = State.Discharging;


	//---------------------------------------------- Cells
	public int cellCount = 12;
	public float nominalCellVoltage;
	public float dischargeCellVoltage;
	public float currentCellVolage;


	//---------------------------------------------- Variables
	public float capacity = 10; //[Ah]
	public float currentCapacity;
	public float seriesCellResistance = 0.01f; //[ohms]
	public float packSeriesResistance; //total pack resistance
	public float SoC; //state of charge - equivalent to battery level in percentage points
	public float packVoltage;

	public float standbyDischargeCurrent = 0.02f;
	public float outputVoltage;
	public float outputCurrent;
	public float availablePower;
	public float chargingCurrent;
	public float timeRemaining;
	public bool low, full;


	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void Start()
	{
		currentCapacity = capacity;
		SoC = 100f;
		nominalCellVoltage = getVoc();
		currentCellVolage = nominalCellVoltage;
		packVoltage = nominalCellVoltage * cellCount;
		packSeriesResistance = seriesCellResistance * cellCount;
		outputVoltage = getVoc() * cellCount;
		state = State.Discharging;
	}






	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	public float getVoc()
	{
		if (batteryType == BatteryType.LithiumPolymer)
		{
			if (SoC >= 95) { return Mathf.Lerp(4.15f, 4.20f, (SoC - 95f) * 0.2f); }
			else if (SoC > 90) { return Mathf.Lerp(4.11f, 4.15f, (SoC - 90f) * 0.2f); }
			else if (SoC > 85) { return Mathf.Lerp(4.08f, 4.11f, (SoC - 85f) * 0.2f); }
			else if (SoC > 80) { return Mathf.Lerp(4.02f, 4.08f, (SoC - 80f) * 0.2f); }
			else if (SoC > 75) { return Mathf.Lerp(3.98f, 4.02f, (SoC - 75f) * 0.2f); }
			else if (SoC > 70) { return Mathf.Lerp(3.95f, 3.98f, (SoC - 70f) * 0.2f); }
			else if (SoC > 65) { return Mathf.Lerp(3.91f, 3.95f, (SoC - 65f) * 0.2f); }
			else if (SoC > 60) { return Mathf.Lerp(3.87f, 3.91f, (SoC - 60f) * 0.2f); }
			else if (SoC > 55) { return Mathf.Lerp(3.85f, 3.87f, (SoC - 55f) * 0.2f); }
			else if (SoC > 50) { return Mathf.Lerp(3.84f, 3.85f, (SoC - 50f) * 0.2f); }
			else if (SoC > 45) { return Mathf.Lerp(3.82f, 3.84f, (SoC - 45f) * 0.2f); }
			else if (SoC > 40) { return Mathf.Lerp(3.80f, 3.82f, (SoC - 40f) * 0.2f); }
			else if (SoC > 35) { return Mathf.Lerp(3.79f, 3.80f, (SoC - 35f) * 0.2f); }
			else if (SoC > 30) { return Mathf.Lerp(3.77f, 3.79f, (SoC - 30f) * 0.2f); }
			else if (SoC > 25) { return Mathf.Lerp(3.75f, 3.77f, (SoC - 25f) * 0.2f); }
			else if (SoC > 20) { return Mathf.Lerp(3.73f, 3.75f, (SoC - 20f) * 0.2f); }
			else if (SoC > 15) { return Mathf.Lerp(3.71f, 3.73f, (SoC - 15f) * 0.2f); }
			else if (SoC > 10) { return Mathf.Lerp(3.69f, 3.71f, (SoC - 10f) * 0.2f); }
			else if (SoC > 5) { return Mathf.Lerp(3.61f, 3.69f, (SoC - 05f) * 0.2f); }
			else { return Mathf.Lerp(3.27f, 3.61f, (SoC) * 0.2f); }
		}

		//temporarily using LiPo values 
		else if (batteryType == BatteryType.LithiumIon)
		{
			if (SoC >= 95) { return Mathf.Lerp(4.15f, 4.20f, (SoC - 95f) * 0.2f); }
			else if (SoC > 90) { return Mathf.Lerp(4.11f, 4.15f, (SoC - 90f) * 0.2f); }
			else if (SoC > 85) { return Mathf.Lerp(4.08f, 4.11f, (SoC - 85f) * 0.2f); }
			else if (SoC > 80) { return Mathf.Lerp(4.02f, 4.08f, (SoC - 80f) * 0.2f); }
			else if (SoC > 75) { return Mathf.Lerp(3.98f, 4.02f, (SoC - 75f) * 0.2f); }
			else if (SoC > 70) { return Mathf.Lerp(3.95f, 3.98f, (SoC - 70f) * 0.2f); }
			else if (SoC > 65) { return Mathf.Lerp(3.91f, 3.95f, (SoC - 65f) * 0.2f); }
			else if (SoC > 60) { return Mathf.Lerp(3.87f, 3.91f, (SoC - 60f) * 0.2f); }
			else if (SoC > 55) { return Mathf.Lerp(3.85f, 3.87f, (SoC - 55f) * 0.2f); }
			else if (SoC > 50) { return Mathf.Lerp(3.84f, 3.85f, (SoC - 50f) * 0.2f); }
			else if (SoC > 45) { return Mathf.Lerp(3.82f, 3.84f, (SoC - 45f) * 0.2f); }
			else if (SoC > 40) { return Mathf.Lerp(3.80f, 3.82f, (SoC - 40f) * 0.2f); }
			else if (SoC > 35) { return Mathf.Lerp(3.79f, 3.80f, (SoC - 35f) * 0.2f); }
			else if (SoC > 30) { return Mathf.Lerp(3.77f, 3.79f, (SoC - 30f) * 0.2f); }
			else if (SoC > 25) { return Mathf.Lerp(3.75f, 3.77f, (SoC - 25f) * 0.2f); }
			else if (SoC > 20) { return Mathf.Lerp(3.73f, 3.75f, (SoC - 20f) * 0.2f); }
			else if (SoC > 15) { return Mathf.Lerp(3.71f, 3.73f, (SoC - 15f) * 0.2f); }
			else if (SoC > 10) { return Mathf.Lerp(3.69f, 3.71f, (SoC - 10f) * 0.2f); }
			else if (SoC > 5) { return Mathf.Lerp(3.61f, 3.69f, (SoC - 05f) * 0.2f); }
			else { return Mathf.Lerp(3.27f, 3.61f, (SoC) * 0.2f); }
		}
		else return 0;
	}





	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	private void Update()
	{
		SoC = (currentCapacity / capacity) * 100f;

		// ---------------------------------- State
		if (state == State.Discharging)
		{
			full = false;
			if (outputCurrent > 0)
			{
				timeRemaining = (currentCapacity) / outputCurrent;//time remaining in seconds
				currentCapacity -= (outputCurrent * (Time.deltaTime / (3600)));
			}
			else { currentCapacity -= (standbyDischargeCurrent * (Time.deltaTime / (3600))); }
			if (SoC < 20) { low = true; }
		}
		else
		{
			low = false;

			//--------------- Charging Logic

			if (SoC > 100.2f)
			{
				state = State.Discharging;
				full = true;
			}
		}



		// -------------------------------- Ouput
		if (currentCapacity < 0.1f) { currentCapacity = 0; }
		currentCellVolage = getVoc();
		if (currentCapacity > 0.1f) { outputVoltage = currentCellVolage * cellCount; } else { outputVoltage = 0f; }
		availablePower = currentCapacity * outputVoltage;
	}
}
