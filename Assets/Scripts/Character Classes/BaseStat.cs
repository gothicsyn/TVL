/// <summary>
/// 
/// BaseStat.cs
/// M A Plant - 21.10.13
/// 
/// This generates the base class for all players in game. 
/// 
/// </summary>

public class BaseStat {
	public const int STARTING_EXP_COST = 100; 	// Public int incase of file failure, will be set to allow modifying of attributes at start up
	
	private int _baseValue;						// The base value of this stat
	private int _buffValue;						// The amount of buff to this stat
	private int _expToLevel;					// The total amount needed to raise this skill
	private float _levelModifier;				// The applied to the exp needed to raise the skill
		
	private string _name;						// This is the name of the Attribute

	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat(){
		UnityEngine.Debug.Log("BaseStat Stat Created");
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
		_name = "";
	}

#region Basic Setters & Getters	
	/// <summary>
	/// Gets or sets the BaseValue.
	/// </summary>
	/// <value>
	/// The _baseValue.
	/// </value>
	public int BaseValue {
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the BuffValue.
	/// </summary>
	/// <value>
	/// The _buffValue.
	/// </value>
	public int BuffValue {
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the LevelModifier.
	/// </summary>
	/// <value>
	/// The _levelModifier.
	/// </value>
	public float LevelModifier {
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
	
	/// <summary>
	/// Gets or sets the ExpToLevel.
	/// </summary>
	/// <value>
	/// The _expToLevel.
	/// </value>
	public int ExpToLevel {
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}
	
	/// <summary>
	/// Gets or sets the _name.
	/// </summary>
	/// <value>
	/// The _name.
	/// </value>
	public string Name{
		get{ return _name;}
		set{ _name = value; }
	}

#endregion
	
	/// <summary>
	/// Recalculates the ExpToLevel and Returns it.
	/// </summary>
	/// <returns>
	/// The ExpToLevel.
	/// </returns>
	private int CalculateEXPtoLevel(){
		return (int)(_expToLevel * _levelModifier);
	}
	
	/// <summary>
	/// Assign the new value to ExpToLevel and then increase the _baseValue by one.
	/// </summary>
	public void LevelUp(){
		_expToLevel = CalculateEXPtoLevel();
		_baseValue++;
	}
	
	/// <summary>
	/// Recalculates the Adjusted Base Value and Returns it.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int AdjustedBaseValue {
		get{ return _baseValue + _buffValue; }
	}
}
