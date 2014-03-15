using UnityEngine;
using System.Collections;
using System; // Added for Enum Class

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;
	
	private int statStartingPos = 40;
	
	public GUISkin mySkin;
	
	public GameObject playerPrefab;
	
	
	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity ) as GameObject; 
		
		pc.name = "Character";
		
//		_toon = new PlayerCharacter();
//		_toon.Awake();
		_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}
		
			_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI(){
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();

		DisplayVitals();

		DisplaySkills();
	
		if(_toon.Name == "" || pointsLeft > 0)
			DisplayCreateLabel();
		else
			DisplayCreateButton();
	}
	
		private void DisplayName(){
			GUI.Label(new Rect(10, 10, 50, 25), "Name:");
			_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);
	}
	
	private void DisplayAttributes(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			GUI.Label(new Rect(OFFSET, 										// X Position 
								statStartingPos + (cnt * LINE_HEIGHT), 		// Y Position
								STAT_LABEL_WIDTH, 							// Width 
								LINE_HEIGHT), 								// Height 
						((AttributeName)cnt).ToString());
			
			GUI.Label(new Rect(STAT_LABEL_WIDTH + OFFSET, 					// X Position 
								statStartingPos + (cnt * LINE_HEIGHT), 		// Y Position
								BASEVALUE_LABEL_WIDTH, 						// Width
								LINE_HEIGHT), 								// Height
						_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString() );
			
			if(GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,	// X Position
								statStartingPos + (cnt * BUTTON_HEIGHT), 				// Y Position
								BUTTON_WIDTH, 											// Width
								BUTTON_HEIGHT), "-")){									// Height
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE){
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft ++;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH, 	// X Position
								statStartingPos + (cnt * BUTTON_HEIGHT), 								// Y Position
								BUTTON_WIDTH,  															// Width
								BUTTON_HEIGHT), "+")){													// Height
				if(pointsLeft > 0){
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft --;
					_toon.StatUpdate();
				}
			}
		}
	}
	
	private void DisplayVitals(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			GUI.Label(new Rect(OFFSET,									// X Position 
				statStartingPos + ((cnt + 7) * LINE_HEIGHT), 			// Y Position
				STAT_LABEL_WIDTH, 										// Width
				LINE_HEIGHT), 											// Height
				((VitalName)cnt).ToString() );							
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH,				// X position 
				statStartingPos + ((cnt + 7) * LINE_HEIGHT), 			// Y Position
				BASEVALUE_LABEL_WIDTH, 									// Width
				LINE_HEIGHT), 											// Height
				_toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplaySkills(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2+ OFFSET * 2,	// X Position 
				statStartingPos + (cnt * LINE_HEIGHT), 																// Y Position
				STAT_LABEL_WIDTH, 																					// Width
				LINE_HEIGHT), 																						// Width
				((SkillName)cnt).ToString() );
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2+ OFFSET * 2 + STAT_LABEL_WIDTH, statStartingPos + (cnt * LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplayPointsLeft(){
			GUI.Label(new Rect(250, 10, 200, 25), "Exp Remaining: " + pointsLeft.ToString());		
	}
	
	private void DisplayCreateLabel(){
		GUI.Label(new Rect( Screen.width/ 2 - 50, statStartingPos + (10 * LINE_HEIGHT), 100, LINE_HEIGHT), "Creating ..", "Button");
	}
	
	private void DisplayCreateButton(){
		if(GUI.Button(new Rect(Screen.width / 2 - 50,					// X Position 
				statStartingPos + (10 * LINE_HEIGHT), 					// Y Position
				100, 													// Width
				LINE_HEIGHT 											// Height		
			), "Create")) {	
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();;
			
			// Method to save Current Value to max Modified Values as Character Saves before the game is started.
			UpdateCurValues();
			
			// Save Character Data
			gsScript.SaveCharacterData();
			
			Application.LoadLevel("Level1");
		}
	}
	
	private void UpdateCurValues() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
}