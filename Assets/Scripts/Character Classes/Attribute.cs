/// <summary>
/// Attribute.cs
/// 25-10-13
/// M A Plant
/// 
/// The class for all Character Attributes in game
/// </summary>

public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50; 	// This is the starting cost for all character attributes
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute(){
		UnityEngine.Debug.Log("Attribute Created");
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}
}

/// <summary>
/// A List of all attribuites that will be used in game
/// </summary>
public enum AttributeName {
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma
}