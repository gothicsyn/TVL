/// <summary>
/// Skill.cs
/// M A Plant
/// 21-10-13
/// 
/// This class conatins all the extra functions required to calculate a players Skills
/// </summary>
public class Skill : ModifiedStat {
	private bool _known;				// A boolean value to determine if a player knows a skill or not
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill() {
		UnityEngine.Debug.Log ("Skill Created");
		_known = false;
		ExpToLevel = 30;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value>
	/// <c>true</c> if known; otherwise, <c>false</c>.
	/// </value>
	public bool Known {
		get{ return _known; }
		set{ _known = value; }
	}
}


/// <summary>
/// A List of skills the player can leanr and use
/// </summary>
public enum SkillName {
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
	