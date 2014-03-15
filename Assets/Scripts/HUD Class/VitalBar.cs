// <summary>
/// VitaBar.cs
/// 28-10-13
/// M A Plant
/// 
/// This class is responsible for displaying the vital bar for the player or a mob
/// </summary>

using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar;   	// This boolean value tells us if its the player health or mob health bar
	
	private int maxBarLength;			// Maximum length of the Vital Bar overall
	private int curBarLength;			// The current length of the bar vs the % of health
	private GUITexture _display;		// The reference to the GUI component
	
	
	void Awake(){
		_display = gameObject.GetComponent<GUITexture>();	
	}

	// Use this for initialization
	void Start () {
		maxBarLength = (int)_display.pixelInset.width;
		
		curBarLength = maxBarLength;
		_display.pixelInset = CalculatePosition();
		
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// This method is called when a gameobject is enabled
	public void OnEnable() {
		if(_isPlayerHealthBar){
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		}
		else {
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	// This method is called when a gameobject is disabled
	public void OnDisable() {
		if(_isPlayerHealthBar){
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		}
		else {
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	
	
	// This method calculates the size of the healthbar in relation to the % of Health the target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth){
//		Debug.Log ("Event Heard : curHealth = " + curHealth + " - maxHealth = " + maxHealth);
		
		curBarLength = (int)((curHealth / (float)maxHealth) * maxBarLength);		// This Calculates the current Vital Bar length based on the players Health %
		
//		_display.pixelInset= new Rect(_display.pixelInset.x, _display.pixelInset.y, curBarLength, _display.pixelInset.height);
		_display.pixelInset= CalculatePosition();

	}
	
	
	// Setting the healthbar to the player or mob
	public void SetPlayerHealthBar(bool b){
		_isPlayerHealthBar = b;
	}
	
	private Rect CalculatePosition(){
		float yPos = _display.pixelInset.y / 2 - 10;
		
		if(!_isPlayerHealthBar){
			float xPos = (maxBarLength - curBarLength) - (maxBarLength / 4 + 10);
			return new Rect(xPos, yPos, curBarLength, _display.pixelInset.height);
		}
		
		return new Rect(_display.pixelInset.x, yPos, curBarLength, _display.pixelInset.height);
	}
	
	private void ToggleDisplay(bool show){
		_display.enabled = show;
	}
		
}
