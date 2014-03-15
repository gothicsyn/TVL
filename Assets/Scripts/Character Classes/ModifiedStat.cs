// <summary>
/// ModifiedStat.cs
/// 27-10-13
/// M A Plant
/// 
/// This is the base class for all Stats that will be modified by Attributes
/// </summary>

using System.Collections.Generic;					// Generic was added to enable use of List<>

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods;			// A list of Attributes that modify this stat 
	private int _modValue; 							// The amount added to _baseValue from the modifiers
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat() {
		UnityEngine.Debug.Log("Modified Stat Created");
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Add a Modifying Attribute to our list of mods for the ModifiedStat.
	/// </summary>
	/// <param name='mod'>
	/// Mod.
	/// </param>
	public void AddModifier( ModifyingAttribute mod) {
		_mods.Add(mod);
	}
	
	/// <summary>
	/// Reset _modValue to 0 
	/// Check to see if at least one ModifyingAttribute is present in the list of mods
	/// If we do, the iterate through the list and then add the AdjustedBaseValue * Ratio to the _modValue
	/// </summary>
	private void CalculateModValue() {
		_modValue = 0;
		
		if(_mods.Count > 0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
	}
	
	/// <summary>
	/// This function will overide AdjustedBaseValue from inside the BaseStat class
	/// Calculates the AdjustedBaseValue from teh BaseValue + BuffValue + _modValue
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int AdjustedBaseValue {
		get{ return BaseValue + BuffValue + _modValue; }
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update() {
		CalculateModValue();
	}
	
	// May No Longer Be Required
	public string GetModifiedAttributeString(){
		string temp = "";
	
//		UnityEngine.Debug.Log(_mods.Count);

		for(int cnt = 0; cnt < _mods.Count; cnt++){
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;
			
			if(cnt < _mods.Count - 1)
				temp += "|";

			UnityEngine.Debug.Log(temp);
		}
		
		return temp;
	}
	
}

/// <summary>
/// A structure that holds an Attribute and a Ratio which will then be added as a modifying attribute to the ModifiedStats
/// </summary>
public struct ModifyingAttribute {
	public Attribute attribute;			// The Attribute to be used as a Modifier
	public float ratio;					// The percentage of attributes that is added to the ModifiedStat
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name='att'>
	/// Att. = The attribute to be used
	/// </param>
	/// <param name='rat'>
	/// Rat. = The ratio to use
	/// </param>
	public ModifyingAttribute(Attribute att, float rat){
		UnityEngine.Debug.Log("Modifying Attribute Created");
		attribute = att;
		ratio = rat;
	}
}
