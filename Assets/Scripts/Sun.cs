// <summary>
/// Sun.cs
/// 29-11-13
/// M A Plant
/// 
/// This script works in tandem with the GameTime script to control sun/moon light levels
/// </summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Environmentals/Sun")]
public class Sun : MonoBehaviour {
	public float maxLightBrightness;
	public float minLightBrightness;

	public float maxFlareBrightness;
	public float minFlareBrightness;

	public bool giveLight = false;

	void start(){
		if(GetComponent<Light>() != null)
			giveLight = true;
	}
}
