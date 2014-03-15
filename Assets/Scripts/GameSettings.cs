// <summary>
/// ModifiedStat.cs
/// 25-10-13
/// M A Plant
/// 
/// This is the game settings file for the game
/// </summary>

using UnityEngine;
using System.Collections;
using System; 		// Added for Enum Class

public class GameSettings : MonoBehaviour {
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point"; 	// This is the gameobject that the player will spawn upon at the start of the level
	
	void Awake(){
		DontDestroyOnLoad(this);
	}
	
	public void SaveCharacterData(){
		GameObject pc = GameObject.Find("Character");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
//		PlayerPrefs.DeleteAll();
		
		PlayerPrefs.SetString("Player Name", pcClass.Name);
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp To Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp To Level", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Current Value", pcClass.GetVital(cnt).CurValue);
			
			PlayerPrefs.SetString(((VitalName)cnt) + " - Mods", pcClass.GetVital(cnt).GetModifiedAttributeString());
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp To Level", pcClass.GetSkill(cnt).ExpToLevel);

//			PlayerPrefs.SetString(((SkillName)cnt) + " - Mods", pcClass.GetSkill(cnt).GetModifiedAttributeString());
		}
	}
	
	public void LoadCharacterData(){
		GameObject pc = GameObject.Find("Character");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		pcClass.Name = PlayerPrefs.GetString("Player Name", "Default Name");
		

		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp To Level", Attribute.STARTING_EXP_COST);
		}
		
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp To Level", 0);

			// Gets adjusted Value before the current Value
			pcClass.GetVital(cnt).Update();
			
			// Gets stored Value for the curValue of each Vital
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - CurValue", 1);
		}
			
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Exp To Level", 0);
		}
		
		//Out CurValues
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			Debug.Log(((SkillName)cnt).ToString() + " _ " + pcClass.GetSkill(cnt).BaseValue + "|" + pcClass.GetSkill(cnt).ExpToLevel);
		}
	}
}
