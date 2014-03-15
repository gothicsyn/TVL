/// <summary>
/// Vital.cs
/// M A Plant
/// 21-10-13
/// 
/// This class conatins all the extra functions required to calculate a players Vitals
/// </summary>
public class Vital : ModifiedStat {
	private int _curValue;							// The current value of this vital
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital(){
		UnityEngine.Debug.Log ("Vital Created");
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}
	
	
	/// <summary>
	/// When getting _curValue ensures it is not greater than AdjustedBaseValue
	/// If it is, then set it to the same as AdjustedBaseValue
	/// </summary>
	/// <value>
	/// The current value.
	/// </value>
	public int CurValue {
		get{
			if(_curValue > AdjustedBaseValue)
				_curValue = AdjustedBaseValue;
			
			return _curValue;
		}
		set{ _curValue = value; }
	}
}

/// <summary>
/// A list of the Vitals our character will have
/// </summary>
public enum VitalName {
	Health,
	Energy,
	Mana
}