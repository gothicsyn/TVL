// <summary>
/// GameTime.cs
/// 29-11-13
/// M A Plant
/// 
/// This is the script responsible for control of time and the day/night cycle of the game. It will also
/// control rotation of the sun/moon in the night sky.
/// </summary>
using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public enum TimeOfDay{
		Idle,
		SunRise,
		SunSet
	}

	public Transform[] sun;					// An array to hold all sun/moon
	public float dayCycleInMinutes = 1;		// How many real minutes and in game day is equal to

	public float sunRise;					// Time of Sunrise
	public float sunSet;					// Time of Sunset
	public float SkyboxBlendModifier;		// SThe speed at which the textures in the Skybox blend

	public Color amLightMin;					// Minimum abient Light
	public Color amLightMax;					// Max ambient Light

	public float morningLight;				// Turn of lamps and laterns
	public float nightLight;				// Turn on lamps and laterns
	private bool _isMorning = false;

	private Sun[] _sunScript;				// An array to hold all sun.cs references
	private float _degreeRotation;			// How many degrees each sun/moon rotates
	private float _timeOfDay;				// Track the passage of time through the day

	private float _dayCycleInSeconds;		// The number of real time seconds in an in game day
	
	// Time Constants are set below
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;

	private TimeOfDay _tod;
	private float _noonTime;				// This is the the time of day (noon) exactly halfway through the cycle
	private float _morningLength;			// Length of time between Sunrise & Noon
	private float _afternoonLength;			// Length of time between Noon & Sunset

	// Use this for initialization
	void Start () {
		_tod = TimeOfDay.Idle;
		// Get the number of real time seconds for the in game day
		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;

		// Change the skybox from box one to box two from the first box in the pattern
		RenderSettings.skybox.SetFloat("_Blend", 0);

		_sunScript = new Sun[sun.Length];

		// Ensure all suns have the sun script, if not this will add it
		for(int cnt = 0; cnt < sun.Length; cnt ++){
			Sun temp = sun[cnt].GetComponent<Sun>();

			if(temp == null){
				Debug.LogWarning("Variable Not Found .. Creating Standard");
				sun[cnt].gameObject.AddComponent<Sun>();
				temp = sun[cnt].GetComponent<Sun>();
			}
			_sunScript[cnt] = temp;
		}

		// Start the day at 0 seconds
		_timeOfDay = 0;

		// Set the degree of rotation, to the angle of rotation
		_degreeRotation = DEGREES_PER_SECOND * DAY / (_dayCycleInSeconds);

		sunRise *= _dayCycleInSeconds;
		sunSet *= _dayCycleInSeconds;
		_noonTime = _dayCycleInSeconds / 2;
		_morningLength = _noonTime - sunRise;			// Length of the morning period in seconds
		_afternoonLength = sunSet - _noonTime;			// Length of the evening period in seconds
		morningLight *= _dayCycleInSeconds;
		nightLight *= _dayCycleInSeconds;
	
		// Sets lighting to the min value of the sun script
		SetupLighting();
	}
	
	// Update is called once per frame
	void Update () {
		// Update the time of day
		_timeOfDay += Time.deltaTime;

		// If the timer is greater than the length of the set day, reset the timer
		if(_timeOfDay > _dayCycleInSeconds)
			_timeOfDay -= _dayCycleInSeconds;

//		Debug.Log(_timeOfDay);

// Control the outdoor lighting effects according to the time of day

		if(!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight){
			_isMorning = true;
			Messenger<bool>.Broadcast("Morning Light", true);
			Debug.Log ("Morning Mode");
		}
		else if(_isMorning && _timeOfDay > nightLight){
			_isMorning = false;
			Messenger<bool>.Broadcast("Morning Light", false);
			Debug.Log ("Night Mode");
		}

// Position the sun in the sky by adjusting the angle the flare is shining from
		for(int cnt = 0; cnt < sun.Length; cnt ++)
			sun[cnt].Rotate(new Vector3(_degreeRotation, 0 ,0) * Time.deltaTime);

		if(_timeOfDay > sunRise && _timeOfDay < _noonTime){
			AdjustLighting(true);
		}
		else if (_timeOfDay > _noonTime && _timeOfDay < sunSet) {
			AdjustLighting(false);
		}
		// The sun is past the SunRise point, before the SunSet point, and the day skybox is not fully faded in
		if(_timeOfDay > sunRise &&
		   _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1){
			_tod = GameTime.TimeOfDay.SunRise;
			BlendSkyBox();
		}
		else if(_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0){
			_tod = GameTime.TimeOfDay.SunSet;
			BlendSkyBox();
		}
		else{
			_tod = GameTime.TimeOfDay.Idle;
		}
	}

	private void BlendSkyBox(){
		float temp = 0; 

		switch(_tod){
		case TimeOfDay.SunRise:
			temp = (_timeOfDay - sunRise) / _dayCycleInSeconds * SkyboxBlendModifier;	
		break;
		case TimeOfDay.SunSet:
			temp = (_timeOfDay - sunSet) / _dayCycleInSeconds * SkyboxBlendModifier;
			temp = 1 - temp;
		break;
		}

//		Debug.Log(temp);
		RenderSettings.skybox.SetFloat("_Blend", temp);
	}

	private void SetupLighting() {
		RenderSettings.ambientLight = amLightMin;
			
		for(int cnt = 0; cnt < _sunScript.Length; cnt ++){
			if(_sunScript[cnt].giveLight){
				sun[cnt].GetComponent<Light>().intensity = _sunScript[cnt].minLightBrightness;
			}
		}
	}

	private void AdjustLighting(bool brighten){
		float pos = 1;	

		if(brighten){
			pos = (_timeOfDay - sunRise) / _morningLength;		// Find position of the sun in the morning sky
			}
		else{
			pos = (sunSet - _timeOfDay) / _afternoonLength;		// Find position of the sun in the afternoon sky
		}

		RenderSettings.ambientLight = new Color(amLightMin.r + amLightMax.r * pos, 
		                                         amLightMin.g + amLightMax.g * pos,
		                                         amLightMin.b + amLightMax.b * pos);
		for(int cnt = 0; cnt < _sunScript.Length; cnt ++){
			if(_sunScript[cnt].giveLight){
				_sunScript[cnt].GetComponent<Light>().intensity = _sunScript[cnt].maxLightBrightness * pos;
			}
		}
	}
}
