// <summary>
/// TimedLighting.cs
/// 01-12-13
/// M A Plant
/// 
/// This is the script responsible for control of lighting in game dependant on the time of day.
/// </summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Environmentals/Timed Light")]
public class TimedLighting : MonoBehaviour {
	public void OnEnable(){
		Messenger<bool>.AddListener("Morning Light", OnToggleLight);
	}

	public void OnDisable(){
		Messenger<bool>.RemoveListener("Morning Light", OnToggleLight);
	}
	private void OnToggleLight(bool b){
		if(b){
			GetComponent<Light>().enabled = false;
		}
		else{
			GetComponent<Light>().enabled = true;
		}
	}
}
