using UnityEngine;


public class PhantomReadout : MonoBehaviour
{
  
    //------------------------------------------ Selectibles
    public enum DialType { Airspeed, Altitude }
    public DialType dialType = DialType.Airspeed;
    public enum AltimeterType { FourDigit, ThreeDigit }
    public AltimeterType altimeterType = AltimeterType.ThreeDigit;



    //------------------------------------------ Connections
    public PhantomController controller;
    public RectTransform needle;
    public RectTransform digitOneContainer;
    public RectTransform digitTwoContainer;
    public RectTransform digitThreeContainer;
    public RectTransform digitFourContainer;



    //------------------------------------------ Variables
    public float digitOne;
    public float digitTwo;
    public float digitThree;
    public float digitFour;
    public float digitFive;

    public float digitOneTranslation;
    public float digitTwoTranslation;
    public float digitThreeTranslation;
    public float digitFourTranslation;
    float needleRotation;
    float smoothRotation;
    public float maximumValue, dialValue;




	// ----------------------------------------------------------------------------------------------------------------------------------------------------------
	void FixedUpdate()
	{
		if (controller != null && controller.gameObject.activeSelf)
		{
			///--------------------------------------------------------AIRSPEED
			if (dialType == DialType.Airspeed)
			{
				//COLLECT VALUE
				dialValue = controller.core.forwardSpeed * Oyedoyin.MathBase.toKnots;

				//EXTRACT DIGITS
				digitOne = (dialValue % 10f);
				digitTwo = Mathf.Floor((dialValue % 100.0f) / 10.0f);
				digitThree = Mathf.Floor((dialValue % 1000.0f) / 100.0f);

				//CALCULATE DIAL POSITIONS
				float digitOnePosition = digitOne * -digitOneTranslation;
				float digitTwoPosition = digitTwo * -digitTwoTranslation; if (digitOne > 9.0f) { digitTwoPosition += (digitOne - 9.0f) * -digitTwoTranslation; }
				float digitThreePosition = digitThree * -digitThreeTranslation; if ((digitTwo * 10) > 99.0f) { digitThreePosition += ((digitTwo * 10f) - 99.0f) * -digitThreeTranslation; }

				//SET POSITIONS
				if (digitOneContainer != null) { digitOneContainer.localPosition = new Vector3(0, digitOnePosition, 0); }
				if (digitTwoContainer != null) { digitTwoContainer.localPosition = new Vector3(0, digitTwoPosition, 0); }
				if (digitThreeContainer != null) { digitThreeContainer.localPosition = new Vector3(0, digitThreePosition, 0); }
			}


			///---------------------------------------------------------ALTIMETER
			if (dialType == DialType.Altitude)
			{
				//COLLECT VALUE
				dialValue = controller.core.currentAltitude * Oyedoyin.MathBase.toFt;
				maximumValue = 10000f;

				//EXTRACT DIGITS
				digitOne = ((dialValue % 100.0f) / 20.0f);//20FT Spacing
				digitTwo = Mathf.Floor((dialValue % 1000.0f) / 100.0f);
				digitThree = Mathf.Floor((dialValue % 10000.0f) / 1000.0f);
				digitFour = Mathf.Floor((dialValue % 100000.0f) / 10000.0f);

				//CALCULATE DIAL POSITIONS
				float digitOnePosition = digitOne * -digitOneTranslation;
				float digitTwoPosition = digitTwo * -digitTwoTranslation;
				if ((digitOne * 20) > 90.0f) { digitTwoPosition += ((digitOne * 20f) - 90.0f) / 10.0f * -digitTwoTranslation; }
				float digitThreePosition = digitThree * -digitThreeTranslation;
				if ((digitTwo * 100) > 990.0f) { digitThreePosition += ((digitTwo * 100) - 990.0f) / 10.0f * -digitThreeTranslation; }
				float digitFourPosition = 0f;
				if (altimeterType == AltimeterType.FourDigit) { digitFourPosition = digitFour * -digitFourTranslation; if ((digitThree * 1000) > 9990.0f) { digitFourPosition += ((digitThree * 1000) - 9990.0f) / 10f * -digitFourTranslation; } }


				//SET POSITIONS
				if (digitOneContainer != null) { digitOneContainer.localPosition = new Vector3(0, digitOnePosition, 0); }
				if (digitTwoContainer != null) { digitTwoContainer.localPosition = new Vector3(0, digitTwoPosition, 0); }
				if (digitThreeContainer != null) { digitThreeContainer.localPosition = new Vector3(0, digitThreePosition, 0); }
				if (altimeterType == AltimeterType.FourDigit) { if (digitFourContainer != null) { digitFourContainer.localPosition = new Vector3(0, digitFourPosition, 0); } }

			}


			//-----------------------------------------------------------------NEEDLE
			if (needle != null)
			{
				needleRotation = Mathf.Lerp(0, 360, dialValue / maximumValue);
				smoothRotation = Mathf.Lerp(smoothRotation, needleRotation, Time.deltaTime * 5);
				needle.transform.eulerAngles = new Vector3(needle.transform.eulerAngles.x, needle.transform.eulerAngles.y, -smoothRotation);
			}
		}
	}
}
